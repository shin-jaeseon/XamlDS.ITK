using System;
using System.Threading;
using System.Threading.Tasks;

namespace XamlDS.ITK.ViewModels.Assets.PowerGenerators;

/// <summary>
/// Base ViewModel for power generator assets (e.g., diesel, wind, solar with inverter, etc.).
/// Provides common telemetry, lifecycle operations, and state tracking.
/// </summary>
public abstract class PowerGeneratorAvm(string name) : AssetVm(name)
{
    private double _ratedPowerKw;
    /// <summary>
    /// Nameplate capacity (kW). Used for UI scaling and validation.
    /// </summary>
    public double RatedPowerKw
    {
        get => _ratedPowerKw;
        set
        {
            var clamped = Math.Max(0, value);
            if (SetProperty(ref _ratedPowerKw, clamped))
            {
                // Load factor depends on RatedPowerKw
                OnPropertyChanged(nameof(LoadFactor));
                // Ensure output stays within new bounds
                if (_outputPowerKw > clamped && clamped > 0)
                {
                    OutputPowerKw = clamped;
                }
            }
        }
    }

    private double _outputPowerKw;
    /// <summary>
    /// Current real-time output power (kW).
    /// </summary>
    public double OutputPowerKw
    {
        get => _outputPowerKw;
        set
        {
            var max = _ratedPowerKw > 0 ? _ratedPowerKw : double.MaxValue;
            var clamped = Math.Clamp(value, 0, max);
            if (SetProperty(ref _outputPowerKw, clamped))
            {
                OnPropertyChanged(nameof(LoadFactor));
            }
        }
    }

    /// <summary>
    /// Ratio of current output to rated capacity (0..1). Returns 0 when RatedPowerKw is not set.
    /// </summary>
    public double LoadFactor => _ratedPowerKw <= 0 ? 0 : _outputPowerKw / _ratedPowerKw;

    private double _efficiency = 1.0; // 0..1
    /// <summary>
    /// Electrical efficiency (0..1). If unknown, leave at 1.0.
    /// </summary>
    public double Efficiency
    {
        get => _efficiency;
        set => SetProperty(ref _efficiency, Math.Clamp(value, 0.0, 1.0));
    }

    private bool _isRunning;
    /// <summary>
    /// Indicates whether the generator is currently running.
    /// </summary>
    public bool IsRunning
    {
        get => _isRunning;
        protected set
        {
            if (SetProperty(ref _isRunning, value))
            {
                if (value)
                {
                    LastStartedAt = DateTimeOffset.Now;
                }
                else
                {
                    LastStoppedAt = DateTimeOffset.Now;
                }
                OnPropertyChanged(nameof(RuntimeSinceStart));
            }
        }
    }

    private bool _isFaulted;
    /// <summary>
    /// Fault state flag. When true, StartAsync should not engage until reset.
    /// </summary>
    public bool IsFaulted
    {
        get => _isFaulted;
        protected set => SetProperty(ref _isFaulted, value);
    }

    private DateTimeOffset? _lastStartedAt;
    /// <summary>
    /// Timestamp of last successful start.
    /// </summary>
    public DateTimeOffset? LastStartedAt
    {
        get => _lastStartedAt;
        protected set
        {
            if (SetProperty(ref _lastStartedAt, value))
            {
                OnPropertyChanged(nameof(RuntimeSinceStart));
            }
        }
    }

    private DateTimeOffset? _lastStoppedAt;
    /// <summary>
    /// Timestamp of last stop.
    /// </summary>
    public DateTimeOffset? LastStoppedAt
    {
        get => _lastStoppedAt;
        protected set => SetProperty(ref _lastStoppedAt, value);
    }

    private double _totalEnergyKWh;
    /// <summary>
    /// Lifetime energy produced (kWh), accumulated via <see cref="Tick"/> or derived logic.
    /// </summary>
    public double TotalEnergyKWh
    {
        get => _totalEnergyKWh;
        protected set => SetProperty(ref _totalEnergyKWh, Math.Max(0, value));
    }

    /// <summary>
    /// If running, the duration since <see cref="LastStartedAt"/>; otherwise 0.
    /// </summary>
    public TimeSpan RuntimeSinceStart => IsRunning && LastStartedAt is { } s
        ? DateTimeOffset.Now - s
        : TimeSpan.Zero;

    /// <summary>
    /// Accumulates energy and updates runtime-driven properties.
    /// Call periodically with the elapsed time since last tick.
    /// </summary>
    public void Tick(TimeSpan elapsed)
    {
        if (elapsed <= TimeSpan.Zero) return;

        if (IsRunning && _outputPowerKw > 0)
        {
            var energyKWh = _outputPowerKw * elapsed.TotalHours;
            TotalEnergyKWh += energyKWh;
        }

        OnPropertyChanged(nameof(RuntimeSinceStart));
    }

    /// <summary>
    /// Convenience method to update common telemetry at once.
    /// </summary>
    public void UpdateTelemetry(double? outputPowerKw = null, double? efficiency = null, bool? isFaulted = null)
    {
        if (outputPowerKw is double p) OutputPowerKw = p;
        if (efficiency is double e) Efficiency = e;
        if (isFaulted is bool f) IsFaulted = f;
    }

    /// <summary>
    /// Attempts to start the generator. Derived classes should implement device-specific behavior in <see cref="OnStartAsync"/>.
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (IsRunning || IsFaulted) return;
        await OnStartAsync(cancellationToken).ConfigureAwait(false);
        IsRunning = true;
    }

    /// <summary>
    /// Attempts to stop the generator. Derived classes should implement device-specific behavior in <see cref="OnStopAsync"/>.
    /// </summary>
    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (!IsRunning) return;
        await OnStopAsync(cancellationToken).ConfigureAwait(false);
        IsRunning = false;
        OutputPowerKw = 0;
    }

    /// <summary>
    /// Clears a fault condition in software. Override to add device-level clearing if needed.
    /// </summary>
    public virtual Task ResetFaultAsync(CancellationToken cancellationToken = default)
    {
        IsFaulted = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Device/technology-specific start routine.
    /// </summary>
    protected abstract Task OnStartAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Device/technology-specific stop routine.
    /// </summary>
    protected abstract Task OnStopAsync(CancellationToken cancellationToken);
}
