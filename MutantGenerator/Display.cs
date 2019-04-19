using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Engine;
using System.IO;

namespace MutantGenerator
{
    public partial class Display : Form
    {
        // stores the character and print document in the display class
        private Character _character;      
        private PrintDocument printDocument1 = new PrintDocument();

        public Display()
        {
            InitializeComponent();
            Generator();
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {            
            Generator();
        }

        private void btnMutate_Click(object sender, EventArgs e)
        {
            Mutate();
        }        

        // beginning of program
        private void Generator()
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);


            btnMutate.Enabled = true;
            ClearForm();            

            // refreshes lists            
            Animal.NewAnimals();
            Powers.NewPowers();
            Paths.NewPaths();
            Gifts.NewGifts();
            Background.NewBackgrounds();
            Skills.NewSkills();
            Story.NewStories();

            // creates new blank character
            _character = Character.CreateDefaultCharacter();            

            // roll stats, background, skills and story
            _character.IsPhysical = Dice.FlipCoin();
            _character.Stats = Dice.RollStats(_character.IsPhysical, _character.Stats);
            _character.Sanity = _character.Stats["Intelligence"] + _character.Stats["Willpower"];
            _character.Background = Background.ChooseBackground(_character.IsPhysical, _character.Background);
            _character.Skills = Skills.AssignSkills(_character.Background.Skills, _character.Skills, _character.Background.SkillsLimit);
            _character.Story = Story.ChooseStory(_character.Background.ID); 
            
            DisplayHuman();
        }
        private void DisplayStats()
        {
            lblStr.Text = "Strength: " + _character.Stats["Strength"].ToString();
            lblAgi.Text = "Agility: " + _character.Stats["Agility"].ToString();
            lblSta.Text = "Stamina: " + _character.Stats["Stamina"].ToString();
            lblInt.Text = "Intelligence: " + _character.Stats["Intelligence"].ToString();
            lblWil.Text = "Willpower: " + _character.Stats["Willpower"].ToString();
            lblCha.Text = "Charisma " + _character.Stats["Charisma"].ToString();
            lblInjuries.Text = "Injuries: 0 of " + _character.Injuries.ToString();
            lblSanity.Text = "Sanity: " + _character.Sanity.ToString();
            lblLuck.Text = "Luck: " + _character.Luck.ToString();
        }
        public void DisplayHuman()
        {
            DisplayStats();
            lblBackground.Text = "Background: " + _character.Background.Name;
            lblStory.Text = _character.Story.Desc;
            lblSkills.Text = "Skills:\n\n";
            foreach (Skills s in _character.Skills)
            {
                lblSkills.Text += s.Name + " " + s.Level.ToString() + "\n";
            }
        }

        private void Mutate()
        {
            btnMutate.Enabled = false;
            _character.Mutated = true;

            // roll path, animal, powers and gifts
            _character.Paths = Paths.ChoosePath(_character.Story.ID);
            _character.Stats[_character.Paths.Modifier]++;
            _character.Sanity = _character.Stats["Intelligence"] + _character.Stats["Willpower"] -1;            
            _character.Luck++;
            _character.Animal = Animal.ChooseAnimal(_character.Paths.ID);
            _character.Powers = Powers.AssignPowers(_character.Animal.Powers, _character.Powers);
            _character.Gifts = Gifts.AssignGifts(_character.Paths.Name);            
            if (_character.Powers.Exists(a => a.ID == 28 || a.ID == 38 || a.ID == 47 || a.ID == 48))
            {
                _character.Injuries++;
            }

            DisplayMutant();           
        }

        public void DisplayMutant()
        {
            DisplayStats();
            lblAnimal.Text = "Animal: " + _character.Animal.Name;
            lblPath.Text = "Path: " + _character.Paths.Name;
            lblPowers.Text = "Powers:\n\n";
            foreach (Powers p in _character.Powers)
            {
                lblPowers.Text += p.Name + "\n";
            }
            lblGifts.Text = "Gifts:\n\n";
            foreach (Gifts g in _character.Gifts)
            {
                lblGifts.Text += g.Name + "\n";
            }
            lblAnimal.Visible = true;
            lblPath.Visible = true;
            lblPowers.Visible = true;
            lblGifts.Visible = true;
        }
       
        // resets form to blank state
        public void ClearForm()
        {                   
            lblBackground.Text = "Background:";
            lblSkills.Text = "Skills:";
            lblStory.Text = "";
            lblAnimal.Visible = false;
            lblPath.Visible = false;
            lblPowers.Visible = false;
            lblGifts.Visible = false;
        }        

        private void btnSave_Click(object sender, EventArgs e)
        {            
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save Character as XML File";
            save.Filter = "XML Files (*.xml)|*.xml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.Write(_character.ToXmlString());
                write.Dispose();
            }
        }

        private void DisplayDefault()
        {
            lblInjuries.Text = "Injuries: 0 of " + _character.Injuries.ToString();
            lblLuck.Text = "Luck: " + _character.Luck.ToString();
            lblSanity.Text = "Sanity: 0";
            lblStr.Text = "Strength: 0";
            lblAgi.Text = "Agility: 0";
            lblSta.Text = "Stamina: 0";
            lblInt.Text = "Intelligence: 0";
            lblWil.Text = "Willpower: 0";
            lblCha.Text = "Charisma: 0";
            ClearForm();
            btnMutate.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
            load.Title = "Load Character XML file";
            load.Filter = "XML Files (*.xml)|*.xml";

            if(load.ShowDialog() == DialogResult.OK)
            {
                StreamReader read = new StreamReader(File.OpenRead(load.FileName));
                _character = Character.LoadFromXml(read.ReadToEnd());
            }

            if (_character.Background.Name == "Default")
            {
                DisplayDefault();
            }
            else
            {
                if (_character.Mutated == true)
                {
                    btnMutate.Enabled = false;
                    DisplayMutant();
                }
                else
                {
                    ClearForm();
                }

                DisplayHuman();
            }
          
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            printDocument1.Print();
            // may throw exception if no permission to print or no printer installed
        }

        Bitmap memoryImage;

        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender,
           System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
    }
}
