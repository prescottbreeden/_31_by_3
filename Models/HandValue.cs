using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public class HandValue
    {
        public static List<Card> hearts = new List<Card>();
        public static List<Card> diamonds = new List<Card>();
        public static List<Card> spades = new List<Card>();
        public static List<Card> clubs = new List<Card>();
        public static List<Card> aces = new List<Card>();
        public static int[] num_suits = new int[4];
        public static int hand_value { get; set; }
        public static int hearts_value { get; set; }
        public static int clubs_value  {get; set; }
        public static int spades_value { get; set; }
        public static int diamonds_value { get; set; }
        public static int[] suit_values = new int[4];
    
        public HandValue()
        {

        }
        public static int Calculate(Player player)
        {
            hearts_value = 0;
            diamonds_value = 0;
            spades_value = 0;
            clubs_value = 0;
            
            hearts.Clear();
            diamonds.Clear();
            spades.Clear();
            clubs.Clear();
            aces.Clear();
            
            // build lists of each suit-type for each players hand
            for (var i = 0; i < player.hand.Count; i++)
            {
                if (player.hand[i].suit == "hearts")
                {
                    hearts.Add(player.hand[i]);
                    hearts_value += player.hand[i].value;
                }
                else if (player.hand[i].suit == "diamonds")
                {
                    diamonds.Add(player.hand[i]);
                    diamonds_value += player.hand[i].value;
                }
                else if (player.hand[i].suit == "spades")
                {
                    spades.Add(player.hand[i]);
                    spades_value += player.hand[i].value;                
                }
                else if (player.hand[i].suit == "clubs")
                {
                    clubs.Add(player.hand[i]);
                    clubs_value += player.hand[i].value;                
                }
            }
            // find highest value suit - set to hand value of player
            suit_values[0] = hearts_value;
            suit_values[1] = diamonds_value;
            suit_values[2] = spades_value;
            suit_values[3] = clubs_value; 
            hand_value = suit_values.Max();
            foreach(Card c in player.hand)
            {
                if(c.value == 11)
                {
                    aces.Add(c);
                    System.Console.WriteLine("FOUND UNO ACE");
                }
            }
            foreach(Card ca in aces)
            {
                System.Console.WriteLine(ca.suit);
            }
            if(aces.Count == 3)
            {
                System.Console.WriteLine("FOUND 3 ACES!!!!!!");
                hand_value = 32;
            }

            // save number of cards for each suit in hand
            // num_suits[0] = hearts.Count;
            // num_suits[1] = diamonds.Count;
            // num_suits[2] = spades.Count;
            // num_suits[3] = clubs.Count;

            return hand_value;
        }
    }
}