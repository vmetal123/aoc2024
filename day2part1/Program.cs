// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

int safeReportsCount = 0;

foreach (var line in lines)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToList();

    bool isDecreasingOrder = parts[0] > parts[1];
    bool isSafe = true;
    var first = parts[0];

    for (int i = 1; i < parts.Count; i++)
    {
        if (isDecreasingOrder && first < parts[i])
        {
            isSafe = false;
            break;
        }

        if (!isDecreasingOrder && first > parts[i])
        {
            isSafe = false;
            break;
        }

        var difference = isDecreasingOrder
            ? first - parts[i]
            : parts[i] - first;
        if (difference == 0 || difference > 3)
        {
            isSafe = false;
            break;
        }
        first = parts[i];
    }
    if (isSafe)
        safeReportsCount++;
}

Console.WriteLine(safeReportsCount);