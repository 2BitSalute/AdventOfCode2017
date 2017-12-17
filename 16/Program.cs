using System;
using System.IO;
using System.Collections.Generic;

namespace _16
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 314;
            Console.WriteLine(Part2(input, 2017));
            Console.WriteLine(Part2(input, 50000000));
        }

        static int Part2(int input, int times)
        {
            int curr = 0;
            int after0 = -1;

            for (int i = 1; i <= times; i++)
            {
                curr = (curr + input) % i;
                if (curr == 0)
                {
                    after0 = i;
                }

                curr++;
            }

            return after0;
        }

        static int Part1(int input, int times)
        {
            List<int> buffer = new List<int>(times + 1) { 0 };

            int curr = 0;

            for (int i = 1; i <= times; i++)
            {
                curr = (curr + input) % buffer.Count;
                buffer.Insert(curr + 1, i);
                curr++;
            }

            return buffer[(curr + 1) % buffer.Count];
        }

        public static void Print(List<int> buffer, int curr)
        {
            for(int i = 0; i < buffer.Count; i++)
            {
                Console.Write(i == curr ? "({0}) " : "{0} ", buffer[i]);
            }

            Console.WriteLine();
        }



        static void Main15()
        {
            string input = File.ReadAllText("input.txt");
            int numPrograms = 16;

            char[] line = new char[numPrograms];
            for (int i = 0; i < numPrograms; i++)
            {
                line[i] = (char)('a' + i);
            }

            int counter = 0;
            string origLine = string.Join("", line);
            for (int i = 0; i < 1000000000 % 63; i++)
            {
                line = SolvePart1(line, input);
                string lineString = string.Join("", line);

                counter++;
                if (lineString == origLine)
                {
                    break;
                }
            }
            
            Console.WriteLine(string.Join("", line) + " " + counter);
        }

        public static char[] SolvePart1(char[] line, string input)
        {
            string[] moves = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach(var move in moves)
            {
                switch(move[0])
                {
                    case 's':
                        int i = int.Parse(move.Substring(1));
                        Spin(i, line);
                        break;
                    case 'x':
                        string[] tokens = move.Substring(1).Split('/', StringSplitOptions.RemoveEmptyEntries);
                        int x = int.Parse(tokens[0]);
                        int y = int.Parse(tokens[1]);

                        Exchange(x, y, line);

                        break;

                    case 'p':
                        string[] pTokens = move.Substring(1).Split('/', StringSplitOptions.RemoveEmptyEntries);

                        Partner(pTokens[0][0], pTokens[1][0], line);
                        break;
                    
                    default:
                        throw new Exception();
                }
            }

            return line;
        }

        public static void Partner(char c1, char c2, char[] line)
        {
            Exchange(IndexOf(c1, line), IndexOf(c2, line), line);
        }

        public static int IndexOf(char c, char[] line)
        {
            for(int i = 0; i < line.Length; i++)
            {
                if (line[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }

        public static void Exchange(int i, int j, char[] line)
        {
            char temp = line[i];
            line[i] = line[j];
            line[j] = temp;
        }

        public static void Spin(int i, char[] line)
        {
            char[] temp = new char[i];

            for (int j = i - 1; j >= 0; j--)
            {
                temp[j] = line[line.Length - (i - j)];
            }

            for (int j = line.Length - 1; j >= i; j--)
            {
                line[j] = line[j - i];
            }

            for (int j = 0; j < temp.Length; j++)
            {
                line[j] = temp[j];
            }
        }
    }
}
