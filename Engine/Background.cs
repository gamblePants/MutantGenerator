using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Background
    {
        public int ID;
        public string Name;
        public char StatsBase; // 'p' physical, 'm' mental, 'b' both, 'x' sub-background
        public Dictionary<string, int> Skills;        
        public bool SubBackground;
        public int[] SkillsLimit; // some backgrounds limit skill level

        public static List<Background> backgrounds;
        public static List<Background> teachers;

        // constructors
        public Background()
        {
        }

        public Background(int id, string name, char statsBase, Dictionary<string, int> skills, bool subBackground,int[] skillsLimit)
        {
            ID = id;
            Name = name;
            StatsBase = statsBase;
            Skills = skills;            
            SubBackground = subBackground;
            SkillsLimit = skillsLimit; // skillsLimit[0] = demolitions, skillsLimit[1] = pick locks, skillsLimit[2] = pick pockets
        }

        // creates list of Backgrounds
        public static void NewBackgrounds()
        {
            backgrounds = new List<Background>()
            {
                new Background(1, "Soldier/Policeman", 'p', new Dictionary<string, int>{{"Firearms", 2}, {"Athletics", 2}, {"Drive", 2}, {"Brawl", 1}, {"Melee", 1}}, false, new int[]{3,3,1}),
                new Background(2, "Nurse/Doctor", 'b', new Dictionary<string, int>{{"Medicine", 3}, {"Academics", 2}, {"Science", 1}}, false, new int[]{1,0,1}),
                new Background(3, "Scientist", 'm', new Dictionary<string, int>{{"Science", 3}, {"Academics", 2}, {"Mathematics", 1}, {"Computer", 1}}, true, new int[]{3,0,1}), // or academic
                new Background(4, "Performer/Artist", 'b', new Dictionary<string, int>{{"Performance", 3}, {"Academics", 1}}, false, new int[]{1,1,1}),
                new Background(5, "Hunter", 'p', new Dictionary<string, int>{{"Tracking", 2}, {"Stealth", 1}, {"Firearms", 2}, {"Navigation", 1}, {"Survival", 1}}, true, new int[]{2,1,1}), // or farmer
                new Background(6, "Mechanic/Construction", 'p', new Dictionary<string, int>{{"Mechanics", 3}, {"Drive", 1}, {"Athletics", 1}}, false, new int[]{3,3,1}),
                new Background(7, "Electrician", 'b', new Dictionary<string, int>{{"Electrical", 3}, {"Mathematics", 1} }, true, new int[]{3,3,1}), // or engineer
                new Background(8, "Computer Tech", 'm', new Dictionary<string, int>{{"Computer", 3}, {"Mathematics", 1}}, false, new int[]{1,0,1}),
                new Background(9, "Athlete", 'p', new Dictionary<string, int>{{"Athletics", 3}}, true, new int[]{1,0,1}), // or thug
                new Background(10, "Priest/Shaman", 'm', new Dictionary<string, int>{{"Academics", 2}, {"Occult", 1}, {"Performance", 1}}, false, new int[]{1,0,1}),
                new Background(11, "Pilot", 'b', new Dictionary<string, int>{{"Drive", 2}, {"Pilot", 2}, {"Navigation", 1}}, true, new int[]{1,1,1}), // or technician
                new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 1}}, true, new int[]{1,0,1}), // roll for type of teacher
                new Background(13, "Thief", 'p', new Dictionary<string, int>{{"Stealth", 2}, {"Pick Locks", 2}, {"Pick Pockets", 2}}, true, new int[]{3,3,3}), // or scout
                new Background(14, "Businessman/Merchant", 'b', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 1}}, false, new int[]{1,0,1}),
                new Background(15, "Journalist/Author", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 2}, { "Language", 1 }}, false, new int[]{1,1,1}),
                new Background(16, "Academic", 'x', new Dictionary<string, int>{{"Academics", 3}, {"Science", 1}, {"Language", 1}}, false, new int[]{1,0,1}),
                new Background(17, "Farmer", 'x', new Dictionary<string, int>{{"Farming", 3}, {"Science", 1}, {"Survival", 1}}, false, new int[]{1,0,1}),
                new Background(18, "Engineer", 'x', new Dictionary<string, int>{{"Mathematics", 2}, {"Science", 1}, {"Mechanics", 1}, {"Electrical", 1}, {"Computer", 1}}, false, new int[]{3,3,1}),
                new Background(19, "Thug", 'x', new Dictionary<string, int>{{"Brawl", 2}, {"Athletics", 1}, {"Melee", 1}}, false, new int[]{3,3,13}),
                new Background(20, "Technician", 'x', new Dictionary<string, int>{{"Drive", 1 }, {"Navigation", 1}, {"Computer", 1}, {"Electrical", 1}, {"Mathematics", 1 }}, false, new int[]{3,3,1}),
                new Background(21, "Scout", 'x', new Dictionary<string, int>{{"Stealth", 2}, {"Navigation", 2}, {"Tracking", 2}}, false, new int[]{3,3,3})
            };
        }

        public static Background ChooseBackground(bool isPhysical, Background background)
        {
            int typeOf = Dice.DiceRoller(3);
            int selected = Dice.DiceRoller(5) -1;            

            List<Background> physical = new List<Background>();
            physical = backgrounds.Where(a => a.StatsBase == 'p').ToList();
            List<Background> mental = new List<Background>();
            mental = backgrounds.Where(a => a.StatsBase == 'm').ToList();
            List<Background> both = new List<Background>();
            both = backgrounds.Where(a => a.StatsBase == 'b').ToList();

            // changes odds so there is an equal chance of selecting 'p', 'm' or 'b' backgrounds (based on isPhysical)
            if (isPhysical)
            {
                if (typeOf == 1)
                {
                    background = both[selected];
                }
                else
                {
                    background = physical[selected];
                }
            }
            else
            {
                if (typeOf == 1)
                {
                    background = both[selected];
                }
                else
                {
                    background = mental[selected];
                }
            }           

            // calculate sub-backgrounds
            if (background.SubBackground == true)
            {
                background = CalculateSubBackground(background);
            }
            
            // 1 in 6 soldier/policeman (or scout/spy) backgrounds get demolitions skills
            if (background.ID == 1 && Dice.DiceRoller(6) == 6 || background.ID == 21 && Dice.DiceRoller(6) == 6)
            {
                background.Skills.Add("Science", 1);
                background.Skills.Add("Electrical", 1);
                background.Skills.Add("Demolitions", 1);
            }
            
            return background;
        }

        public static Background CalculateSubBackground(Background background)
        {
            // teacher sub-background
            if (background.ID == 12)
            {
                int rnd = Dice.DiceRoller(6) -1;

                teachers = new List<Background>()
                {
                    new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 1}, {"Mathematics", 2}}, true, new int[]{1,0,1}),
                    new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 1}, {"Language", 2}}, true, new int[]{1,0,1}),
                    new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 2}, { "Performance", 1}}, true, new int[]{1,0,1}),
                    new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 1}, { "Science", 2 }}, true, new int[]{1,0,1}),
                    new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 2}}, true, new int[]{1,0,1}),
                    new Background(12, "Teacher/Lecturer", 'm', new Dictionary<string, int>{{"Academics", 1}, {"Performance", 1}, {"Athletics", 1}}, true, new int[]{1,0,1})
                };
                return teachers[rnd];              
            }

            // other sub-backgrounds
            int[,] subBackgrounds = new int[,] { { 3, 16 }, { 5, 17 }, { 7, 18 }, { 9, 19 }, { 11, 20 }, { 13, 21 } };

            for (int i = 0; i < subBackgrounds.Length/2; i++)
            {
                // 50% chance of keeping original background, 50% chance of taking the related sub-background
                if (background.ID == subBackgrounds[i, 0] && Dice.FlipCoin())
                {                    
                    return backgrounds.First(a => a.ID == subBackgrounds[i, 1]);                    
                }
            }
            return background;
        }
    }
}