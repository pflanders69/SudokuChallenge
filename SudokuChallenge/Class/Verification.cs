using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuChallenge.Class
{
    public class Verification
    {
        public static bool VerifySolution(int[,] puzzle)
        {
            try
            {
                //go through all the tile values and verify they adhere to sudoku rules
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int tileValue = puzzle[i, j];

                        if (ValidateTile(puzzle, i, j, tileValue))
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
            catch
            {
                throw;
            }
        }

        public static bool ValidateTile(int[,] puzzle, int row, int column, int number)
        {
            try
            {
                //make sure the tile value adhears to sudoku rules
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
                    int firstRowOfBlock = Helper.GetFirstRowOrColumnOfBlock(row);
                    int firstColumnOfBlock = Helper.GetFirstRowOrColumnOfBlock(column);

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
            catch
            {
                throw;
            }
        }
    }
}
