using System;
using System.Collections.Generic;
using System.IO;

namespace _19
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] rows = File.ReadAllLines("input.txt");

            const int Row = 0;
            const int Col = 1;
            const int S = 0;
            const int N = 1;
            const int W = 2;
            const int E = 3;

            List<int[]> directions = new List<int[]>
            {
                // 0 is row, 1 is column
                new int[] { 1, 0 }, // S
                new int[] { -1, 0 }, // N
                new int[] { 0, -1 }, // W
                new int[] { 0, 1 }, // E
            };

            int rowCurr = 0;
            int colCurr = rows[0].IndexOf('|');

            int[] dir = directions[S];

            List<char> path = new List<char>();

            bool canMove = true;

            int steps = 0;

            while(canMove)
            {
                steps++;

                rowCurr += dir[Row];
                colCurr += dir[Col];

                if (rowCurr < 0 || rowCurr == rows.Length ||
                    colCurr < 0 || colCurr == rows[0].Length)
                {
                    Console.WriteLine("Row is {0}, col is {1}", rowCurr, colCurr);

                    // Are all rows guaranteed to have the same length?
                    break;
                }

                char curr = rows[rowCurr][colCurr];
                switch (curr)
                {
                    case '+':
                        //Console.WriteLine("Deciding on a direction");

                        // TODO: figure out where we can turn
                        for (int i = S; i <= E; i++)
                        {
                            int[] candidate = directions[i];
                            int candidateRow = rowCurr + candidate[Row];
                            int candidateCol = colCurr + candidate[Col];

                            //Console.WriteLine("Candidate direction row + {0} col + {1}", candidate[Row], candidate[Col]);
                            //Console.WriteLine("Candidate row {0} col {1}", candidateRow, candidateCol);

                            if (candidate[Row] == -1 * dir[Row] && candidate[Col] == -1 * dir[Col])
                            {
                                //Console.WriteLine("Skipping the opposite direction of the current direction");
                                // Don't go backwards
                                continue;
                            }

                            if (candidate[Row] == dir[Row] && candidate[Col] == dir[Col])
                            {
                                //Console.WriteLine("Skipping the same direction of the current direction");
                                // Don't go forwards
                                continue;
                            }

                            if (candidateRow < 0 || candidateRow == rows.Length ||
                                candidateCol < 0 || candidateCol == rows[0].Length)
                            {
                                //Console.WriteLine("Skipping the out-of-bounds direction");
                                // Out of bounds
                                continue;
                            }

                            //Console.WriteLine("Symbol at row {0} col {1} is '{2}'", candidateRow, candidateCol, rows[candidateRow][candidateCol]);
                            if (rows[candidateRow][candidateCol] != ' ')
                            {
                                //Console.WriteLine("Found a directiont to go");

                                dir = candidate;
                                break;
                            }

                            //Console.WriteLine("Nothing here");
                        }

                        break;
                    case '-':
                    case '|':
                        // continue
                        break;
                    case ' ':
                        canMove = false;
                        break;
                    default:
                        Console.WriteLine("Encountered {0}", curr);

                        path.Add(curr);
                        break;
                }
            }

            Console.WriteLine("{0} to hit {1}", steps, string.Join("", path));
        }
    }
}
