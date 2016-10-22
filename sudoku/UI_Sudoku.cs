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
    public partial class UI_Sudoku : UserControl
    {
        public UI_Sudoku(sudoku s)
        {
            InitializeComponent();
            int tRow = 0;
            int tCol = 0;
            for(int i = 1;i<= 9; i++)
            {
                UI_Group newgroup = new UI_Group(s.getGroup(i));
                newgroup.Anchor = AnchorStyles.None;
                newgroup.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(newgroup,tCol,tRow);
                tCol++;
                if(tCol == 3)
                {
                    tCol = 0;
                    tRow++;
                }
            }
        }
    }
}
