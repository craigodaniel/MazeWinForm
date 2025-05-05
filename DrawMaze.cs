using System.Text;

namespace Maze.Maze
{
    internal class DrawMaze
    {
        static int _MAZEWIDTH = 100;
        
        private static int[,] FillMaze(int height, int width, int value)
        {
            int[,] maze = new int[height, width];
            int row = maze.GetLength(0);
            int col = maze.GetLength(1);

            for (int i = 0; i < row * col; i++)
            {
                maze[i / col, i % col] = value;
            }           

            return maze;
        }

        private static int[,] NumberedMaze(int height, int width)
        {
            int[,] maze = new int[height, width];
            int row = maze.GetLength(0);
            int col = maze.GetLength(1);

            for (int i = 0; i < row * col; i++)
            {
                maze[i / col, i % col] = (i / col) * 10 + (i % col);
            }

            return maze;
        }

        public static void PrintMaze(int[,] maze)
        {
            int row = maze.GetLength(0);
            int col = maze.GetLength(1);

            var sb = new StringBuilder();
            
            for (int y = 0; y < row; y++)
            {
                string line = "";
                for (int x = 0; x < col; x++)
                {
                    char tile;
                    switch (maze[y, x])
                    {
                        case 0:
                            tile = ' ';
                            break;
                        case 1:
                            tile = '#';
                            break;
                        default:
                            tile = 'X';
                            break;
                    }
                
                    if (x == 0) {  line = tile.ToString(); }
                    else
                    {
                        line = string.Format("{0}{1}", line, tile.ToString());
                    }
                    
                }
                sb.AppendLine(line);
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(sb);
        }

        public static void PrintMazeSlow(int[,] maze)
        {
            int row = maze.GetLength(0);
            int col = maze.GetLength(1);

            var sb = new StringBuilder();

            for (int y = 0; y < row; y++)
            {
                string line = "";
                for (int x = 0; x < col; x++)
                {
                    char tile;
                    switch (maze[y, x])
                    {
                        case 0:
                            tile = ' ';
                            break;
                        case 1:
                            tile = '#';
                            break;
                        default:
                            tile = 'X';
                            break;
                    }

                    if (x == 0) { line = tile.ToString(); }
                    else
                    {
                        line = string.Format("{0}{1}", line, tile.ToString());
                    }

                }
                sb.AppendLine(line);
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(sb);
            Thread.Sleep(10);
        }

        public static void PrintMazeSlowColor(int[,] maze)
        {
            int row = maze.GetLength(0);
            int col = maze.GetLength(1);

            

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (maze[y,x] == 0) 
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                    //else if (maze[y,x] == 1) { Console.BackgroundColor = ConsoleColor.Black; }
                    
                }
                
            }

            //Thread.Sleep(10);
        }

        public static void DrawSection(Tuple<int,int> cell)
        {
            Console.SetCursorPosition(cell.Item2, cell.Item1);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.ResetColor();
        }
        public static void PrintNumberedMaze(int height, int width)
        {
            PrintMaze(NumberedMaze(height, width));
        }

        public static void PrintBlankMaze(int height, int width) 
        {
            PrintMaze(FillMaze(height, width, 0));
        }

        
    }
}
