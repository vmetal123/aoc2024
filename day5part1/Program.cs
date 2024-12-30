// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;

var file = File.ReadAllText("input.txt");

var parts = file.Split(Environment.NewLine + Environment.NewLine);

var rules = parts[0].Split(Environment.NewLine)
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Select(x => x.Split('|'))
    .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
    .ToArray();

var updates = parts[1].Split(Environment.NewLine)
    .Select(x => x.Split(',')
    .Select(x => int.Parse(x)).ToArray())
    .ToArray();

int sum = 0;
for (int i = 0; i < updates.Length; i++)
{
    if (IsValid(updates[i]))
    {
        sum += updates[i][updates[i].Length / 2];
    }
}

bool IsValid(int[] update)
{
    for (int i = 0; i < update.Length; i++)
    {
        var currentNumber = update[i];

        foreach (var rule in rules)
        {
            (int left, int right) = rule;
            int leftIndex = Array.IndexOf(update, left);
            int rightIndex = Array.IndexOf(update, right);

            if (currentNumber == left &&
                rightIndex != -1 &&
                rightIndex < i)
            {
                return false;
            }
            else if (currentNumber == right &&
                        leftIndex != -1 &&
                        leftIndex > i)
            {
                return false;
            }
        }
    }

    return true;
}

Console.WriteLine(sum);