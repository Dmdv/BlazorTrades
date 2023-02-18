namespace BlazorTrades.Data;

public class LiquidityProvider
{
    private const decimal MinPriceChange = 0.01M;
    private const decimal MaxPriceChange = 0.05M;

    private readonly string _tickerSymbol;
    private readonly Random _random;

    public LiquidityProvider(string tickerSymbol)
    {
        _tickerSymbol = tickerSymbol;
        _random = new Random();
    }

    public (decimal Price, decimal Volume) GenerateTrade()
    {
        var lastTradePrice = GetLastTradePrice();
        var priceChange = lastTradePrice * 
                          (decimal)_random.NextDouble() * (MaxPriceChange - MinPriceChange) +
                          MinPriceChange;
        // Determine whether the price is rising or falling
        var isPriceRising = _random.NextDouble() < 0.5;
        var tradePrice = isPriceRising ? lastTradePrice + priceChange : lastTradePrice - priceChange;
        // Generate a random trading volume between 1 and 10000
        var tradeVolume = (long)(_random.NextDouble() * 10000) + 1;

        return (tradePrice, tradeVolume);
    }

    private decimal GetLastTradePrice()
    {
        return 100M + (decimal)_random.NextDouble() * 100M;
    }
}