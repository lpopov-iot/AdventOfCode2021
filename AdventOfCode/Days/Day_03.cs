using System.Text;

namespace AdventOfCode.Days;

public class Day_03 : BaseDay
{
    private readonly string _input;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var nums = _input.Split(Environment.NewLine);

        var binResult = GetBinResult(nums, GetOccurrenceList(nums));

        var gamma = Convert.ToInt32(binResult, 2);
        var epsilon = Convert.ToInt32(ReverseBits(binResult), 2);

        return new ValueTask<string>((gamma*epsilon).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var nums = _input.Split(Environment.NewLine);

        var oxygenGenRating = ExecuteBitCriteria(nums, false);
        var co2sScrubberRating = ExecuteBitCriteria(nums, true);

        return new ValueTask<string>((Convert.ToInt32(oxygenGenRating, 2) * Convert.ToInt32(co2sScrubberRating, 2)).ToString());
    }

    private string ExecuteBitCriteria(string[] input, bool isReversed)
    {
        for (int i = 0; i < input[0].Length; i++)
        {
            if (input.Length == 1)
            {
                break;
            }

            var occList = GetOccurrenceList(input);
            var binPattern = GetBinResult(input, GetOccurrenceList(input));

            if (isReversed)
            {
                binPattern = ReverseBits(binPattern);
            }

            if (input.Length % 2 == 0 && occList[i] == input.Length / 2)
            {
                var sb = new StringBuilder(binPattern);
                var result = isReversed
                    ? "0"
                    : "1";
                sb[i] = Convert.ToChar(result);
                binPattern = sb.ToString();
            }

            input = input.Where(x => x[i] == binPattern[i]).ToArray();
        }

        return input.First();
    }

    private static string GetBinResult(string[] nums, List<int> occurrenceList)
    {
        var binResult = "";

        foreach (var occurence in occurrenceList)
        {
            if (occurence >= nums.Length / 2 + 1)
            {
                binResult += 1;
            }
            else
            {
                binResult += 0;
            }
        }

        return binResult;
    }

    private static List<int> GetOccurrenceList(string[] nums)
    {
        var occurrenceDict = new List<int>(new int[nums[0].Length]);

        for (var index = 0; index < nums.Length; index++)
        {
            var num = nums[index];
            for (var i = 0; i < num.Length; i++)
            {
                if (char.GetNumericValue(num[i]) == 1)
                {
                    occurrenceDict[i]++;
                }
            }
        }

        return occurrenceDict;
    }

    private string ReverseBits(string binResult)
    {
        var sb = new StringBuilder(binResult);
        for (int i = 0; i < binResult.Length; i++)
        {
            sb[i] = sb[i] == '0' ? '1' : '0';
        }

        return sb.ToString();
    }
}
