namespace AdventOfCode.Days;

public class Day_01 : BaseDay
{
    private readonly string _input;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int increase = 0;
        int? lastValue = null;

        foreach (var strDepth in _input.Split(Environment.NewLine))
        {
            var depth = int.Parse(strDepth);

            if (depth > lastValue)
            {
                increase++;
            }

            lastValue = depth;
        }

        return new ValueTask<string>(increase.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int increase = 0;

        var inputs = _input.Split(Environment.NewLine);
        var curPos = 1;
        int? lastValue = null;

        while (curPos < inputs.Length - 1)
        {
            var sums = new List<int>();

            for (int numRuns = 0; numRuns < 2; numRuns++)
            {
                var curSum = 0;

                for (var i = 0; i < 3 ; i++, curPos++)
                {
                    var depth = int.Parse(inputs[curPos - 1]);
                    curSum += depth;
                }

                curPos -= 2;

                if (curSum > lastValue)
                {
                    increase++;
                }

                lastValue = curSum;
            }
        }

        return new ValueTask<string>(increase.ToString());
    }
}
