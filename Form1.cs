using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Media;
using System.Speech.Synthesis;

//GLOVE CONTROL SOFTWARE V1.0
//THE PENNSYLVANIA STATE UNIVERSITY

//Need to do:
/*
 * 
 */

namespace WristbandCsharp
{
    public partial class Form1 : Form
    {

        int intensityPercent = 50; //actually, the duration
        int durationPercent = 50; //actually, the intensity

        string right = "right.wav";
        string left = "left.wav";
        string up = "800hz_UP.wav";
        string down = "150hz_DOWN.wav";
        string found = "ITEM_FOUND.wav";

        int camera = 0; //changed temporarily to -1 from internet source. Was originally 1.

        Capture cap;
        Image<Bgr, Byte> image;
        Arduino arduino = null;

        //initialize vocal processing
        SpeechSynthesizer reader = new SpeechSynthesizer(); 

        //Declaring procedure array for random selection
        //string[] itemlists = { "item1Retreive, item2Retrieve, item3Retrieve, item4Retreive, item5Retreive" };

        //List<string> monkeyButt = new List<string>();
       // monkeyButt.Add("item1");

        //set up procedure calls
        List<string> itemArray = new List<string>() { "item1Retrieve", "item2Retrieve", "item3Retrieve", "item4Retreive", "item5Retreive" };

        //Publicly declare buttons below so they are global.
        Button FF_Intro = new Button();
        Button FF_Alternate = new Button();
        Button FF_Switch = new Button();
        Button FF_Stay = new Button();
        Button FF_ShelfScan = new Button();


        Button SBBQ_Intro = new Button();
        Button SBBQ_ShelfScan = new Button();

        Button SoupIntro = new Button();
        Button Soup2for1 = new Button();
        Button SoupGet2 = new Button();
        Button SoupGet1 = new Button();
        Button SoupShelfScan1 = new Button();
        Button SoupDepth = new Button();
        Button SoupShelfScan2 = new Button();

        Button RedHotIntro = new Button();
        Button RedHotShelfScan = new Button();

        Button HeinzIntro = new Button();
        Button HeinzShelfScan = new Button();

        Button FINISHED = new Button();

        int globalRandomRemaining = 5;
        int lastProcedureIndex = -1;

        //Now set true/false flags for whether or not an item was used.
        bool item1 = false;
        bool item2 = false;
        bool item3 = false;
        bool item4 = false;
        bool item5 = false;

        Random rnd = new Random(); //genereate new random object for selecting from the array


