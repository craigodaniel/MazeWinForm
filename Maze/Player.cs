using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeWinForm.Maze
{
    public class Player
    {
        public bool IsAlive { get; set; }
        public Color PlayerColor { get; set; }
        public Color PathColor { get; set; }
        public List<Coordinate> MoveList { get; set; }
        public List<Coordinate> VisitedCells { get; set; }
        
        

    }
}
