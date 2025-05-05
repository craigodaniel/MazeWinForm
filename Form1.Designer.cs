namespace MazeWinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            btn_Reset_Maze = new Button();
            btn_New_Game = new Button();
            btn_Solve_Maze = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(652, 31);
            button1.Name = "button1";
            button1.Size = new Size(120, 29);
            button1.TabIndex = 0;
            button1.Text = "Create Maze";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btn_Reset_Maze
            // 
            btn_Reset_Maze.Location = new Point(652, 66);
            btn_Reset_Maze.Name = "btn_Reset_Maze";
            btn_Reset_Maze.Size = new Size(120, 29);
            btn_Reset_Maze.TabIndex = 2;
            btn_Reset_Maze.Text = "Reset";
            btn_Reset_Maze.UseVisualStyleBackColor = true;
            btn_Reset_Maze.Click += btn_Reset_Maze_Click;
            // 
            // btn_New_Game
            // 
            btn_New_Game.Location = new Point(652, 136);
            btn_New_Game.Name = "btn_New_Game";
            btn_New_Game.Size = new Size(118, 29);
            btn_New_Game.TabIndex = 3;
            btn_New_Game.Text = "New Game";
            btn_New_Game.UseVisualStyleBackColor = true;
            btn_New_Game.Click += btn_New_Game_Click;
            // 
            // btn_Solve_Maze
            // 
            btn_Solve_Maze.Location = new Point(652, 101);
            btn_Solve_Maze.Name = "btn_Solve_Maze";
            btn_Solve_Maze.Size = new Size(118, 29);
            btn_Solve_Maze.TabIndex = 4;
            btn_Solve_Maze.Text = "Solve Maze";
            btn_Solve_Maze.UseVisualStyleBackColor = true;
            btn_Solve_Maze.Click += btn_Solve_Maze_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 753);
            Controls.Add(btn_Solve_Maze);
            Controls.Add(btn_New_Game);
            Controls.Add(btn_Reset_Maze);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button btn_Reset_Maze;
        private Button btn_New_Game;
        private Button btn_Solve_Maze;
    }
}
