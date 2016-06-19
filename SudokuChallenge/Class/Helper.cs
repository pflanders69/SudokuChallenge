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
        #region Static Variables

        public static string filePath = string.Empty;

        #endregion Static Variables

        #region Methods

        public static int[,] loadPuzzleFromFile(string fileName)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void writeSudokuSolutionFile(int[,] puzzle)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool solvePuzzle(int[,] puzzle, int row, int column)
        {
            try
            {
                //make sure the requested row and column are within the array size
                if (row < 9 && column < 9)
                {
                    //go to the next tile if the current one has a value other than zero
                    if (puzzle[row, column] > 0)
                    {
                        //go to next column
                        if (column + 1 < 9)
                        {
                            return solvePuzzle(puzzle, row, column + 1);
                        }
                        //go to then next row if all provious row columns are filled
                        else if (row + 1 < 9)
                        {
                            return solvePuzzle(puzzle, row + 1, 0);
                        }
                        //complete
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 9; ++i)
                        {
                            //try all numbers and populate tile if available, otherwise set to 0
                            if (isPossiblePlay(puzzle, row, column, i + 1))
                            {
                                //set tile to possible value
                                puzzle[row, column] = i + 1;

                                //go to the next column
                                if (column + 1 < 9)
                                {
                                    if (solvePuzzle(puzzle, row, column + 1))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        //if a possible value is not available go to the previous tile and reset to 0
                                        //this is a flag to try the next possible value
                                        puzzle[row, column] = 0;
                                    }
                                }
                                else if (row + 1 < 9)
                                {
                                    //start the next row
                                    if (solvePuzzle(puzzle, row + 1, 0))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        //if a possible value is not available go to the previous tile and reset to 0
                                        //this is a flag to try the next possible value
                                        puzzle[row, column] = 0;
                                    }
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                    }

                    //could not find an available value
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool isPossiblePlay(int[,] puzzle, int row, int column, int number)
        {
            try
            {
                for (int i = 0; i < 9; ++i)
                {
                    //return false if the number exists in any of the mutual areas

                    //check entire row to see if the number already exists
                    if (puzzle[row, i] == number)
                    {
                        return false;
                    }

                    //check entire column to see if number already exists
                    if (puzzle[i, column] == number)
                    {
                        return false;
                    }
                }

                //get the first row and first column of the block containing the passed in row column pair
                int firstRowOfBlock = getFirstRowOrColumnOfBlock(row);
                int firstColumnOfBlock = getFirstRowOrColumnOfBlock(column);

                //check entire block to see if number already exists 
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (puzzle[firstRowOfBlock + j, firstColumnOfBlock + k] == number)
                        {
                            return false;
                        }

                        if (puzzle[firstRowOfBlock + k, firstColumnOfBlock + j] == number)
                        {
                            return false;
                        }
                    }
                }

                //return true if number is not found in any of the mutual areas
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int getFirstRowOrColumnOfBlock(int i)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool verifySolution(int[,] puzzle)
        {
            try
            {
                //go through all the tile values and verify they adhere to soduko rules
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int tileVal = puzzle[i, j];

                        if (validateTile(puzzle, i, j, tileVal))
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool validateTile(int[,] puzzle, int row, int column, int number)
        {
            try
            {
                //make sure the tile value adhears to Soduko rules
                for (int i = 0; i < 9; ++i)
                {
                    //check entire row to see if the number already exists
                    if (puzzle[row, i] == number && i != column)
                    {
                        return false;
                    }

                    //check entire column to see if number already exists
                    if (puzzle[i, column] == number && i != row)
                    {
                        return false;
                    }

                    //get the first row and first column of the block containing the passed in row column pair
                    int firstRowOfBlock = getFirstRowOrColumnOfBlock(row);
                    int firstColumnOfBlock = getFirstRowOrColumnOfBlock(column);

                    //check entire block to see if number already exists 
                    for (int j = 0; j < 3; j++)
                    {
                        //verify tile value is not in the same column of the block
                        if ((puzzle[firstRowOfBlock + j, firstColumnOfBlock] == number)
                            && (firstRowOfBlock + j != row))
                        {
                            return false;
                        }

                        //verify tile value is not in the same row of the block
                        if (puzzle[firstRowOfBlock, firstColumnOfBlock + j] == number
                            && (firstColumnOfBlock + j != column))
                        {
                            return false;
                        }
                    }
                }

                //return true if number is not found in any of the mutual areas
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Methods

    }
}
