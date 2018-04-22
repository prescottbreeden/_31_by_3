using System;
using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public class GameOver
    {
        public List<Player> AllPlayers = new List<Player>();
        public Player Winner = new Player();
        public Player Loser = new Player();
        public List<Player> TieScores = new List<Player>();
        public int HighestRankingCard;
        public int Pot { get; set; }

        public GameOver()
        {
            
        }

        public GameOver(List<Player> all_players)
        {
            this.AllPlayers = all_players;

            // set highest hand to winner
            foreach(Player player in AllPlayers)
            {
                if(player.hand_value > Winner.hand_value)
                {
                    this.Winner = player;
                }
            }
            foreach(Player player in AllPlayers)
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
                            Winner = player;
                        }
                    }
                }
            }

            // check if winner is knocker
            if(Winner.knocked == false)
            {
                foreach(Player player in all_players)
                {
                    if(player.knocked)
                    {
                        this.Loser = player;
                        Loser.chips--;
                    }
                }
            }
            else
            {
                foreach(Player player in AllPlayers)
                {
                if(player.hand_value < Loser.hand_value)
                    {
                        this.Loser = player;
                    }
                }
                Loser.chips--;
            }
        }
        public GameOver(Player winner, List<Player> all_players)
        {
            this.Winner = winner;

            foreach(Player player in all_players)
            {
                if(player != winner)
                {
                    player.chips--;
                    Pot++;
                }
            }
        }        
    }
}