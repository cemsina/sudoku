using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public partial class UI_Group : UserControl
    {
        public List<SudokuTextBox> SudokuTextBoxList;
        public UI_Group(List<Unit> uList)
        {
            InitializeComponent();
            for (int i = 0;i < uList.Count(); i++)
            {
                SudokuTextBox t = new SudokuTextBox(uList[i]);
                Program.SudokuTextBoxList.Add(t);
                tableLayoutPanel1.Controls.Add(t, uList[i].location.ColumnNo.ToLayoutNumber(), uList[i].location.RowNo.ToLayoutNumber());
            }
        }
    }
}
