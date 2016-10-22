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
        public Unit(Location loc)
        {
            this.location = loc;
            this.posibilities = new List<int>();
            this.value = 0;
        }
    }
}
