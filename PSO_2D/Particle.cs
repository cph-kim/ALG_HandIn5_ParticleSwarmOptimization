namespace PSO_2D
{
    public class Particle
    {
        // 0 = x
        // 1 = y
        public double[] position;
        public double[] velocity;
        public double distance;
        public double[] ownBestPosition;
        public double ownBestDistance;

        public Particle(double[] pos, double[] vel, double[] ownBestPos, double dist, double ownBestDist)
        {
            position = pos;
            velocity = vel;
            distance = dist;
            ownBestPosition = ownBestPos;
            ownBestDistance = ownBestDist;
        }

        public override string ToString()
        {
            string s = "";
            s += "==========================\n";
            s += "Position: ";

            for (int i = 0; i < this.position.Length; ++i)
                s += this.position[i].ToString("F4") + " ";

            s += "\n";
            s += "Distance = " + this.distance.ToString("F4") + "\n";
            s += "Velocity: ";

            for (int i = 0; i < this.velocity.Length; ++i)
                s += this.velocity[i].ToString("F4") + " ";

            s += "\n";
            s += "Best Position: ";

            for (int i = 0; i < this.ownBestPosition.Length; ++i)
                s += this.ownBestPosition[i].ToString("F4") + " ";

            s += "\n";
            s += "Best Error = " + this.ownBestDistance.ToString("F4") + "\n";
            s += "==========================\n";
            return s;
        }
    }
}