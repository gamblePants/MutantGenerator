using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Story
    {
        public int ID;
        public string Desc;
        public int[] AvailBackgrounds;  //story restricted to certain backgrounds
        public string[] AvailPaths; // paths restricted by story

        public static List<Story> stories;
        public static List<Story> availStories;

        // constructors
        public Story()
        {
        }

        public Story(int id, string desc, string[] availPaths)
        {
            ID = id;
            Desc = desc;
            AvailBackgrounds = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            AvailPaths = availPaths;
        }

        public Story(int id, string desc, int[] availBackgrounds, string[] availPaths)
        {
            ID = id;
            Desc = desc;
            AvailBackgrounds = availBackgrounds;
            AvailPaths = availPaths;
        }

        // create list of stories
        public static void NewStories()
        {
            stories = new List<Story>()
            {
                new Story(1, "A friend from the past has commited suicide. The funeral draws back old faces and memories", new string[]{"The Yellow Sign","Crawling Chaos"}),
                new Story(2, "A young student has a strange experience while competing in a chess tournament at university.", new int[]{2,3,4,8,12,14,15,16,18,20}, new string[]{"The Silver Key"}),
                new Story(3, "Police are called to a private laboratory, where a doctor has been visciously murdered.", new int[]{1,2,3,14,15,16,13,19}, new string[]{"The Black Goat","The Unclean"}),
                new Story(4, "While digging up boulders in the field, farmers unearth a huge stone with strange carvings.", new int[]{1,3,5,6,7,9,10,12,15,16,17,21}, new string[]{"The Black Goat", "Sleepers of N'Kai","Chaugnar Faugn"}),
                new Story(5, "Four men are charged with murder. As their lawyer searches for answers, he finds more questions.", new int[]{1,4,9,10,12,13,14,15,16,19}, new string[]{"Crawling Chaos", "Sleepers of N'Kai", "Chaugnar Faugn"}),
                new Story(6, "A professor's family is threatened in an attempt to enlist her services.", new int[]{1,3,8,12,14,15,16,21}, new string[]{"Chaugnar Faugn", "Daughters of Atlach-nacha", "The Silver Key"}),
                new Story(7, "Two friends are fixing up a property in the country. One of them wakes up in the hills with no memory of the previous night.", new string[]{"The Burrowers", "Chaugnar Faugn"}),
                new Story(8, "In the outback a group of prospectors are working a claim. One morning they discover tracks suggesting trespassers.", new int[]{3,5,6,7,11,13,14,17,18,20,21}, new string[]{"The Burrowers","Sleepers of N'Kai"}),
                new Story(9, "A letter is received from an old aquaintance, who begs for help.", new string[]{"The Unclean", "Crawling Chaos", "The Silver Key"}),
                new Story(10, "A composer who struggles with anxiety and PTSD, starts to hear things that don't make sense.", new int[]{2,4,10,12,16,20}, new string[]{"The Yellow Sign", "The Silver Key"}),
                new Story(11, "An ex-soldier is hired as part of a salvage team for a shipwreck.", new int[]{1,2,3,6,7,8,11,13,14,16,18,19,20}, new string[]{"Deep Ones"}),
                new Story(12, "In the desert a train breaks down, and passengers seek shelter in the few buildings nearby.", new string[]{"The Burrowers", "Sleepers of N'Kai", "The Unclean"}),
                new Story(13, "A company has lost touch with one of their researchers. They send a couple of employees to investigate.", new int[]{1,2,3,8,11,14,16,18,19,20,21}, new string[]{"Daughters of Atlach-nacha"}),
                new Story(14, "Play rehearsals are beset by a series of strange events.", new int[]{4,7,12,13,14,15,16,20}, new string[]{"The Yellow Sign", "Crawling Chaos"}),
                new Story(15, "In a small town near a ski resort, people report sightings of a strange figure at night time.", new int[]{1,2,4,5,6,9,11,13,14,15,17,20}, new string[]{"The Unclean", "Chaugnar Faugn"}),
                new Story(16, "Nurses working in a remote community begin to see unexplained cases of infection.", new int[]{1,2,3,5,6,9,11,13,17,21}, new string[]{"The Unclean"}),
                new Story(17, "An engineer's friend tries to convince him to help rob a house.", new int[]{6,7,9,11,13,14,18,19,20}, new string[]{"The Silver Key", "Crawling Chaos"}),
                new Story(18, "A street performer meets a charismatic man who introduces her to a clandestine group of artists.", new int[]{4,8,9,10,12,14,15,16,19,20}, new string[]{"The Yellow Sign", "The Black Goat", "Daughters of Atlach-nacha"}),
                new Story(19, "After suffering a nervous breakdown, a man sends himself to a clinic to find stability and peace.", new string[]{"The Yellow Sign"}),
                new Story(20, "While on vacation with her boyfriend, a young woman goes sleep walking and wakes up on the beach.", new string[]{"Deep Ones"}),
                new Story(21, "A man follows his brother to a kink party. He meets a masked woman whose body contorts in unnatural ways.", new string[]{"The Black Goat"}),
                new Story(22, "A teenager brings home a bone necklace from a construction site, and starts to hear things.", new int[]{1,2,5,6,7,8,9,12,13,14,15,18,19}, new string[]{"The Unclean", "Sleepers of N'Kai","The Black Goat" }),
                new Story(23, "A small group of friends from university are invited to their lecturer's holiday home on the coast.", new int[]{3,4,5,8,9,12,14,16}, new string[]{"Deep Ones", "The Black Goat"}),
                new Story(24, "On a mountain climbing expedition, one of the group makes a bad decision.", new string[]{"The Burrowers", "Sleepers of N'Kai"}),
                new Story(25, "Two neighbours join their local knitting group, and meet a woman who claims she can talk to spiders.", new int[]{1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,20,21}, new string[]{"Daughters of Atlach-nacha"}),
                new Story(26, "In the country a group of parents wait for their kids to return from a scout camp.", new int[]{1,2,3,4,5,6,7,8,9,11,12,14,15,16,17,18,20,21}, new string[]{"Crawling Chaos", "Sleepers of N'Kai", "Chaugnar Faugn" }),
                new Story(27, "Residents in a nursing home are plagued by bad dreams.", new string[]{"Chaugnar Faugn", "Crawling Chaos"}),
                new Story(28, "Under the city construction workers dig vast tunnels. One of the workers goes missing.", new int[]{1,2,3,6,7,9,11,14,15,18,19,20}, new string[]{ "Sleepers of N'Kai", "The Burrowers"}),
                new Story(29, "The owner of a small plane is offered $50000 to smuggle cargo from Tasmania.", new int[]{1,2,5,6,7,9,11,13,14,18,19,20,21}, new string[]{"The Silver Key", "Daughters of Atlach-nacha", "Deep Ones" }),
                new Story(30, "A family out spotlighting have their animals taken by something in the night.", new int[]{1,2,3,4,5,6,7,8,9,11,12,14,15,16,17,18,20,21 }, new string[]{"The Burrowers"}),
            };
        }

        public static Story Get (int id)
        {
            return stories.FirstOrDefault(a => a.ID == id);
        }

        public static Story ChooseStory(int background)
        {
            Story story = new Story();
            availStories = new List<Story>();

            for (int i = 0; i < stories.Count(); i++)
            {
                if(Array.Exists(stories[i].AvailBackgrounds, element => element == background))
                {
                    availStories.Add(stories[i]);
                }
            }
            
            int rnd = Dice.DiceRoller(availStories.Count()) -1;
            story = availStories[rnd];

            return story;
        }
    }
}
