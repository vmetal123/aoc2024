// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

int safeReportsCount = 0;

foreach (var line in lines)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToList();


    if (IsSafe(parts))
    {
        safeReportsCount++;
    }
    else
    {
        for (int i = 0; i < parts.Count; i++)
        {
            var backup = parts.ToList();
            backup.RemoveAt(i);

            if (IsSafe(backup))
            {
                safeReportsCount++;
                break;
            }
        }
    }
}

bool IsSafe(List<int> parts)
{
    bool isDecreasingOrder = parts[0] > parts[1];
    var first = parts[0];

    for (int i = 1; i < parts.Count; i++)
    {
        if (isDecreasingOrder && first < parts[i])
        {
            return false;
        }

        if (!isDecreasingOrder && first > parts[i])
        {
            return false;
        }

        var difference = isDecreasingOrder
            ? first - parts[i]
            : parts[i] - first;
        if (difference < 1 || difference > 3)
        {
            return false;
        }
        first = parts[i];
    }

    return true;
}

Console.WriteLine(safeReportsCount);