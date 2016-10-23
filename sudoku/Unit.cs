using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class Unit
    {
        public Location location;
        public int value;
        public List<int> posibilities;
        public int GroupNo //readonly
        {
            get {
                int r = this.location.RowNo;
                int c = this.location.ColumnNo;
                //row1
                if(r>=1 && r<=3 && c>=1 && c<=3)
                {
                    return 1;
                }
                if (r >= 1 && r <= 3 && c >= 4 && c <= 6)
                {
                    return 2;
                }
                if (r >= 1 && r <= 3 && c >= 7 && c <= 9)
                {
                    return 3;
                }
                //row2
                if (r >= 4 && r <= 6 && c >= 1 && c <= 3)
                {
                    return 4;
                }
                if (r >= 4 && r <= 6 && c >= 4 && c <= 6)
                {
                    return 5;
                }
                if (r >= 4 && r <= 6 && c >= 7 && c <= 9)
                {
                    return 6;
                }
                //row3
                if (r >= 7 && r <= 9 && c >= 1 && c <= 3)
                {
                    return 7;
                }
                if (r >= 7 && r <= 9 && c >= 4 && c <= 6)
                {
                    return 8;
                }
                if (r >= 7 && r <= 9 && c >= 7 && c <= 9)
                {
                    return 9;
                }
                return 0;
            }
            set { }
        }
        public Unit(Location loc)
        {
            this.location = loc;
            this.posibilities = new List<int>();
            this.value = 0;
        }
    }
}
