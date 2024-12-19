using System.Diagnostics;

var start = Stopwatch.GetTimestamp();

var lines = File.ReadAllLines("input.txt");

var dict = new Dictionary<string, int>();
int[] left = [];

foreach (var line in lines)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    left = [.. left, int.Parse(parts[0])];
    if (dict.ContainsKey(parts[1]))
    {
        dict[parts[1]] += 1;
    }
    else
    {
        dict[parts[1]] = 1;
    }
}

int result = 0;
for (int i = 0; i < left.Length; i++)
{
    if (dict.ContainsKey(left[i].ToString()))
    {
        result += left[i] * dict[left[i].ToString()];
    }
    else
    {
        result += 0;
    }
}

Console.WriteLine(result);

var end = Stopwatch.GetElapsedTime(start);

Console.WriteLine(end.TotalMilliseconds);