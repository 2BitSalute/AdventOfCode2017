using System;
using System.Collections.Generic;
using System.IO;

namespace _20
{
    class Program
    {
        /*
        
p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>    -4 -3 -2 -1  0  1  2  3  4
p=< 4,0,0>, v=< 0,0,0>, a=<-2,0,0>                         (0)(1)

p=< 4,0,0>, v=< 1,0,0>, a=<-1,0,0>    -4 -3 -2 -1  0  1  2  3  4
p=< 2,0,0>, v=<-2,0,0>, a=<-2,0,0>                      (1)   (0)

p=< 4,0,0>, v=< 0,0,0>, a=<-1,0,0>    -4 -3 -2 -1  0  1  2  3  4
p=<-2,0,0>, v=<-4,0,0>, a=<-2,0,0>          (1)               (0)

p=< 3,0,0>, v=<-1,0,0>, a=<-1,0,0>    -4 -3 -2 -1  0  1  2  3  4
p=<-8,0,0>, v=<-6,0,0>, a=<-2,0,0>                         (0)   
         */

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            
            HashSet<Particle> particles = new HashSet<Particle>();

            foreach(var line in lines)
            {
                var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                Coordinate position = ParseCoordinate(tokens[0]);
                Coordinate velocity = ParseCoordinate(tokens[1]);
                Coordinate acceleration = ParseCoordinate(tokens[2]);

                Particle particle = new Particle
                {
                    Position = position,
                    Velocity = velocity,
                    Acceleration = acceleration
                };

                particles.Add(particle);

                Console.WriteLine("Position: " + position);
                Console.WriteLine("Velocity: " + velocity);
                Console.WriteLine("Acceleration: " + acceleration);

                Console.WriteLine();
            }

            int counter = 0;

            while(counter < 50000)
            {
                HashSet<Particle> newParticles = new HashSet<Particle>();
                List<Particle> exclusionList = new List<Particle>();
                foreach(var particle in particles)
                {
                    particle.Move();

                    if (newParticles.Contains(particle))
                    {
                        exclusionList.Add(particle);
                    }
                    else
                    {
                        newParticles.Add(particle);
                    }
                }

                foreach(var destroyed in exclusionList)
                {
                    newParticles.Remove(destroyed);
                }

                if (newParticles.Count < particles.Count)
                {
                    Console.WriteLine("Had {0} particles, now have {1}", particles.Count, newParticles.Count);
                    particles = newParticles;
                }

                counter++;
            }

            Console.Write(particles.Count);
        }

        static void MainPart1(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            
            List<Particle> particles = new List<Particle>();

            foreach(var line in lines)
            {
                var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                Coordinate position = ParseCoordinate(tokens[0]);
                Coordinate velocity = ParseCoordinate(tokens[1]);
                Coordinate acceleration = ParseCoordinate(tokens[2]);

                Particle particle = new Particle
                {
                    Position = position,
                    Velocity = velocity,
                    Acceleration = acceleration
                };

                particles.Add(particle);

                Console.WriteLine("Position: " + position);
                Console.WriteLine("Velocity: " + velocity);
                Console.WriteLine("Acceleration: " + acceleration);

                Console.WriteLine();
            }

            int closestParticle = -1;

            int counter = 0;

            while(true)
            {
                foreach(var particle in particles)
                {
                    particle.Move();
                }

                counter++;

                if (counter % 10000 == 0)
                {
                    int newClosest = GetClosest(particles);
                    if (closestParticle != newClosest)
                    {
                        closestParticle = newClosest;

                        Console.WriteLine("New closest is {0}", newClosest);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.Write("{0} is the closest", GetClosest(particles));
        }

        public static int GetClosest(List<Particle> particles)
        {
            int closestParticle = -1;
            long closest = long.MaxValue;
            for(int i = 0; i < particles.Count; i++)
            {
                if (particles[i].Distance < closest)
                {
                    closestParticle = i;
                    closest = particles[i].Distance;
                }
            }

            return closestParticle;
        }

        public static Coordinate ParseCoordinate(string token)
        {
            var tokens = token.Split(new char[] { '<', '>', ',' }, StringSplitOptions.RemoveEmptyEntries);

            return new Coordinate
            {
                X = long.Parse(tokens[1]),
                Y = long.Parse(tokens[2]),
                Z = long.Parse(tokens[3])
            };
        }

        public class Particle : IEquatable<Particle>
        {
            public Coordinate Velocity
            {
                get; set;
            }

            public Coordinate Acceleration
            {
                get; set;
            }

            public Coordinate Position
            {
                get; set;
            }

            public long Distance
            {
                get
                {
                    return
                        Math.Abs(this.Position.X) +
                        Math.Abs(this.Position.Y) +
                        Math.Abs(this.Position.Z);
                }
            }

            public void Move()
            {
                this.Velocity.Add(this.Acceleration);
                this.Position.Add(Velocity);
            }

            public bool Equals(Particle other)
            {
                return this.Position.Equals(other.Position);
            }

            public override bool Equals(object obj)
            {
                var other = obj as Particle;
                return this.Position.Equals(other.Position);
            }

            public override int GetHashCode()
            {
                return this.Position.GetHashCode();
            }
        }

        public class Coordinate : IEquatable<Coordinate>
        {
            public long X {get;set;}
            public long Y {get;set;}
            public long Z {get;set;}

            public void Multiply(Coordinate other)
            {
                this.X *= other.X;
                this.Y *= other.Y;
                this.Z *= other.Z;
            }

            public void Add(Coordinate other)
            {
                this.X += other.X;
                this.Y += other.Y;
                this.Z += other.Z;
            }

            public override string ToString()
            {
                return string.Format("X={0}, Y={1}, Z={2}", this.X, this.Y, this.Z);
            }

            public bool Equals(Coordinate other)
            {
                return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            }

            public override bool Equals(object obj)
            {
                var other = obj as Coordinate;
                return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            }

            public override int GetHashCode()
            {
                long result = this.X;
                result = (result * 397) ^ this.Y;
                result = (result * 397) ^ this.Z;
                return (int)result;
            }
        }

        public static long ManhattanDistance(long x, long y, long z)
        {
            return x + y + z;
        }
    }
}
