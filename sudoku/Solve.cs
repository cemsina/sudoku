using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class SudokuSolver
    {
        public bool isLocked;
        public bool isChanged;
        public SudokuSolver()
        {
            isLocked = false;
            foreach(SudokuTextBox t in Program.SudokuTextBoxList)
            {
                t.Enabled = false;
            }
            foreach(Unit u in Program.mainSudoku.unitList)
            {
                if(u.value != 0) { continue; }
                int[] posArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                u.posibilities = posArr.ToList();
            }
        }
        public void checkRows()
        {
            for(int i = 1; i <= 9; i++)
            {
                List<Unit> uList = Program.mainSudoku.getRow(i);
                foreach(Unit newu in uList)
                {
                    if(newu.value != 0) { continue; }
                    int firstCheck = newu.posibilities.Count();
                    foreach (Unit u in uList)
                    {
                        newu.posibilities.Remove(u.value);
                    }
                    if(newu.posibilities.Count == 1) {
                        newu.value = newu.posibilities[0];
                    }
                    if(firstCheck != newu.posibilities.Count() || newu.value != 0)
                    {
                        this.isChanged = true;
                    }
                }
            }
        }
        public void checkColumns()
        {
            for (int i = 1; i <= 9; i++)
            {
                List<Unit> uList = Program.mainSudoku.getColumn(i);
                foreach (Unit newu in uList)
                {
                    if (newu.value != 0) { continue; }
                    int firstCheck = newu.posibilities.Count();
                    foreach (Unit u in uList)
                    {
                        newu.posibilities.Remove(u.value);
                    }
                    if (newu.posibilities.Count == 1)
                    {
                        newu.value = newu.posibilities[0];
                    }
                    if (firstCheck != newu.posibilities.Count() || newu.value != 0)
                    {
                        this.isChanged = true;
                    }
                }
            }
        }
        public void checkGroups()
        {
            for (int i = 1; i <= 9; i++)
            {
                List<Unit> uList = Program.mainSudoku.getGroup(i);
                foreach (Unit newu in uList)
                {
                    if (newu.value != 0) { continue; }
                    int firstCheck = newu.posibilities.Count();
                    foreach (Unit u in uList)
                    {
                        newu.posibilities.Remove(u.value);
                    }
                    if (newu.posibilities.Count == 1)
                    {
                        newu.value = newu.posibilities[0];
                    }
                    if (firstCheck != newu.posibilities.Count() || newu.value != 0)
                    {
                        this.isChanged = true;
                    }
                }
            }
        }
        private void check(List<Unit> uList)
        {

        }
        public void showUI() {
            foreach(SudokuTextBox t in Program.SudokuTextBoxList)
            {
                t.Text = t.unit.value.ToString();
            }
        }
    }
}
