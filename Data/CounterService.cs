namespace BlazorTrades.Data;

public class CounterService : IDisposable
{
    private readonly Random _rnd = new();
    private readonly Timer? _timer;
    
    public event Action? OnChange;

    public CounterService()
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }
    
    public int CurrentValue { get; set; }
    
    private void DoWork(object? state)
    {
        UpdateValue();
        OnChange?.Invoke();
    }
    
    private void UpdateValue()
    {
        CurrentValue = _rnd.Next(100);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}