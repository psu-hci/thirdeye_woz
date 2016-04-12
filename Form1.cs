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
        int proximity = 50;  // closeness of participant to object

        int timerCount = 0;
        int timerCountMax = 10;

        // add the sound files here so that they can be changed easier.
        //string left = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps\WristbandCSharp\WristbandCsharp\left.wav";
        //string down = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps\WristbandCSharp\WristbandCsharp\150hz_DOWN.wav";
        //string right = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps\WristbandCSharp\WristbandCsharp\right.wav";
        //string up = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps\WristbandCSharp\WristbandCsharp\800hz_UP.wav";
        //string found = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps\WristbandCSharp\WristbandCsharp\ITEM_FOUND.wav";

        int camera = 0; //changed temporarily to -1 from internet source. Was originally 1.

        Capture cap;
        Image<Bgr, Byte> image;

        // initialize a player for playing sound files
        // System.Media.SoundPlayer player = new SoundPlayer();

        // initialize and arduino
        Arduino arduino = null;

        // initialize vocal processing
        SpeechSynthesizer reader = new SpeechSynthesizer();

        // contains the name of the current cereal
        string cerealName = "";
        // contains the feedback type, currently
        string feedbackType = "";
        List<string> feedbackTypes = new List<string> { "Haptic", "Tonal", "Speech", "Haptic and Speech", "Haptic and Tonal" };

        //Declaring procedure array for random selection
        //string[] itemlists = { "item1Retreive, item2Retrieve, item3Retrieve, item4Retreive, item5Retreive" };

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

        // this will control all feedback given to the participant
        FeedBackPlayer feedbackPlayer = new FeedBackPlayer();

        // create a new log writer
        string logLocation = @"C:\Users\thirdeye\Desktop\log.txt";
        logWriter log;


        public Form1()
        {
            // FOR TESTING PURPOSES, SET THIS TO A FASTER SPEED
            reader.Rate = 0;
            // SET THIS BACK TO 0 FOR EXPERIMENT

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


            // start the log writing
            log = new logWriter(logLocation);
            log.newExperiment("New Experiment");
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

            // this will work for now, but should be changed.
            // comboBox2.Items.Remove("COM4");
        }

        // LEFT
        private void button1_Click_1(object sender, EventArgs e)
        {
//            Console.WriteLine(timerCount);
//            Console.WriteLine(timerCountMax);
//            Console.WriteLine();
            timerCount = timerCountMax;
            feedbackPlayer.setLeft();
            log.writeTimeAction("left click");
            //if (arduino!= null)
            //{
            //    arduino.SendPacket(0, intensityPercent, durationPercent);
            //}

            //if (TonalFeedback.Checked)
            //{

            //    player.SoundLocation = left;
            //    player.Load();
            //    player.PlayLooping();
            //}

            //if (SpokenFeedback.Checked)
            //{
            //    //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\left-Spoken.wav");
            //    //playerVoice.Play();
            //    //Removed above to swap for native speech
            //    reader.SpeakAsync("Left");
            //}
        }

        // RIGHT
        private void button2_Click_1(object sender, EventArgs e)
        {
            timerCount = timerCountMax;
            feedbackPlayer.setRight();
            log.writeTimeAction("right click");
            //direction = "right";
            //proximity = 50;
            //proximityLevel.Text = "50";
            //proximitySlider.Value = 5;

            //if (arduino != null)
            //{
            //    arduino.SendPacket(2, intensityPercent, durationPercent);
            //}

            //if (TonalFeedback.Checked)
            //{
            //    player.SoundLocation = right;
            //    player.Load();
            //    player.PlayLooping();
            //}

            //if (SpokenFeedback.Checked)
            //{
            //    //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\right-Spoken.wav");
            //    //playerVoice.Play();
            //    reader.SpeakAsync("Right");
            //}
        }

        // DOWN
        private void button3_Click(object sender, EventArgs e)
        {
            timerCount = timerCountMax;
            feedbackPlayer.setDown();
            log.writeTimeAction("down click");
            //direction = "down";
            //proximity = 50;
            //proximityLevel.Text = "50";
            //proximitySlider.Value = 5;

            //if (arduino != null)
            //{
            //    arduino.SendPacket(3, intensityPercent, durationPercent);
            //}

            //if (TonalFeedback.Checked)
            //{
            //    player.SoundLocation = down;
            //    player.Load();
            //    player.PlayLooping();
            //}

            //if (SpokenFeedback.Checked)
            //{
            //    //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\down-Spoken.wav");
            //    //playerVoice.Play();
            //    reader.SpeakAsync("Down");
            //}
        }

        // UP
        private void button7_Click_1(object sender, EventArgs e)
        {
            timerCount = timerCountMax;
            feedbackPlayer.setUp();
            log.writeTimeAction("up click");
            //direction = "up";
            //proximity = 50;
            //proximityLevel.Text = "50";
            //proximitySlider.Value = 5;

            //if (arduino != null)
            //{
            //    arduino.SendPacket(1, intensityPercent, durationPercent);
            //}

            //player.SoundLocation = up;
            //player.Load();

            //if (TonalFeedback.Checked)
            //{
            //    player.SoundLocation = up;
            //    player.Load();
            //    player.PlayLooping();
            //}

            //if (SpokenFeedback.Checked)
            //{
            //    //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\up-Spoken.wav");
            //    //playerVoice.Play();
            //    reader.SpeakAsync("Up");
            //}
        }

        // FOUND
        private void button8_Click_1(object sender, EventArgs e)
        {
            timerCount = timerCountMax;
            feedbackPlayer.setFound();
            log.writeTimeAction("found click");
            //direction = "found";
            //proximity = 50;
            //proximityLevel.Text = "50";
            //proximitySlider.Value = 5;

            //if (arduino != null)
            //{
            //    arduino.SendPacket(5, intensityPercent, durationPercent);
            //}

            //if (TonalFeedback.Checked)
            //{
            //    player.SoundLocation = found;
            //    player.Load();
            //    player.Play();
            //}

            //if (SpokenFeedback.Checked)
            //{
            //    reader.SpeakAsync("Item found. Begin to move your hand forward");
            //    //System.Media.SoundPlayer playerVoice = new System.Media.SoundPlayer(@"C:\Users\Jake\Desktop\WizardofOz-v3 wAudio\WristbandCSharp\WristbandCsharp\found-Spoken.wav");
            //    //playerVoice.Play();
            //}
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

        /// <summary>
        /// Allows for control with keyboard: 
        /// W, Up - up
        /// S, Down - down
        /// A, Left - left
        /// D, Right - right
        /// E, Enter - forward
        /// Q - stop
        /// F - step forward
        /// R - step backward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void resetColors ()
        {
            leftFeedback.BackColor = button11.BackColor;
            rightFeedback.BackColor = button11.BackColor;
            upFeedback.BackColor = button11.BackColor;
            downFeedback.BackColor = button11.BackColor;
            itemFoundFeedback.BackColor = button11.BackColor;
            StopFeedback.BackColor = button11.BackColor;
            stepBackwardFeedback.BackColor = button11.BackColor;
            stepForwardFeedback.BackColor = button11.BackColor;
        }

        private void Form1_OnKeyDown(object sender, KeyEventArgs e) //added this in, changed from protected to private
        {
            timerCount = timerCountMax;
            feedbackPlayer.cancelSpeech();
            resetColors();

            //base.OnKeyDown(e); //removed for v2
            //char input = char.Parse(e.KeyCode);
            switch (e.KeyCode) //changed to keycode from KeyChar
            {
                case (Keys.Left):
                case (Keys.A): //a
                    leftFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setLeft();
                    log.writeTimeAction("Left Key Press");
                    break;
                case (Keys.Right):
                case (Keys.D):  //d
                    rightFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setRight();
                    log.writeTimeAction("Right Key Press");
                    break;
                case (Keys.Up):
                case (Keys.W): //w
                    upFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setUp();
                    log.writeTimeAction("Up Key Press");
                    break;
                case (Keys.Down):
                case (Keys.S): //s
                    downFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setDown();
                    log.writeTimeAction("Down Key Press");
                    break;
                case (Keys.Enter):
                case (Keys.E): //e
                    itemFoundFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setFound();
                    log.writeTimeAction("Item Found Key Press");
                    break;
                case (Keys.Q):
                    StopFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setStop();
                    log.writeTimeAction("Stop Key Press");
                    break;
                case (Keys.F):
                    stepForwardFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setForward();
                    log.writeTimeAction("Step Forward Key Press");
                    break;
                case (Keys.R):
                    stepBackwardFeedback.BackColor = Color.LimeGreen;
                    feedbackPlayer.setBackward();
                    log.writeTimeAction("Step Backward Key Press");
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

        //private void proximitySlider_Scroll(object sender, EventArgs e)
        //{
        //    proximity = this.proximitySlider.TabIndex * 10;
        //    //Can put in text later if we need to.
        //    proximityLevel.Text = "" + this.proximitySlider.TabIndex * 10;
        //}

        //private void proximitySlider_ValueChanged(object sender, EventArgs e)
        //{
        //    proximity = proximitySlider.Value * 10;
        //    proximityLevel.Text = (proximitySlider.Value * 10).ToString();
        //}

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
            log.writeTimeAction("Introduction1");
            //Introduction  - muted temporarily
            //checkAudio(); //remove temporarily. PUT IT BACK ON
            //randomSelection();


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
            reader.SpeakAsync("Whenever you’re ready, just extend your hand in the direction of the shelf. For this item, we will be using the tone-based feedback. You can go ahead and start whenever you are ready.");
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

        //private void randomSelection()
        //{
        //    //Function used to pick the order of events. Creates new buttons, sets controls automatically.
        //    //string[] itemArray = new string[] {"item1Retreive, item2Retrieve, item3Retrieve, item4Retreive, item5Retreive"}; (placed this as global)

            
        //    if (globalRandomRemaining != 0)
        //    {

        //        int randomIndex = rnd.Next(5); //generates the random number

        //        //Need to check if the number has been used before. Otherwise, this will run forever. UGHHHHHHHH
        //        //Need to generate a number between 0 and 4. This part still works. Now will set flags of true/flase for whether each of them has been used. If used, repeat.
        //        //Get rid of global remaining reduction. 

        //        if (randomIndex == 0)
        //        {
        //            if (item1 == false)
        //            {
        //                item1Retrieve();
        //                //itemArray.Remove("item1Retreive");
        //                item1 = true;
        //                globalRandomRemaining--;
        //            }
        //            else { randomSelection(); }
        //        }
        //        else if (randomIndex == 1)
        //        { 
        //            if (item2 == false)
        //            {
        //                item2Retrieve(); 
        //                //itemArray.Remove("item2Retrieve");
        //                item2 = true;
        //                globalRandomRemaining--;
        //            }
        //            else { randomSelection(); }

        //        }
        //        else if (randomIndex == 2)
        //        {
        //            if (item3 == false)
        //            {
        //                item3Retrieve();
        //                //itemArray.Remove("item3Retrieve");
        //                item3 = true;
        //                globalRandomRemaining--;
        //            }
        //            else { randomSelection(); }
        //        }
        //        else if (randomIndex == 3)
        //        {
        //            if (item4 == false)
        //            {
        //                item4Retrieve();
        //                //itemArray.Remove("item4Retrieve");
        //                item4 = true;
        //                globalRandomRemaining--;
        //            }
        //            else { randomSelection(); }
        //        }
        //        else if (randomIndex == 4)
        //        {
        //            if (item5 == false)
        //            {
        //                item5Retrieve();
        //                //itemArray.Remove("item5Retrieve");
        //                item5 = true;
        //                globalRandomRemaining--;
        //            }
        //            else { randomSelection(); }
        //        }
        //        //Above if statements activate the appropriate function and then remove the item from the list.
                
        //    }
        //    else
        //    {
        //        FINISHED.Location = new Point(1400, 100);
        //        FINISHED.Height = 40;
        //        FINISHED.Width = 150;
        //        FINISHED.Name = "FINISHED";
        //        FINISHED.Text = "FINISHED";
        //        FINISHED.Click += new EventHandler(FINISHED_Click);
        //        Controls.Add(FINISHED);
        //    }

        //}

        // introduction to retrieve corn pops
        

        // start a shelf scan
        // IMPLEMENT TIMER HERE
        private void beginShelfScan()
        {
            beginShelfScanButton.Visible = true;
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
                feedbackPlayer.initializeArduino((string)comboBox2.SelectedItem);
                //arduino = new Arduino((string)comboBox2.SelectedItem);
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
            //randomSelection();
        }

        private void nextItem_Click(object sender, EventArgs e)
        {
            endSearch.Visible = false;
            startSearchButton.Visible = false;
            beginShelfScanButton.Visible = false;
            nextItem.Visible = false;
            feedbackButtons.Visible = false;

            if (cerealName == "Corn Pops")
            {
                cerealName = "Mini Wheats";
                foodIntroButton.Text = "Mini Wheats Introduction";
            }
            else if (cerealName == "Mini Wheats")
            {
                cerealName = "Frosted Flakes";
                foodIntroButton.Text = "Frosted Flakes Introduction";
            }
            else if (cerealName == "Frosted Flakes")
            {
                //cerealName = "Fruit Loops";
                //foodIntroButton.Text = "Fruit Loops Introduction";
                //alternative.Visible = false;
                //getAlternative.Visible = false;
                //getOriginal.Visible = false;
                cerealName = "";
                foodIntroButton.Visible = false;

            }
            //else if (cerealName == "Fruit Loops")
            //{
                //cerealName = "Raisin Bran";
                //foodIntroButton.Text = "Raisin Bran Introduction";
                //twoForOne.Visible = false;
                //get1.Visible = false;
                //get2.Visible = false;
            //}
            else
            {
                cerealName = "";
                foodIntroButton.Visible = false;
            }


            //if (lastProcedureIndex == 0)
            //{
            //    Controls.Remove(FF_Intro);
            //    Controls.Remove(FF_Alternate);
            //    Controls.Remove(FF_Switch);
            //    Controls.Remove(FF_Stay);
            //    Controls.Remove(FF_ShelfScan);
            //    //remove frosted flakes elements
            //}
            //else if (lastProcedureIndex == 1)
            //{
            //    Controls.Remove(SBBQ_Intro);
            //    Controls.Remove(SBBQ_ShelfScan);
            //}
            //else if (lastProcedureIndex == 2)
            //{
            //    Controls.Remove(SoupIntro);
            //    Controls.Remove(Soup2for1);
            //    Controls.Remove(SoupGet1);
            //    Controls.Remove(SoupGet2);
            //    Controls.Remove(SoupShelfScan1);
            //    Controls.Remove(SoupDepth);
            //    Controls.Remove(SoupShelfScan2);
            //}
            //else if (lastProcedureIndex == 3)
            //{
            //    Controls.Remove(RedHotIntro);
            //    Controls.Remove(RedHotShelfScan);
            //}
            //else if (lastProcedureIndex == 4)
            //{
            //    Controls.Remove(HeinzIntro);
            //    Controls.Remove(HeinzShelfScan);
            //}
            ////randomSelection();

        }

        private void debugString_Click(object sender, EventArgs e)
        {

        }

        private void motorActive_Click(object sender, EventArgs e)
        {

        }

        //Can't hear button
        private void CantHear_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("I'm having some trouble hearing you. Try speaking a little louder. Just say 'Alex' to see if I can hear you.");
        }

        //Can hear button
        private void CanHear_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("OK, great, I can hear you clearly.");
        }

        private void WrongItem_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("You have grabbed the incorrect item. Would you like to try to grab the correct item again?");
            log.writeTimeAction("Wrong Item Button");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            reader.SpeakAsync("Ok, let's rescan the shelf. When ready, extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            log.writeTimeAction("Re-search Button");
        }

        private void SkipResearch_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("OK, let's just skip this item and move on to the next one.");
            log.writeTimeAction("Skip Re-Search button");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("As you move through our store today, you can keep your arm resting at your side. Don't hold up your arm until I tell you to do so. For each item that we find, we will test a different style of retrieving items.");
            log.writeTimeAction("Introduction 2");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("While grabbing each item, reach forward when directed to do so until you touch an item on the shelf. If the device does not give you any further corrects, go ahead and grab the item. If it offers directional assistance after touching an item, it means you are slightly off-target and the device will try to get you on track. Let's get started.");
            log.writeTimeAction("Introduction 3");
            cerealName = "No Cereal";
            feedbackType = feedbackTypeAssigner();

        }

        private void button11_Click_2(object sender, EventArgs e)
        {
            feedbackPlayer.setTonal(true);
            feedbackPlayer.setSpoken(false);
            feedbackPlayer.setVibration(false);

            reader.Speak("Before grabbing the next item, let's practice how you are going to grab the next food item from the shelf. This time, you will be hearing a tone based sound. I will now play the sound you will hear to move left.");
            feedbackPlayer.setLeft();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move to the right");
            feedbackPlayer.setRight();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move up");
            feedbackPlayer.setUp();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move down");
            feedbackPlayer.setDown();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to stop");
            feedbackPlayer.setStop();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to step backward");
            feedbackPlayer.setBackward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to step forward");
            feedbackPlayer.setForward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Finally, this is the sound you will hear to move your hand forward.");
            feedbackPlayer.setFound();
            feedbackPlayer.giveFeedback(1, 0);
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

            feedbackPlayer.setTonal(false);
            feedbackPlayer.setSpoken(true);
            feedbackPlayer.setVibration(false);

            reader.Speak("This time, you will be hearing a spoken-word based sound system. I will now play the sound you will hear to move left.");
            feedbackPlayer.setLeft();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move to the right");
            feedbackPlayer.setRight();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move up");
            feedbackPlayer.setUp();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to move down");
            feedbackPlayer.setDown();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to step backward");
            feedbackPlayer.setBackward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound you will hear to step forward");
            feedbackPlayer.setForward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("This is the sound you will hear to stop.");
            feedbackPlayer.setStop();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Finally, this is the sound you will hear to move your hand forward.");
            feedbackPlayer.setFound();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1500);
            reader.Speak("Would you like to hear these sounds again, or move on?");
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            feedbackPlayer.setTonal(false);
            feedbackPlayer.setSpoken(false);
            feedbackPlayer.setVibration(true);

            reader.Speak("This time, you will feel a haptic vibration based system. I will now activate the motor you will feel to move left.");
            feedbackPlayer.setLeft();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to move to the right");
            feedbackPlayer.setRight();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to move up");
            feedbackPlayer.setUp();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to move down");
            feedbackPlayer.setDown();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to step backward");
            feedbackPlayer.setBackward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to step forward");
            feedbackPlayer.setForward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration you will feel to stop.");
            feedbackPlayer.setStop();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Finally, this is the vibration you will feel to move your hand forward.");
            feedbackPlayer.setFound();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1500);
            reader.Speak("Would you like to hear these sounds again, or move on?");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            feedbackPlayer.setTonal(true);
            feedbackPlayer.setSpoken(false);
            feedbackPlayer.setVibration(true);

            reader.Speak("This time, you will feel a haptic vibration based system as weel as hear a tonal feedback, I will now activate the motor you will feel, and play the sound you will hear to move left.");
            feedbackPlayer.setLeft();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound and the vibration to hear and feel to move to the right");
            feedbackPlayer.setRight();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound and the vibration to hear and feel to move up");
            feedbackPlayer.setUp();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound and the vibration to hear and feel to move down");
            feedbackPlayer.setDown();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound and the vibration to hear and feel to step backward");
            feedbackPlayer.setBackward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound and the vibration to hear and feel to step forward");
            feedbackPlayer.setForward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the sound and the vibration to hear and feel to stop.");
            feedbackPlayer.setStop();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Finally, this is the sound and the vibration you will hear and feel to move your hand forward.");
            feedbackPlayer.setFound();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1500);
            reader.Speak("Would you like to hear these sounds again, or move on?");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            feedbackPlayer.setTonal(false);
            feedbackPlayer.setSpoken(true);
            feedbackPlayer.setVibration(true);

            reader.Speak(". This time, you will feel a haptic vibration based system as well as hear a speech feedback. I will now activate the motor you will feel, and play the sound you will hear to move left.");
            feedbackPlayer.setLeft();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration and the sound you will feel and hear to move to the right");
            feedbackPlayer.setRight();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration and the sound you will feel and hear to move up");
            feedbackPlayer.setUp();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration and the sound you will feel and hear to move down");
            feedbackPlayer.setDown();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1000);
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration and the sound you will feel and hear to step backward");
            feedbackPlayer.setBackward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration and the sound you will feel and hear to step forward");
            feedbackPlayer.setForward();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Now the vibration and the sound you will feel and hear to stop.");
            feedbackPlayer.setStop();
            feedbackPlayer.giveFeedback(1, 0);

            Thread.Sleep(1500);
            reader.Speak("Finally, this is the vibration and the sound you will feel and hear to move your hand forward.");
            feedbackPlayer.setFound();
            feedbackPlayer.giveFeedback(1, 0);
            Thread.Sleep(1500);
            reader.Speak("Would you like to hear these sounds again, or move on?");
        }

        private void TonalFeedback_CheckedChanged(object sender, EventArgs e)
        {
            if (TonalFeedback.Checked) {
                feedbackPlayer.setTonal(true);
            }
            else
            {
                feedbackPlayer.setTonal(false);
            }
        }

        // STOP
        private void StopButton_Click(object sender, EventArgs e)
        {
            feedbackPlayer.setStop();
            feedbackPlayer.giveFeedback(1, 0);
            log.writeTimeAction("stop click");
        }

        // CALL FEEDBACK METHOD BASED ON A TIMER
        private void feedback(object sender, EventArgs e)
        {
            timerCount = timerCount + 1;
            feedbackPlayer.giveFeedback(timerCount, timerCountMax);
            if (timerCount >= timerCountMax) timerCount = 0;
        }

        // FORWARD
        private void forwardButton_Click(object sender, EventArgs e)
        {
            feedbackPlayer.setForward();
            log.writeTimeAction("stepforward click");
        }

        // BACKWARD
        private void backwardButton_Click(object sender, EventArgs e)
        {
            feedbackPlayer.setBackward();
            log.writeTimeAction("stepbackward click");
        }

        // spoken feedback box is checked
        private void SpokenFeedback_CheckedChanged(object sender, EventArgs e)
        {
            if (SpokenFeedback.Checked)
            {
                feedbackPlayer.setSpoken(true);
            }
            else
            {
                feedbackPlayer.setSpoken(false);
            }
        }

        // the introduction for each item
        private void button24_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is a box of " + cerealName + ". One of the lab assistants can help you get to that section of our shelves now.");
            log.writeTimeAction(foodIntroButton.Text);

            feedbackButtons.Visible = true;
            //if (cerealName == "Corn Pops") {
            //    beginShelfScanButton.Visible = true;
            //}
            //else if (cerealName == "Mini Wheats")
            //{
            //    beginShelfScanButton.Visible = true;
            //}
            //else if (cerealName == "Frosted Flakes")
            //{
            //    alternative.Visible = true;
            //}
            //else if (cerealName == "Fruit Loops")
            //{
            //    twoForOne.Visible = true;
            //}
            //else if (cerealName == "Raisin Bran")
            //{
            //    beginShelfScanButton.Visible = true;
            //}
        }



        // start a shelf scan for the new item
        private void beginShelfScanButton_Click(object sender, EventArgs e)
        {
            reader.Speak("This time, to find and retrieve this box of cereal, we will be using the " + feedbackType + " feedback to help guide you. Whenever you’re ready, just extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            nextItem.Visible = true;
            log.writeTimeAction(feedbackType);
        }

        public string feedbackTypeAssigner()
        {
            string feedbackType;
            if (feedbackTypes.Count != 0)
            {
                Random random = new Random();
                int index = random.Next(feedbackTypes.Count);
                feedbackType = feedbackTypes[index];
                feedbackTypes.RemoveAt(index);

                feedbackPlayer.setTonal(false);
                feedbackPlayer.setSpoken(false);
                feedbackPlayer.setVibration(false);


                if (feedbackType == "Haptic")
                {
                    feedbackPlayer.setVibration(true);
                }

                else if (feedbackType == "Tonal")
                {
                    feedbackPlayer.setTonal(true);
                }

                else if (feedbackType == "Speech")
                {
                    feedbackPlayer.setSpoken(true);
                }

                else if (feedbackType == "Speech and Tonal")
                {
                    feedbackPlayer.setSpoken(true);
                    feedbackPlayer.setTonal(true);
                }

                else if (feedbackType == "Haptic and Speech")
                {
                    feedbackPlayer.setVibration(true);
                    feedbackPlayer.setSpoken(true);
                }
            }

            else
            {
                feedbackType = "none";
            }

            return feedbackType;
        }



        // end the shelf scan for the item
        private void endSearch_Click(object sender, EventArgs e)
        {
            feedbackPlayer.setNone();

            endSearch.Visible = false;
            startSearchButton.Visible = false;
            //beginShelfScanButton.Visible = false;
            //nextItem.Visible = false;
            //feedbackButtons.Visible = false;


            if (cerealName == "Corn Pops")
            {
                cerealName = "Mini Wheats";
                foodIntroButton.Text = "Mini Wheats Introduction";
            }
            else if (cerealName == "Mini Wheats")
            {
                cerealName = "Frosted Flakes";
                foodIntroButton.Text = "Frosted Flakes Introduction";
            }
            else if (cerealName == "Frosted Flakes")
            {
                cerealName = "Fruit Loops";
                foodIntroButton.Text = "Fruit Loops Introduction";
                cerealName = "";
                foodIntroButton.Visible = false;

            }
            //else if (cerealName == "Fruit Loops")
            //{
            //cerealName = "Raisin Bran";
            //foodIntroButton.Text = "Raisin Bran Introduction";
            //twoForOne.Visible = false;
            //get1.Visible = false;
            //get2.Visible = false;
            //}
            else
            {
                cerealName = "";
                foodIntroButton.Visible = false;
            }

            //feedbackType = feedbackTypeAssigner();
            //if (feedbackType == "none")
            //{
            //    nextItem.Visible = true;
            //}
            log.writeTimeAction("Finish Scan");

        }

        private void getAlternative_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Okay, we'll get the generic Frosted Flakes instead.");
            beginShelfScanButton.Visible = true;
        }

        private void getOriginal_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Okay, we'll stick with the Frosted Flakes.");
            beginShelfScanButton.Visible = true;
        }

        private void get2_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Okay, I'll add a second box of cereal to the list.");
            beginShelfScanButton.Visible = true;
        }

        private void get1_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("Okay, we'll just get the one box of cereal.");
            beginShelfScanButton.Visible = true;
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            log.writeTimeAction("Start Scan ");
        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label5_Click_3(object sender, EventArgs e)
        {

        }

        private void Tonal_Click(object sender, EventArgs e)
        {
            feedbackType = "Tonal";
            feedbackPlayer.setTonal(true);
            feedbackPlayer.setSpoken(false);
            feedbackPlayer.setVibration(false);
            //beginShelfScanButton.Visible = true;

            reader.Speak("This time, to find and retrieve this box of cereal, we will be using the " + feedbackType + " feedback to help guide you. Whenever you’re ready, just extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            //nextItem.Visible = true;
            log.writeTimeAction(feedbackType);
        }

        private void Speech_Click(object sender, EventArgs e)
        {
            feedbackType = "Spoken";
            feedbackPlayer.setTonal(false);
            feedbackPlayer.setSpoken(true);
            feedbackPlayer.setVibration(false);
            //beginShelfScanButton.Visible = true;

            reader.Speak("This time, to find and retrieve this box of cereal, we will be using the " + feedbackType + " feedback to help guide you. Whenever you’re ready, just extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            //nextItem.Visible = true;
            log.writeTimeAction(feedbackType);

        }

        private void Haptic_Click(object sender, EventArgs e)
        {
            feedbackType = "Haptic";
            feedbackPlayer.setTonal(false);
            feedbackPlayer.setSpoken(false);
            feedbackPlayer.setVibration(true);
            //beginShelfScanButton.Visible = true;

            reader.Speak("This time, to find and retrieve this box of cereal, we will be using the " + feedbackType + " feedback to help guide you. Whenever you’re ready, just extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            //nextItem.Visible = true;
            log.writeTimeAction(feedbackType);

        }

        private void tonalAndHaptic_Click(object sender, EventArgs e)
        {
            feedbackType = "Tonal and Haptic";
            feedbackPlayer.setTonal(true);
            feedbackPlayer.setSpoken(false);
            feedbackPlayer.setVibration(true);
            //beginShelfScanButton.Visible = true;

            reader.Speak("This time, to find and retrieve this box of cereal, we will be using the " + feedbackType + " feedback to help guide you. Whenever you’re ready, just extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            //nextItem.Visible = true;
            log.writeTimeAction(feedbackType);
        }

        private void speechAndHaptic_Click(object sender, EventArgs e)
        {
            feedbackType = "Speech and Haptic";
            feedbackPlayer.setTonal(false);
            feedbackPlayer.setSpoken(true);
            feedbackPlayer.setVibration(true);
            //beginShelfScanButton.Visible = true;

            reader.Speak("This time, to find and retrieve this box of cereal, we will be using the " + feedbackType + " feedback to help guide you. Whenever you’re ready, just extend your hand towards the shelf.");
            // create a timer here
            startSearchButton.Visible = true;
            endSearch.Visible = true;
            //nextItem.Visible = true;
            log.writeTimeAction(feedbackType);

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is a box of Corn Pops. One of the lab assistants can help you get to that section of our shelves now.");
            log.writeTimeAction("Corn Pops Introduction");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is a box of Mini Wheats. One of the lab assistants can help you get to that section of our shelves now.");
            log.writeTimeAction("Mini Wheats Introduction");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is a box of Frosted Flakes. One of the lab assistants can help you get to that section of our shelves now.");
            log.writeTimeAction("Frosted Flakes Introduction");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is a box of Rasin Bran. One of the lab assistants can help you get to that section of our shelves now.");
            log.writeTimeAction("Rasin Bran Introduction");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            reader.SpeakAsync("The next item on our list is a box of Fruit Loops. One of the lab assistants can help you get to that section of our shelves now.");
            log.writeTimeAction("Froot Loops Introduction");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            proximity = this.proximitySlider.TabIndex * 10;
            //Can put in text later if we need to.
            //proximityLabel.Text = "" + this.proximitySlider.TabIndex * 10;

        }


        private void proximitySlider_ValueChanged(object sender, EventArgs e)
        {
            proximity = proximitySlider.Value * 10;
            proximityLabel.Text = (proximitySlider.Value * 10).ToString();
        }

        private void label5_Click_4(object sender, EventArgs e)
        {

        }

        private void StopFeedback_Click(object sender, EventArgs e)
        {

        }
    }
}
