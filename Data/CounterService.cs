namespace BlazorTrades.Data;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

public class CounterService
{
    private readonly ILogger<CounterService> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly Random _rnd = new();
    public event Action? OnChange;

    public CounterService(
        ILogger<CounterService> logger,
        IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        
        Task.Run(Run);
    }

    public int CurrentValue { get; private set; }

    private void Run()
    {
        var cts = CancellationTokenSource.CreateLinkedTokenSource(_appLifetime.ApplicationStopping);
        
        Observable
            .Interval(TimeSpan.FromSeconds(1))
            .ObserveOn(TaskPoolScheduler.Default)
            .Subscribe(_ => DoWork(cts.Token), cts.Token);
    }

    private void DoWork(CancellationToken cancelToken)
    {
        cancelToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Updating value");
        UpdateValue();
        OnChange?.Invoke();
    }
    
    private void UpdateValue()
    {
        CurrentValue = _rnd.Next(100);
    }
}