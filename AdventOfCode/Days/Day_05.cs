using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days;

internal record Coordinate(int x, int y);

public class Day_05 : BaseDay
{
    private readonly string[] _input;

    public Day_05()
    {
        _input = File.ReadAllText(InputFilePath)
            .Split(Environment.NewLine);
    }

    public override ValueTask<string> Solve_1()
    {
        var coordList = GetCoordsFromInput();

        coordList.RemoveAll(coord =>
        {
            if (coord.Item1.x == coord.Item2.x || coord.Item1.y == coord.Item2.y)
            {
                return false;
            }

            return true;
        });

        var seenCoords = new Dictionary<Coordinate, int>();

        foreach (var coords in coordList)
        {
            var linePoints = GetLinePoints(coords.Item1, coords.Item2);

            foreach (var linePoint in linePoints)
            {
                if (!seenCoords.TryAdd(linePoint, 0))
                {
                    seenCoords[linePoint]++;
                }
            }
        }

        return new ValueTask<string>(seenCoords.Count(x => x.Value > 0).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var coordList = GetCoordsFromInput();
        
        var seenCoords = new Dictionary<Coordinate, int>();

        foreach (var coords in coordList)
        {
            var linePoints = GetLinePoints(coords.Item1, coords.Item2);

            foreach (var linePoint in linePoints)
            {
                if (!seenCoords.TryAdd(linePoint, 0))
                {
                    seenCoords[linePoint]++;
                }
            }
        }

        return new ValueTask<string>(seenCoords.Count(x => x.Value > 0).ToString());
    }

    private List<(Coordinate, Coordinate)> GetCoordsFromInput()
    {
        var coordList = new List<(Coordinate, Coordinate)>();

        foreach (var line in _input)
        {
            var coords = line.Split(" -> ");

            var start = coords[0].Split(',');
            var end = coords[1].Split(',');

            coordList.Add((
                new Coordinate(int.Parse(start[0]), int.Parse(start[1])),
                new Coordinate(int.Parse(end[0]), int.Parse(end[1]))
            ));
        }

        return coordList;
    }

    private List<Coordinate> GetLinePoints(Coordinate p1, Coordinate p2)
    {
        var linePoints = new List<Coordinate>();

        if (p1.y == p2.y)
        {
            var startX = Math.Min(p1.x, p2.x);
            var endX = Math.Max(p1.x, p2.x);

            for (int i = startX; i <= endX; i++)
            {
                linePoints.Add(new Coordinate(i, p2.y));
            }
        }

        if (p1.x == p2.x)
        {
            var startY = Math.Min(p1.y, p2.y);
            var endY = Math.Max(p1.y, p2.y);

            for (int i = startY; i <= endY; i++)
            {
                linePoints.Add(new Coordinate(p2.x, i));
            }
        }

        if (p1.x != p2.x && p1.y != p2.y)
        {
            var absX = Math.Abs(p1.x - p2.x);

            linePoints.Add(p1);
            for (var i = 1; i < absX; i++)
            {
                var prevCoord = linePoints[i - 1];

                int newX, newY;
                if (p2.x < prevCoord.x)
                {
                    newX = prevCoord.x - 1;
                }
                else
                {
                    newX = prevCoord.x + 1;
                }

                if (p2.y < prevCoord.y)
                {
                    newY = prevCoord.y - 1;
                }
                else
                {
                    newY = prevCoord.y + 1;
                }

                linePoints.Add(new Coordinate(newX, newY));
            }

            linePoints.Add(p2);
        }

        return linePoints;
    }
}


