using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace SudokuChallenge.Class
{
    public class Helper
    {
        #region Static Fields

        private static string filePath = string.Empty;

        #endregion Static Fields

        #region Methods

        public static int[,] LoadPuzzleFromFile(string fileName)
        {
            try
            {
                string line = string.Empty;
                int[,] puzzle = new int[9, 9];
                int lineCount = 0;
                filePath = System.Windows.Forms.Application.StartupPath + "\\Puzzles\\" + fileName;

                if (File.Exists(filePath))
                {
                    // Read the file and load the Array
                    System.IO.StreamReader file = new System.IO.StreamReader(filePath);

                    while ((line = file.ReadLine()) != null)
                    {
                        int colNum = 0;

                        foreach (char chr in line.ToArray())
                        {
                            //returns -1 if character does not represent a number
                            int tempInt = Convert.ToInt32(Char.GetNumericValue(chr));

                            //replace non integer characters with a 0(zero)
                            if (tempInt > -1)
                            {
                                puzzle[lineCount, colNum] = tempInt;
                            }
                            else
                            {
                                puzzle[lineCount, colNum] = 0;
                            }

                            colNum++;
                        }

                        lineCount++;
                    }

                    file.Close();
                }

                return puzzle;

            }
            catch
            {
                throw;
            }
        }

        public static void WriteSudokuSolutionFile(int[,] puzzle)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Path.ChangeExtension(filePath, ".sln.txt"));

                for (int i = 1; i < 10; ++i)
                {
                    for (int j = 1; j < 10; ++j)
                    {
                        sw.Write(puzzle[i - 1, j - 1]);
                    }

                    sw.WriteLine();
                }

                sw.Close();
            }
            catch
            {
                throw;
            }
        }        

        public static int GetFirstRowOrColumnOfBlock(int i)
        {
            try
            {
                int[] tmpArray;

                tmpArray = new int[3] { 0, 1, 2 };
                if (tmpArray.Contains(i))
                {
                    return 0;
                }

                tmpArray = new int[3] { 3, 4, 5 };
                if (tmpArray.Contains(i))
                {
                    return 3;
                }

                tmpArray = new int[3] { 6, 7, 8 };
                if (tmpArray.Contains(i))
                {
                    return 6;
                }

                return -1;
            }
            catch
            {
                throw;
            }
        }

        #endregion Methods

    }
}
