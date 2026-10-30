namespace StockReport;

/// <summary>
/// One line of the warehouse stock file: a product, how many are on
/// the shelf, and what one of them costs. A record, because a stock
/// line is data — two lines with the same values are the same line.
/// </summary>
public record StockItem(
    string Sku,
    string Name,
    string Category,
    int Quantity,
    decimal UnitPrice)
{
    /// <summary>What the whole line is worth: quantity times price.</summary>
    public decimal LineValue => Quantity * UnitPrice;
}
