using System.Globalization;

namespace StockReport;

/// <summary>Reads the stock CSV file into a list of StockItem.</summary>
public static class StockFile
{
    public static List<StockItem> Load(string path)
    {
        string[] lines = File.ReadAllLines(path);
        var items = new List<StockItem>();

        // Line 0 is the header row; data starts at line 1.
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;
            items.Add(ParseRow(lines[i]));
        }
        return items;
    }

    public static StockItem ParseRow(string row)
    {
        string[] fields = row.Split(',');
        return new StockItem(
            Sku: fields[0],
            Name: fields[1],
            Category: fields[2],
            Quantity: int.Parse(fields[3], CultureInfo.InvariantCulture),
            UnitPrice: decimal.Parse(fields[4], CultureInfo.InvariantCulture));
    }
}
