namespace BlazorTrades.Data;

/// <summary>
/// Data row displayed at client 
/// </summary>
internal record StockView(int Position, string Ticker, decimal SpotPrice, int Qty1, int Qty0, int QtyDelta);

/// <summary>
/// Market current data
/// </summary>
internal record Stock(string Ticker, decimal Price, int Qty, DateTime CurrentTime);