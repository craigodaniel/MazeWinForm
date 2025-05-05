using System.Windows.Forms;
using System.Xml.Linq;

namespace MazeWinForm
{
    public partial class Form1 : Form
    {
        int height_in_cells = 30;
        int width_in_cells = 30;
        public Form1()
        {
            InitializeComponent();
            FillMaze();

        }
        public void FillMaze()
        {
            int height = 3 + 2 * (height_in_cells - 1); // 3+2(n-1)
            int width = 3 + 2 * (width_in_cells - 1); // 3+2(n-1)

            int cnt = 0;
            Color bkcolor = Color.Red;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bkcolor = Color.Black;

                    CreateLabel(x * 10, y * 10, bkcolor);
                }
            }
        }

        public void CreateLabel(int x, int y, Color bkcolor)
        {
            Label label = new Label();
            label.Location = new Point(x, y);
            label.Width = 10;
            label.Height = 10;
            label.Text = "";
            label.BackColor = bkcolor;
            this.Controls.Add(label);

        }

        public void DrawSection(Tuple<int, int> cell)
        {
            int skipCnt = 0;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() != typeof(Label))
                {
                    skipCnt++;
                }
            }
            int width = 3 + 2 * (width_in_cells - 1); // 3+2(n-1)
            int index = (cell.Item1) + ((cell.Item2) * width) + skipCnt;

            if (index < this.Controls.Count)
            {
                Console.WriteLine(index.ToString() + " " + cell.ToString());
                Control label = this.Controls[index];
                label.BackColor = Color.White;
                label.Refresh();
                //Thread.Sleep(1);
            }
            


        }

        public void ResetMaze()
        {
            foreach (Control cell in this.Controls)
            {
                if(cell.GetType() == typeof(Label))
                {
                    cell.BackColor = Color.Black;
                }
                
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Maze.MazeGenerator.GenerateMaze(height_in_cells, width_in_cells, this);
        }

        private void btn_Reset_Maze_Click(object sender, EventArgs e)
        {
            ResetMaze();
            this.Refresh();
        }
    }
}
