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
            solve.SolveNormalMethod(); ;
            solve.AfterSolved();
            //removeTextChangedHandlers();
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
            var axxxx = Program.mainSudoku;
            foreach (SudokuTextBox t in Program.SudokuTextBoxList)
            {
                Unit tUnit = t.unit;
                if(tUnit.value == 0) {
                    t.Text += "?(";
                    int i = 0;
                    foreach(int p in tUnit.posibilities)
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
                    t.Text = tUnit.value.ToString();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SudokuSolver solve = new SudokuSolver();
            solve.Solve();
            //removeTextChangedHandlers();
            solve.AfterSolved();
            showUI();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")//save
            {
                int i = 0;
                foreach(Unit u in Program.mainSudoku.unitList)
                {
                    if(u.value == 0) { continue; }
                    if(i == 0)
                    {
                        textBox1.Text += u.location.RowNo.ToString() + "," + u.location.ColumnNo.ToString() + "," + u.value.ToString();
                    }
                    else
                    {
                        textBox1.Text += "|"+u.location.RowNo.ToString() + "," + u.location.ColumnNo.ToString() + "," + u.value.ToString();
                    }
                    i++;
                }
            }
            else
            {
                foreach (Unit u in Program.mainSudoku.unitList)
                {
                    u.value = 0;
                }
                String str = textBox1.Text;
                String[] strArr = str.Split('|');
                foreach(string s in strArr)
                {
                    String[] o = s.Split(',');
                    Location loc = new Location(Convert.ToInt32(o[0]), Convert.ToInt32(o[1]));
                    Program.mainSudoku.unitList.getUnitByLocation(loc).value = Convert.ToInt32(o[2]);
                }
                showUI();
            }
        }
    }
}
