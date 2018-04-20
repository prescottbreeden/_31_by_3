using System.Collections.Generic;

namespace _31_by_3
{
    public class GameMaster
    {
        public Deck deck { get; set; }
        public List<Player> players { get; set; }
        public int turn { get; set; }
        public GameMaster()
        {

        }

        // public override string ToString()
        // {
        //     return deck + " " + players;
        // }
    }
}