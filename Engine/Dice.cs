using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Engine
{
    public class Dice
    {
        // random number generator
        private static readonly RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

        // method for rolling 1 die (d6, d10, d20 etc..)
        public static int DiceRoller(int die)
        {
            byte[] random = new byte[1];
            generator.GetBytes(random);
            double asciiValue = Convert.ToDouble(random[0]);
            double rounded = Math.Max(0, (asciiValue / 255d) - 0.00000000001d);
            double result = Math.Floor(rounded * die) + 1;
            return (int)(result);
        }       

        // flips a coin: 1 is true, 2 is false
        public static Boolean FlipCoin()
        {
            int coin = DiceRoller(2);
            if (coin == 1)
            {
                return true;
            }
            return false;
        }

        public static Dictionary<string, int> RollStats(bool isPhysical, Dictionary<string, int> stats)
        {
            // stats are split in 2 groups: physical (strength, agility, stamina) and mental (intelligence, willpower, charisma)
            // one group gets 6 points to spend (primary), the other group gets 4 points (secondary)
            int[] primaryGroup = new int[3];
            int[] secondaryGroup = new int[3];            
            primaryGroup = AssignPoints(6, primaryGroup);
            secondaryGroup = AssignPoints(4, secondaryGroup);

            // flips coin to decide if physical or mental is the primary group
            // each stat starts out with 1 point, then assigned the rest from groups
            if (isPhysical == true)
            {
                stats["Strength"] = primaryGroup[0] + 1;
                stats["Agility"] = primaryGroup[1] + 1;
                stats["Stamina"] = primaryGroup[2] + 1;
                stats["Intelligence"] = secondaryGroup[0] + 1;
                stats["Willpower"] = secondaryGroup[1] + 1;
                stats["Charisma"] = secondaryGroup[2] + 1;
            }
            else
            {
                stats["Strength"] = secondaryGroup[0] + 1;
                stats["Agility"] = secondaryGroup[1] + 1;
                stats["Stamina"] = secondaryGroup[2] + 1;
                stats["Intelligence"] = primaryGroup[0] + 1;
                stats["Willpower"] = primaryGroup[1] + 1;
                stats["Charisma"] = primaryGroup[2] + 1;
            }
            return stats;
        }

        // randomly fills 3 attributes in an array with the points alloted
        public static int[] AssignPoints(int points, int[] array)
        {
            for (int i = 0; i < points; i++)
            {
                int chosenStat = DiceRoller(3) - 1;
                // no more than 3 points to a stat
                if (array[chosenStat] < 3)
                {
                    array[chosenStat]++;
                }
                else
                {
                    i--;
                }
            }

            return array;
        }
        
    }
}
