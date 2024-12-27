// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("input.txt");

var rows = lines.Length;
var cols = lines[0].Length;

int result = 0;
for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        if (CharAt(row, col) != 'A')
        {
            continue;
        }

        if (IsMAndS(CharAt(row + 1, col + 1), CharAt(row - 1, col - 1)) &&
            IsMAndS(CharAt(row + 1, col - 1), CharAt(row - 1, col + 1)))
        {
            result++;
        }
    }
}

Console.WriteLine(result);

char? CharAt(int row, int col)
{
    if (row < 0 || row >= rows || col < 0 || col >= cols)
    {
        return null;
    }

    return lines[row][col];
}

bool IsMAndS(char? v1, char? v2) =>
(v1 == 'M' && v2 == 'S') || (v1 == 'S' && v2 == 'M');