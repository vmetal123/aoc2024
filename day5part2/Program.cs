// See https://aka.ms/new-console-template for more information
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
    if (!IsValid(updates[i]))
    {
        var (graph, degree) = CreateGraph(rules, updates[i]);
        sum += Order(updates[i], graph, degree);
    }
}

Console.WriteLine(sum);

int Order(
    int[] update,
    Dictionary<int, List<int>> graph,
    Dictionary<int, int> degree)
{
    LinkedList<int> queue = new(update.Where(x => degree[x] == 0));
    int[] sortedUpdate = [];

    while (queue.Count > 0)
    {
        var current = queue.First!.Value;
        queue.RemoveFirst();
        sortedUpdate = [.. sortedUpdate, current];
        if (!graph.ContainsKey(current))
        {
            continue;
        }
        foreach (var neighbor in graph[current])
        {
            degree[neighbor]--;
            if (degree[neighbor] == 0)
            {
                queue.AddLast(neighbor);
            }
        }
    }

    return sortedUpdate[sortedUpdate.Length / 2];
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

(Dictionary<int, List<int>>, Dictionary<int, int>) CreateGraph(
    (int, int)[] rules,
    int[] update)
{
    var graph = new Dictionary<int, List<int>>();
    var degree = new Dictionary<int, int>();

    foreach (var rule in rules)
    {
        var (x, y) = rule;
        if (update.Contains(x) && update.Contains(y))
        {
            if (!graph.ContainsKey(x))
            {
                graph.Add(x, [y]);
            }
            else
            {
                graph[x] = [.. graph[x], y];
            }

            if (!degree.ContainsKey(y))
            {
                degree.Add(y, 1);
            }
            else
            {
                degree[y]++;
            }
            if (!degree.ContainsKey(x))
            {
                degree.Add(x, 0);
            }
        }
    }

    return (graph, degree);
}