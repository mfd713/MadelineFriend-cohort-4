namespace RogueLike
{
    public class Hero
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int Power { get; set; }
        public Location Location { get; set; }

        public Hero()
        {
            Location = new Location();
        }
    }
}