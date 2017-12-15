using System;
using System.Collections.Generic;

namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generator A starts with 116
            // Generator B starts with 299

            // TEST:
            // A uses 65, while
            // generator B uses 8921

            // generator A uses factor 16807;
            // generator B uses factor 48271

            // Divide by 2147483647

            int divisor = 2147483647;

            //int numPairs = 40000000;
            int numPairs = 5000000;

            int genAFactor = 16807;
            int genBFactor = 48271;

            // long genA = 65;
            // long genB = 8921;

            long genA = 116; // 65;
            long genB = 299; //8921;

            int count = 0;

            Queue<long> genAQueue = new Queue<long>();
            Queue<long> genBQueue = new Queue<long>();

            int i = 0;

            while (i < numPairs)
            {
                genA = genA * genAFactor % divisor;
                genB = genB * genBFactor % divisor;

                if (genA % 4 == 0)
                {
                    genAQueue.Enqueue(genA);
                }

                if (genB % 8 == 0)
                {
                    genBQueue.Enqueue(genB);
                }

                //Console.WriteLine("i is {0}, genA {1}, genB {2}", i, genA, genB);
                //Console.WriteLine(Convert.ToString(genA & 0xFFFF, 2));
                //Console.WriteLine(Convert.ToString(genB & 0xFFFF, 2));

                if (genAQueue.Count > 0 && genBQueue.Count > 0)
                {
                    long genAVal = genAQueue.Dequeue();
                    long genBVal = genBQueue.Dequeue();

                    if ((genAVal & 0xFFFF) == (genBVal & 0xFFFF))
                    {
                        //Console.WriteLine("i is {0}, genA {1}, genB {2}", i, genAVal, genBVal);
                        //Console.WriteLine(Convert.ToString(genAVal & 0xFFFF, 2));
                        //Console.WriteLine(Convert.ToString(genBVal & 0xFFFF, 2));

                        count++;
                    }

                    i++;
                }
            }

            Console.WriteLine("Total count is {0}", count);
        }
    }
}
