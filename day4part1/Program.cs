// See https://aka.ms/new-console-template for more information
using System.IO.Pipelines;

var lines = File.ReadAllLines("input.txt");

(int, int)[] directions = [
    (1,0),
    (1,1),
    (0,1),
    (-1,1),
    (-1,0),
    (-1,-1),
    (0,-1),
    (1,-1)
];

int rows = lines.Length;
int cols = lines[0].Length;
int count = 0;

for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        if (lines[row][col] != 'X')
        {
            continue;
        }

        for (int k = 0; k < directions.Length; k++)
        {
            if (SearchWord(row, col, directions[k], "XMAS"))
            {
                count++;
            }
        }
    }
}

bool SearchWord(
    int row,
    int col,
    (int, int) direction,
    string word)
{
    (int colDir, int rowDir) = direction;

    for (int charIndex = 0; charIndex < word.Length; charIndex++)
    {
        int positionRow = row + rowDir * charIndex;
        int positionCol = col + colDir * charIndex;

        if (positionRow < 0 || positionRow >= rows || positionCol < 0 || positionCol >= cols)
        {
            return false;
        }

        var currentChar = lines[positionRow][positionCol];
        var currentWordChar = word[charIndex];

        if (currentChar != currentWordChar)
        {
            return false;
        }
    }

    return true;
}

Console.WriteLine(count);