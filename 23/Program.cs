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

            long a = 1;
            long b = 0;
            long c = 0;
            long d = 0;
            long e = 0;
            long f = 0;
            long g = 0;
            long h = 0;

            b = 67;
            c = b;

            if (a != 0)
            {
                b *= 100;
                b += 100000;
                c = b;
                c += 17000;
            }

            Console.WriteLine("b = {0}", b);
            Console.WriteLine("c = {0}", c);

            while (true)
            {
                f = 1;
                d = 2;

                do
                {
                    for (e = 2; e - b != 0; e++)
{
                    e = 2;

                    do
                    {}
                        if (d * e - b == 0)
                        {
                            f = 0;
                        }

                        // g = d;
                        // g *= e;
                        // mult++;

                        // g -= b;

                        // if (g == 0)
                        // {
                        //     f = 0;
                        // }

                        e++;
                        g = e;
                        g -= b;
                    } while (g != 0);

                    if (f != 0)
                    {
                        Console.WriteLine("e = {0}", e);
                        Console.WriteLine("f = {0}", f);
                    }

                    d++;
                    g = d;
                    g -= b;
                } while (g != 0);

                if (f == 0)
                {
                    Console.WriteLine("H is " + h);
                    h++;
                }
                else
                {
                    Console.WriteLine("f is not zero");
                }

                g = b;
                // g -= c;

                // c is always 123700
                // Therefore, we exit when g is -123700
                // And actually, we exit when B is -123700
                if (g - c == 0) // (g == 0)
                {
                    Console.WriteLine("Finally");
                    break;
                }

                b += 17;
            }


            Console.WriteLine(h);
        }

        static void MainFoo()
        {
            string[] instructions = File.ReadAllLines("input.txt");

            long numMul = 0;

            string instruction = string.Empty;
            long curr = 0;

            Dictionary<string, long> registers = new Dictionary<string, long>();
            /*
            a = 1
            b = 106700
            c = 123700
            f = 1
            d = 2
            e = 2651
            g = -104049
             */
            registers.Add("a", 1);
            // // registers.Add("b", 123700);
            // // registers.Add("c", 123700);
            // // registers.Add("d", 2);
            // // registers.Add("f", 0);
            // // registers.Add("g", 0);
            // // registers.Add("e", 0);

            // // curr = 24;

            do {
                //Console.WriteLine(curr + ": " + instructions[curr]);
                
                var tokens = instructions[curr].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                instruction = tokens[0];

                string register = tokens[1];

                if (!registers.ContainsKey(register))
                {
                    long literal;
                    if(!long.TryParse(register, out literal))
                    {
                        literal = 0;
                    }

                    registers.Add(register, literal);
                }

                long value = 0;
                if (tokens.Length > 2)
                {
                    if (registers.ContainsKey(tokens[2]))
                    {
                        value = registers[tokens[2]];
                    }
                    else
                    {
                        value = long.Parse(tokens[2]);
                    }
                }

                long offset = 1;

                switch(instruction)
                {
                    case SET:
                        registers[register] = value;
                        break;
                    case SUB:
                        registers[register] -= value;
                        break;
                    case MUL:
                        numMul++;
                        registers[register] *= value;
                        break;
                    case JNZ:
                        if (registers[register] != 0)
                        {
                            //Console.WriteLine("Setting offset to " + value);
                            offset = value;
                        }
                        break;
                }

                curr += offset;

                if (curr == 19)
                {
                    foreach(var pair in registers)
                    {
                        //Console.WriteLine("{0} = {1}", pair.Key, pair.Value);
                    }
                }

            } while (curr >= 0 && curr < instructions.Length);

            Console.WriteLine(registers["h"]);
        }

        static void MainOld(string[] args)
        {
            string[] instructions = File.ReadAllLines("input.txt");

            var p0 = new Program(0, instructions);
            var p1 = new Program(1, instructions);

            p0.OtherQueue = p1.Queue;
            p1.OtherQueue = p0.Queue;

            do
            {
                p1.Run();
                p0.Run();
            } while(p1.Queue.Count != 0);

            Console.WriteLine(p1.SendCounter);
        }

        const string SND = "snd";
        const string SET = "set";
        const string ADD = "add";
        const string SUB = "sub";
        const string MUL = "mul";
        const string MOD = "mod";
        const string RCV = "rcv";
        const string JNZ = "jnz";

        const string JGZ = "jgz";

        private long curr = 0;
        private string[] instructions;
        private Dictionary<string, long> registers = new Dictionary<string, long>();

        public long SendCounter { get; set; }

        public Queue<long> Queue { get; }

        public Queue<long> OtherQueue { get; set; }

        public string Name { get; set; }

        public Program(int pValue, string[] instructions)
        {
            this.registers.Add("p", pValue);
            this.instructions = instructions;
            this.Queue = new Queue<long>();
            this.Name = pValue.ToString();
        }

        public void Run()
        {
            string instruction = string.Empty;

            while (true)
            {
                Console.WriteLine("Program {0}: {1}", this.Name, instructions[curr]);
                
                var tokens = instructions[curr].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                instruction = tokens[0];

                string register = tokens[1];

                if (!registers.ContainsKey(register))
                {
                    long literal;
                    if(!long.TryParse(register, out literal))
                    {
                        literal = 0;
                    }

                    registers.Add(register, literal);
                }

                long value = 0;
                if (tokens.Length > 2)
                {
                    if (registers.ContainsKey(tokens[2]))
                    {
                        value = registers[tokens[2]];
                    }
                    else
                    {
                        value = long.Parse(tokens[2]);
                    }
                }

                long offset = 1;
                switch(instruction)
                {
                    case SND:
                        this.OtherQueue.Enqueue(registers[register]);
                        this.SendCounter++;
                        break;
                    case SET:
                        registers[register] = value;
                        break;
                    case ADD:
                        registers[register] += value;
                        break;
                    case MUL:
                        registers[register] *= value;
                        break;
                    case MOD:
                        registers[register] %= value;
                        break;
                    case RCV:
                        if (this.Queue.Count > 0)
                        {
                            registers[register] = this.Queue.Dequeue();
                        }
                        else
                        {
                            foreach(var pair in registers)
                            {
                                Console.WriteLine("{0} = {1}", pair.Key, pair.Value);
                            }

                            return;
                        }
                        break;
                    case JGZ:
                        if (registers[register] > 0)
                        {
                            offset = value;
                        }
                        break;
                }

                curr += offset;
            }
        }

        static void MainPart1(string[] args)
        {
            string[] instructions = File.ReadAllLines("input.txt");

            long lastSound = -1;
            long lastRecovered = -1;

            string instruction = string.Empty;
            long curr = 0;

            Dictionary<string, long> registers = new Dictionary<string, long>();

            do {
                Console.WriteLine(instructions[curr]);
                
                var tokens = instructions[curr].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                instruction = tokens[0];

                string register = tokens[1];

                if (!registers.ContainsKey(register))
                {
                    registers.Add(register, 0);
                }

                long value = 0;

                if (tokens.Length > 2)
                {
                    if (registers.ContainsKey(tokens[2]))
                    {
                        value = registers[tokens[2]];
                    }
                    else
                    {
                        value = int.Parse(tokens[2]);
                    }
                }

                long offset = 1;

                switch(instruction)
                {
                    case SND:
                        lastSound = registers[register];
                        break;
                    case SET:
                        registers[register] = value;
                        break;
                    case ADD:
                        registers[register] += value;
                        break;
                    case MUL:
                        registers[register] *= value;
                        break;
                    case MOD:
                        registers[register] %= value;
                        break;
                    case RCV:
                        if (registers[register] != 0)
                        {
                            lastRecovered = lastSound;
                        }
                        break;
                    case JGZ:
                        if (registers[register] > 0)
                        {
                            offset = value;
                        }
                        break;
                }

                curr += offset;

                foreach(var pair in registers)
                {
                    Console.WriteLine("{0} = {1}", pair.Key, pair.Value);
                }
                Console.WriteLine("Last sound {0}, last recovered {1}", lastSound, lastRecovered);

            } while (instruction != RCV || lastRecovered < 0);

            Console.WriteLine(lastRecovered);
        }
    }
}
