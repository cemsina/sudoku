using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class Location
    {
        public int RowNo;
        public int ColumnNo;
        public Location(int rNo = 0,int cNo = 0)
        {
            this.RowNo = rNo;
            this.ColumnNo = cNo;
        }
        public virtual bool Equals(Location obj)
        {
            if(obj.RowNo == this.RowNo && obj.ColumnNo == this.ColumnNo)
            {
                return true;
            }
            return false;
        }

    }
}
