using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class sudoku
    {
        public List<Unit> unitList;
        public sudoku(List<Unit> uList)
        {
            unitList = new List<Unit>();
            for(int r =1; r<= 9; r++)
            {
                for(int c=1; c <= 9; c++)
                {
                    Location loc = new Location(r, c);
                    Unit u = uList.getUnitByLocation(loc);
                    if(u != null)
                    {
                        unitList.Add(u);
                    }else
                    {
                        Unit newU = new Unit(loc);
                        unitList.Add(newU);
                    }
                }
            }
        }
        
        public List<Unit> getGroup(int groupNo)
        {
            switch (groupNo)
            {
                case 1:
                    return getGroupHandler(1,3,1,3);
                    break;
                case 2:
                    return getGroupHandler(1,3,4,6);
                    break;
                case 3:
                    return getGroupHandler(1,3,7,9);
                    break;
                case 4:
                    return getGroupHandler(4,6,1,3);
                    break;
                case 5:
                    return getGroupHandler(4,6,4,6);
                    break;
                case 6:
                    return getGroupHandler(4,6,7,9);
                    break;
                case 7:
                    return getGroupHandler(7,9,1,3);
                    break;
                case 8:
                    return getGroupHandler(7,9,4,6);
                    break;
                case 9:
                    return getGroupHandler(7,9,7,9);
                    break;
                default:
                    return null;
            }
        }
        public List<Unit> getColumn(int columnNo)
        {
            List<Unit> rtrn = new List<Unit>();
            for(int i = 0; i < this.unitList.Count(); i++)
            {
                Unit u = this.unitList[i];
                if(u.location.ColumnNo == columnNo) {
                    rtrn.Add(u);
                }
            }
            return rtrn;
        }
        public List<Unit> getRow(int rowNo)
        {
            List<Unit> rtrn = new List<Unit>();
            for (int i = 0; i < this.unitList.Count(); i++)
            {
                Unit u = this.unitList[i];
                if (u.location.RowNo == rowNo)
                {
                    rtrn.Add(u);
                }
            }
            return rtrn;
        }
        private List<Unit> getGroupHandler(int rStart,int rFinish,int cStart,int cFinish)
        {
            List<Unit> rtrn = new List<Unit>();
            for(int r = rStart;r <= rFinish; r++)
            {
                for(int c = cStart;c <= cFinish; c++)
                {
                    rtrn.Add(this.unitList.getUnitByLocation(new Location(r,c)));
                }
            }
            return rtrn;
        }
    }
}
