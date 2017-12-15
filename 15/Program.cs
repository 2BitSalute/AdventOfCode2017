using System;
using System.Collections.Generic;

namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = DateTime.Now;

            ViaEnumerator();

            Console.WriteLine("The enumerator solution took {0}", DateTime.Now - start);

            start = DateTime.Now;

            OrigMain();

            Console.WriteLine("The queue solution took {0}", DateTime.Now - start);
        }

        public static void ViaEnumerator()
        {
            int numPairs = 5000000;
            int count = 0;

            var genA = Values(curr: 116, factor: 16807, modulo: 4);
            var genB = Values(curr: 299, factor: 48271, modulo: 8);

            for (int i = 0; i < numPairs; i++)
            {
                genA.MoveNext();
                genB.MoveNext();

                long genAVal = genA.Current;
                long genBVal = genB.Current;

                if ((genAVal & 0xFFFF) == (genBVal & 0xFFFF))
                {
                    count++;
                }
            }

            Console.WriteLine("Total count is {0}", count);
        }

        public static IEnumerator<long> Values(long curr, long factor, int modulo)
        {
            const int Divisor = 2147483647;

            while(true)
            {
                curr = curr * factor % Divisor;

                if (curr % modulo == 0)
                {
                    yield return curr;
                }
            }
        }

        public class Generator
        {
            private const int Divisor = 2147483647;

            private readonly long factor;
            private readonly long modulo;

            private long curr;

            public Generator(long start, long factor, int modulo)
            {
                this.curr = start;
                this.factor = factor;
                this.modulo = modulo;
            }

            public IEnumerator<long> Values()
            {
                while(true)
                {
                    this.curr = this.curr * this.factor % Divisor;

                    if (this.curr % this.modulo == 0)
                    {
                        yield return this.curr;
                    }
                }
            }
        }

        public static void OrigMain()
        {
            // Generator A starts with 116
            // Generator B starts with 299

            // TEST:
            // A uses 65, while
            // generator B uses 8921

            // generator A uses factor 16807;
            // generator B uses factor 48271

            // Divide by 2147483647

            // int numPairs = 40000000;
            // long genA = 65;
            // long genB = 8921;

            int numPairs = 5000000;
            int genAFactor = 16807;
            int genBFactor = 48271;

            long genA = 116;
            long genB = 299;

            int divisor = 2147483647;

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
