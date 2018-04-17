using System;
using System.Collections.Generic;

namespace _31_by_3
{
    public class Deck
    {
        public Card[] deck;
        public int current_card { get; set; }
        public const int NUMBER_OF_CARDS = 52;
        public List<Card> discard_pile = new List<Card>();
        public Random ranNum;

        public Deck()
        {
            string[] faces = {"Two", "Three", "Four", "Five", "Six", "Seven",
                            "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace"};
            string[] suits = {"Hearts", "Clubs", "Spades", "Diamonds"};
            int[] values = {2,3,4,5,6,7,8,9,10,10,10,10,11};
            deck = new Card[NUMBER_OF_CARDS];
            current_card = 0;
            ranNum = new Random();
            for(int count = 0; count < deck.Length; count++)
                deck[count] = new Card(faces[count % 13], suits[count / 13], values[count % 13]);

        }
        public void Shuffle()
        {
            current_card = 0;
            for (int first = 0; first < deck.Length; first++)
            {
                int second = ranNum.Next(NUMBER_OF_CARDS);
                Card temp = deck[first];
                deck[first] = deck[second];
                deck[second] = temp;
            }
        }
        public void DrawFromDeck(Player player)
        {
            if (current_card < NUMBER_OF_CARDS)
                player.hand.Add(deck[current_card++]);
            else
                System.Console.WriteLine("No more cards!!");;
        }
        public void MoveTopCardToDiscardPile()
        {
            if(current_card < NUMBER_OF_CARDS)
                discard_pile.Insert(0, deck[current_card++]);
        }
        
        public void DrawFromDiscard(Player player)
        {
            player.hand.Add(discard_pile[0]);
            discard_pile.RemoveAt(0);
        }
        public void ShowDiscardPile()
        {
            if(discard_pile.Count == 0)
                System.Console.WriteLine("Error... Discard pile is empty");
            else
            {   
                System.Console.WriteLine("Top card in discard pile stack:");
                System.Console.WriteLine(discard_pile[0]);
            }
        }
    }
}