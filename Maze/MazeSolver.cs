using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeWinForm.Maze
{
    internal class MazeSolver
    {
        
        public static void WallFollower(int height_in_cells, int width_in_cells, Form1 form)
        {
            //Right hand follow
            int height = 3 + 2 * (height_in_cells - 1); // 3+2(n-1)
            int width = 3 + 2 * (width_in_cells - 1); // 3+2(n-1)

            int[,] maze = MazeGenerator.GenerateMaze(height_in_cells, width_in_cells, form);
            Tuple<int, int> _startPos = new Tuple<int, int>(1, 1);
            Tuple<int, int> _endPos = new Tuple<int, int>(height - 2, width - 2);

            string direction = "N";
            Coordinate startPos = Coordinate.ConvertToCoord(_startPos);            
            Coordinate endPos = Coordinate.ConvertToCoord(_endPos);
            Coordinate currentPos = Coordinate.Copy(startPos);
            Coordinate nextPos = Coordinate.Copy(currentPos);
            Coordinate lastPos = Coordinate.Copy(currentPos);
            Color playerColor = Color.Red;
            Color pathColor = Color.Yellow;
            form.DrawPosition(endPos, Color.Green);
            Coordinate[] coords = new Coordinate[] { currentPos, nextPos,lastPos };
            Dictionary<string, int> coordList = new Dictionary<string, int>();


            while (!Coordinate.IsEqual(coords[0], endPos))
            {
                bool movedForward = false;
                // if open on right turn right and move forward
                if (!IsWallRight(coords[0], maze, direction))
                {
                    direction = TurnRight(direction);
                    coords = MoveForward(direction, coords[0], coords[1], coords[2]);
                    movedForward = true;
                }
                // if wall on right and open ahead, move forward
                else if (IsPathAhead(coords[0], maze, direction))
                {
                    coords = MoveForward(direction, coords[0], coords[1], coords[2]);
                    movedForward = true;
                }
                // if wall on right and wall ahead, turn left
                else
                {
                    direction = TurnLeft(direction);
                    movedForward = false;
                }

                if (movedForward)
                {
                    string key = "";
                    key = coords[0].X.ToString() + "-" + coords[0].Y.ToString();
                    if (coordList.ContainsKey(key))
                    {
                        coordList[key]++;
                        pathColor = Color.Yellow;
                    }
                    else
                    {
                        coordList.Add(key, 1);
                        pathColor = Color.Green;
                    }
                }
                

                form.DrawPosition(coords[2], pathColor);
                form.DrawPosition(coords[0], playerColor);
                Thread.Sleep(10);
            }

            Console.Beep(); // you win!

        }

        public static List<Coordinate> GetWallFollowerMoveList(int[,] maze, Tuple<int, int> _startPos, int steps_to_live)
        {
            //Right hand follow
            string direction = "N";
            Coordinate startPos = Coordinate.ConvertToCoord(_startPos);
            Coordinate currentPos = Coordinate.Copy(startPos);
            Coordinate nextPos = Coordinate.Copy(currentPos);
            Coordinate lastPos = Coordinate.Copy(currentPos);
            Coordinate[] coords = new Coordinate[] { currentPos, nextPos, lastPos };
            List<Coordinate> moveList = new List<Coordinate>();

            int cellCnt = 0;
            foreach (int cell in maze)
            {
                if (cell == 0)
                {
                    cellCnt++;
                }
            }
            cellCnt = cellCnt * 2;

            while (cellCnt > 0)
            {
                // if open on right turn right and move forward
                if (!IsWallRight(coords[0], maze, direction))
                {
                    direction = TurnRight(direction);
                    coords = MoveForward(direction, coords[0], coords[1], coords[2]);
                }
                // if wall on right and open ahead, move forward
                else if (IsPathAhead(coords[0], maze, direction))
                {
                    coords = MoveForward(direction, coords[0], coords[1], coords[2]);
                }
                // if wall on right and wall ahead, turn left
                else
                {
                    direction = TurnLeft(direction);
                }

                moveList.Add(Coordinate.Copy(coords[0])); //currentPos
                cellCnt--;
            }

            return moveList;

        }

        private static Coordinate[] MoveForward(string direction, Coordinate currentPos, Coordinate nextPos, Coordinate lastPos)
        {
            nextPos = Coordinate.Copy(currentPos);

            switch (direction)
            {
                case "N":
                    nextPos = Coordinate.IncrUp(nextPos);
                    break;

                case "E":
                    nextPos = Coordinate.IncrRight(nextPos);
                    break;

                case "S":
                    nextPos = Coordinate.IncrDown(nextPos);
                    break;

                case "W":
                    nextPos = Coordinate.IncrLeft(nextPos);
                    break;
            }

            lastPos = Coordinate.Copy(currentPos);
            currentPos = Coordinate.Copy(nextPos);

            return new Coordinate[] { currentPos, nextPos, lastPos };
        }

        private static string TurnRight(string direction)
        {
            switch (direction)
            {
                case "N":
                    direction = "E";
                    break;
                case "E":
                    direction = "S";
                    break;
                case "S":
                    direction = "W";
                    break;
                case "W":
                    direction = "N";
                    break;
                default:
                    //should not happen
                    direction = "N";
                    break;
            }
            return direction;
        }

        private static string TurnLeft(string direction)
        {
            switch (direction)
            {
                case "N":
                    direction = "W";
                    break;
                case "E":
                    direction = "N";
                    break;
                case "S":
                    direction = "E";
                    break;
                case "W":
                    direction = "S";
                    break;
                default:
                    //should not happen
                    direction = "N";
                    break;
            }
            return direction;
        }

        private static bool IsPath(Coordinate position, int[,] maze)
        {
            return (maze[position.Y, position.X] != 1);
            
        }

        private static bool IsPathAhead(Coordinate position, int[,] maze, string direction)
        {
            switch (direction)
            {
                case "N":
                    return (maze[position.Y -1, position.X] != 1);
                case "E":
                    return (maze[position.Y, position.X + 1] != 1);
                case "S":
                    return (maze[position.Y + 1, position.X] != 1);
                case "W":
                    return (maze[position.Y, position.X - 1] != 1);
                default:
                    //should not happen
                    return false;
            }
        }

        private static bool IsWallRight(Coordinate position, int[,] maze, string direction)
        {
            switch (direction)
            {
                case "N":
                    return (maze[position.Y, position.X + 1] != 0); //check wall E
                case "E":
                    return (maze[position.Y + 1, position.X] != 0); //check wall S
                case "S":
                    return (maze[position.Y, position.X - 1] != 0); //check wall W
                case "W":
                    return (maze[position.Y - 1, position.X] != 0); //check wall N
                default:
                    //should not happen
                    return false;
            }
        }

        
        public static Tuple<int,int> GetBottomLeftCorner(int[,] maze)
        {
            int x = 0;
            int y = 0;
            for (int row = 0;  row < maze.GetLength(0); row++)
            {
                for (int col = 0; col < maze.GetLength(1); col++)
                {
                    if (maze[row, col] == 0)
                    {
                        x = row;
                        y = col;
                    }
                }
            }

            return Tuple.Create(x, y);
        }

        public static Tuple<int, int> GetTopRightCorner(int[,] maze)
        {
            int x = 0;
            int y = 0;

            for (int row = 0; row < maze.GetLength(0); row++)
            {
                for (int col = 0; col < maze.GetLength(1); col++)
                {
                    if (maze[row, col] == 0)
                    {
                        x = row;
                        y = col;
                        return Tuple.Create(x, y);
                    }
                }
            }

            return Tuple.Create(x, y);
        }
    }
}
