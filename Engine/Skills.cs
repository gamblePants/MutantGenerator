using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Skills
    {
        public int ID;
        public string Name;
        public int Level;
        public int PreReqNumber;
        public string[] PreReqSkills;
        public int BackLimit; // BackLimit -1 matches index of Background.skillsLimited

        public static List<Skills> allSkills;

        // constructors
        public Skills(string name, int level)
        {
            Name = name;
            Level = level;
        }

        public Skills(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public Skills(int id, string name, int backLimit)
        {
            ID = id;
            Name = name;
            BackLimit = backLimit;
        }

        public Skills(int id, string name, int preReqNum, string[] preReqSkills)
        {
            ID = id;
            Name = name;
            PreReqNumber = preReqNum;
            PreReqSkills = preReqSkills;
        }

        public Skills(int id, string name, int preReqNum, string[] preReqSkills, int backLimit)
        {
            ID = id;
            Name = name;
            PreReqNumber = preReqNum;
            PreReqSkills = preReqSkills;
            BackLimit = backLimit;
        }

        // creates list of skills
        public static void NewSkills()
        {
            allSkills = new List<Skills>()
            {
                new Skills(1, "Academics"),
                new Skills(2, "Animal Handling"),
                new Skills(3, "Athletics"),
                new Skills(4, "Brawl"),
                new Skills(5, "Computer"),
                new Skills(6, "Demolitions", 1, new string[]{"Electrical", "Science"}, 1),
                new Skills(7, "Drive"),
                new Skills(8, "Electrical"),
                new Skills(9, "Farming"),
                new Skills(10, "Firearms"),
                new Skills(11, "Language"),
                new Skills(12, "Mathematics"),
                new Skills(13, "Mechanics"),
                new Skills(14, "Medicine", 3, new string[]{"Academics", "Science"}),
                new Skills(15, "Melee"),
                new Skills(16, "Navigation"),
                new Skills(17, "Occult"),
                new Skills(18, "Performance"),
                new Skills(19, "Pick Locks", 2),
                new Skills(20, "Pick Pockets", 3),
                new Skills(21, "Pilot", 1, new string[]{"Drive"}),
                new Skills(22, "Science", 3, new string[]{"Academics", "Mathematics"}),
                new Skills(23, "Stealth"),
                new Skills(24, "Survival"),
                new Skills(25, "Tracking")
            };
        }

        public static Skills GetByName(string name)
        {
            return allSkills.First(a => a.Name == name);
        }
       
        public static List<Skills> AssignSkills(Dictionary<string, int> bSkills, List<Skills> skills, int[] limits)
        {
            int points = 20;

            // assigns skills from background
            foreach (KeyValuePair<string, int> s in bSkills)
            {
                Skills skill = GetByName(s.Key);
                skill.Level = s.Value;
                skills.Add(skill);
                points -= s.Value;
            }

            // randomly purchases the rest of skills with remaining points
            while (points > 0)
            {
                int rnd = Dice.DiceRoller(25) - 1;
                Skills selected = allSkills[rnd];

                // has skill already - checks to see if extra level can be purchased
                if (skills.Exists(a => a.ID == selected.ID))
                {
                    int index = skills.FindIndex(a => a.ID == selected.ID);
                    if (skills[index].Level == 3)
                    {
                        continue; // cannot have a skill higher than level 3 - does not purchase extra level
                    }
                    if (selected.Name == "Occult")
                    {
                        continue; // occult cannot be higher than level 1 - does not purchase extra level
                    }                    
                    if (selected.PreReqNumber > 1 && skills[index].Level == selected.PreReqNumber -1 && !CheckPreRequisites(selected, skills))
                    {
                        continue; // requires certain skills to have this skill at certain level - fails check, does not purchase extra level
                    }
                    if (selected.BackLimit > 0 && !CheckBackgroundLimits(selected.ID, skills, limits))
                    {                       
                         continue; // only certain backgrounds can get this skill at higher level - fails check, does not purchase extra level                     
                    }

                    skills[index].Level++;
                    points--;
                    continue; // passes all checks - purchases extra level for skill
                }

                // does not have skill already - checks to see if it can be purchased
                if (selected.Name == "Occult")
                {
                    continue; // only available to priest/shaman (given) - does not purchase skill
                }
                if (selected.Name == "Pick Locks" && limits[1] == 0 || selected.Name == "Pick Locks" && !skills.Any(a => a.ID == 8 || a.ID == 13))
                {                    
                    continue; // does not have prerequisite background or skills for pick locks - does not purchase skill
                }
                if (selected.PreReqNumber == 1 && !CheckPreRequisites(selected, skills))
                {
                    continue; // fails pre-requisite check - does not purchase skill
                }

                selected.Level = 1;
                skills.Add(selected);
                points--; // purchases new skill
            }

            skills = skills.OrderBy(a => a.Name).ToList();
            return skills;
        }

        // checks list of skills for pre-requisite skills and returns false if any of them don't exist
        public static bool CheckPreRequisites(Skills selected, List<Skills> skills)
        {            
            foreach (string s in selected.PreReqSkills)
            {
                if (!skills.Exists(a => a.Name == s))
                {
                    return false;
                }
            }          

            return true;
        }

        // if the skill is already at the highest level the background will allow, returns false
        public static bool CheckBackgroundLimits(int id, List<Skills> skills, int[] limits)
        {
            int index = skills.FindIndex(a => a.ID == id);
            for (int i = 0; i < limits.Length; i++)
            {
                if (skills[index].BackLimit - 1 == i && skills[index].Level == limits[i])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}