using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public class AI : Player
    {
        public List<Card> hearts = new List<Card>();
        public List<Card> diamonds = new List<Card>();
        public List<Card> spades = new List<Card>();
        public List<Card> clubs = new List<Card>();
        public int[] num_suits = new int[4];
        public int[] suit_values = new int[4];
        public string max_suit_type { get; set; }
        public int hearts_value { get; set; }
        public int clubs_value  {get; set; }
        public int spades_value { get; set; }
        public int diamonds_value { get; set; }
        public AI(Player player)
        {
            this.hand = player.hand;
            this.hearts_value = 0;
            this.diamonds_value = 0;
            this.spades_value = 0;
            this.clubs_value = 0;

            this.hearts.Clear();
            this.diamonds.Clear();
            this.spades.Clear();
            this.clubs.Clear();

            // build lists of each suit-type for each players hand - WORKING
            for (var i = 0; i < this.hand.Count; i++)
            {
                if (this.hand[i].suit == "Hearts")
                {
                    this.hearts.Add(this.hand[i]);
                    this.hearts_value += this.hand[i].value;
                }
                else if (this.hand[i].suit == "Diamonds")
                {
                    this.diamonds.Add(this.hand[i]);
                    this.diamonds_value += this.hand[i].value;
                }
                else if (this.hand[i].suit == "Spades")
                {
                    this.spades.Add(this.hand[i]);
                    this.spades_value += this.hand[i].value;                
                }
                else if (this.hand[i].suit == "Clubs")
                {
                    this.clubs.Add(this.hand[i]);
                    this.clubs_value += this.hand[i].value;                
                }
            }
            // find highest value suit - set to hand value of this - WORKING
            this.suit_values[0] = this.hearts_value;
            this.suit_values[1] = this.diamonds_value;
            this.suit_values[2] = this.spades_value;
            this.suit_values[3] = this.clubs_value;
            this.hand_value = this.suit_values.Max();

            this.num_suits[0] = this.hearts.Count;
            this.num_suits[1] = this.diamonds.Count;
            this.num_suits[2] = this.spades.Count;
            this.num_suits[3] = this.clubs.Count;
        }
    
    }
    
}