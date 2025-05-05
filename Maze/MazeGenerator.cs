using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeWinForm.Maze
{
    internal class MazeGenerator
    {
        static List<Tuple<int, int>> _VisitedCells = new List<Tuple<int, int>>();
        static Stack<Tuple<int, int>> _stack = new Stack<Tuple<int, int>>();

        public static int[,] GenerateMaze(int height_in_cells, int width_in_cells, Form1 theForm)
        {
            // Iterative implementation (with stack)
            //https://en.wikipedia.org/wiki/Maze_generation_algorithm

            Reset(); //clear global variables
            int[,] maze = NewMazeWithCells(height_in_cells, width_in_cells);


            // Choose the initial cell, mark it as visited and push it to the stack
            Tuple<int, int> startCell = new Tuple<int, int>(1, 1);
            _VisitedCells.Add(startCell);
            _stack.Push(startCell);


            while (_stack.Count > 0) // while the stack is not empty
            {
                Tuple<int, int> currentCell = _stack.Pop(); //pop cell from stack and make it a current cell

                List<Tuple<int, int>> unvisitedNeighborCells = GetUnvisitedNeighborCells(currentCell, maze);

                if (unvisitedNeighborCells.Count > 0) //if the current cell has any neighbors which have not been visited
                {
                    _stack.Push(currentCell); //push current cell to stack


                    Tuple<int, int> chosenCell = GetRandomUnivisitedNeighborCell(unvisitedNeighborCells); //choose on of the unvisited neighbors

                    maze[(currentCell.Item1 + chosenCell.Item1) / 2, (currentCell.Item2 + chosenCell.Item2) / 2] = 0; //remove the wall between the current cell and the chosen cell

                    //draw new sections
                    theForm.DrawSection(currentCell);
                    theForm.DrawSection(chosenCell);
                    theForm.DrawSection(Tuple.Create((currentCell.Item1 + chosenCell.Item1) / 2, (currentCell.Item2 + chosenCell.Item2) / 2));
                    //theForm.Refresh();
                    //Thread.Sleep(1);

                    //mark chosen cell as visited and push to the stack
                    _VisitedCells.Add(chosenCell);
                    _stack.Push(chosenCell);

                    //Console.Clear();
                    //PrintMazeSlowColor(maze);
                }

            }

            return maze;
        }

        public static int[,] GenerateMazeGame(int height_in_cells, int width_in_cells, Form1 form)
        {
            // Iterative implementation (with stack)
            //https://en.wikipedia.org/wiki/Maze_generation_algorithm

            Reset(); //Clear list and stack
            int[,] maze = NewMazeWithCells(height_in_cells, width_in_cells);


            // Choose the initial cell, mark it as visited and push it to the stack
            Tuple<int, int> startCell = new Tuple<int, int>(1, 1);
            _VisitedCells.Add(startCell);
            _stack.Push(startCell);


            while (_stack.Count > 0) // while the stack is not empty
            {
                Tuple<int, int> currentCell = _stack.Pop(); //pop cell from stack and make it a current cell

                List<Tuple<int, int>> unvisitedNeighborCells = GetUnvisitedNeighborCells(currentCell, maze);

                if (unvisitedNeighborCells.Count > 0) //if the current cell has any neighbors which have not been visited
                {
                    _stack.Push(currentCell); //push current cell to stack


                    Tuple<int, int> chosenCell = GetRandomUnivisitedNeighborCell(unvisitedNeighborCells); //choose on of the unvisited neighbors

                    maze[(currentCell.Item1 + chosenCell.Item1) / 2, (currentCell.Item2 + chosenCell.Item2) / 2] = 0; //remove the wall between the current cell and the chosen cell

                    //draw new sections
                    form.DrawSection(currentCell);
                    form.DrawSection(chosenCell);
                    form.DrawSection(Tuple.Create((currentCell.Item1 + chosenCell.Item1) / 2, (currentCell.Item2 + chosenCell.Item2) / 2));
                    //Thread.Sleep(1);

                    //mark chosen cell as visited and push to the stack
                    _VisitedCells.Add(chosenCell);
                    _stack.Push(chosenCell);

                    //Console.Clear();
                    //PrintMazeSlowColor(maze);
                }

            }

            return maze;
        }
        private static int[,] NewMazeWithCells(int height_in_cells, int width_in_cells)
        {
            int height = 3 + 2 * (height_in_cells - 1); // 3+2(n-1)
            int width = 3 + 2 * (width_in_cells - 1); // 3+2(n-1)

            int[,] maze = new int[height, width];
            int rowCnt = maze.GetLength(0);
            int colCnt = maze.GetLength(1);

            for (int i = 0; i < rowCnt * colCnt; i++)
            {
                int row = i / colCnt;
                int col = i % colCnt;
                int value;

                if ((row - 1) % 2 == 0 && (col - 1) % 2 == 0) //-1 to shift walls up and left
                {
                    value = 0; // 0 for path
                }
                else
                {
                    value = 1; // 1 for wall
                }

                maze[row, col] = value;
            }

            return maze;
        }

        private static int[,] NewMazeWithCells()
        {
            int height = Console.BufferHeight; //where 0.5(n - 3) + 1 is whole number aka odd integer > 3
            if (height % 2 == 0) { height--; } //prevent maze from oveflowing console buffer
            int width = Console.BufferWidth;
            if (width % 2 == 0) { width--; }

            int[,] maze = new int[height, width];
            int rowCnt = maze.GetLength(0);
            int colCnt = maze.GetLength(1);

            for (int i = 0; i < rowCnt * colCnt; i++)
            {
                int row = i / colCnt;
                int col = i % colCnt;
                int value;

                if ((row - 1) % 2 == 0 && (col - 1) % 2 == 0) //-1 to shift walls up and left
                {
                    value = 0;
                }
                else
                {
                    value = 1;
                }

                maze[row, col] = value;
            }

            return maze;
        }

        private static List<Tuple<int, int>> GetUnvisitedNeighborCells(Tuple<int, int> cell, int[,] maze)
        {
            List<Tuple<int, int>> neighborCells = new List<Tuple<int, int>>();
            int y = cell.Item1;
            int x = cell.Item2;
            int rowCnt = maze.GetLength(0);
            int colCnt = maze.GetLength(1);

            if (y - 2 > 0) { neighborCells.Add(Tuple.Create(y - 2, x)); }
            if (x - 2 > 0) { neighborCells.Add(Tuple.Create(y, x - 2)); }
            if (y + 2 < rowCnt) { neighborCells.Add(Tuple.Create(y + 2, x)); }
            if (x + 2 < colCnt) { neighborCells.Add(Tuple.Create(y, x + 2)); }

            for (int i = 0; i < neighborCells.Count; i++)
            {
                if (_VisitedCells.Contains(neighborCells[i]))
                {
                    neighborCells.Remove(neighborCells[i]);
                    i--;
                }
            }

            return neighborCells;

        }

        private static Tuple<int, int> GetRandomUnivisitedNeighborCell(List<Tuple<int, int>> unvisitedNeighborCells)
        {
            var rand = new Random();
            return unvisitedNeighborCells[rand.Next(unvisitedNeighborCells.Count)];
        }

        public static void Reset()
        {
            _VisitedCells.Clear();
            _stack.Clear();
        }
    }
}
