// See https://aka.ms/new-console-template for more information
char[] lines = File.ReadAllText("input.txt").ToCharArray();

string operation = string.Empty;

int j = 0;
int result = 0;
while (j < lines.Length)
{
    if (lines[j] != 'm')
    {
        j++;
        continue;
    }

    operation = $"{lines[j]}{lines[j + 1]}{lines[j + 2]}";

    if (operation != "mul")
    {
        operation = string.Empty;
        j++;
        continue;
    }

    j += 3;

    if (lines[j] != '(')
    {
        operation = string.Empty;
        j++;
        continue;
    }

    j++;
    string x = string.Empty;
    while (char.IsDigit(lines[j]))
    {
        x += lines[j];
        j++;
    }

    if (lines[j] != ',')
    {
        operation = string.Empty;
        x = string.Empty;
        j++;
        continue;
    }

    j++;
    string y = string.Empty;
    while (char.IsDigit(lines[j]))
    {
        y += lines[j];
        j++;
    }

    if (lines[j] != ')')
    {
        operation = string.Empty;
        x = string.Empty;
        y = string.Empty;
        j++;
        continue;
    }

    result += int.Parse(x) * int.Parse(y);
    operation = string.Empty;
    x = string.Empty;
    y = string.Empty;
    j++;
}

Console.WriteLine(result);