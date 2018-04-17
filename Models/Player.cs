using System;
using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public class Player : BaseEntity
    {
        public string name { get; set; }
        public bool isHuman { get; set; }
        public int tokens { get; set; }
        public List<Card> hand = new List<Card>();
        public List<Card> hearts = new List<Card>();
        public List<Card> diamonds = new List<Card>();
        public List<Card> spades = new List<Card>();
        public List<Card> clubs = new List<Card>();
        public int[] num_suits = new int[4];
        public bool isDealer { get; set; }
        public int player_seat { get; set; }
        public int hand_value { get; set; }
        public int hearts_value { get; set; }
        public int clubs_value  {get; set; }
        public int spades_value { get; set; }
        public int diamonds_value { get; set; }
        public int[] suit_values = new int[4];
        public bool knocked { get; set; }

        public Player()
        {
            this.tokens = 3;
            this.isDealer = false;
            this.knocked = false;
        }
        public Player(string name, bool isHuman = true)
            : this()
        {
            this.isHuman = isHuman;
            this.name = name;
        }
        public string CreateRandomName()
        {
            string[] ListOfNames = {"Eleonora","Lola","Jenifer","Ollie","Denna","Iesha","Estrella","Kera","Zachery","Lonnie","Louis","Josef","Debra","Valentine","Del","Gaston","Valentin","Lashell","Tamar","Ayako","Dierdre","Ethelyn","Ilda","In","Margaret","Vania","Harris","Candance","Sherell","Fausto","Devora","Wilford","Estell","Dorinda","Otilia","Margit","Kaci","Isaac","Anna","Casimira","Bea","Margie","Hana","Tona","David","Wilber","Junie","Silas","Stefan","Moshe","Gilberte","Aleen","Bernice","Eliseo","Kenneth","Derrick","Yevette","Ezekiel","Sandy","Reid","Sanda","Tyesha","Trudy","Ilene","Laurence","Leigha","William","Royal","Moises","Long","Berenice","Debby","Marlon","Rex","Tammy","Edythe","Leticia","Leonie","Logan","Katherin","Mirian","Margorie","Robby","Ivan","Harriet","Micki","Everette","Lavona","Sylvie","Tawny","Rita","Eldora","Princess","Leann","Amos","Yadira","Raul","Buck","Emory","Nora", "Lawyer", "Prescott", "Justin"};
            Random rand = new Random();
            var RandomName = ListOfNames[rand.Next(0,ListOfNames.Length)];
            return RandomName;
        }

        public override string ToString()
        {
            return name;
        }   
    }
}