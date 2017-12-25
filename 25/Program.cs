namespace _25
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public enum State
        {
            A,
            B,
            C,
            D,
            E,
            F,
        }

        public static void Main(string[] args)
        {
            Part1();
        }

        public static void Part1()
        {
            Node curr = new Node();
            State state = State.A;

            int numOps = 12683008;

            for (int i = 0; i < numOps; i++)
            {
                switch(state)
                {
                    case State.A:
                        if (curr.Bit)
                        {
                            curr.Bit = false;                        
                            curr = curr.MoveLeft();
                        }
                        else
                        {
                            curr.Bit = true;
                            curr = curr.MoveRight();
                        }

                        state = State.B;

                        break;
                    case State.B:
                        if (curr.Bit)
                        {
                            curr.Bit = false;
                            curr = curr.MoveRight();
                            state = State.E;
                        }
                        else
                        {
                            curr.Bit = true;
                            curr = curr.MoveLeft();
                            state = State.C;
                        }

                        break;

                    case State.C:
                        if (curr.Bit)
                        {
                            curr.Bit = false;
                            curr = curr.MoveLeft();
                            state = State.D;
                        }
                        else
                        {
                            curr.Bit = true;
                            curr = curr.MoveRight();
                            state = State.E;
                        }
                        break;
                    case State.D:
                        curr.Bit = true;
                        curr = curr.MoveLeft();
                        state = State.A;
                        break;

                    case State.E:
                        if (curr.Bit)
                        {
                            state = State.F;
                        }
                        else
                        {
                            state = State.A;
                        }

                        curr.Bit = false;
                        curr = curr.MoveRight();

                        break;

                    case State.F:
                        if (curr.Bit)
                        {
                            state = State.A;
                        }
                        else
                        {
                            state = State.E;
                        }

                        curr.Bit = true;
                        curr = curr.MoveRight();

                        break;
                    default:
                        throw new Exception("Unexpected state");
                }
            }

            PrintCheckSum(curr);
        }

        public class Node
        {
            public bool Bit {get;set;}
            public Node Left {get;set;}
            public Node Right{get;set;}

            public Node MoveRight()
            {
                if (this.Right == null)
                {
                    this.Right = new Node();
                    this.Right.Left = this;
                }
                
                return this.Right;
            }

            public Node MoveLeft()
            {
                if (this.Left == null)
                {
                    this.Left = new Node();
                    this.Left.Right = this;
                }
                
                return this.Left;
            }
        }

        public static void SmallPart1()
        {
            Node curr = new Node();
            State state = State.A;

            int numOps = 6;



            for (int i = 0; i < numOps; i++)
            {
                switch(state)
                {
                    case State.A:
                        if (curr.Bit)
                        {
                            curr.Bit = false;                        
                            curr = curr.MoveLeft();
                        }
                        else
                        {
                            curr.Bit = true;
                            curr = curr.MoveRight();
                        }

                        state = State.B;

                        break;
                    case State.B:
                        if (curr.Bit)
                        {
                            curr = curr.MoveRight();
                        }
                        else
                        {
                            curr.Bit = true;
                            curr = curr.MoveLeft();
                        }

                        state = State.A;

                        break;
                    default:
                        throw new Exception("Unexpected state");
                }
            }

            PrintCheckSum(curr);
        }

        public static void PrintCheckSum(Node curr)
        {
            int sum = 0;

            Node left = curr.Left;
            while (left != null)
            {
                if (left.Bit)
                {
                    sum++;
                }

                left = left.Left;
            }

            while (curr != null)
            {
                if (curr.Bit)
                {
                    sum++;
                }

                curr = curr.Right;
            }

            Console.WriteLine("Checksum: {0}", sum);
        }
    }
}
