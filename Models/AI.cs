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
        public string max_suit_type { get; set; }
        public int[,] HandCombinations = new int [4,3] {{0,1,2},{0,1,3},{0,2,3},{1,2,3}};
        public int[] num_suits = new int[4];
        public int[] suit_values = new int[4];
        public int worst_value { get; set; }
        public int hearts_value { get; set; }
        public int clubs_value  {get; set; }
        public int spades_value { get; set; }
        public int diamonds_value { get; set; }
        public string best_suit { get; set; }
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

            for (var i = 0; i < this.hand.Count; i++)
            {
                if (this.hand[i].suit == "hearts")
                {
                    this.hearts.Add(this.hand[i]);
                    this.hearts_value += this.hand[i].value;
                }
                else if (this.hand[i].suit == "diamonds")
                {
                    this.diamonds.Add(this.hand[i]);
                    this.diamonds_value += this.hand[i].value;
                }
                else if (this.hand[i].suit == "spades")
                {
                    this.spades.Add(this.hand[i]);
                    this.spades_value += this.hand[i].value;                
                }
                else if (this.hand[i].suit == "clubs")
                {
                    this.clubs.Add(this.hand[i]);
                    this.clubs_value += this.hand[i].value;                
                }
            }
            this.suit_values[0] = this.hearts_value;
            this.suit_values[1] = this.diamonds_value;
            this.suit_values[2] = this.spades_value;
            this.suit_values[3] = this.clubs_value;
            this.hand_value = this.suit_values.Max();
            
            this.worst_value = this.hand_value;

            this.num_suits[0] = this.hearts.Count;
            this.num_suits[1] = this.diamonds.Count;
            this.num_suits[2] = this.spades.Count;
            this.num_suits[3] = this.clubs.Count;


            for(int i = 0; i < num_suits.Length; i++)
            {
                if(num_suits[i] != 0)
                {
                    if(suit_values[i] < this.worst_value)
                    {
                        this.worst_value = suit_values[i];
                    }
                    if(suit_values[i] == this.hand_value)
                    {
                        switch(i)
                         {
                            case 0:
                                this.best_suit = "hearts";
                                break;
                            case 1:
                                this.best_suit = "diamonds";
                                break;
                            case 2:
                                this.best_suit = "spades";
                                break;
                            case 3:
                                this.best_suit = "clubs";
                                break;
                        }
                    }
                }
            }
        }  
        public Card ChooseDiscard(AI player)
        {
            Player TestHand = new Player();
            Card min = player.hand[0];

            for(int i = 0; i < 4; i++)
            {
                TestHand.hand.Clear();
                for(int idx = 0; idx < 3; idx++)
                {
                    int index = HandCombinations[i,idx];
                    TestHand.hand.Add(player.hand[index]);
                }
                int Temp = HandValue.Calculate(TestHand);
                if(Temp == player.hand_value)
                {
                    min = player.hand[3 - i];
                }
            }
            if(player.num_suits.Contains(4))
            {
                foreach(Card c in player.hand)
                {
                    if(c.value < min.value)
                    {
                        min = c;
                    }   
                }
            }
            else
            {
                foreach(Card c in player.hand)
                {
                    if(c.suit != player.best_suit)
                    {
                        if(c.value < min.value)
                        {
                            min = c;
                        }
                    }
                }
            }

            return min;
        }
        public bool EvaluateDiscardCard(AI player, Card DiscardCard)
        {
            Player TestHand = new Player();
            foreach(Card c in player.hand)
            {
                TestHand.hand.Add(c);
            }
            TestHand.hand.Add(DiscardCard);
            AI Calculate = new AI(TestHand);

            Card min = ChooseDiscard(Calculate);
            if(min == DiscardCard)
            {
                return false;
            }
            else
            {
                TestHand.hand.Remove(min);
                TestHand.hand_value = HandValue.Calculate(TestHand);
                if(TestHand.hand_value < 11 && TestHand.hand_value > player.hand_value + 5)
                {
                    return true;
                }
                else if(TestHand.hand_value > 11 && TestHand.hand_value < 20 && TestHand.hand_value > player.hand_value + 3)
                {
                    return true;
                }
                else if(TestHand.hand_value >= 20 && TestHand.hand_value > player.hand_value)
                {
                    return true;
                }
                else if(TestHand.hand_value == player.hand_value && DiscardCard.value == 11)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

