using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;

internal record BingoEntry(int value, bool hit)
{
    public int value { get; init; } = value;
    public bool hit { get; set; } = hit;
}

internal class Day_04 : BaseDay
{
    private readonly string _input;
    private readonly string[] _splitInput;
    private readonly string[] _nums;
    private readonly List<BingoEntry>[,] _boards;
    private readonly int _numBoards;

    public Day_04()
    {
        _splitInput = File.ReadAllText(InputFilePath).Split("\r\n\r\n");

        _nums = _splitInput[0].Split(',');
        _boards = GenerateBoards(_splitInput[1..]);
        _numBoards = _boards.Length / 5;
    }

    public override ValueTask<string> Solve_1()
    {
        for (var i = 0; i < _nums.Length; i++)
        {
            var num = _nums[i];

            if (i >= 5)
            {
                CheckHits(_boards, out var winningBoard);

                if (winningBoard != null)
                {
                    return CalculateReturnFigure(winningBoard);
                }
            }

            MarkHits(_boards, num);
        }

        return new ValueTask<string>("test1");
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>("test2");
    }
    private List<BingoEntry>[,] GenerateBoards(string[] boardStrings)
    {
        var board = new List<BingoEntry>[boardStrings.Length, 5];

        for (var i = 0; i < boardStrings.Length; i++)
        {
            var boardStr = boardStrings[i];
            var rows = boardStr.Split(Environment.NewLine);

            for (var j = 0; j < 5; j++)
            {
                var row = rows[j];
                var nums = row.Split(' ').ToList();
                nums.RemoveAll(x => x == "");

                board[i, j] = new List<BingoEntry>();
                board[i, j].AddRange(nums.Select(x => new BingoEntry(int.Parse(x), false)));
            }
        }

        return board;
    }

    private ValueTask<string> CalculateReturnFigure(object winningBoard)
    {
        throw new NotImplementedException();
    }

    private object CheckHits(List<BingoEntry>[,] boards, out object unknown)
    {
        throw new NotImplementedException();
    }

    private void MarkHits(List<BingoEntry>[,] boards, string num)
    {
        for (var i = 0; i < _numBoards; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                var row = _boards[i, j];
                for (var k = 0; k < row.Count; k++)
                {
                    var bingoEntry = row[k];
                    if (bingoEntry.value == int.Parse(num))
                    {
                        bingoEntry.hit = true;
                    }
                }
            }
        }
    }
}


