# MutantGenerator

WinForms program (runs on Windows) that randomly generates characters for a role-playing game. When program runs it rolls a human character and displays it to the GUI. User has the option to roll a new human character, mutate the existing character, save the character, load a saved character, or print a screen capture of the GUI.

![mutantGenerator.PNG](https://gamblepants.github.io/img/mutantGenerator.PNG)

## Purpose

As a tool for role-playing games. Designed for game-masters to roll a new character on the fly.

## How to run program

Go to the [Release](https://github.com/gamblePants/MutantGenerator/releases) page, download the "Release.zip" folder to your computer and extract it. Inside the folder double-click on the MutantGenerator.exe file to run (runs on windows only).

You might get a Windows Defender warning the first time running the program: "Windows protected your PC". Click on "More info" then "Run anyway" (or just "Run anyway").

## Functionality

- running the program randomly generates a human
- roll a new random character by hitting the Roll Human button
- or mutate the existing human character by hitting the Mutate button (can't mutate same human more than once)
- save either human or mutant by hitting the Save button (saves XML file to local computer)
- load up a saved character by hitting the Load button
- print GUI by hitting the Print button (looks a little ugly at this stage, but seems to work)

## Game system

Game system is a custom set of rules I'm working on based on d10 dice (rules not provided). The same logic can be applied to other RPG systems. 

## Program Structure

There are 2 projects within the Visual Studio solution: Engine and MutantGenerator. The Display.cs class in the MutantGenerator project is where the program runs from. It accesses classes from the Engine project by using the Engine namespace at the top, and displays to the WinForms GUI.

Most of the classes are for creating the character structure and properties; one class for the character's animal type, one class for the character's background, one class for the character's story etc. 

Most of the classes also hold lists of data in them, such as the Skills.cs class; which contains the entire list of skills (called allSkills) and the methods to access the data.

## Code snippets

### List of gifts from the Gifts.cs class 

```C#
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
                new Gifts(09, "Leprechaun", "The Yellow Sign", 1)
            };
```

The above list is shortened for this example. There are 60 gifts in total. Each Gift object has an ID (int), Name (string), Path that it belongs to (string) and how much it costs to purchase during character creation - Cost (int). The allGifts list is accessed by the AssignGifts() method below.

### AssignGifts() method from the Gifts.cs class

```C#
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
```

In the above method a new temporary list of gifts is created from allGifts (based on the character's path). The temporary list has 6 gifts the character can purchase during character creation. The gifts cost either 1 or 2 points and the character has 3 points to spend. 

int rnd is a random number between 1 and the number of remaining gifts left in the temporary list. If the character has enough points left to purchase the randomly selected gift it is added to their list of gifts, and removed from the temporary list (cannot get the same gift twice).

When all 3 points are spent the list of gifts is ordered alphabetically and the method returns it.
