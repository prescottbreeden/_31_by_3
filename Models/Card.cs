using System;
using System.Collections.Generic;

namespace _31_by_3
{
    public class Card
    {
        public string face { get; set; }
        public string suit { get; set; }
        public int value { get; set; }
        
        public Card()
        {
            
        }
        public Card(string face, string suit, int value)
            : this()
        {
            this.face = face;
            this.suit = suit;
            this.value = value;
        }
        public override string ToString()
        {
            return face + " of " + suit;
        }    
    }
    
}