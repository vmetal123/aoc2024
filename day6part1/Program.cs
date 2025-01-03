// See https://aka.ms/new-console-template for more information
string[] lines = File.ReadAllLines("input.txt");

var grid = new char[lines.Length, lines[0].Length];
var visited = new bool[lines.Length, lines[0].Length];

const char left = '<';
const char right = '>';
const char up = '^';
const char down = 'v';
const char stop = '#';

(int, int) startPosition = (0, 0);

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[0].Length; j++)
    {
        grid[i, j] = lines[i][j];

        if (grid[i, j] == up)
        {
            startPosition = (i, j);
        }
    }
}

Dictionary<char, (int, int)> moves = new Dictionary<char, (int, int)>
{
    { up, (-1, 0) },
    { right, (0, 1)},
    { left, (0, -1)},
    { down, (1, 0)},
};

Dictionary<char, char> turns = new Dictionary<char, char> {
    { up, right },
    { right, down },
    { down, left },
    { left, up },
};

bool IsInside(int x, int y)
{
    return x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1);
}

(int x, int y) = startPosition;

while (IsInside(x, y))
{
    visited[x, y] = true;
    var move = moves[grid[x, y]];
    var symbolDirection = moves.Keys.Single(k => k == grid[x, y]);

    int newx = x + move.Item1;
    int newy = y + move.Item2;

    while (grid[newx, newy] != stop)
    {
        visited[newx, newy] = true;
        newx += move.Item1;
        newy += move.Item2;
        if (!IsInside(newx, newy))
        {
            break;
        }
    }

    if (!IsInside(newx, newy))
    {
        break;
    }

    x = newx - move.Item1;
    y = newy - move.Item2;
    var turn = turns[symbolDirection];
    grid[x, y] = turn;
}

Console.WriteLine(visited.Cast<bool>().Count(b => b));