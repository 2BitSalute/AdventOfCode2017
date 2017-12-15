using System;
using System.Collections.Generic;

namespace _14
{
    class Program
    {
        public static void Main()
        {
            string testPrefix = "xlqgujun-";
            string realPrefix  = "flqrgnkx-";
            string prefix = testPrefix;

            char[,] grid = new char[128,128];

            int i = 0;
            int j = 0;

            for(i = 0; i < 128; i++)
            {
                string input = prefix + i;
                string hash = ComputeHash(input);

                //Console.WriteLine("Input {0}, hash {1}, {2}", input, hash, hash.Length);

                for(j = 0; j < hash.Length; j++)
                {
                    char digit = hash[j];
                    Fill(digit, grid, i, 4 * j);
                }
            }
/*
            int sum = 0;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    sum += grid[j, i] == '#' ? 1 : 0;
                    Console.Write(grid[j, i]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("Sum is {0}", sum);
*/
            int region = 0;

            bool isRow = true;

            i = 0;
            j = 0;

            /*/
            while (true)
            {
                Console.WriteLine("i is {0}, j is {1}", i, j);

                if (grid[j, i] == '#')
                {
                    int count = Search(grid, i, j, region++);

                    if (region == '9')
                    {
                        Console.WriteLine("Count of region 8 is {0}", count);
                        break;
                    }
                }

                if (isRow)
                {
                    i++;
                    isRow = false;
                }
                else
                {
                    j++;
                    isRow = true;
                }
            }
*/
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { 0 , 0 });

            bool[,] visited = new bool[128,128];
            visited[0,0] = true;

            while (queue.Count > 0)
            {
                int[] coordinates = queue.Dequeue();
                i = coordinates[0];
                j = coordinates[1];

                if (grid[j, i] == '#')
                {
                    region++;
                    int count = Search(grid, i, j, 'X');
                }

                    for(int l = j - 1; l <= j + 1; l++)
                    {
                        for(int k = i - 1; k <= i + 1; k++)
                        {
                            if (!IsVisited(visited, k, l))
                            {
                                visited[l, k] = true;
                                queue.Enqueue(new int[] { k, l });
                            }
                        }
                    }
            }

            for (i = 0; i < 128; i++)
            {
                for (j = 0; j < 128; j++)
                {
                    Console.Write(grid[j, i]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("{0} regions", region);
        }

        public static bool IsVisited(bool[,] visited, int i, int j)
        {
            if (j < 0 || i < 0 || j == 128 || i == 128 || visited[j, i])
            {
                return true;
            }
            
            return false;
        }

        public static int Search(char[,] grid, int i, int j, char region)
        {
            if (j < 0 || i < 0 || j == 128 || i == 128 || grid[j, i] != '#')
            {
                return 0;
            }

            grid[j, i] = region;

            return 1 +
                Search(grid, i + 1, j, region) +
                Search(grid, i - 1, j, region) +
                Search(grid, i, j + 1, region) +
                Search(grid, i, j - 1, region);
        }

        public static void Fill(char c, char[,] grid, int i, int baseJ)
        {
            //Console.WriteLine("Base j is {0}", baseJ);

            int num = int.Parse(c + "", System.Globalization.NumberStyles.HexNumber);
            string val = Convert.ToString(num, 2).PadLeft(4, '0');

            for (int j = 0; j < 4; j++)
            {
                grid[baseJ + j, i] = val[j] == '0' ? '.' : '#';
            }
        }

        public static string ComputeHash(string sInput)
        {
            List<int> inputList = new List<int>();
            foreach (char c in sInput.ToCharArray())
            {
                inputList.Add(c);
            }

            inputList.AddRange(new int[] { 17, 31, 73, 47, 23 });

            int[] input = inputList.ToArray();

            int size = 0;

            int length = 256;

            int[] buffer = new int[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = i;
            }

            int curr = 0;

            for (int k = 0; k < 64; k++)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    int[] sub = new int[input[i]];
                    for (int j = 0; j < sub.Length; j++)
                    {
                        sub[j] = buffer[(curr + j) % buffer.Length];
                    }

                    Array.Reverse(sub);

                    for (int j = 0; j < sub.Length; j++)
                    {
                        buffer[(curr + j) % buffer.Length] = sub[j];
                    }

                    curr = (curr + size + input[i]) % length;
                    size++;
                }
            }

            int[] dense = new int[16];
            string s = "";
            for (int i = 0; i < 16; i++)
            {
                int result = buffer[i * 16];
                for (int j = 1; j < 16; j++)
                {
                    result = result ^ buffer[i * 16 + j];
                }

                s += result.ToString("X2");
            }

            return s;
        }
    }
}
