using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Paths
    {
        public int ID;
        public string Name;
        public string Modifier;

        public static List<Paths> paths;

        // constructors
        public Paths()
        {
        }

        public Paths(int id, string name, string mod)
        {
            ID = id;
            Name = name;
            Modifier = mod;
        }

        // creates list of paths
        public static void NewPaths()
        {
            paths = new List<Paths>()
            {
                new Paths (1, "Crawling Chaos", "Willpower"), 
                new Paths (2, "The Yellow Sign", "Charisma"), 
                new Paths (3, "Deep Ones", "Strength"), 
                new Paths (4, "The Black Goat", "Charisma"), 
                new Paths (5, "The Unclean", "Stamina"), 
                new Paths (6, "Daughters of Atlach-nacha", "Agility"), 
                new Paths (7, "Chaugnar Faugn", "Strength"), 
                new Paths (8, "The Burrowers", "Willpower"), 
                new Paths (9, "Sleepers of N'Kai", "Agility"), 
                new Paths (10, "The Silver Key", "Intelligence") 
            };
        }

        public static Paths ChoosePath(int storyId)
        {
            Story story = Story.Get(storyId);
            int rnd = Dice.DiceRoller(story.AvailPaths.Length)-1;            
            string name = story.AvailPaths[rnd];
            return paths.First(a => a.Name == name);     
        }
       
    }

    
}
