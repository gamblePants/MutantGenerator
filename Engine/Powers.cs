using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Powers
    {
        public int ID { get; set; }
        public string Name { get; set; }        
        public int Cost { get; set; } 

        public static List<Powers> allPowers = new List<Powers>();

        // miscellaneous constructor
        public Powers()
        {
        }

        // main constructor
        public Powers(int id, string name, int cost)
        {
            ID = id;
            Name = name;
            Cost = cost;
        }

        public static void NewPowers()
        {
            allPowers = new List<Powers>()
            {
                new Powers(1, "Claws (Str+1)", 1),
                new Powers(2, "Teeth (Str)", 1),
                new Powers(3, "Digging", 1),
                new Powers(4, "Advanced Hearing (init +1)", 1),
                new Powers(5, "Increase Metabolism (+1str,agi,sta)", 2),                
                new Powers(6, "Fly (up to 50kmph)", 1),                
                new Powers(7, "Venom Sting (Str)(injury)", 2),                  
                new Powers(8, "Advanced Vision (init +1)(track -2dif)", 1),
                new Powers(9, "Advanced Smell (init +1)(track -2dif)", 1),                
                new Powers(10, "Breathe Underwater", 1),
                new Powers(11, "Venom Bite (Str)(paralysis)", 2),                     
                new Powers(12, "Tentacles (climb -2dif)", 1),
                new Powers(13, "Multi-limb Attack (2 att per round)", 2),
                new Powers(14, "Chameleon Skin (stealth -2dif)", 1),
                new Powers(15, "Squeeze Through Bars", 1),
                new Powers(16, "Nightvision", 1),
                new Powers(17, "Teeth (Str+1)", 1),
                new Powers(18, "Deafening Drone (-1 for 1/2 rounds)", 1),                
                new Powers(19, "Mimick Sound (-2dif)", 1), 
                new Powers(20, "Fly (up to 100kmph)", 2),
                new Powers(21, "Bite (Str)", 1),
                new Powers(22, "Withstand Starvation && Extreme Temps (-3dif)", 1), 
                new Powers(23, "Drought Cocoon", 1),
                new Powers(24, "Frog Glue (climb -2dif)", 1),
                new Powers(25, "Claws (Str)", 1),
                new Powers(26, "Lockjaw Teeth (Str+1)(auto dam)", 1),                 
                new Powers(27, "Prehensile Trunk (partial hand)", 1),
                new Powers(28, "Thick Hide (AR:1)", 1),
                new Powers(29, "Tusks (Str+2)", 2),
                new Powers(30, "Advanced Vision (init +1)", 1),
                new Powers(31, "Sticky Pads (climb -2dif)", 1), 
                new Powers(32, "Acid Vomit (Con+1)(auto dam)", 1), 
                new Powers(33, "Venom Bite (Str)(injury)", 2),
                new Powers(34, "Web Strands (ambush +1, climb -1dif)", 1),
                new Powers(35, "Setules on Hands/Feet (climb -1dif)", 1),
                new Powers(36, "Echo Location (pitch black)", 1),
                new Powers(37, "Claws (Str+1)(climb -2dif)", 1),
                new Powers(38, "Scaly Hide (AR:1)", 1),
                new Powers(39, "Whip Tail (Str)(knockdown)", 1),
                new Powers(40, "Horns (Str)", 1),
                new Powers(41, "Climbing (-2dif)", 1),
                new Powers(42, "Spin Web (Str to break)(climb -2dif)", 2),            
                new Powers(43, "Blood Sucking (Str+1)(auto dam)", 1),
                new Powers(44, "Detect Vibrations (-1dif stealth skills)", 1), 
                new Powers(45, "Sting Bite (Str+1)", 1),
                new Powers(46, "Defensive Toxins (+1 dam brawl)", 1),               
                new Powers(47, "Exoskeleton (AR:2/4)(when curled)", 2),
                new Powers(48, "Exoskeleton (AR:2)", 2),
                new Powers(49, "Claws (Str)(climb -1dif)", 1),
                new Powers(50, "Infrared Vision (in pitch black)", 1),
                new Powers(51, "Quick Strike (+1 brawl/melee)", 1),
                new Powers(52, "Venom Bite (Str)(sickness)", 2),                
                new Powers(53, "Venom Spines (Str)(sickness)", 2), 
                new Powers(54, "Venom Sting (Str)(injury)", 2), 
                new Powers(55, "Regeneration (heals 3x faster)", 2),
                new Powers(56, "Swimming (-2dif)", 1),
                new Powers(57, "Venom Bite (Str)(necrotic)", 2)               
            };
        }

        // assigns animal powers to character
        public static List<Powers> AssignPowers(List<int> index, List<Powers> powers)
        {
            
            List<Powers> temp = new List<Powers>();
            // creates list of available powers by animal ID
            foreach (int p in index)
            {
                temp.Add(allPowers.First(allPowers => allPowers.ID == p));                
            }
            
            // character has 3 points to spend, some powers cost 1 point, some cost 2 points
            int points = 3;
            
            while (points > 0)
            {
                // all available powers bought, exit loop
                if (temp.Count == 0)
                {
                    break;
                }
                // removes power/s that causes infinite loop when purchasing               
                if (points == 2 && temp.Count > 1 && temp.Count(a => a.Cost == 1) == 1) 
                {
                    temp.RemoveAll(a => a.Cost == 1);
                }                
                // adds one random power from available list (then deletes it from available list)
                int rnd = Dice.DiceRoller(temp.Count) -1;               
                if (temp[rnd].Cost <= points)
                {                    
                    powers.Add(temp[rnd]);
                    points -= temp[rnd].Cost;
                    temp.Remove(temp[rnd]);                    
                }
            }            

            powers = powers.OrderBy(a => a.Name).ToList();
            return powers;            
        }
    }
}