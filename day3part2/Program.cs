// See https://aka.ms/new-console-template for more information
using System.Runtime.Serialization;

var lines = File.ReadAllText("input.txt").ToCharArray();

string instruction = string.Empty;
int doInstructionIndex = -1;
int dontInstructionIndex = -1;

int j = 0;
int result = 0;
while (j < lines.Length)
{
    if (lines[j] != 'm' && lines[j] != 'd')
    {
        j++;
        continue;
    }

    if (lines[j] == 'd')
    {
        var currentDoinstruction = $"{lines[j]}{lines[j + 1]}";
        if (currentDoinstruction != "do")
        {
            j += 2;
            continue;
        }

        j += 2;
        if (lines[j] == '(' && lines[j + 1] == ')')
        {
            doInstructionIndex = j;
            j += 2;
            continue;
        }

        currentDoinstruction += $"{lines[j]}{lines[j + 1]}{lines[j + 2]}";
        if (currentDoinstruction != "don't")
        {
            j += 3;
            continue;
        }

        j += 3;
        if (lines[j] == '(' && lines[j + 1] == ')')
        {
            dontInstructionIndex = j;
            j += 2;
            continue;
        }
    }

    if (lines[j] == 'm')
    {
        if (dontInstructionIndex != -1 && dontInstructionIndex > doInstructionIndex)
        {
            j++;
            continue;
        }

        var currentInstruction = $"{lines[j]}{lines[j + 1]}{lines[j + 2]}";
        if (currentInstruction != "mul")
        {
            j += 3;
            continue;
        }

        j += 3;
        if (lines[j] != '(')
        {
            j++;
            continue;
        }

        j++;
        var x = string.Empty;
        while (char.IsDigit(lines[j]))
        {
            x += lines[j];
            j++;
        }

        if (lines[j] != ',')
        {
            j++;
            continue;
        }

        j++;
        var y = string.Empty;
        while (char.IsDigit(lines[j]))
        {
            y += lines[j];
            j++;
        }

        if (lines[j] != ')')
        {
            j++;
            continue;
        }

        if ((doInstructionIndex == -1 && dontInstructionIndex == -1) ||
            doInstructionIndex > dontInstructionIndex)
        {
            result += int.Parse(x) * int.Parse(y);
            j++;
        }
    }
}

Console.WriteLine(result);