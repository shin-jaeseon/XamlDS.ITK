using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace XamlDS.ITK.WPF.Controls.Adorners
{
    /// <summary>
    /// An adorner that draws a lightweight grid overlay. Properties are set directly from the host code-behind
    /// and not data-bound. The adorner caches a frozen <see cref="StreamGeometry"/> for performance and
    /// invalidates the cache when visual parameters change.
    /// 
    /// IMPORTANT:
    /// - This class assumes an <c>AdornerLayer</c> is available for the element you attach it to. If
    ///   <c>AdornerLayer.GetAdornerLayer(adornedElement)</c> returns null, the adorner cannot be added.
    ///   Do not attempt to modify the visual tree or inject an AdornerLayer automatically; prefer the
    ///   calling code (developer) to decide the appropriate target and handling strategy.
    /// - Always call <see cref="Detach()"/> when you remove the adorner (or when the adorned element is
    ///   unloaded) to detach event handlers and release cached resources. Failure to call <see cref="Detach()"/>
    ///   may result in event handler leaks or retained references to visuals and thus memory leaks.
    /// </summary>
    public class GridLineAdorner : Adorner
    {
        /// <summary>Spacing between grid lines in device-independent units.</summary>
        public double Spacing { get => _spacing; set { _spacing = value; InvalidateCache(); } }

        /// <summary>Thickness of grid lines in device-independent units.</summary>
        public double Thickness { get => _thickness; set { _thickness = value; InvalidateCache(); } }

        /// <summary>Brush used to draw grid lines.</summary>
        public Brush LineBrush { get => _lineBrush; set { _lineBrush = value; InvalidateCache(); } }

        /// <summary>Opacity applied to grid lines (0.0 - 1.0).</summary>
        public double LineOpacity { get => _lineOpacity; set { _lineOpacity = value; InvalidateCache(); } }

        /// <summary>Grid origin type.</summary>
        public GridOrigin Origin { get => _origin; set { _origin = value; InvalidateCache(); } }

        /// <summary>When true, snap coordinates to device pixels for crisper lines.</summary>
        public bool SnapToDevicePixels { get => _snap; set { _snap = value; InvalidateCache(); } }

        // backing fields with defaults similar to GridLinePane
        private double _spacing = 48.0d;
        private double _thickness = 1.0d;
        private Brush _lineBrush = Brushes.LightGray;
        private double _lineOpacity = 0.25d;
        private GridOrigin _origin = GridOrigin.Center;
        private bool _snap = true;

        // cache
        private StreamGeometry? _cachedGeometry;
        private Size _cachedSize = Size.Empty;
        private double _cachedSpacing = double.NaN;
        private double _cachedScaleX = double.NaN;
        private double _cachedScaleY = double.NaN;
        private GridOrigin _cachedOrigin = (GridOrigin)(-1);
        private bool _cachedSnap = false;

        /// <summary>
        /// Create a new GridLineAdorner attached to the specified element.
        /// </summary>
        /// <param name="adornedElement">Element to adorn. Must be a UIElement.</param>
        public GridLineAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            // The adorner is non-interactive by design so it doesn't block input.
            IsHitTestVisible = false;

            // NOTE: We subscribe to SizeChanged/Unloaded on the adorned element to keep the geometry cache
            // up to date. Because of those subscriptions, callers MUST call Detach() when removing the adorner
            // (or ensure the adorned element is unloaded) to avoid keeping event handler references.
            if (adornedElement is FrameworkElement fe)
            {
                fe.SizeChanged += OnAdornedElementSizeChanged;
                fe.Unloaded += OnAdornedElementUnloaded;
            }
        }

        private void OnAdornedElementUnloaded(object? sender, RoutedEventArgs e)
        {
            Detach();
        }

        private void OnAdornedElementSizeChanged(object? sender, SizeChangedEventArgs e)
        {
            InvalidateCache();
            InvalidateVisual();
        }

        /// <summary>
        /// Remove event handlers. Call this when removing adorner to avoid leaks.
        /// IMPORTANT: Call Detach() after removing the adorner from the AdornerLayer to fully release
        /// references and prevent memory leaks caused by event subscriptions.
        /// </summary>
        public void Detach()
        {
            if (AdornedElement is FrameworkElement fe)
            {
                fe.SizeChanged -= OnAdornedElementSizeChanged;
                fe.Unloaded -= OnAdornedElementUnloaded;
            }

            _cachedGeometry = null;
        }

        private static bool AreClose(double a, double b)
        {
            if (double.IsNaN(a) && double.IsNaN(b)) return true;
            if (double.IsNaN(a) || double.IsNaN(b)) return false;
            const double eps = 1e-6;
            return Math.Abs(a - b) <= eps;
        }

        private void InvalidateCache()
        {
            _cachedGeometry = null;
            // ensure visual is redrawn
            InvalidateVisual();
        }

        private static double SnapCoordLocal(double value, double scale, bool doSnap)
        {
            if (!doSnap) return value;
            return Math.Round(value * scale) / scale;
        }

        private void EnsureGeometry(double width, double height)
        {
            var size = new Size(width, height);
            var dpi = VisualTreeHelper.GetDpi(AdornedElement);
            double scaleX = dpi.DpiScaleX;
            double scaleY = dpi.DpiScaleY;

            bool needRecreate = _cachedGeometry == null
                || _cachedSize != size
                || !AreClose(_cachedSpacing, _spacing)
                || !AreClose(_cachedScaleX, scaleX)
                || !AreClose(_cachedScaleY, scaleY)
                || _cachedOrigin != _origin
                || _cachedSnap != _snap;

            if (!needRecreate) return;

            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                double originX = (_origin == GridOrigin.Center) ? width / 2.0 : 0.0;
                double originY = (_origin == GridOrigin.Center) ? height / 2.0 : 0.0;

                // Vertical right
                for (double x = originX; x <= width; x += _spacing)
                {
                    double sx = SnapCoordLocal(x, scaleX, _snap);
                    ctx.BeginFigure(new Point(sx, SnapCoordLocal(0, scaleY, _snap)), false, false);
                    ctx.LineTo(new Point(sx, SnapCoordLocal(height, scaleY, _snap)), true, false);
                }

                // Vertical left
                for (double x = originX - _spacing; x >= 0; x -= _spacing)
                {
                    double sx = SnapCoordLocal(x, scaleX, _snap);
                    ctx.BeginFigure(new Point(sx, SnapCoordLocal(0, scaleY, _snap)), false, false);
                    ctx.LineTo(new Point(sx, SnapCoordLocal(height, scaleY, _snap)), true, false);
                }

                // Horizontal down
                for (double y = originY; y <= height; y += _spacing)
                {
                    double sy = SnapCoordLocal(y, scaleY, _snap);
                    ctx.BeginFigure(new Point(SnapCoordLocal(0, scaleX, _snap), sy), false, false);
                    ctx.LineTo(new Point(SnapCoordLocal(width, scaleX, _snap), sy), true, false);
                }

                // Horizontal up
                for (double y = originY - _spacing; y >= 0; y -= _spacing)
                {
                    double sy = SnapCoordLocal(y, scaleY, _snap);
                    ctx.BeginFigure(new Point(SnapCoordLocal(0, scaleX, _snap), sy), false, false);
                    ctx.LineTo(new Point(SnapCoordLocal(width, scaleX, _snap), sy), true, false);
                }
            }

            geometry.Freeze();

            _cachedGeometry = geometry;
            _cachedSize = size;
            _cachedSpacing = _spacing;
            _cachedScaleX = scaleX;
            _cachedScaleY = scaleY;
            _cachedOrigin = _origin;
            _cachedSnap = _snap;
        }

        /// <summary>
        /// Render the adorner. This draws cached geometry with a pen created from current brush/opacity/thickness.
        /// </summary>
        /// <param name="drawingContext">DrawingContext supplied by WPF.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var size = AdornedElement.RenderSize;
            double width = size.Width;
            double height = size.Height;
            if (width <= 0 || height <= 0) return;

            EnsureGeometry(width, height);
            if (_cachedGeometry == null) return;

            Brush brush = (_lineBrush ?? Brushes.Transparent).Clone();
            brush.Opacity = Math.Max(0.0, Math.Min(1.0, _lineOpacity));
            brush.Freeze();

            Pen pen = new Pen(brush, Math.Max(0.0, _thickness));
            pen.Freeze();

            drawingContext.DrawGeometry(null, pen, _cachedGeometry);
        }
    }
}
