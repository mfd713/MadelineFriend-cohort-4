using System;

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

        public void SpawnMonster(int width, int height, Hero hero)
        {
            Random random = new Random();
            this.Location.X = random.Next(0, width);
            this.Location.Y = random.Next(0, height);
            this.Power = hero.Level;
            Health = 10 + (random.Next(1, 5)*hero.Level);
        }

        public void MonsterFollow(Location target) 
        {
            //find the distance from Monsters local to target local
            double distance = target.Distance(this.Location);
            
            //move 1 vertically and 1 horizontally toward that target if distance is more than 1
            if (distance > 1)
            {
                //target is above
                if(target.Y < this.Location.Y)
                {
                    this.Location.Y--;
                }
                //target is below
                if (target.Y > this.Location.Y)
                {
                    this.Location.Y++;
                }
                //target is left of
                if (target.X < this.Location.X)
                {
                    this.Location.X--;
                }
                //target is right of
                if (target.X > this.Location.X)
                {
                    this.Location.X++;
                }
            }
        }
    }
}