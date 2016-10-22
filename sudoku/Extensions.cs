using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public static class Extensions
    {
        public static Unit getUnitByLocation(this List<Unit> uList,Location loc)
        {
            for(int i = 0; i < uList.Count(); i++)
            {
                if (uList[i].location.Equals(loc))
                {
                    return uList[i];
                }
            }
            return null;
        }
        public static int ToLayoutNumber(this int x)
        {
            if(x > 3)
            {
                return (x % 3) -1;
            }else
            {
                return x - 1;
            }
        }
    }
}
