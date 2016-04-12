using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WristbandCsharp
{
    class FeedBackPlayer
    {
        private String direction;
        bool spoken, tonal, vibration;
        Arduino arduino;
        System.Media.SoundPlayer tonalPlayer;
        System.Speech.Synthesis.SpeechSynthesizer speechPlayer;
        int pulseDuration = 20; // duration of arduino during pulsing
        int duration = 50;  // duration of arduino vibration
        int intensity = 100;  // intensity of arduino vibration
        int waitTime = 500;  // arbitary wait time (in ms) to wait before playing feedback again

        string leftTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\left.wav";
        string downTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\down.wav";
        string rightTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\right.wav";
        string upTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\up.wav";
        string foundTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\found2.wav";
        string stepBackwardTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\stepBackward2.wav";
        string stepForwardTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\stepForward2.wav";
        string stopTonal = @"C:\Users\thirdeye\Desktop\WizardofOz-v5-RandomSteps - Edits\WristbandCSharp\WristbandCsharp\stop3.wav";

        string stepBack = "Step backward";
        string stepForward = "Step forward";
        string foundSpeech = "Reach forward";
        string downSpeech = "Down";
        string upSpeech = "Up";
        string leftSpeech = "Left";
        string rightSpeech = "Right";
        string stopSpeech = "Stop";

        public FeedBackPlayer()
        {
            setNone();
            setTonal(false);
            setSpoken(false);
            setVibration(false);
            this.tonalPlayer = new System.Media.SoundPlayer();
            this.speechPlayer = new System.Speech.Synthesis.SpeechSynthesizer();
            
        }

        public void giveFeedback(int timerCount, int timerCountMax)
        {
//            Console.WriteLine(timerCount);
//            Console.WriteLine(timerCountMax);
//            Console.WriteLine();
            if (timerCount >= timerCountMax) {
                // up
                if (direction == "up")
                {
                    if (vibration)
                    {
                        arduino.SendPacket(1, pulseDuration, intensity);
                    }

                    if (tonal)
                    {
                        tonalPlayer.Stop();
                        tonalPlayer.Play();
                    }
                    if (spoken)
                    {
                        speechPlayer.SpeakAsync(upSpeech);
                    }
                }

                // down
                else if (direction == "down")
                {
                    if (vibration)
                    {
                        arduino.SendPacket(3, pulseDuration, intensity);
                    }

                    if (tonal)
                    {
                        tonalPlayer.Stop();
                        tonalPlayer.Play();
                    }
                    if (spoken)
                    {
                        speechPlayer.SpeakAsync(downSpeech);
                    }
                }

                // left 
                else if (direction == "left")
                {
                    if (vibration)
                    {

                        arduino.SendPacket(0, pulseDuration, intensity);
                    }

                    if (tonal)
                    {
                        tonalPlayer.Stop();
                        tonalPlayer.Play();
                    }
                    if (spoken)
                    {
                        speechPlayer.SpeakAsync(leftSpeech);
                    }
                }

                // right


                else if (direction == "right")
                {
                    if (vibration)
                    {
                        arduino.SendPacket(2, pulseDuration, intensity);
                    }

                    if (tonal)
                    {
                        tonalPlayer.Stop();
                        tonalPlayer.Play();
                    }
                    if (spoken)
                    {
                        speechPlayer.SpeakAsync(rightSpeech);
                    }
                }
            }

            
            // backward
            if (direction == "backward")
            {
                if (tonal)
                {
                    tonalPlayer.Stop();
                    tonalPlayer.Play();
                }
                if (spoken)
                {
                    speechPlayer.SpeakAsync(stepBack);
                }
                if (vibration)
                {
                    // actual
                    //arduino.SendPacket(3, 50, intensity);
                    //Thread.Sleep(600);
                    //arduino.SendPacket(3, 20, intensity);

                    // testing
                    arduino.SendPacket(0, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(3, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(0, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(3, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(0, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(3, 20, intensity);


                }

                setNone();
            }

            // forward
            else if (direction == "forward")
            {
                if (tonal)
                {
                    tonalPlayer.Stop();
                    tonalPlayer.Play();
                }
                if (spoken)
                {
                    speechPlayer.SpeakAsync(stepForward);
                }
                if (vibration)
                {
                    // actual
                    //arduino.SendPacket(1, 20, intensity);
                    //Thread.Sleep(300);
                    //arduino.SendPacket(1, 50, intensity);


                    // testing
                    arduino.SendPacket(1, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(2, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(1, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(2, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(1, 20, intensity);
                    Thread.Sleep(200);
                    arduino.SendPacket(2, 20, intensity);
                }

                setNone();
            }

            // at target


            else if (direction == "found")
            {
                if (vibration)
                {
                    if (timerCount >= timerCountMax)

                    {
                        arduino.SendPacket(0, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(1, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(2, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(3, 10, intensity);
                        Thread.Sleep(200);

                        arduino.SendPacket(5, 30, intensity);
                        Thread.Sleep(500);

                        arduino.SendPacket(0, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(1, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(2, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(3, 10, intensity);
                        Thread.Sleep(200);

                        arduino.SendPacket(5, 30, intensity);
                        Thread.Sleep(500);

                        setNone();

                    }
                }

                if (tonal)
                {
                    tonalPlayer.Stop();
                    tonalPlayer.Play();
                    setNone();
                }
                if (spoken)
                {
                    speechPlayer.SpeakAsync(foundSpeech);
                    setNone();
                }
            }

            // stop
            else if (direction == "stop")
            {
                if (spoken)
                {
                    speechPlayer.SpeakAsync(stopSpeech);
                    setNone();
                }
                if (vibration)
                {
                    if (timerCount >= timerCountMax)
                    {
                        arduino.SendPacket(5, 10, intensity);
                        Thread.Sleep(200);
                        arduino.SendPacket(5, 10, intensity);
                        setNone();

                    }
                }
                if (tonal)
                {
                    tonalPlayer.Stop();
                    tonalPlayer.Play();
                    setNone();
                }
            }

            // none
            else if (direction == "none")
            {
            }
        }

        public void initializeArduino(string portName)
        {
            this.arduino = new Arduino(portName);
        }

        public void closeArduino()
        {
            this.arduino.ClosePort();
        }

        public void setTonal(bool tonal)
        {
            this.tonal = tonal;
        }

        public void setSpoken(bool spoken)
        {
            this.spoken = spoken;
        }

        public void setVibration(bool vibration)
        {
            this.vibration = vibration;
        }

        // methods to change the direction of the object's feedback
        public void cancelSpeech()
        {
            speechPlayer.SpeakAsyncCancelAll();
        }

        public void setNone()
        {
            direction = "none";
        }
        
        public void setLeft()
        {
            direction = "left";
            tonalPlayer.SoundLocation = leftTonal;
            tonalPlayer.Load();
        }

        public void setRight()
        {
            direction = "right";

            tonalPlayer.SoundLocation = rightTonal;
        }

        public void setDown()
        {
            direction = "down";

            tonalPlayer.SoundLocation = downTonal;
            tonalPlayer.Load();
        }

        public void setUp()
        {
            direction = "up";

            tonalPlayer.SoundLocation = upTonal;
            tonalPlayer.Load();
        }

        public void setStop()
        {
            direction = "stop";
            tonalPlayer.SoundLocation = stopTonal;
            tonalPlayer.Load();
        }

        public void setFound()
        {
            direction = "found";

            tonalPlayer.SoundLocation = foundTonal;
            tonalPlayer.Load();
        }

        public void setBackward()
        {
            direction = "backward";

            tonalPlayer.SoundLocation = stepBackwardTonal;
            tonalPlayer.Load();
        }

        public void setForward()
        {
            direction = "forward";

            tonalPlayer.SoundLocation = stepForwardTonal;
            tonalPlayer.Load();
        }
    }
}
