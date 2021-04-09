namespace RogueLike
{
    public class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Board(int height, int width)
        {
            Width = width;
            Height = height;
        }


    }
}