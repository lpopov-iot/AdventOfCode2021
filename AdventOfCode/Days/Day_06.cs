using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;

public class Day_06 : BaseDay
{
    private readonly string[] _input;
    private long[] _school;

    public class Lanternfish
    {
        private Lanternfish()
        {
        }

        public Lanternfish(int daysToReproduce)
        {
            DaysToReproduce = daysToReproduce;
        }

        public int DaysToReproduce { get; set; } = 8;

        public void LiveDay(out Lanternfish babyFish)
        {
            if (DaysToReproduce == 0)
            {
                DaysToReproduce = 6;
                babyFish = new Lanternfish();
                return;
            }

            DaysToReproduce--;
            babyFish = null;
        }
    }

    public Day_06()
    {
        _input = File.ReadAllText(InputFilePath).Split(',');
    }

    public override ValueTask<string> Solve_1()
    {
        _school = new long[9];
        var epochCount = 80;

        SeedSchool();
        SimulateDays(epochCount);
        
        return new ValueTask<string>(_school.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        _school = new long[9];
        var epochCount = 256;

        SeedSchool();
        SimulateDays(epochCount);

        return new ValueTask<string>(_school.Sum().ToString());
    }

    private void SimulateDays(int epochCount)
    {
        for (int i = 0; i < epochCount; i++)
        {
            _school[(i + 7) % 9] += _school[i % 9];
        }
    }

    private void SeedSchool()
    {
        foreach (var entry in _input)
        {
            _school[int.Parse(entry)]++;
        }
    }
}

