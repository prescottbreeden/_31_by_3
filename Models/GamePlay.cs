using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public static class GamePlay
    {
        public static int SizeOfHand = 3;
        public static int Pot { get; set; }

        public static Deck BuildAndShuffle()
        {
            Deck CurrentDeck = new Deck();
            CurrentDeck.deck = CurrentDeck.NewDeck();
            CurrentDeck.deck = CurrentDeck.Shuffle(CurrentDeck.deck);
            return CurrentDeck;
        }
            
        public static void Deal(List<Player> players, Deck deck)
        {
            //Deal 3 cards to each player
            for(var i = 0; i < SizeOfHand; i++)
            {
                foreach (var player in players)
                    deck.DrawFromDeck(player);
            }
            deck.MoveTopCardToDiscardPile();
        }
        public static List<Player> CreatePlayers(List<string> PlayerSelect)
        {
            // generate human players in game
            var Players = new List<Player>();

            foreach(var player in PlayerSelect)
            {
                if(player=="zxc")
                {
                    Players.Add(new Player(name: player, isHuman: false));
                }
                else
                {
                    Players.Add(new Player(name: player));
                }
            }
            Players[2].name = Player.CreateRandomEarthName();
            Players[3].name = Player.CreateRandomFunnyName();
            
            // Place players at table
            for(var i = 0; i < Players.Count; i++)
            {
                Players[i].player_seat = i+1;
            }
            
            // Set first dealer
            Players[0].isDealer = true;

            return Players;
        }
    }
}