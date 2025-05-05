using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeWinForm.Maze
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Coordinate ConvertToCoord(Tuple<int, int> yxTuple)
        {
            Coordinate coord = new Coordinate();
            coord.X = yxTuple.Item2;
            coord.Y = yxTuple.Item1;
            return coord;
        }

        public static Coordinate IncrUp(Coordinate coord)
        {
            coord.Y--;
            return coord;
        }
        public static Coordinate IncrRight(Coordinate coord)
        {
            coord.X++;
            return coord;
        }

        public static Coordinate IncrDown(Coordinate coord)
        {
            coord.Y++;
            return coord;
        }

        public static Coordinate IncrLeft(Coordinate coord)
        {
            coord.X--;
            return coord;
        }

        public static Coordinate Copy(Coordinate from)
        {
            Coordinate coord = new Coordinate();
            coord.X = from.X;
            coord.Y = from.Y;
            return coord;
        }

        public static bool IsEqual(Coordinate a, Coordinate b)
        {
            return (a.X == b.X && a.Y == b.Y); //returns true if a and b coordinates are the same
        }

    }
}
