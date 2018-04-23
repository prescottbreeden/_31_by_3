using System;
using System.Collections.Generic;
using System.Linq;

namespace _31_by_3
{
    public class Player : BaseEntity
    {
        public string name { get; set; }
        public bool isHuman { get; set; }
        public int chips { get; set; }
        public bool knocked { get; set; }
        public int player_seat { get; set; }
        public int hand_value { get; set; }
        public List<Card> hand = new List<Card>();

        public Player()
        {
            this.chips = 3;
            this.knocked = false;
            this.hand_value = 0;
        }
        public Player(string name, bool isHuman = true)
            : this()
        {
            if(name == "zxc")
            {
                this.name = this.CreateRandomName();
                this.isHuman = false;
            }
            else
            {
                this.name = name;
                this.isHuman = true;
            }
        }
        public string CreateRandomName()
        {
            string[] ListOfNames = {"Admiral","Alexandra","Andy","Ashely","Alan","Chanthy","Chris","Dmitri","Donovan","Dustin","Emily","Francisco","Graham","Ian","Javier","Josiah","Justin","JJ","Joyce","Larry","Lawyer","Mark","Mike","Nick","Noelle","Phil","Prescott","Rose","Sierra","Stephen","Sun","Susie","Tiannia","Tim"};
            Random rand = new Random();
            var RandomName = ListOfNames[rand.Next(0,ListOfNames.Length)];
            return RandomName;
        }
        public static string CreateRandomFunnyName()
        {
            string[] ListOfNames = {"LocalHost:5000","Missing Semicolon","Infinite Loop","Undefined","Internal Server Error","Ceiling Cat","0011011000111001","Waiting for compile","Missing Return Statement","Syntax Error","Buggy Code","404","Bad Gateway","Towelie"};
            Random rand = new Random();
            var RandomName = ListOfNames[rand.Next(0,ListOfNames.Length)];
            return RandomName;
        }
        public static string CreateRandomEarthName()
        {
            string[] ListOfNames = {"Captain Planet","Groot","Smokey da Bear","ManBearPig"};
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

// Eleonora","Lola","Jenifer","Ollie","Denna","Iesha","Estrella","Kera","Zachery","Lonnie","Louis","Josef","Debra","Valentine","Del","Gaston","Valentin","Lashell","Tamar","Ayako","Dierdre","Ethelyn","Ilda","In","Margaret","Vania","Harris","Candance","Sherell","Fausto","Devora","Wilford","Estell","Dorinda","Otilia","Margit","Kaci","Isaac","Anna","Casimira","Bea","Margie","Hana","Tona","David","Wilber","Junie","Silas","Stefan","Moshe","Gilberte","Aleen","Bernice","Eliseo","Kenneth","Derrick","Yevette","Ezekiel","Sandy","Reid","Sanda","Tyesha","Trudy","Ilene","Laurence","Leigha","William","Royal","Moises","Long","Berenice","Debby","Marlon","Rex","Tammy","Edythe","Leticia","Leonie","Logan","Katherin","Mirian","Margorie","Robby","Ivan","Harriet","Micki","Everette","Lavona","Sylvie","Tawny","Rita","Eldora","Princess","Leann","Amos","Yadira","Raul","Buck","Emory","Nora", 