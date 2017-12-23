using System;
using System.Collections.Generic;
using System.IO;

namespace _23
{
    class Program
    {
        public static void Main()
        {
            int mult = 0;

            long b = 0;
            long c = 0;
            long d = 0;
            long e = 0;
            long f = 0;

            long h = 0;

            b = 67;
            c = b;

            b *= 100;
            b += 100000;
            c = b;
            c += 17000;

            while(b <= c)
            {
                for (int i = 2; i <= b / 2; i++)
                {
                    if (b % i == 0)
                    {
                        h++;
                        break;
                    }
                }

                b += 17;
            }

            Console.WriteLine("{0} non-primes", h);

            // Part 1
            b = 67;
            c = b;

            while (true)
            {
                f = 1;
                d = 2;

                for (d = 2; d - b != 0; d++)
                {
                    for (e = 2; e - b != 0; e++)
                    {
                        mult++;
                        if (d * e - b == 0)
                        {
                            f = 0;
                        }
                    }
                }

                if (f == 0)
                {
                    h++;
                }

                if (b - c == 0)
                {
                    break;
                }

                b += 17;
            }

            Console.WriteLine(mult + " multiplications");
        }
    }
}
