using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace sudoku
{
    public class SudokuSolver
    {
        public bool isLocked;
        public bool valueIsChanged;
        public bool isNotRealSudoku;
        public bool posibilityIsChanged;
        public bool dataIsChanged;
        public bool tryingPosibilitiesActive;
        public SudokuSolver()
        {

            isLocked = false; isNotRealSudoku = false; dataIsChanged = false; tryingPosibilitiesActive = false;
            //foreach(SudokuTextBox t in Program.SudokuTextBoxList)
            //{
            //    t.Enabled = false;
            //}
            AddAllPosibilities();
        }
        public void AddAllPosibilities()
        {
            foreach (Unit u in Program.mainSudoku.unitList)
            {
                if (u.value != 0) { continue; }
                int[] posArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                u.posibilities = posArr.ToList();
            }
        }
        public void raiseIsNotRealSudoku(Location loc)
        {
            this.isNotRealSudoku = true;
            if(tryingPosibilitiesActive == true) { return; }
            MessageBox.Show("This is not a real sudoku or some of your sudoku values are wrong. We found one => Location (Row:" + loc.RowNo.ToString() + ", Column:" + loc.ColumnNo.ToString());
        }
        public void AfterSolved()
        {
            this.isLocked = true;
            foreach (Unit u in Program.mainSudoku.unitList)
            {
                if (u.value == 0)
                {
                    MessageBox.Show("We couldn't solve this sudoku. Please check if there is any value which you forgot.");
                    return;
                }
            }
            // BİTİMDE SUDOKU DOĞRU MU KONTROLÜ YAPILACAK
            MessageBox.Show("Sudoku has beed solved.");
        }
        public void Solve()
        {
            SolveNormalMethod();
            if (checkIfSolved() == false)
            {
                SolveWithTryingPosibilitiesMethod();
            }
        }
        public void SolveNormalMethod()
        {
            this.dataIsChanged = true;
            SolveUnits();
            // for hard sudoku
            if (checkIfSolved() == false)
            {
                SolveUnitsByPosibilities();
            }
        }
        public sudoku CloneMainSudoku()
        {
            List<Unit> newUnitList = new List<Unit>();
            foreach (Unit u in Program.mainSudoku.unitList)
            {
                Unit newUnit = new Unit(new Location(u.location.RowNo, u.location.ColumnNo));
                newUnit.value = u.value;
                foreach(int p in u.posibilities)
                {
                    newUnit.posibilities.Add(p);
                }
                newUnitList.Add(newUnit);
            }
            return new sudoku(newUnitList);
        }
        public void SolveWithTryingPosibilitiesMethod()
        {
            // try each posibility
            this.tryingPosibilitiesActive = true;
            for (int ii = 0;ii< Program.mainSudoku.unitList.Count(); ii++)
            {
                Unit u = Program.mainSudoku.unitList[ii];
                if (u.value != 0) { continue; }
                bool tryAgain = true;
                while(tryAgain == true)
                {
                    u = Program.mainSudoku.unitList[ii];
                    tryAgain = false;
                    for (int i = 0; i < u.posibilities.Count(); i++)
                    {
                        int p = u.posibilities[i];
                        sudoku BackupSudoku = CloneMainSudoku(); //yedek
                        u.value = p;
                        this.isNotRealSudoku = false;
                        SolveNormalMethod();
                        confirmSolution();
                        if (this.isNotRealSudoku == true)
                        {
                            Program.mainSudoku = BackupSudoku;
                        }
                        else
                        {
                            if(checkIfSolved() == true)
                            {
                                this.isLocked = true;
                                return;
                            }
                            else
                            {
                                Program.mainSudoku = BackupSudoku;
                            }
                        }
                    }
                }
            }
        }
        public void confirmSolution()
        { 
            for(int i=1;i<= 9; i++)
            {
                // confirm row
                List<Unit> rowList = Program.mainSudoku.getRow(i);
                foreach(Unit uu in rowList)
                {
                    if (uu.value == 0) { continue; }
                    foreach (Unit u in rowList)
                    {
                        if(u.value == 0) { continue; }
                        if (uu.location.Equals(u.location)) { continue; }
                        if(uu.value == u.value) { this.isNotRealSudoku = true; return; }
                    }
                }
                // confirm column
                List<Unit> columnList = Program.mainSudoku.getColumn(i);
                foreach (Unit uu in columnList)
                {
                    if (uu.value == 0) { continue; }
                    foreach (Unit u in columnList)
                    {
                        if (u.value == 0) { continue; }
                        if (uu.location.Equals(u.location)) { continue; }
                        if (uu.value == u.value) { this.isNotRealSudoku = true; return; }
                    }
                }
                // confirm group 
                List<Unit> groupList = Program.mainSudoku.getGroup(i);
                foreach (Unit uu in groupList)
                {
                    if (uu.value == 0) { continue; }
                    foreach (Unit u in groupList)
                    {
                        if (u.value == 0) { continue; }
                        if (uu.location.Equals(u.location)) { continue; }
                        if (uu.value == u.value) { this.isNotRealSudoku = true; return; }
                    }
                }
            }
            this.isNotRealSudoku = false;
        }
        public void SolveUnits()
        {
            while (this.dataIsChanged == true)
            {
                this.dataIsChanged = false;
                foreach (Unit u in Program.mainSudoku.unitList)
                {
                    SolveUnit(u);
                }
            }
        }
        public bool checkIfSolved()
        {
            foreach (Unit u in Program.mainSudoku.unitList)
            {
                if (u.value == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public void SolveUnit(Unit u)
        {
            if (isLocked == true || isNotRealSudoku == true) { return; }
            if(u.value != 0) { return; }
            int firstCheck = u.posibilities.Count();
            // check row
            List<Unit> rowList = Program.mainSudoku.getRow(u.location.RowNo);
            FindPosibilities(u, rowList);
            if(checkPosibilities(u) == true) { return; }
            // check column
            List<Unit> columnList = Program.mainSudoku.getColumn(u.location.ColumnNo);
            FindPosibilities(u, columnList);
            if (checkPosibilities(u) == true) { return; }
            // check group
            List<Unit> groupList = Program.mainSudoku.getGroup(u.GroupNo);
            FindPosibilities(u, groupList);
            if (checkPosibilities(u) == true) { return; }
            if (firstCheck != u.posibilities.Count())
            {
                this.dataIsChanged = true;
            }
        }
        public void SolveUnitsByPosibilities()
        {
            if(this.isNotRealSudoku == true) { return; }
            this.dataIsChanged = true;
            while (this.dataIsChanged == true)
            {
                this.dataIsChanged = false;
                // check row
                for (int i = 1; i <= 9; i++)
                {
                    FindTheSinglePosibility(Program.mainSudoku.getRow(i));
                }
                // check column
                for (int i = 1; i <= 9; i++)
                {
                    FindTheSinglePosibility(Program.mainSudoku.getColumn(i));
                }
                // check group
                for (int i = 1; i <= 9; i++)
                {
                    FindTheSinglePosibility(Program.mainSudoku.getGroup(i));
                }
            }
        }
        public void FindTheSinglePosibility(List<Unit> uList) //For High-Level Sudoku
        {
            List<int> NeedList = new List<int>();
            // fill the need list
            foreach(Unit u in uList)
            {
                foreach(int x in u.posibilities)
                {
                    if (!NeedList.Contains(x))
                    {
                        NeedList.Add(x);
                    }
                }
            }
            //find the single
            foreach(int n in NeedList)
            {
                List<Unit> MatchingUnitList = new List<Unit>();
                foreach(Unit u in uList)
                {
                    if(u.value != 0) { continue; }
                    if (u.posibilities.Contains(n)) {
                        MatchingUnitList.Add(u);
                    }
                }
                if (MatchingUnitList.Count == 1)
                {
                    MatchingUnitList[0].value = n;
                    this.dataIsChanged = true;
                    SolveUnits();
                    this.dataIsChanged = true;
                }
            }
        }
        public bool checkPosibilities(Unit u)
        {
            if (u.posibilities.Count() == 0)
            {
                raiseIsNotRealSudoku(u.location);
            }
            if (u.posibilities.Count == 1)
            {
                u.value = u.posibilities[0];
                this.dataIsChanged = true;
                return true;
            }
            return false;
        }
        public void FindPosibilities(Unit myU,List<Unit> uList)
        {
            foreach (Unit u in uList)
            {
                myU.posibilities.Remove(u.value);
            }
            removeIfSamePosibilitiesExist(myU, uList);
        }
        public void removeIfSamePosibilitiesExist(Unit myU, List<Unit> uList)//For Extreme Sudoku
        {
            foreach(Unit u in uList)
            {
                if(u.value != 0) { continue; }
                if (myU.location.Equals(u.location)) { continue; }
                List<Unit> UnitsHaveSamePosibilities = new List<Unit>();
                foreach (Unit uu in uList)
                {
                    if (uu.value != 0) { continue; }
                    if (myU.location.Equals(uu.location)) { continue; }
                    if (u.location.Equals(uu.location)) { continue; }
                    int matches = CountEqualPosibilities(u, uu);
                    if(matches == u.posibilities.Count())
                    {
                        UnitsHaveSamePosibilities.Add(uu);
                    }
                }
                if (u.posibilities.Count() == UnitsHaveSamePosibilities.Count()+1)
                {
                    foreach(int p in u.posibilities)
                    {
                        myU.posibilities.Remove(p);
                    }
                    
                }
            }
        }
        public int CountEqualPosibilities(Unit u1,Unit u2)
        {
            if(u1.posibilities.Count != u2.posibilities.Count()) { return 0; }
            int matches = 0;
            foreach (int p in u1.posibilities)
            {
                if (u2.posibilities.Contains(p)) { matches++; }
            }
            return matches;
        }
        
    }
}
