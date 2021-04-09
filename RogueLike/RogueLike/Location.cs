namespace RogueLike
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Location other) 
        {
            return (X == other.X && Y == other.Y);
        }

        public double Distance(Location other)
        {
            //return (System.Math.Sqrt(((other.X - this.X)  ^  2) + ((other.Y - this.Y)  ^  2)));
            return System.Math.Abs(other.X - this.X) + System.Math.Abs(other.Y - this.Y);
        }
    }
}