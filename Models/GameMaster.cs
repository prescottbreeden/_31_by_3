using System.Collections.Generic;

namespace _31_by_3
{
    public class GameMaster
    {
        public int dealer { get; set; }
        public int turn { get; set; }
        public int GameSpeed {get; set;}
        public Deck deck { get; set; }
        public bool knocked { get; set; }
        public List<Player> players { get; set; }
        public GameOver endRound { get; set; }
        public bool endGame;
        public bool AllAI { get; set;}
        public bool SinglePlayer { get; set; }
        public GameMaster()
        {
        
        }
        public GameMaster(List<string> PlayerSelect)
        {
            Deck NewDeck = GamePlay.BuildAndShuffle();
            List<Player> Players = GamePlay.CreatePlayers(PlayerSelect);
            
            this.deck = NewDeck;
            this.players = Players;
            this.AllAI = true;
            this.knocked = false;
            this.GameSpeed = 2;

            GamePlay.Deal(this.players, this.deck);
            int HumanCount = 0;
            foreach(Player player in players)
            {
                player.hand_value = HandValue.Calculate(player);
                if(player.isHuman)
                {
                    this.AllAI = false;
                    HumanCount++;
                }
            }
            if(HumanCount == 1)
            {
                this.SinglePlayer = true;
            }
        }

        public GameMaster(GameMaster PreviousGame)
        {
            Deck NewDeck = GamePlay.BuildAndShuffle();
            this.deck = NewDeck;

            List<Player> NextGame = new List<Player>();
            foreach(Player player in PreviousGame.players)
            {
                if(player.chips >= 0)
                {
                    NextGame.Add(player);
                    player.hand.Clear();
                    player.knocked = false;
                }
            }
            if(NextGame.Count == 1)
            {
                this.endGame = true;
            }
            this.players = NextGame;
            this.dealer = PreviousGame.dealer;
            this.GameSpeed = PreviousGame.GameSpeed;
            this.knocked = false;
            this.AllAI = true;
            this.dealer++;
            if(this.dealer >= players.Count)
            {
                this.dealer = 0;
            }
            this.turn = this.dealer;

            GamePlay.Deal(this.players, this.deck);
            int HumanCount = 0;
            foreach(Player player in players)
            {
                player.hand_value = HandValue.Calculate(player);
                if(player.isHuman)
                {
                    this.AllAI = false;
                    HumanCount++;
                }
            }
            if(HumanCount == 1)
            {
                this.SinglePlayer = true;
            }

        }
    }
}