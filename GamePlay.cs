using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    
    public class GamePlay
    {
        public int SizeOfHand = 3;

        public Deck BuildAndShuffle()
        {
            Deck current_deck = new Deck();
            current_deck.Shuffle();
            return current_deck;
        }
            
        public void Deal(List<Player> players, Deck deck)
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
                    Players.Add(new Player());
                }
                else
                {
                    Players.Add(new Player(player));
                }
            }
            
            // Place players at table
            for(var i = 0; i < Players.Count; i++)
            {
                Players[i].player_seat = i+1;
            }
            
            // Set first dealer
            Players[0].isDealer = true;

            return Players;
        }

        public void CalculateHandValue(Player player)
        {
            //dump out old hand values
            player.hearts_value = 0;
            player.diamonds_value = 0;
            player.spades_value = 0;
            player.clubs_value = 0;

            player.hearts.Clear();
            player.diamonds.Clear();
            player.spades.Clear();
            player.clubs.Clear();

            // build lists of each suit-type for each players hand
            for (var i = 0; i < player.hand.Count; i++)
            {
                if (player.hand[i].suit == "Hearts")
                {
                    player.hearts.Add(player.hand[i]);
                    player.hearts_value += player.hand[i].value;
                }
                else if (player.hand[i].suit == "Diamonds")
                {
                    player.diamonds.Add(player.hand[i]);
                    player.diamonds_value += player.hand[i].value;
                }
                else if (player.hand[i].suit == "Spades")
                {
                    player.spades.Add(player.hand[i]);
                    player.spades_value += player.hand[i].value;                
                }
                else if (player.hand[i].suit == "Clubs")
                {
                    player.clubs.Add(player.hand[i]);
                    player.clubs_value += player.hand[i].value;                
                }
            }
            // find highest value suit - set to hand value of player
            player.suit_values[0] = player.hearts_value;
            player.suit_values[1] = player.diamonds_value;
            player.suit_values[2] = player.spades_value;
            player.suit_values[3] = player.clubs_value;
            player.hand_value = player.suit_values.Max();

            // save number of cards for each suit in hand
            player.num_suits[0] = player.hearts.Count;
            player.num_suits[1] = player.diamonds.Count;
            player.num_suits[2] = player.spades.Count;
            player.num_suits[3] = player.clubs.Count;
        }
    }
}