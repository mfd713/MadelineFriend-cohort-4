using Gomoku.Game;

namespace Gomoku.Players
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; private set; }       

        public HumanPlayer(string name)
        {
            Name = name;
        }

        public Stone GenerateMove(Stone[] previousMoves)
        {
            return null;
        }
    }
}
