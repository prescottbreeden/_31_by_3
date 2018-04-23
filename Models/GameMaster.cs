using System.Collections.Generic;

namespace _31_by_3
{
    public class GameMaster
    {
        public int dealer { get; set; }
        public int turn { get; set; }
        public Deck deck { get; set; }
        public bool knocked { get; set; }
        public List<Player> players { get; set; }
        public GameOver endGame { get; set; }
        public bool AllAI { get; set;}
        public bool SinglePlayer { get; set; }
        public GameMaster()
        {
        
        }
        public GameMaster(List<string> PlayerSelect)
            : this()
        {
            Deck NewDeck = GamePlay.BuildAndShuffle();
            List<Player> Players = GamePlay.CreatePlayers(PlayerSelect);
            
            this.deck = NewDeck;
            this.players = Players;
            this.AllAI = true;
            this.knocked = false;

            GamePlay.Deal(this.players, this.deck);
            List<int> Humans = new List<int>();
            for(var i = 0; i < Players.Count; i++)
            {
                Players[i].hand_value = HandValue.Calculate(Players[i]);
                if(Players[i].isHuman == true)
                {
                    this.AllAI = false;
                    Humans.Add(i);
                }
            }
            if(Humans.Count == 1)
            {
                this.SinglePlayer = true;
            }
        }

        public GameMaster(GameMaster PreviousGame)
            : this() 
        {
            Deck NewDeck = GamePlay.BuildAndShuffle();
            
            this.deck = NewDeck;
            this.players = PreviousGame.players;
            this.AllAI = PreviousGame.AllAI;
            this.SinglePlayer = PreviousGame.SinglePlayer;
            this.dealer = PreviousGame.dealer;
            this.knocked = false;

            foreach (Player player in players)
            {
                player.hand.Clear();
                player.knocked = false;
            }
            this.dealer++;
            if(this.dealer == 4)
            {
                this.dealer = 0;
            }
            this.turn = this.dealer;
            if(this.turn == 4)
            {
                this.turn = 0;
            }

            GamePlay.Deal(this.players, this.deck);
            foreach(Player player in players)
            {
                player.hand_value = HandValue.Calculate(player);
            }

        }
    }
}