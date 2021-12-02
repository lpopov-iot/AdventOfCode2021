namespace AdventOfCode.Days;

public class Day_02 : BaseDay
{
    private readonly string _input;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var depth = 0;
        var horizontal = 0;

        foreach (var line in _input.Split(Environment.NewLine))
        {
            var command = line.Split(" ");

            var movement = command[0];
            var magnitute = int.Parse(command[1]);

            switch (movement)
            {
                case "forward":
                    horizontal += magnitute;
                    break;
                case "down":
                    depth += magnitute;
                    break;
                case "up":
                    depth -= magnitute;
                    break;
            }
        }

        return new ValueTask<string>((depth * horizontal).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var aim = 0;
        var depth = 0;
        var horizontal = 0;

        foreach (var line in _input.Split(Environment.NewLine))
        {
            var command = line.Split(" ");

            var movement = command[0];
            var magnitute = int.Parse(command[1]);

            switch (movement)
            {
                case "forward":
                    horizontal += magnitute;
                    depth += (aim * magnitute);
                    break;
                case "down":
                    aim += magnitute;
                    break;
                case "up":
                    aim -= magnitute;
                    break;
            }
        }

        return new ValueTask<string>((depth * horizontal).ToString());
    }
}
