using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace sudoku
{
    public class SudokuTextBox : TextBox
    {
        public EventHandler TextChangedEventHandler;
        private Unit p_unit;
        public Unit unit
        {
            get { return Program.mainSudoku.unitList.getUnitByLocation(location); }
        }
        public Location location;
        public SudokuTextBox(Location loc)
        {
            TextChangedEventHandler = new EventHandler(this.TextChangedHandler);
            this.TextChanged += TextChangedEventHandler;
            this.EnabledChanged += new EventHandler(this.EnabledChangedHandler);
            location = loc;
            this.Enabled = true;
            this.Dock = DockStyle.Fill;
            this.Anchor = AnchorStyles.None;
        }
        private void EnabledChangedHandler(object sender,EventArgs e)
        {
            if(this.Enabled == true)
            {
                this.BackColor = Color.AntiqueWhite;
            }else
            {
                this.BackColor = Color.Gray;
            }
        }
        private void TextChangedHandler(object sender, EventArgs e)
        {
            int v;
            try
            {
                v = Convert.ToInt32(this.Text);
                if(v == 0) { this.Text = ""; }
            }
            catch
            {
                this.unit.value = 0;
                this.Text = "";
                return;
            }
            if (v > 9 || v < 1)
            {
                this.Text = "";
                this.unit.value = 0;
            }
            else
            {
                this.unit.value = v;
            }   
        }
    }
}