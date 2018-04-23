using System;
using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public class GameOver
    {
        public Player Winner = new Player();
        public List<Player> TieScores = new List<Player>();
        public bool callTieBreaker = false;
        public int HighestRankingCard;
        public int Pot { get; set; }

        public GameOver()
        {
            
        }

        public GameOver(GameMaster gameMaster)
        {

            // set highest hand to winner
            foreach(Player player in gameMaster.players)
            {
                if(player.hand_value > Winner.hand_value)
                {
                    this.Winner = player;
                }
            }
            foreach(Player player in gameMaster.players)
            {
                if(player.hand_value == Winner.hand_value)
                {
                    TieScores.Add(player);
                }
            }
            if(TieScores.Count > 1)
            {
                string HighestRankingFace = TieScores[0].hand[0].face;
                HighestRankingCard = Int32.Parse(HighestRankingFace);
                foreach(Player player in TieScores)
                {
                    foreach(Card c in player.hand)
                    {
                        int face = Int32.Parse(c.face);
                        if(face > HighestRankingCard)
                        {
                            HighestRankingCard = face;
                            this.Winner = player;
                        }
                        if(face == HighestRankingCard)
                        {
                            callTieBreaker = true;
                        }
                    }
                }
                if(callTieBreaker)
                {
                    // foreach(Player player in all_players)
                    // {
                    //     player.hand.OrderByDescending(c => c.value);
                    // }
                    // for(var card = 0; card < 3; i++)
                    // {
                    //     for(var player = 0; player < all_players.Count; player++)
                    //     {
                    //         if(all_players[player].hand[card]>)
                    //     }
                    // }
                }
            }

            // check if winner is knocker
            if(Winner.knocked == false)
            {
                foreach(Player player in gameMaster.players)
                {
                    if(player.knocked)
                    {
                        player.chips--;
                    }
                }
            }
            else
            {
                Player Loser = gameMaster.players[0];
                foreach(Player player in gameMaster.players)
                {
                if(player.hand_value < Loser.hand_value) // double check with tie logic
                    {
                        Loser = player;
                    }
                }
                Loser.chips--;
            }
            System.Console.WriteLine("NEW CHIP BALANCE ***************************");
             foreach(Player person in gameMaster.players)
            {
                System.Console.WriteLine(person.name + " has chips: " + person.chips);
            }
        }
        public GameOver(Player winner, GameMaster gameMaster)
        {
            this.Winner = winner;

            foreach(Player player in gameMaster.players)
            {
                if(player != this.Winner)
                {
                    player.chips--;
                    Pot++;
                }
            }
        }        
    }
}