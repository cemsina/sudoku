using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static sudoku mainSudoku;
        public static UI_Sudoku UI_mainSudoku;
        public static List<SudokuTextBox> SudokuTextBoxList;
        public static Form mainForm;
        [STAThread]
        static void Main()
        {
            SudokuTextBoxList = new List<SudokuTextBox>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
