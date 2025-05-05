using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeWinForm.Maze
{
    internal class GameController
    {
        static List<Player> _PlayerList = new List<Player>();
        static int _AliveCnt;

        public static void NewGame(int height_in_cells, int width_in_cells, Form1 form)
        {
            int[,] maze = MazeGenerator.GenerateMazeGame(height_in_cells,width_in_cells, form);
            int lifespan = 10000;
            GeneratePlayers(maze,lifespan);
            _AliveCnt = _PlayerList.Count;
            int step = 0;
            while (step < _PlayerList[0].MoveList.Count)
            {
                NextStep(step);
                Update(step, form);
                step++;
                Thread.Sleep(1);
            }
        }

        private static void Update(int step, Form1 form) 
        {
            int prevStep = step - 1;
            if (prevStep < 0) { prevStep = 0; }

            foreach (Player player in _PlayerList)
            {
                if (player.IsAlive)
                {
                    form.DrawPosition(player.MoveList[step], player.PlayerColor);
                    form.DrawPosition(player.MoveList[prevStep], player.PathColor);
                }
            }
        }

        private static void GeneratePlayers(int[,] maze, int lifespan)
        {
            _PlayerList.Clear();

            int minStartRow = 1;
            int minStartCol = 1;
            int maxStartRow = maze.GetLength(0) - 2;
            int maxStartCol = maze.GetLength(1) - 2;

            Player player1 = new Player();
            player1.IsAlive = true;
            player1.PlayerColor = Color.Purple;
            player1.PathColor = Color.Blue;
            player1.MoveList = MazeSolver.GetWallFollowerMoveList(maze, Tuple.Create(minStartRow, minStartCol), lifespan);
            player1.VisitedCells = new List<Coordinate>();
            _PlayerList.Add(player1);

            //Player player2 = new Player();
            //player2.IsAlive = true;
            //player2.PlayerColor = Color.DarkYellow;
            //player2.PathColor = Color.Yellow;
            //player2.MoveList = MazeSolver.GetWallFollowerMoveList(maze, Tuple.Create(minStartRow, maxStartCol), lifespan);
            //player2.VisitedCells = new List<Coordinate>();
            //_PlayerList.Add(player2);

            //Player player3 = new Player();
            //player3.IsAlive = true;
            //player3.PlayerColor = Color.DarkGreen;
            //player3.PathColor = Color.Green;
            //player3.MoveList = MazeSolver.GetWallFollowerMoveList(maze, Tuple.Create(maxStartRow, minStartCol), lifespan);
            //player3.VisitedCells = new List<Coordinate>();
            //_PlayerList.Add(player3);

            Player player4 = new Player();
            player4.IsAlive = true;
            player4.PlayerColor = Color.Orange;
            player4.PathColor = Color.Red;
            player4.MoveList = MazeSolver.GetWallFollowerMoveList(maze, Tuple.Create(maxStartRow, maxStartCol), lifespan);
            player4.VisitedCells = new List<Coordinate>();
            _PlayerList.Add(player4);


        }

        private static void NextStep(int step)
        {
            foreach (Player player in _PlayerList)
            {
                if (player.IsAlive)
                {
                    if (IsCollision(player, step) && _AliveCnt > 1)
                    {
                        player.IsAlive = false;
                        _AliveCnt--;
                    }
                    else
                    {
                        player.VisitedCells.Add(player.MoveList[step]);
                    }
                }
            }
        }

        private static bool IsCollision(Player player, int step)
        {
            foreach (Player opponent in _PlayerList)
            {
                if (player != opponent)
                {
                    foreach(Coordinate coord in opponent.VisitedCells)
                    {
                        if(Coordinate.IsEqual(coord, player.MoveList[step]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
