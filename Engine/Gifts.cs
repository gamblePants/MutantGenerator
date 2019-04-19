using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Gifts
    {
        public int ID;
        public string Name;
        public string Path;
        public int Cost;

        public static List<Gifts> allGifts;

        public Gifts()
        {
        }

        public Gifts(int id, string name, string path, int cost)
        {
            ID = id;
            Name = name;
            Path = path;
            Cost = cost;
        }

        public static void NewGifts()
        {
            allGifts = new List<Gifts>()
            {
                new Gifts(01, "Summon Insects", "Crawling Chaos", 1), 
                new Gifts(02, "Mutate", "Crawling Chaos", 1), 
                new Gifts(03, "Scratching", "Crawling Chaos", 1), 
                new Gifts(04, "Made of Maggots", "Crawling Chaos", 2), 
                new Gifts(05, "Ohrwurm", "Crawling Chaos", 2), 
                new Gifts(06, "Uncontrollable", "Crawling Chaos", 2), 
                new Gifts(07, "Dance of the King", "The Yellow Sign", 1), 
                new Gifts(08, "Hallucination", "The Yellow Sign", 1),
                new Gifts(09, "Leprechaun", "The Yellow Sign", 1),
                new Gifts(10, "Between Places", "The Yellow Sign", 2), 
                new Gifts(11, "Doppleganger", "The Yellow Sign", 2), 
                new Gifts(12, "Suggestion", "The Yellow Sign", 2), 
                new Gifts(13, "Break their Bones", "Deep Ones", 1), 
                new Gifts(14, "Unwanted Guest", "Deep Ones", 1),
                new Gifts(15, "Intangible", "Deep Ones", 1),
                new Gifts(16, "Communion", "Deep Ones", 2), 
                new Gifts(17, "The Parasite", "Deep Ones", 2), 
                new Gifts(18, "Heal Wounds", "Deep Ones", 2), 
                new Gifts(19, "Tangleweed", "The Black Goat", 1), 
                new Gifts(20, "Seduce", "The Black Goat", 1), 
                new Gifts(21, "Self-spawn", "The Black Goat", 1),
                new Gifts(22, "Unholy Union", "The Black Goat", 2),
                new Gifts(23, "Crawling Orgy", "The Black Goat", 2), 
                new Gifts(24, "She Who Guides Me", "The Black Goat", 2), 
                new Gifts(25, "Infect", "The Unclean", 1), 
                new Gifts(26, "Talk to the Dead", "The Unclean", 1), 
                new Gifts(27, "Sanctuary", "The Unclean", 1), 
                new Gifts(28, "Grow Tumours", "The Unclean", 2),
                new Gifts(29, "Suppressed", "The Unclean", 2),
                new Gifts(30, "Allow Them", "The Unclean", 2), 
                new Gifts(31, "Under Shadow", "Daughters of Atlach-nacha", 1),
                new Gifts(32, "Confuse", "Daughters of Atlach-nacha", 1), 
                new Gifts(33, "Summon Spiders", "Daughters of Atlach-nacha", 1), 
                new Gifts(34, "Minor Illusion", "Daughters of Atlach-nacha", 2), 
                new Gifts(35, "Killing Grace", "Daughters of Atlach-nacha", 2), 
                new Gifts(36, "House of Lies", "Daughters of Atlach-nacha", 2),                                                                                 
                new Gifts(37, "Stone Flesh", "Chaugnar Faugn", 1), 
                new Gifts(38, "Horror From the Hills", "Chaugnar Faugn", 1), 
                new Gifts(39, "The Voorish Sign", "Chaugnar Faugn", 1), 
                new Gifts(40, "Suggestion", "Chaugnar Faugn", 2), 
                new Gifts(41, "Skin Maker", "Chaugnar Faugn", 2), 
                new Gifts(42, "Mind Block", "Chaugnar Faugn", 2),
                new Gifts(43, "Spit Acid", "The Burrowers", 1), 
                new Gifts(44, "Infect", "The Burrowers", 1),
                new Gifts(45, "Extinguish Light", "The Burrowers", 1), 
                new Gifts(46, "Turn Others", "The Burrowers", 2),
                new Gifts(47, "Shudde's Chant", "The Burrowers", 2), 
                new Gifts(48, "Dreams of Earth", "The Burrowers", 2), 
                new Gifts(49, "Frenzy", "Sleepers of N'Kai", 1), 
                new Gifts(50, "Premonition", "Sleepers of N'Kai", 1), 
                new Gifts(51, "Under Shadow", "Sleepers of N'Kai", 1), 
                new Gifts(52, "Self-spawn", "Sleepers of N'Kai", 2), 
                new Gifts(53, "Call Tsathoggua", "Sleepers of N'Kai", 2),
                new Gifts(54, "Poison Tongue", "Sleepers of N'Kai", 2), 
                new Gifts(55, "Intangible", "The Silver Key", 1),
                new Gifts(56, "Voorish Sign", "The Silver Key", 1), 
                new Gifts(57, "Silver Tongue", "The Silver Key", 1), 
                new Gifts(58, "Distort", "The Silver Key", 2), 
                new Gifts(59, "Mindtrap", "The Silver Key", 2), 
                new Gifts(60, "Backwards", "The Silver Key", 2), 
            };           
        }

        public static List<Gifts> AssignGifts(string path)
        {
            List<Gifts> temp = new List<Gifts>();
            temp = allGifts.Where(a => a.Path == path).ToList();
            List<Gifts> gifts = new List<Gifts>();
            int points = 3;
            
            while (points > 0)
            {
                int rnd = Dice.DiceRoller(temp.Count) -1;

                if (points >= temp[rnd].Cost)
                {
                    points -= temp[rnd].Cost;
                    gifts.Add(temp[rnd]);
                    temp.Remove(temp[rnd]);
                }
            }

            gifts = gifts.OrderBy(a => a.Name).ToList();
            return gifts;
        }
    }
}
