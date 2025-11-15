using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XamlDS.ITK.WPF.Controls
{
    public class TouchButtonBase : Control, ICommandSource
    {
        static TouchButtonBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TouchButtonBase), new FrameworkPropertyMetadata(typeof(TouchButtonBase)));

            // Make it focusable by default so it can receive keyboard events (Enter/Space)
            //FocusableProperty.OverrideMetadata(typeof(ClickControl), new FrameworkPropertyMetadata(true));

            // Coerce IsEnabled so it reflects both the base value and Command.CanExecute, like Button
            UIElement.IsEnabledProperty.OverrideMetadata(
                typeof(TouchButtonBase),
                new FrameworkPropertyMetadata(true, null, CoerceIsEnabled));
        }

        // Read-only DP: IsPressed
        private static readonly DependencyPropertyKey IsPressedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(IsPressed),
                typeof(bool),
                typeof(TouchButtonBase),
                new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsPressedProperty = IsPressedPropertyKey.DependencyProperty;

        public bool IsPressed
        {
            get => (bool)GetValue(IsPressedProperty);
            protected set => SetValue(IsPressedPropertyKey, value);
        }

        // ICommandSource: Command
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(TouchButtonBase),
                new FrameworkPropertyMetadata(null, OnCommandChanged));

        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        // ICommandSource: CommandParameter
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                nameof(CommandParameter),
                typeof(object),
                typeof(TouchButtonBase),
                new FrameworkPropertyMetadata(null, OnCommandParameterChanged));

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        // ICommandSource: CommandTarget
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(
                nameof(CommandTarget),
                typeof(IInputElement),
                typeof(TouchButtonBase),
                new FrameworkPropertyMetadata(null, OnCommandTargetChanged));

        public IInputElement? CommandTarget
        {
            get => (IInputElement?)GetValue(CommandTargetProperty);
            set => SetValue(CommandTargetProperty, value);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TouchButtonBase)d;

            if (e.OldValue is ICommand oldCommand)
            {
                oldCommand.CanExecuteChanged -= control.OnCanExecuteChanged;
            }

            if (e.NewValue is ICommand newCommand)
            {
                newCommand.CanExecuteChanged += control.OnCanExecuteChanged;
            }

            control.UpdateCanExecute();
        }

        private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TouchButtonBase)d).UpdateCanExecute();
        }

        private static void OnCommandTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TouchButtonBase)d).UpdateCanExecute();
        }

        private void OnCanExecuteChanged(object? sender, System.EventArgs e)
        {
            UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            // Recompute IsEnabled using coercion to respect both base IsEnabled and Command.CanExecute
            CoerceValue(UIElement.IsEnabledProperty);
        }

        private static object CoerceIsEnabled(DependencyObject d, object baseValue)
        {
            var control = (TouchButtonBase)d;

            // If base value is false (e.g., explicitly disabled), stay disabled regardless of Command
            bool baseEnabled = (bool)baseValue;
            if (!baseEnabled)
            {
                // Ensure pressed state is cleared if disabled
                if (control.IsPressed)
                    control.IsPressed = false;
                return false;
            }

            var command = control.Command;
            if (command == null)
            {
                // No command -> use base value
                return baseEnabled;
            }

            var parameter = control.CommandParameter;
            bool canExecute;

            if (command is RoutedCommand routed)
            {
                canExecute = routed.CanExecute(parameter, control.CommandTarget ?? control);
            }
            else
            {
                canExecute = command.CanExecute(parameter);
            }

            return baseEnabled && canExecute;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!IsEnabled)
                return;

            if (e.ButtonState == MouseButtonState.Pressed)
            {
                CaptureMouse();
                IsPressed = true;
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!IsEnabled)
                return;

            if (IsMouseCaptured)
            {
                // Keep pressed only while pointer stays over the control
                IsPressed = IsMouseOver;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (!IsEnabled)
                return;

            if (IsMouseCaptured)
            {
                bool shouldClick = IsPressed && IsMouseOver;
                ReleaseMouseCapture();
                IsPressed = false;

                if (shouldClick)
                {
                    ExecuteCommand();
                    e.Handled = true;
                }
            }
            else if (!e.Handled && IsMouseOver)
            {
                // Fallback: treat as click if we didn't have capture
                ExecuteCommand();
                e.Handled = true;
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            IsPressed = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!IsEnabled)
                return;

            if (!e.Handled)
            {
                if (e.Key == Key.Enter)
                {
                    ExecuteCommand();
                    e.Handled = true;
                }
                else if (e.Key == Key.Space && !IsPressed)
                {
                    // Begin keyboard press; click will occur on KeyUp
                    IsPressed = true;
                    e.Handled = true;
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!IsEnabled)
                return;

            if (!e.Handled && e.Key == Key.Space)
            {
                bool wasPressed = IsPressed;
                IsPressed = false;
                if (wasPressed)
                {
                    ExecuteCommand();
                    e.Handled = true;
                }
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            IsPressed = false;
        }

        private void ExecuteCommand()
        {
            var command = Command;
            if (command == null)
                return;

            var parameter = CommandParameter;

            if (command is RoutedCommand routed)
            {
                var target = CommandTarget ?? this;
                if (routed.CanExecute(parameter, target))
                {
                    routed.Execute(parameter, target);
                }
            }
            else
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
    }
}
