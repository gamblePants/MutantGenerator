using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Path { get; set; }
        public List<int> Powers { get; set; }

        public static List<Animal> animalList;
        public static List<Animal> animalChoice;


        // miscellaneous constructor
        public Animal()
        {
        }

        // main constructor
        public Animal(int id, string name, int path, List<int> powers)
        {
            ID = id;
            Name = name;           
            Path = path;
            Powers = powers;
        }
        
        // creates list of animals
        public static void NewAnimals()
        {
            animalList = new List<Animal>()
            {
                new Animal(1, "Antechinus", 4, new List<int>{1, 2, 3, 4, 5}),
                new Animal(2, "Bee", 4, new List<int>{6, 7, 8, 9}),
                new Animal(3, "Bluebottle", 3, new List<int>{10, 54, 15, 56}),
                new Animal(4, "Blue-ringed Octopus",1, new List<int>{10, 11, 12, 13, 14, 15, 56}),
                new Animal(5, "Cat", 10, new List<int>{49, 2, 4, 9, 16}),
                new Animal(6, "Cicada", 2, new List<int>{6, 18, 4, 49}),
                new Animal(7, "Cockatoo", 2, new List<int>{19, 20, 21}),
                new Animal(8, "Cockroach", 9, new List<int>{22, 6, 49, 16, 9}),
                new Animal(9, "Crucifix Toad", 9, new List<int>{3, 24, 23}),
                new Animal(10, "Dog", 2, new List<int>{25, 26, 9, 4, 16}),
                new Animal(11, "Elephant", 7, new List<int>{27, 28, 29, 4}),
                new Animal(12, "Fly", 5, new List<int>{30, 6, 31, 32, 9}),
                new Animal(13, "Funnel-web", 6, new List<int>{11, 44, 3, 34}),
                new Animal(14, "Ghost Bat", 9, new List<int>{20, 36, 49, 4}),
                new Animal(15, "Goanna", 5, new List<int>{37, 38, 39}),
                new Animal(16, "Goat", 4, new List<int>{40, 41}),
                new Animal(17, "Golden Orb Weaver", 6, new List<int>{41, 42, 33, 44,}),                
                new Animal(18, "Ibis", 5, new List<int>{20, 21}),
                new Animal(19, "Leech", 7, new List<int>{28, 43, 44, 50, 9, 15}),                
                new Animal(20, "Magpie", 10, new List<int>{19, 20, 21,}),
                new Animal(21, "Marsupial Mole", 8, new List<int>{3, 9, 1, 44}),
                new Animal(22, "Meat Ant", 8, new List<int>{45, 46, 48, 9, 44, 41}),
                new Animal(23, "Millipede", 1, new List<int>{46, 47, 44}),
                new Animal(24, "Mosquito", 7, new List<int>{50, 43, 6, 9}),
                new Animal(25, "Rat", 8, new List<int>{2, 3, 21, 4, 9, 16}),
                new Animal(26, "Red-bellied Black", 10, new List<int>{52, 50, 51, 44, 9}),
                new Animal(27, "Stonefish", 3, new List<int>{53, 10, 14, 56}),
                new Animal(28, "White-tail", 6, new List<int>{57, 44, 35, }),
                new Animal(29, "Wobbegong", 3, new List<int>{26, 10, 9, 8, 56}),
                new Animal(30, "Worm", 1, new List<int>{3, 44, 15, 55})
            };
        }

        // creates a list of all animals belonging to that path and randomly returns one
        public static Animal ChooseAnimal(int path)
        {
            List<Animal> animalChoice = animalList.Where(a => a.Path == path).ToList();
            return animalChoice[Dice.DiceRoller(3) - 1];            
        }
    }
}