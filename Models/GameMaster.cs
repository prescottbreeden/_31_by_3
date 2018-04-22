using System.Collections.Generic;

namespace _31_by_3
{
    public class GameMaster
    {
        public int turn { get; set; }
        public Deck deck { get; set; }
        public bool knocked { get; set; }
        public List<Player> players { get; set; }
        public GameOver endGame { get; set; }
        public GameMaster()
        {

        }

        // public override string ToString()
        // {
        //     return deck + " " + players;
        // }
    }
}