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
            Program.mainForm = this;
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

            Program.UI_mainSudoku.Size = new Size(this.Size.Width,this.Size.Height);
            this.Text = Program.UI_mainSudoku.Size.Width.ToString();
            Program.UI_mainSudoku.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SudokuSolver solve = new SudokuSolver();
            solve.Solve();
            solve.AfterSolved();
            removeTextChangedHandlers();
            showUI();
        }
        public void removeTextChangedHandlers()
        {
            button1.Enabled = false;
            foreach (SudokuTextBox t in Program.SudokuTextBoxList)
            {
                t.TextChanged -= t.TextChangedEventHandler;
            }
        }
        public void showUI()
        {
            foreach (SudokuTextBox t in Program.SudokuTextBoxList)
            {
                if(t.unit.value == 0) {
                    t.Text += "?(";
                    int i = 0;
                    foreach(int p in t.unit.posibilities)
                    {
                        if (i == 0) {
                            t.Text += p.ToString();
                        }
                        else
                        {
                            t.Text += ","+p.ToString();
                        }
                        i++;
                    }
                    t.Text += ")";
                }
                else
                {
                    t.Text = t.unit.value.ToString();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Program.mainSudoku.unitList.getUnitByLocation(new Location(1, 1)).value = 2;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(1, 7)).value = 3;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(1, 8)).value = 8;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(3, 1)).value = 1;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(3, 3)).value = 3;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(3, 6)).value = 4;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(3, 8)).value = 5;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(3, 9)).value = 7;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(4, 1)).value = 5;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(4, 3)).value = 7;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(4, 4)).value = 3;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(4, 6)).value = 2;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(4, 7)).value = 8;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(4, 8)).value = 1;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(5, 7)).value = 2;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(5, 8)).value = 3;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(5, 9)).value = 6;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(6, 5)).value = 8;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(7, 7)).value = 1;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(8, 3)).value = 2;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(8, 4)).value = 8;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(9, 2)).value = 6;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(9, 6)).value = 7;
            Program.mainSudoku.unitList.getUnitByLocation(new Location(9, 8)).value = 4;
            showUI();
        }
    }
}
