using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuChallenge.Class
{
    public class Solver
    {
        public static bool SolvePuzzle(int[,] puzzle, int row, int column)
        {
            try
            {
                //make sure the requested row and column are within the array size
                if (row < 9 && column < 9)
                {
                    int nextColumn = column + 1;
                    int nextRow = row + 1;

                    //go to the next tile if the current one has a value other than zero
                    if (puzzle[row, column] > 0)
                    {
                        //go to next column
                        if (nextColumn < 9)
                        {
                            return SolvePuzzle(puzzle, row, nextColumn);
                        }
                        //go to then next row if the previous rows columns are filled
                        else if (row + 1 < 9)
                        {
                            return SolvePuzzle(puzzle, nextRow, 0);
                        }

                        //complete
                        return true;

                    }
                    else
                    {
                        for (int i = 0; i < 9; ++i)
                        {
                            //try all numbers and populate tile if available, otherwise set to 0
                            if (IsPossiblePlay(puzzle, row, column, i + 1))
                            {
                                //set tile to possible value
                                puzzle[row, column] = i + 1;

                                //go to the next column
                                if (nextColumn < 9)
                                {
                                    if (SolvePuzzle(puzzle, row, nextColumn))
                                    {
                                        //complete
                                        return true;
                                    }

                                    //if a possible value is not available go to the previous tile and reset to 0
                                    //this is a flag to try the next possible value
                                    puzzle[row, column] = 0;

                                }
                                else if (nextRow < 9)
                                {
                                    //start the next row
                                    if (SolvePuzzle(puzzle, row + 1, 0))
                                    {
                                        //complete
                                        return true;
                                    }

                                    //if a possible value is not available go to the previous tile and reset to 0
                                    //this is a flag to try the next possible value
                                    puzzle[row, column] = 0;

                                }
                                else
                                {
                                    //complete
                                    return true;
                                }
                            }
                        }
                    }

                    //could not find an available value
                    return false;
                }

                //complete
                return true;

            }
            catch
            {
                throw;
            }
        }

        private static bool IsPossiblePlay(int[,] puzzle, int row, int column, int number)
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
                int firstRowOfBlock = Helper.GetFirstRowOrColumnOfBlock(row);
                int firstColumnOfBlock = Helper.GetFirstRowOrColumnOfBlock(column);

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
            catch
            {
                throw;
            }
        }
        
    }
}
