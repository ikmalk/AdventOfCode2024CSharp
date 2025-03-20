using AdventOfCode2024;
using AdventOfCode2024.Day;

int day = 2;
string input_path = $"D:/Work/rider_workspace/AdventOfCode2024/input/Day{day}.txt";

IDay d = GetClass(day);
d.Start(input_path);


IDay GetClass(int index)
{
    IDay[] days =
    {
        new Day1(),
        new Day2()
    };

    return days[index - 1];
}