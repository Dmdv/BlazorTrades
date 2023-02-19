namespace BlazorTrades.Data;

public interface ILiquidityProvider
{
    Trade GenerateTrade();
    public string Ticker { get; }
}

public class LiquidityProvider : ILiquidityProvider
{
    private const decimal MinPriceChange = 0.01M;
    private const decimal MaxPriceChange = 0.05M;

    private readonly Random _random;

    public LiquidityProvider(string tickerSymbol)
    {
        Ticker = tickerSymbol;
        _random = new Random();
    }

    public Trade GenerateTrade()
    {
        var lastTradePrice = GetLastTradePrice();

        var priceChange =
            lastTradePrice *
            Convert.ToDecimal(_random.NextDouble()) * (MaxPriceChange - MinPriceChange) +
            MinPriceChange;

        // Determine whether the price is rising or falling
        var isPriceRising = _random.NextDouble() < 0.5;
        var tradePrice = isPriceRising ? lastTradePrice + priceChange : lastTradePrice - priceChange;

        // Generate a random trading volume between 1 and 10000
        var tradeVolume = Convert.ToDecimal(_random.NextDouble() * 10000) + 1;

        return new Trade(
            decimal.Round(tradePrice, 3),
            decimal.Round(tradeVolume, 3)
        );
    }

    public string Ticker { get; }

    private decimal GetLastTradePrice()
    {
        return 100M + Convert.ToDecimal(_random.NextDouble()) * 100M;
    }
}