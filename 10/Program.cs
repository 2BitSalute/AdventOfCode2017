namespace Foo
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            int[] smallInput = new int[] { 3, 4, 1, 5 };
            //int[] input = Input;

            string sInput = "83,0,193,1,254,237,187,40,88,27,2,255,149,29,42,100";

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

            Console.WriteLine("The answer is " + s);
        }

        public static int[] Input = new int[] { 83, 0, 193, 1, 254, 237, 187, 40, 88, 27, 2, 255, 149, 29, 42, 100 };
    }
}