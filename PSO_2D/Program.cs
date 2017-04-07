using System;

namespace PSO_2D
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Execute();
        }

        int minWidth = -2;
        int maxWidth = 2;
        int minHeight = -2;
        int maxHeight = 2;

        double[] bestKnownPosition = new double[2] { 100, 100 };
        double bestKnownMinDistance = 100;

        int numParticles = 10;
        int maxIterations = 1000;
        Random rnd = new Random();
        Particle[] swarm;

        public void Execute()
        {
            InitParticles();

            // Show globals
            Console.WriteLine("Before Swarm Complete");
            Console.WriteLine("Best global pos: {0}", string.Join(",", bestKnownPosition));
            Console.WriteLine("Best global distance: {0}", bestKnownMinDistance);
            // Show all particles
            for (int i = 0; i < swarm.Length; ++i)
            {
                Console.WriteLine(swarm[i].ToString());
            }

            RunSwarm();

            // Show globals
            Console.WriteLine("After Swarm Complete");
            Console.WriteLine("Best global pos: {0}", string.Join(",", bestKnownPosition));
            Console.WriteLine("Best global distance: {0}", bestKnownMinDistance);
            // Show all particles
            for (int i = 0; i < swarm.Length; ++i)
            {
                Console.WriteLine(swarm[i].ToString());
            }

            Console.Read();
        }

        public void InitParticles()
        {
            swarm = new Particle[numParticles];

            // Initialize
            for (int i = 0; i < swarm.Length; i++)
            {
                double[] tempPos = new double[2];
                tempPos[0] = rnd.NextDouble() * (maxWidth - minWidth) + minWidth;
                tempPos[1] = rnd.NextDouble() * (maxHeight - minHeight) + minWidth;

                double[] tempVel = new double[2];
                tempVel[0] = 0.2;
                tempVel[1] = 0.2;

                double ownBestMinDistance = GetDistanceFromMin(tempPos[0], tempPos[1]);

                swarm[i] = new Particle(tempPos, tempVel, tempPos, ownBestMinDistance, ownBestMinDistance);
            }

            // Set best known global distance
            for (int i = 0; i < swarm.Length; ++i)
            {
                Particle currentParticle = swarm[i];

                if (currentParticle.ownBestDistance < bestKnownMinDistance)
                {
                    bestKnownMinDistance = currentParticle.ownBestDistance;
                }
            }
            Console.WriteLine("Best Global distance set to: {0}", bestKnownMinDistance);
        }

        public void RunSwarm()
        {
            // Run Swarm
            int currentIterationNum = 0;
            while (currentIterationNum < maxIterations)
            {
                // For each particle
                for (int i = 0; i < swarm.Length; ++i)
                {
                    Particle currentParticle = swarm[i];

                    // new velocity for both x and y
                    double[] tempNewVelocity = CalcNewVelocity(currentParticle);
                    currentParticle.velocity = tempNewVelocity;

                    // new position
                    double[] tempNewPosition = CalcNewPosition(currentParticle, tempNewVelocity);
                    currentParticle.position = tempNewPosition;

                    double currentDistanceForParticle = GetDistanceFromMin(currentParticle);
                    currentParticle.distance = currentDistanceForParticle;

                    // If current distance better than the particle own best distance
                    if (currentDistanceForParticle < currentParticle.ownBestDistance)
                    {
                        currentParticle.ownBestPosition = tempNewPosition;
                        currentParticle.ownBestDistance = currentDistanceForParticle;
                    }

                    // If the current distance better than the global best known distance
                    if (currentDistanceForParticle < bestKnownMinDistance)
                    {
                        bestKnownPosition = tempNewPosition;
                        bestKnownMinDistance = currentDistanceForParticle;
                    }
                }
                currentIterationNum++;
            }
        }

        private double[] CalcNewPosition(Particle currentParticle, double[] tempNewVelocity)
        {
            double[] tempNewPosition = new double[2];
            for (int j = 0; j < currentParticle.position.Length; ++j)
            {
                tempNewPosition[j] = currentParticle.position[j] + tempNewVelocity[j];
                if (tempNewPosition[j] < minWidth)
                    tempNewPosition[j] = minWidth;
                else if (tempNewPosition[j] > maxWidth)
                    tempNewPosition[j] = maxWidth;
            }
            return tempNewPosition;
        }

        private double[] CalcNewVelocity(Particle currentParticle)
        {
            double[] tempNewVelocity = new double[2];
            for (int j = 0; j < currentParticle.velocity.Length; ++j)
            {
                tempNewVelocity[j] = rnd.NextDouble() * (maxWidth - 0) + 0;
            }
            return tempNewVelocity;
        }

        public double GetDistanceFromMin(Particle p)
        {
            return GetDistanceFromMin(p.position[0], p.position[1]);
        }

        public double GetDistanceFromMin(double x, double y)
        {
            double trueMin = -0.42888194; // true min for z = x * exp(-(x^2 + y^2))
            double z = x * Math.Exp(-((x * x) + (y * y)));
            return (z - trueMin) * (z - trueMin);
        }
    }
}
