namespace RogueLike
{
    public class Monster
    {
        public int Health { get; set; }
        public Location Location { get; set; }
        public int Power { get; set; }

        public Monster()
        {
            Location = new Location();
        }
    }
}