        public Form1()
        {

            this.KeyPreview = true;

            InitializeComponent();

            RefreshSerialPortList();


            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            cap = new Capture(camera);

            this.KeyDown += new KeyEventHandler(Form1_OnKeyDown); //fixed keyPress problems

            this.KeyUp += new KeyEventHandler(Form1_OnKeyUp); //added to stop the motors


            // 896x504
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 504.0);
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 896.0);
            // 928x522
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 522.0);
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 928.0);
            //// 1024x576
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 576.0);
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1024.0);
            // 1664x936
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 936.0);
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1664.0);
            // 1152x648
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 648.0);
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1152.0);
            // 1920x1080
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 1080.0);
            //cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1920.0);

            Application.Idle += new EventHandler(ShowFromCam);

            //Load up the old script as a demo
            string curDir = Directory.GetCurrentDirectory();
            //webBrowser2.Navigate(new Uri(String.Format("file:///{0}/LabStudyProcedure.html", curDir))); //used to be used. Phased out for natural voice processing.

            //Begin audio capture here
            

        }


        void ShowFromCam(object sender, EventArgs e)
        {

            // Get image.
            image = null;
            while (image == null) image = cap.QueryFrame();
            Image<Bgr, Byte> returnimage = image;

            
            pictureBox1.Image = returnimage.ToBitmap();

        }

        private void RefreshSerialPortList()
        {
            // Combo box 2 - Arduino selection
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (arduino!= null)
            {
                arduino.SendPacket(0, intensityPercent, durationPercent);
            }

            if (TonalFeedback.Checked)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(left);
                player.Play();
            }

            if (SpokenFeedback.Checked)
            {
                //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\left-Spoken.wav");
                //playerVoice.Play();
                //Removed above to swap for native speech
                reader.SpeakAsync("Left");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(3, intensityPercent, durationPercent);
            }

            if (TonalFeedback.Checked)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(down);
                player.Play();
            }

            if (SpokenFeedback.Checked)
            {
                //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\right-Spoken.wav");
                //playerVoice.Play();
                reader.SpeakAsync("Right");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(2, intensityPercent, durationPercent);
            }

            if (TonalFeedback.Checked)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(down);
                player.Play();
            }

            if (SpokenFeedback.Checked)
            {
                //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\down-Spoken.wav");
                //playerVoice.Play();
                reader.SpeakAsync("Down");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(1, intensityPercent, durationPercent);
            }

            if (TonalFeedback.Checked)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(up);
                player.Play();
            }

            if (SpokenFeedback.Checked)
            {
                //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\up-Spoken.wav");
                //playerVoice.Play();
                reader.SpeakAsync("Up");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(5, intensityPercent, durationPercent);
            }

            if (TonalFeedback.Checked)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(found);
                player.Play();
            }

            if (SpokenFeedback.Checked)
            {
                reader.SpeakAsync("Item found. Begin to move your hand forward");
                //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\found-Spoken.wav");
                //playerVoice.Play();
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void Form1_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (arduino != null)
            {
                arduino.SendPacket(0, 0, 0);
            }
        }
        private void Form1_OnKeyDown(object sender, KeyEventArgs e) //added this in, changed from protected to private
        {
            //base.OnKeyDown(e); //removed for v2
            //char input = char.Parse(e.KeyCode);
            switch (e.KeyCode) //changed to keycode from KeyChar
            {
                case (Keys.A): //a
                    if (arduino != null)
                    {
                        arduino.SendPacket(0, intensityPercent, durationPercent);
                        
                    }
                    break;
                case (Keys.D):  //d
                    if (arduino != null)
                    {
                        arduino.SendPacket(2, intensityPercent, durationPercent);
                    }
                    break;
                case (Keys.W): //w
                    if (arduino != null)
                    {
                        arduino.SendPacket(3, intensityPercent, durationPercent);
                    }
                    break;
                case (Keys.S): //s
                    if (arduino != null)
                    {
                        arduino.SendPacket(1, intensityPercent, durationPercent);
                    }
                    break;

                case (Keys.E): //e
                    if (arduino != null)
                    {
                        arduino.SendPacket(5, intensityPercent, durationPercent);
                    }
                    break;
            }

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void intensitySlider_Scroll(object sender, EventArgs e)
        {
            intensityPercent = this.intensitySlider.TabIndex * 10;
            intensityLabel.Text = "" + this.intensitySlider.TabIndex * 10; ;
        }

        private void intensitySlider_ValueChanged(object sender, EventArgs e)
        {
            intensityPercent = intensitySlider.Value * 10; //uses the slider to change vibration strength
            intensityLabel.Text = (intensitySlider.Value * 10).ToString();
        }

        private void vibrationSlider_Scroll(object sender, EventArgs e)
        {
            durationPercent = this.vibrationSlider.TabIndex * 10;
            //Can put in text later if we need to.
            vibLabel.Text = "" + this.vibrationSlider.TabIndex * 10;
        }

        private void vibrationSlider_ValueChanged(object sender, EventArgs e)
        {
            durationPercent = vibrationSlider.Value * 10;
            vibLabel.Text = (vibrationSlider.Value * 10).ToString();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            button9.Text = "It works";
            //reader.SpeakAsync("This sound recognition works fairly well. I wonder how it handles with more complex sentences.");
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            reader.SpeakAsync("Hello, and welcome to the Alex personal shopping assistant! I will be helping you find and get some items off of some store shelves today. To assist us with this job, I have a pre-programmed list of a few items we’ll be picking up. If you have any questions for me at any time, just say ‘Hey, Alex’. Why don’t you try saying that now just to make sure I can hear you?");
            //Introduction  - muted temporarily
            //checkAudio(); //remove temporarily. PUT IT BACK ON
            randomSelection();

        }

        private void label5_Click_2(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item that’s on the shopping list is a box of Frosted Flakes. One of the lab assistants can help you reach that location in our mini grocery store. Once we’re at the Frosted Flakes, we can try to find the item on the shelf.");
            //Speech result for hearing subject in introduction
            //I Heard You
        }

        private void button11_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I'm having some trouble hearing you. Let's try one more time. If you can hear me, say 'Hey, Alex'.");
            //Speech result for not hearing subject.
            //I didn't hear you
        }

        private void button13_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("It seems like we are having technical difficulties. Let's pause here and get this fixed.");
            //Fail
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void FF_Intro_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on your list is a box of Frosted Flakes. A lab assistant can help you reach that area of the store now.");
            //Frosted Flakes intro
        }

        private void FF_Alternate_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, you’ve arrived at the cereal. It looks like there is a cheaper generic brand of Frosted Flakes available. Are you interested in getting those instead?");
            //Frosted Flakes alternate?
        }

        private void FF_Switch_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, we'll get the generic Frosted Flakes instead.");
            //Yes Switch
        }

        private void FF_Stay_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, we'll stick with the Frosted Flakes.");
            //No, keep frosted flakes
        }

        private void FF_ShelfScan_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Whenever you’re ready, just extend your hand in the direction of the shelf. For this item, we will be using the tone-based location method you practiced earlier. You can go ahead and start whenever you are ready.");
            //Begin first shelf scan
        }

        private void SBBQ_Intro_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is some Stubb’s Original BBQ Sauce. One of the lab assistants can help you get to that section of our shelves now.");
            //Item 2 - Stubbs BBQ Sauce
        }

        private void SBBQ_ShelfScan_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("This time, to find and retrieve this BBQ sauce, we will be using the spoken audio feedback you practiced earlier to find the sauce. We can begin whenever you’re ready by extending your hand towards the shelf.");
            //Begin 2nd shelf scan
        }

        private void SoupIntro_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is some Progressive Chicken Noodle Soup. One of the lab assistants can help you get to that section of our shelves now.");
            //Item 3 - Progressive Chicken Noodle Soup
        }

        private void Soup2for1_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok we are in front of the soups now. This time, to find and retrieve the soup, we will be using the haptic glove mixed with spoken feedback to help guide you. Before getting the soup, it looks like there is currently a 2-for-1 sale on this item. Would you like to get two cans of soup or one?");
            //2-for-1
        }

        private void SoupGet2_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, I'll add a second can of soup to the list.");
            //Get 2
        }

        private void SoupGet1_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, we'll just get the one can of soup.");
            //Get 1
        }

        private void SoupShelfScan1_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Whenever you're ready, just extend your hand to the shelf to begin the scan.");
            //Begin 3rd shelf scan
        }

        private void SoupDepth_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("When grabbing the second can of soup, please keep in mind that the other can may be back slightly further than the can you just grabbed.");
        }

        private void SoupShelfScan2_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, now let's grab the second can of soup. Whenever you're ready, just extend your hand to the shelf to begin the scan.");
            //Begin 3rd shelf scan, pt. 2
        }

        private void RedHotIntro_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on your list is some Frank's Red Hot. One of the lab assistants can help you get to that section of our shelves now.");
            //Item 4 - Frank's Red Hot
        }

        private void RedHotShelfScan_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("This time, to find and retrieve the Frank's Red Hot sauce, we will be using haptic feedback through the glove and tonal feedback to help guide you. We can begin whenever you’re ready by extending your hand towards the shelf.");
            //Begin 4th shelf scan
        }

        private void HeinzIntro_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on your list is Heinz Ketchup. One of the lab assistants can help you get to that section of our shelves now.");
            //Item 5 - Heinz Ketchup
        }

        private void HeinzShelfScan_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("To find and retrieve the Heinz Ketchup, we will be using haptic feedback through the glove to help guide you. We can begin whenever you're ready by extending your hand towards the shelf.");
            //Begin 5th shelf scan
        }

        private void FINISHED_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("That's it! You have successfully gotten all of the items on your shopping list. One of the lab assistants will now help you remove the gear and get you set up for debriefing.");
            //DONE!
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            reader.SpeakAsync("I'm sorry. I wasn't able to find the item on the shelf. Would you like to re-scan the shelf, or skip this item?");
            //Failed to find item on shelf scan.
        }

        private void button18_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, we'll skip this item and move onto the next one.");
            //Skip missing item
        }

        private void button35_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, let's scan the shelf again. Whenever you're ready, just extend your hand towards the shelf.");
            //Re-scan missing item
        }

        private void button36_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I'm sorry, I couldn't find it again. Let's just skip this item and move on to the next one.");
            //Failed to find twice
        }

        private void button37_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I have a built-in feature to attempt to verify retrieved items by scanning for barcodes after you've picked up an item. Would you like to enable this feature?");
            //First verify check
        }

        private void button38_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, I'll activate the barcode verification feature. I'll prompt you to scan for the barcode on your items after you pick each of them up.");
            //Yes, use verify feature
        }

        private void button39_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, we won't use this feature today.");
            groupBox4.BackColor = Color.Red;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            //placeholder for item verification box
        }

        private void button19_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I've sucessfully verified the item.");
            //Item verified
        }

        private void button40_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("When ready, rotate the item around while I attempt to locate the barcode");
            //begin to verify
        }

        private void button20_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I was unable to verify the item. Would you like to scan it again?");
            //Verify failed
        }

        private void button41_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("OK, rotate the item around again while I attempt to locate the barcode");
            //Re-Do Yes
        }

        private void button42_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Alright, let's just skip the verification on this item");
            //Re-Do No
        }

        private void button21_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Sorry. I wasn't able to find the barcode again. Let's just skip the verification on this item and move on to the next one.");
            //Verify failed twice
        }

        private void checkAudio()
        {

            //Create new buttons for options
            Button yesHeard = new Button();
            Button noHeard = new Button();


            //Yes, I heard you button
            yesHeard.Location = new Point(1500, 150);
            yesHeard.Height = 40;
            yesHeard.Width = 150;
            yesHeard.Name = "button12";
            yesHeard.Text = "Yes, I Heard You";
            yesHeard.Click += new EventHandler(button12_Click);

            //No, I didn't hear you button
            noHeard.Location = new Point(1300, 150);
            noHeard.Height = 40;
            noHeard.Width = 150;
            noHeard.Name = "button13";
            noHeard.Text = "Sorry, I Can't Hear";
            noHeard.Click += new EventHandler(button11_Click);

            Controls.Add(yesHeard);
            Controls.Add(noHeard);
        }

        private void randomSelection()
        {
            //Function used to pick the order of events. Creates new buttons, sets controls automatically.
            //string[] itemArray = new string[] {"item1Retreive, item2Retrieve, item3Retrieve, item4Retreive, item5Retreive"}; (placed this as global)

            
            if (globalRandomRemaining != 0)
            {

                int randomIndex = rnd.Next(5); //generates the random number

                //Need to check if the number has been used before. Otherwise, this will run forever. UGHHHHHHHH
                //Need to generate a number between 0 and 4. This part still works. Now will set flags of true/flase for whether each of them has been used. If used, repeat.
                //Get rid of global remaining reduction. 

                if (randomIndex == 0)
                {
                    if (item1 == false)
                    {
                        item1Retrieve();
                        //itemArray.Remove("item1Retreive");
                        item1 = true;
                        globalRandomRemaining--;
                    }
                    else { randomSelection(); }
                }
                else if (randomIndex == 1)
                { 
                    if (item2 == false)
                    {
                        item2Retrieve(); 
                        //itemArray.Remove("item2Retrieve");
                        item2 = true;
                        globalRandomRemaining--;
                    }
                    else { randomSelection(); }

                }
                else if (randomIndex == 2)
                {
                    if (item3 == false)
                    {
                        item3Retrieve();
                        //itemArray.Remove("item3Retrieve");
                        item3 = true;
                        globalRandomRemaining--;
                    }
                    else { randomSelection(); }
                }
                else if (randomIndex == 3)
                {
                    if (item4 == false)
                    {
                        item4Retrieve();
                        //itemArray.Remove("item4Retrieve");
                        item4 = true;
                        globalRandomRemaining--;
                    }
                    else { randomSelection(); }
                }
                else if (randomIndex == 4)
                {
                    if (item5 == false)
                    {
                        item5Retrieve();
                        //itemArray.Remove("item5Retrieve");
                        item5 = true;
                        globalRandomRemaining--;
                    }
                    else { randomSelection(); }
                }
                //Above if statements activate the appropriate function and then remove the item from the list.
                
            }
            else
            {
                FINISHED.Location = new Point(1400, 100);
                FINISHED.Height = 40;
                FINISHED.Width = 150;
                FINISHED.Name = "FINISHED";
                FINISHED.Text = "FINISHED";
                FINISHED.Click += new EventHandler(FINISHED_Click);
                Controls.Add(FINISHED);
            }

        }

        private void item1Retrieve()
        {
            //Frosted Flakes, tonal feedback
            lastProcedureIndex = 0;

            /*Button FF_Alternate = new Button();
            Button FF_Switch = new Button();
            Button FF_Stay = new Button();
            Button FF_ShelfScan = new Button();*/
            //Create all buttons

            FF_Intro.Location = new Point(1400, 100);
            FF_Intro.Height = 40;
            FF_Intro.Width = 150;
            FF_Intro.Name = "FF_Alternate";
            FF_Intro.Text = "Frosted Flakes Intro";
            FF_Intro.Click += new EventHandler(FF_Intro_Click);

            FF_Alternate.Location = new Point(1400, 150);
            FF_Alternate.Height = 40;
            FF_Alternate.Width = 150;
            FF_Alternate.Name = "FF_Alternate";
            FF_Alternate.Text = "Frosted Flakes alternate?";
            FF_Alternate.Click += new EventHandler(FF_Alternate_Click);

            FF_Switch.Location = new Point(1200, 200);
            FF_Switch.Height = 40;
            FF_Switch.Width = 150;
            FF_Switch.Name = "FF_Switch";
            FF_Switch.Text = "Yes, Switch?";
            FF_Switch.Click += new EventHandler(FF_Switch_Click);

            FF_Stay.Location = new Point(1600, 200);
            FF_Stay.Height = 40;
            FF_Stay.Width = 150;
            FF_Stay.Name = "FF_Stay";
            FF_Stay.Text = "No, Keep Frosted Flakes?";
            FF_Stay.Click += new EventHandler(FF_Stay_Click);

            FF_ShelfScan.Location = new Point(1400, 300);
            FF_ShelfScan.Height = 40;
            FF_ShelfScan.Width = 150;
            FF_ShelfScan.Name = "FF_ScanShelf";
            FF_ShelfScan.Text = "Begin 1st Shelf Scan";
            FF_ShelfScan.Click += new EventHandler(FF_ShelfScan_Click);

            //Then provide details for all buttons

            Controls.Add(FF_Intro);
            Controls.Add(FF_Alternate);
            Controls.Add(FF_Switch);
            Controls.Add(FF_Stay);
            Controls.Add(FF_ShelfScan);
            //Then place the buttons

            this.TonalFeedback.Checked = true;
            this.SpokenFeedback.Checked = false;
            if (arduino != null)
            {
                arduino.ClosePort();
                arduino = null;
                motorActive.Text = "Motor is NOT active";
                motorActive.ForeColor = Color.Red;
                comboBox2.Text = "OFF";
            }
            //setup control commands for glove automatically. Badass.
        }

        private void item2Retrieve()
        {
            lastProcedureIndex = 1; //this keeps track of which procedure is active/needs to be deleted when switched to next function.
            //Stubbs BBQ Sauce, spoken feedback

            /*Button SBBQ_Intro = new Button();
            Button SBBQ_ShelfScan = new Button();*/

            SBBQ_Intro.Location = new Point(1200, 200);
            SBBQ_Intro.Height = 40;
            SBBQ_Intro.Width = 150;
            SBBQ_Intro.Name = "SBBQ_Intro";
            SBBQ_Intro.Text = "Item 2 - Stubbs BBQ Sauce INTRO";
            SBBQ_Intro.Click += new EventHandler(SBBQ_Intro_Click);

            SBBQ_ShelfScan.Location = new Point(1600, 200);
            SBBQ_ShelfScan.Height = 40;
            SBBQ_ShelfScan.Width = 150;
            SBBQ_ShelfScan.Name = "SBBQ_ShelfScan";
            SBBQ_ShelfScan.Text = "Begin 2nd Shelf Scan";
            SBBQ_ShelfScan.Click += new EventHandler(SBBQ_ShelfScan_Click);

            Controls.Add(SBBQ_Intro);
            Controls.Add(SBBQ_ShelfScan);

            this.TonalFeedback.Checked = false;
            this.SpokenFeedback.Checked = true;
            if (arduino != null)
            {
                arduino.ClosePort();
                arduino = null;
                motorActive.Text = "Motor is NOT active";
                motorActive.ForeColor = Color.Red;
                comboBox2.Text = "OFF";
            } 
        }

        private void item3Retrieve()
        {
            lastProcedureIndex = 2;
            //Progressive Chicken Noodle Soup, spoken feedback, motors

            /*Button SoupIntro = new Button();
            Button Soup2for1 = new Button();
            Button SoupGet2 = new Button();
            Button SoupGet1 = new Button();
            Button SoupShelfScan1 = new Button();
            Button SoupShelfScan2 = new Button();*/

            SoupIntro.Location = new Point(1400, 150);
            SoupIntro.Height = 40;
            SoupIntro.Width = 150;
            SoupIntro.Name = "SoupIntro";
            SoupIntro.Text = "Item 3 - Progressive Chicken Noodle Soup";
            SoupIntro.Click += new EventHandler(SoupIntro_Click);

            Soup2for1.Location = new Point(1400, 200);
            Soup2for1.Height = 40;
            Soup2for1.Width = 150;
            Soup2for1.Name = "Soup2for1";
            Soup2for1.Text = "2 for 1?";
            Soup2for1.Click += new EventHandler(Soup2for1_Click);

            SoupGet2.Location = new Point(1300, 250);
            SoupGet2.Height = 40;
            SoupGet2.Width = 150;
            SoupGet2.Name = "SoupGet2";
            SoupGet2.Text = "Get 2";
            SoupGet2.Click += new EventHandler(SoupGet2_Click);

            SoupGet1.Location = new Point(1500, 250);
            SoupGet1.Height = 40;
            SoupGet1.Width = 150;
            SoupGet1.Name = "SoupGet1";
            SoupGet1.Text = "Get 1";
            SoupGet1.Click += new EventHandler(SoupGet1_Click);

            SoupShelfScan1.Location = new Point(1400, 300);
            SoupShelfScan1.Height = 40;
            SoupShelfScan1.Width = 150;
            SoupShelfScan1.Name = "SoupShelfScan1";
            SoupShelfScan1.Text = "Begin 3rd Shelf Scan";
            SoupShelfScan1.Click += new EventHandler(SoupShelfScan1_Click);

            SoupDepth.Location = new Point(1400, 350);
            SoupDepth.Height = 40;
            SoupDepth.Width = 300;
            SoupDepth.Name = "SoupDepth";
            SoupDepth.Text = "REMINDER: Soup may be behind other cans";
            SoupDepth.Click += new EventHandler(SoupDepth_Click);

            SoupShelfScan2.Location = new Point(1400, 400);
            SoupShelfScan2.Height = 40;
            SoupShelfScan2.Width = 150;
            SoupShelfScan2.Name = "SoupShelfScan2";
            SoupShelfScan2.Text = "Begin 3rd Shelf Scan, Pt. 2";
            SoupShelfScan2.Click += new EventHandler(SoupShelfScan2_Click);

            Controls.Add(SoupIntro);
            Controls.Add(Soup2for1);
            Controls.Add(SoupGet2);
            Controls.Add(SoupGet1);
            Controls.Add(SoupShelfScan1);
            Controls.Add(SoupDepth);
            Controls.Add(SoupShelfScan2);

            this.SpokenFeedback.Checked = true;
            if (arduino == null)
            {
                arduino = new Arduino((string)comboBox2.SelectedItem);
                motorActive.Text = "Motor is ACTIVE";
                motorActive.ForeColor = Color.Green;
                comboBox2.Text = "ON";
            }
            this.TonalFeedback.Checked = false;

        }

        private void item4Retrieve()
        {
            lastProcedureIndex = 3;
            //Frank's Red Hot. Tonal Feedback, motors
            /*Button RedHotIntro = new Button();
            Button RedHotShelfScan = new Button();*/

            RedHotIntro.Location = new Point(1200, 150);
            RedHotIntro.Height = 40;
            RedHotIntro.Width = 150;
            RedHotIntro.Name = "RedHotIntro";
            RedHotIntro.Text = "Item 4 - Frank's Red Hot";
            RedHotIntro.Click += new EventHandler(RedHotIntro_Click);

            RedHotShelfScan.Location = new Point(1600, 150);
            RedHotShelfScan.Height = 40;
            RedHotShelfScan.Width = 150;
            RedHotShelfScan.Name = "RedHotShelfScan";
            RedHotShelfScan.Text = "Begin 4th Shelf Scan";
            RedHotShelfScan.Click += new EventHandler(RedHotShelfScan_Click);

            Controls.Add(RedHotIntro);
            Controls.Add(RedHotShelfScan);

            this.SpokenFeedback.Checked = false;
            this.TonalFeedback.Checked = true;
            if (arduino == null)
            {
                arduino = new Arduino((string)comboBox2.SelectedItem);
                motorActive.Text = "Motor is ACTIVE";
                motorActive.ForeColor = Color.Green;
                comboBox2.Text = "ON";
            }
        }

        private void item5Retrieve()
        {
            lastProcedureIndex = 4;
            //Heinz Ketchup, Motors only

            /*Button HeinzIntro = new Button();
            Button HeinzShelfScan = new Button();*/


            HeinzIntro.Location = new Point(1200, 150);
            HeinzIntro.Height = 40;
            HeinzIntro.Width = 150;
            HeinzIntro.Name = "HeinzIntro";
            HeinzIntro.Text = "Item 5 - Heinz Ketchup";
            HeinzIntro.Click += new EventHandler(HeinzIntro_Click);

            HeinzShelfScan.Location = new Point(1600, 150);
            HeinzShelfScan.Height = 40;
            HeinzShelfScan.Width = 150;
            HeinzShelfScan.Name = "HeinzIntro";
            HeinzShelfScan.Text = "Begin 5th Shelf Scan";
            HeinzShelfScan.Click += new EventHandler(HeinzShelfScan_Click);


            Controls.Add(HeinzIntro);
            Controls.Add(HeinzShelfScan);

            this.TonalFeedback.Checked = false;
            this.SpokenFeedback.Checked = false;
            if (arduino == null)
            {
                arduino = new Arduino((string)comboBox2.SelectedItem);
                motorActive.Text = "Motor is ACTIVE";
                motorActive.ForeColor = Color.Green;
                comboBox2.Text = "ON";
            }
        }


        private void FAIL_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("It seems like we are having technical difficulties. Let's pause here and get this fixed.");
            //Fail
        }

        private void TEST_RANDOM_Click(object sender, EventArgs e)
        {
            //reader.SpeakAsync("OK, rotate the item around again while I attempt to locate the barcode");
            //Re-Do Yes

            //test random function
            randomSelection();
        }

        private void nextItem_Click(object sender, EventArgs e)
        {
            if (lastProcedureIndex == 0)
            {
                Controls.Remove(FF_Intro);
                Controls.Remove(FF_Alternate);
                Controls.Remove(FF_Switch);
                Controls.Remove(FF_Stay);
                Controls.Remove(FF_ShelfScan);
                //remove frosted flakes elements
            }
            else if (lastProcedureIndex == 1)
            {
                Controls.Remove(SBBQ_Intro);
                Controls.Remove(SBBQ_ShelfScan);
            }
            else if (lastProcedureIndex == 2)
            {
                Controls.Remove(SoupIntro);
                Controls.Remove(Soup2for1);
                Controls.Remove(SoupGet1);
                Controls.Remove(SoupGet2);
                Controls.Remove(SoupShelfScan1);
                Controls.Remove(SoupDepth);
                Controls.Remove(SoupShelfScan2);
            }
            else if (lastProcedureIndex == 3)
            {
                Controls.Remove(RedHotIntro);
                Controls.Remove(RedHotShelfScan);
            }
            else if (lastProcedureIndex == 4)
            {
                Controls.Remove(HeinzIntro);
                Controls.Remove(HeinzShelfScan);
            }
            randomSelection();

        }

        private void debugString_Click(object sender, EventArgs e)
        {

        }

        private void motorActive_Click(object sender, EventArgs e)
        {

        }

        private void CantHear_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I'm having some trouble hearing you. Try speaking a little louder. Just say 'Alex' to see if I can hear you.");
        }

        private void CanHear_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("OK, great, I can hear you clearly. Let's get started.");
        }

        private void WrongItem_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("You have grabbed the incorrect item. Would you like to try to grab the correct item again?");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, let's rescan the shelf. When ready, extend your hand towards the shelf.");
        }

        private void SkipResearch_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("OK, let's just skip this item and move on to the next one.");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("As you move through our store today, you can keep your arm resting at your side. Don't hold up your arm until I tell you to do so. For each item that we find, we will test a different style of retrieving items. Before attempting to get each item, we will walk through a brief demo about what you will be experiencing.");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("While grabbing each item, reach forward when directed to do so until you touch an item on the shelf. If the device does not give you any further corrects, go ahead and grab the item. If it offers directional assistance after touching an item, it means you are slightly off-target and the device will try to get you on track.");
        }

        private void button11_Click_2(object sender, EventArgs e)
        {
            reader.Speak("Before grabbing the next item, let's practice how you are going to grab the next food item from the shelf. This time, you will be using a tone based sound system to help you find items. I will now play the sound you will hear to move left.");
            button1.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move to the right");
            button2.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move up");
            button7.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move down");
            button3.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Finally, this is the sound you will hear to move your hand forward.");
            button8.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Would you like to hear these sounds again, or move on?");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, we'll do this again.");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, let's start");
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            reader.Speak("Before grabbing the next item, let's practice how you are going to grab the next food item from the shelf. This time, you will be using a spoken-word based sound system to help you find items. I will now play the sound you will hear to move left.");
            button1.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move to the right");
            button2.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move up");
            button7.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move down");
            button3.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Finally, this is the sound you will hear to move your hand forward.");
            button8.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Would you like to hear these sounds again, or move on?");
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            reader.Speak("Before grabbing the next item, let's practice how you are going to grab the next food item from the shelf. This time, you will be using a haptic or vibration based system to help you find items. I will now activate the motor you will feel to move left.");
            button1.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to move to the right");
            button2.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to move up");
            button7.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to move down");
            button3.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Finally, this is the vibration you will feel to move your hand forward.");
            button8.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Would you like to feel these motors again, or move on?");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            reader.Speak("Before grabbing the next item, let's practice how you are going to grab the next food item from the shelf. This time, you will be using a haptic or vibration based system as well as tonal feedback to help you find items. I will now activate the motor you will feel, and play the sound you will hear to move left.");
            button1.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration and sound you will feel to move to the right");
            button2.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration and sound you will feel to move up");
            button7.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration and sound you will feel to move down");
            button3.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Finally, this is the vibration and sound you will feel to move your hand forward.");
            button8.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Would you like to feel these motors and hear the sounds again, or move on?");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            reader.Speak("Before grabbing the next item, let's practice how you are going to grab the next food item from the shelf. This time, you will be using a haptic or vibration based system as well as speech feedback to help you find items. I will now activate the motor you will feel, and play the sound you will hear to move left.");
            button1.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration and sound you will feel to move to the right");
            button2.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration and sound you will feel to move up");
            button7.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Now the vibration and sound you will feel to move down");
            button3.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Finally, this is the vibration and sound you will feel to move your hand forward.");
            button8.PerformClick();
            Thread.Sleep(1500);
            reader.Speak("Would you like to feel these motors and hear the sounds again, or move on?");

        }
    }
}
