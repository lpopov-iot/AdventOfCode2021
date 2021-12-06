using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;

internal record BingoEntry(int value, bool hit)
{
    public bool hit { get; set; } = hit;
}

internal class Day_04 : BaseDay
{
    private readonly string _input;
    private readonly string[] _splitInput;
    private readonly string[] _nums;
    private List<BingoEntry>[,] _boards;
    private int _numBoards;

    public Day_04()
    {
        _splitInput = File.ReadAllText(InputFilePath).Split("\r\n\r\n");

        _nums = _splitInput[0].Split(',');
    }

    public override ValueTask<string> Solve_1()
    {
        _boards = GenerateBoards(_splitInput[1..]);

        var returnNum = 0;

        for (var i = 0; i < _nums.Length; i++)
        {
            var num = int.Parse(_nums[i]);

            MarkHits(_boards, num);

            if (i >= 5)
            {
                CheckHits(_boards, out var winningBoards);

                if (winningBoards.Count == 1)
                {
                    returnNum = CalculateReturnFigure(winningBoards[0], num);
                    break;
                }
            }
        }

        return new ValueTask<string>(returnNum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        _boards = GenerateBoards(_splitInput[1..]);

        var returnNum = 0;
        var boardsRemaining = SeedBoardsRemaining();

        for (var i = 0; i < _nums.Length; i++)
        {
            var num = int.Parse(_nums[i]);

            MarkHits(_boards, num);

            if (i >= 5)
            {
                var winningBoardNums = CheckHits(_boards, out var winningBoards);

                if (boardsRemaining.Count == 1 && winningBoardNums.Contains(boardsRemaining[0]))
                {
                    returnNum = CalculateReturnFigure(GetBoard(boardsRemaining[0]), num);
                }

                if (winningBoards.Count > 0 )
                {
                    winningBoardNums.ForEach(x => boardsRemaining.Remove(x));
                }
            }
        }

        return new ValueTask<string>(returnNum.ToString());
    }

    private List<int> SeedBoardsRemaining()
    {
        var returnList = new List<int>();

        for (int i = 0; i <= _numBoards - 1; i++)
        {
            returnList.Add(i);
        }

        return returnList;
    }

    private List<BingoEntry>[,] GenerateBoards(string[] boardStrings)
    {
        var boards = new List<BingoEntry>[boardStrings.Length, 5];

        for (var i = 0; i < boardStrings.Length; i++)
        {
            var boardStr = boardStrings[i];
            var rows = boardStr.Split(Environment.NewLine);

            for (var j = 0; j < 5; j++)
            {
                var row = rows[j];
                var nums = row.Split(' ').ToList();
                nums.RemoveAll(x => x == "");

                boards[i, j] = new List<BingoEntry>();
                boards[i, j].AddRange(nums.Select(x => new BingoEntry(int.Parse(x), false)));
            }
        }

        _numBoards = boards.Length / 5;

        return boards;
    }

    private int CalculateReturnFigure(List<BingoEntry>[,] winningBoard, int winningNum)
    {
        var sumUnmarked = 0;

        foreach (var row in winningBoard)
        {
            sumUnmarked += row.Where(x => !x.hit).Sum(x => x.value);
        }

        return sumUnmarked * winningNum;
    }

    private List<int> CheckHits(List<BingoEntry>[,] boards, out List<List<BingoEntry>[,]> winningBoards)
    {
        var winningBoardNums = new List<int>();
        winningBoards = new List<List<BingoEntry>[,]>();

        for (int i = 0; i < _numBoards; i++)
        {
            var board = GetBoard(i);
            if (AnyHorizontalHits(board) || AnyVerticalHits(board))
            {
                winningBoards.Add(GetBoard(i));
                winningBoardNums.Add(i);
            }
        }

        return winningBoardNums;
    }

    private List<BingoEntry>[,] GetBoard(int n)
    {
        var new_arr = new List<BingoEntry>[1, 5];

        for (int x = 0; x < 5; x++)
        {

            new_arr[0, x] = new List<BingoEntry>();
            new_arr[0, x].AddRange(_boards[n, x]);
        }

        return new_arr;
    }

    private bool AnyVerticalHits(List<BingoEntry>[,] board)
    {
        for (var i = 0; i < 5; i++)
        {
            var column = new List<BingoEntry>();

            foreach (var row in board)
            {
                column.Add(row[i]);
            }

            if (column.All(x => x.hit))
            {
                return true;
            }

        }

        return false;
    }

    private bool AnyHorizontalHits(List<BingoEntry>[,] board)
    {
        foreach (var row in board)
        {
            if (row.All(x => x.hit))
            {
                return true;
            }
        }

        return false;
    }

    private void MarkHits(List<BingoEntry>[,] boards, int num)
    {
        for (var i = 0; i < _numBoards; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                var row = _boards[i, j];
                for (var k = 0; k < row.Count; k++)
                {
                    var bingoEntry = row[k];
                    if (bingoEntry.value == num)
                    {
                        bingoEntry.hit = true;
                    }
                }
            }
        }
    }
}


