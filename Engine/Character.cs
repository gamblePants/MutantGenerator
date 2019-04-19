using System;
using System.Collections.Generic;
using System.Xml;

namespace Engine
{
    public class Character
    { 
        public bool IsPhysical { get; set; }
        public Dictionary<string, int> Stats { get; set; }
        public Paths Paths { get; set; }
        public Animal Animal { get; set; }
        public Background Background { get; set; }        
        public List<Powers> Powers { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Gifts> Gifts { get; set; }     
        public Story Story { get; set; }
        public int Injuries { get; set; }
        public int Sanity { get; set; }    
        public int Luck { get; set; }
        public bool Mutated { get; set; }


        // constructor       
        public Character(int injuries, int luck, bool mutated)
        {
            Injuries = injuries;
            Luck = luck;
            Mutated = mutated;
        }

        // create default character method
        public static Character CreateDefaultCharacter()
        {
            Character character = new Character(7, 3 + Dice.DiceRoller(4), false);
            character.Stats = new Dictionary<string, int>();
            character.Paths = new Paths();
            character.Animal = new Animal();
            character.Background = new Background();
            character.Background.Name = "Default";
            character.Powers = new List<Powers>();
            character.Skills = new List<Skills>();
            character.Gifts = new List<Gifts>();
            character.Story = new Story();
            return character;
        }

        // method to create character object from saved xml file
        public static Character LoadFromXml(string xmlFile)
        {
            try
            {
                XmlDocument characterData = new XmlDocument();
                characterData.LoadXml(xmlFile);

                Character character = Character.CreateDefaultCharacter();

                character.Injuries = Convert.ToInt32(characterData.SelectSingleNode("/Character/Injuries").InnerText);              
                character.Luck = Convert.ToInt32(characterData.SelectSingleNode("/Character/Luck").InnerText);
                character.Mutated = Convert.ToBoolean(characterData.SelectSingleNode("/Character/Mutated").InnerText);
                character.Sanity = Convert.ToInt32(characterData.SelectSingleNode("/Character/Sanity").InnerText);
                character.IsPhysical = Convert.ToBoolean(characterData.SelectSingleNode("/Character/IsPhysical").InnerText);

                character.Stats["Strength"] = Convert.ToInt32(characterData.SelectSingleNode("/Character/Stats/Strength").InnerText);
                character.Stats["Agility"] = Convert.ToInt32(characterData.SelectSingleNode("/Character/Stats/Agility").InnerText);
                character.Stats["Stamina"] = Convert.ToInt32(characterData.SelectSingleNode("/Character/Stats/Stamina").InnerText);
                character.Stats["Intelligence"] = Convert.ToInt32(characterData.SelectSingleNode("/Character/Stats/Intelligence").InnerText);
                character.Stats["Willpower"] = Convert.ToInt32(characterData.SelectSingleNode("/Character/Stats/Willpower").InnerText);
                character.Stats["Charisma"] = Convert.ToInt32(characterData.SelectSingleNode("/Character/Stats/Charisma").InnerText);

                character.Background.ID = Convert.ToInt32(characterData.SelectSingleNode("/Character/Background").Attributes["ID"].Value);
                character.Background.Name = characterData.SelectSingleNode("/Character/Background").Attributes["Name"].Value;
                character.Story.ID = Convert.ToInt32(characterData.SelectSingleNode("/Character/Story").Attributes["ID"].Value);
                character.Story.Desc = characterData.SelectSingleNode("/Character/Story").Attributes["Desc"].Value;

                foreach(XmlNode node in characterData.SelectNodes("/Character/Skills/Skill"))
                {
                    Skills skill = new Skills(node.Attributes["Name"].Value, Convert.ToInt32(node.Attributes["Level"].Value));
                    character.Skills.Add(skill);
                }

                if (character.Mutated == true)
                {
                    character.Paths.Name = characterData.SelectSingleNode("/Character/Path").InnerText;
                    character.Animal.Name = characterData.SelectSingleNode("/Character/Animal").InnerText;

                    foreach(XmlNode node in characterData.SelectNodes("/Character/Powers/Power"))
                    {
                        Powers power = new Powers() { Name = node.Attributes["Name"].Value };
                        character.Powers.Add(power);
                    }

                    foreach(XmlNode node in characterData.SelectNodes("/Character/Gifts/Gift"))
                    {
                        Gifts gift = new Gifts() { Name = node.Attributes["Name"].Value };
                        character.Gifts.Add(gift);
                    }
                }

                return character;
            }
            catch
            {
                // if there is an error with XML data, returns default character
                return Character.CreateDefaultCharacter();
            }
        }
                
        // saves character to xml 
        public string ToXmlString()
        {
            XmlDocument characterData = new XmlDocument();

            // create top level xml node
            XmlNode character = characterData.CreateElement("Character");
            characterData.AppendChild(character);

            // create stats child node
            XmlNode stats = characterData.CreateElement("Stats");            
            character.AppendChild(stats);

            // create child nodes for the stats node
            XmlNode strength = characterData.CreateElement("Strength");
            strength.AppendChild(characterData.CreateTextNode(this.Stats["Strength"].ToString()));
            stats.AppendChild(strength);
            XmlNode agility = characterData.CreateElement("Agility");
            agility.AppendChild(characterData.CreateTextNode(this.Stats["Agility"].ToString()));
            stats.AppendChild(agility);
            XmlNode stamina = characterData.CreateElement("Stamina");
            stamina.AppendChild(characterData.CreateTextNode(this.Stats["Stamina"].ToString()));
            stats.AppendChild(stamina);
            XmlNode intelligence = characterData.CreateElement("Intelligence");
            intelligence.AppendChild(characterData.CreateTextNode(this.Stats["Intelligence"].ToString()));
            stats.AppendChild(intelligence);
            XmlNode willpower = characterData.CreateElement("Willpower");
            willpower.AppendChild(characterData.CreateTextNode(this.Stats["Willpower"].ToString()));
            stats.AppendChild(willpower);
            XmlNode charisma = characterData.CreateElement("Charisma");
            charisma.AppendChild(characterData.CreateTextNode(this.Stats["Charisma"].ToString()));
            stats.AppendChild(charisma);           

            // create injuries, sanity, luck, mutated and isPhysical child nodes
            XmlNode injuries = characterData.CreateElement("Injuries");
            injuries.AppendChild(characterData.CreateTextNode(this.Injuries.ToString()));
            character.AppendChild(injuries);           
            XmlNode sanity = characterData.CreateElement("Sanity");
            sanity.AppendChild(characterData.CreateTextNode(this.Sanity.ToString()));
            character.AppendChild(sanity);           
            XmlNode luck = characterData.CreateElement("Luck");
            luck.AppendChild(characterData.CreateTextNode(this.Luck.ToString()));
            character.AppendChild(luck);           
            XmlNode mutated = characterData.CreateElement("Mutated");
            mutated.AppendChild(characterData.CreateTextNode(this.Mutated.ToString()));
            character.AppendChild(mutated);
            XmlNode isPhysical = characterData.CreateElement("IsPhysical");
            isPhysical.AppendChild(characterData.CreateTextNode(this.IsPhysical.ToString()));
            character.AppendChild(isPhysical);

            // create background node and attributes
            XmlNode background = characterData.CreateElement("Background");
            XmlAttribute idAttribute = characterData.CreateAttribute("ID");
            idAttribute.Value = this.Background.ID.ToString();
            background.Attributes.Append(idAttribute);
            XmlAttribute nameAttribute = characterData.CreateAttribute("Name");
            nameAttribute.Value = this.Background.Name;
            background.Attributes.Append(nameAttribute);
            character.AppendChild(background);

            // create story node and attributes
            XmlNode story = characterData.CreateElement("Story");
            XmlAttribute storyId = characterData.CreateAttribute("ID");
            storyId.Value = this.Story.ID.ToString();
            story.Attributes.Append(storyId);
            XmlAttribute storyDesc = characterData.CreateAttribute("Desc");
            storyDesc.Value = this.Story.Desc;
            story.Attributes.Append(storyDesc);
            character.AppendChild(story);

            // create skills node and node for each skill
            XmlNode skills = characterData.CreateElement("Skills");
            character.AppendChild(skills);
            foreach(Skills s in this.Skills)
            {
                XmlNode skill = characterData.CreateElement("Skill");
                XmlAttribute skillName = characterData.CreateAttribute("Name");
                skillName.Value = s.Name;
                skill.Attributes.Append(skillName);
                XmlAttribute skillLevel = characterData.CreateAttribute("Level");
                skillLevel.Value = s.Level.ToString();
                skill.Attributes.Append(skillLevel);
                skills.AppendChild(skill);
            }

            if(this.Mutated == true)
            {
                // save animal and path
                XmlNode animal = characterData.CreateElement("Animal");
                animal.AppendChild(characterData.CreateTextNode(this.Animal.Name));
                character.AppendChild(animal);
                XmlNode path = characterData.CreateElement("Path");
                path.AppendChild(characterData.CreateTextNode(this.Paths.Name));
                character.AppendChild(path);

                // create powers node and node for each power
                XmlNode powers = characterData.CreateElement("Powers");
                character.AppendChild(powers);
                foreach(Powers p in this.Powers)
                {
                    XmlNode power = characterData.CreateElement("Power");
                    XmlAttribute name = characterData.CreateAttribute("Name");
                    name.Value = p.Name;
                    power.Attributes.Append(name);
                    XmlAttribute cost = characterData.CreateAttribute("Cost");
                    cost.Value = p.Cost.ToString();
                    power.Attributes.Append(cost);
                    powers.AppendChild(power);
                }

                // create gifts node and node for each gift
                XmlNode gifts = characterData.CreateElement("Gifts");
                character.AppendChild(gifts);
                foreach (Gifts g in this.Gifts)
                {
                    XmlNode gift = characterData.CreateElement("Gift");
                    XmlAttribute name = characterData.CreateAttribute("Name");
                    name.Value = g.Name;
                    gift.Attributes.Append(name);
                    XmlAttribute cost = characterData.CreateAttribute("Cost");
                    cost.Value = g.Cost.ToString();
                    gift.Attributes.Append(cost);
                    gifts.AppendChild(gift);
                }
            }            

            return characterData.InnerXml;
        }

    }  
}
