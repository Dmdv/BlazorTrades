using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace BlazorTrades.Data;

/// <summary>
/// Data row displayed at client
/// </summary>
public record Stock(int Position, string Ticker, decimal SpotPrice, decimal Qty1, decimal Qty0, decimal QtyDelta)
{
    public decimal SpotPrice { get; set; } = SpotPrice;
    public decimal Qty1 { get; set; } = Qty1;
    public decimal QtyDelta { get; set; } = QtyDelta;
}

public class StockMarketViewModel
{
    private readonly ILiquidityProvider _provider;
    private readonly IHostApplicationLifetime _appLifetime;

    public event Action? StateUpdated;

    public StockMarketViewModel(Stock stock, ILiquidityProvider provider, IHostApplicationLifetime appLifetime)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        if (_provider.Ticker != stock.Ticker) throw new ArgumentException("Ticker mismatch");

        Model = stock;
        _appLifetime = appLifetime;
    }

    public Stock Model { get; }

    public void Run()
    {
        var cts = CancellationTokenSource.CreateLinkedTokenSource(_appLifetime.ApplicationStopping);

        Random rnd = new();
        var nextDouble = 1000 * rnd.NextDouble();
        
        Observable
            .Interval(TimeSpan.FromMilliseconds(nextDouble))
            .ObserveOn(TaskPoolScheduler.Default)
            .Subscribe(_ => DoWork(cts.Token), cts.Token);
    }

    private void DoWork(CancellationToken tokenSource)
    {
        tokenSource.ThrowIfCancellationRequested();
        var (price, volume) = _provider.GenerateTrade();
        Model.SpotPrice = price;
        Model.Qty1 = (int)volume;
        Model.QtyDelta = Model.Qty0 - Model.Qty1;
        
        StateUpdated?.Invoke();
    }
}