using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Unit> uList = new List<Unit>();
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 1; c <= 9; c++)
                {
                    Location loc = new Location(r, c);
                    Unit u = new Unit(loc);
                    u.value = 0;
                    uList.Add(u);
                }
            }
            Program.mainSudoku = new sudoku(uList);
            Program.UI_mainSudoku = new UI_Sudoku(Program.mainSudoku);
            panel1.Controls.Add(Program.UI_mainSudoku);
            Program.UI_mainSudoku.Dock = DockStyle.Fill;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Program.UI_mainSudoku.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SudokuSolver solve = new SudokuSolver();
            solve.checkGroups();
            solve.checkRows();
            solve.checkColumns();
            solve.showUI();
        }
    }
}
