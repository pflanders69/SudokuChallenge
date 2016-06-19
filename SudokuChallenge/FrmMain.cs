using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SudokuChallenge.Class;
using System.Threading;
using System.IO;

namespace SudokuChallenge
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbBxPuzzles_SelectedIndexChanged(object sender, EventArgs e)
        {            
            selectPuzzle();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            if (Helper.solvePuzzle(puzzle, 0, 0))
            {
                displayPuzzle(puzzle);

                if (Helper.verifySolution(puzzle))
                {
                    Helper.writeSudokuSolutionFile(puzzle);
                }
                else
                {
                    MessageBox.Show("The solution failed validation. The puzzle was not solved.", "Soduko Challenge",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        #endregion Events

        #region Static Variables
        
        private static int[,] puzzle;

        #endregion Variables

        #region Methods

        private void selectPuzzle()
        {
            try
            {
                clearPuzzle();

                string fileName = cmbBxPuzzles.SelectedItem.ToString().ToLower().Replace(" ", string.Empty) + ".txt";

                if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\Puzzles\\" + fileName))
                {
                    MessageBox.Show(cmbBxPuzzles.SelectedItem.ToString() + " was not found. Please select another puzzle.", "Soduko Challenge",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                puzzle = Helper.loadPuzzleFromFile(fileName);

                displayPuzzle(puzzle);

                btnSolve.Enabled = true;               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void displayPuzzle(int[,] puzzle)
        {
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    { 
                        string location = i.ToString() + j.ToString();
                        string value = puzzle[i, j].ToString();

                        if (value != "0")
                        {
                            setTileValue(location, value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void clearPuzzle()
        {
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        string location = i.ToString() + j.ToString();
                        setTileValue(location, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void setTileValue(string location, string tileValue)
        {
            try
            {
                //find the tile for the location provided and set it's value
                foreach (Control control in panel1.Controls)
                {
                    if (control is RichTextBox)
                    {                        
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }                        
                }

                foreach (Control control in panel2.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel3.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel4.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel5.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel6.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel7.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel8.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

                foreach (Control control in panel9.Controls)
                {
                    if (control is RichTextBox)
                    {
                        if (control.Name == "rchTxtBx" + location)
                        {
                            control.Text = " " + tileValue;
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Methods
    }
}
