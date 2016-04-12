﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Drawing;

namespace WristbandCsharp
{
    class Arduino
    {

        private const int DEFAULT_THETA = 0;
        private const int DEFAULT_INTENSITY = 50;
        private const int DEFAULT_DURATION = 5; //changed to 25 for shorter duration
        private ArduinoPort port = null;
        private string comPort = "COM4";

        class ArduinoPort : SerialPort
        {
            // Initializes on COM4.
            //Mine in lab is on COM1 -JW
            //On iMAC, defaults as COM3
            /*public ArduinoPort(string portName)
                : base(portName)*/
            public ArduinoPort(string portName)
                : base(portName)
            {
                Open();


                Console.WriteLine("Initialized on {0}.", portName);
            }
        }

        public Arduino(string portName)
        {
            //port = new ArduinoPort(portName);
            this.comPort = portName;
            port = new ArduinoPort(this.comPort);
            // port = new ArduinoPort("COM4"); //trying to get rid of having to select this
            //MANUALLY ASSIGNED TO COM4, USE COMMAND ABOVE TO BRING BACK SCANNER


            //port = new ArduinoPort(SerialPort.GetPortNames()); rubbish.
        }

        ~Arduino()
        {
            try
            {
                ReleaseCamera();
            }
            catch (System.IO.IOException ioexception)   
            {
                // Port disconnected before camera released.
            }
        }

        

        public void Process(Rectangle rectangle, PointF average, PointF center)
        {

            int thetaPercent = DEFAULT_THETA;
            int intensityPercent = DEFAULT_INTENSITY;
            int durationPercent = DEFAULT_DURATION;

            #region Minimum data sent to Arduino (direction only)

            // Vector pointing from center of screen to the object.
            // Notice we negate the Y term - this is to put theta in terms of the Cartesian plane we generally think in,
            //      not int terms of winforms' coordinates (where down is positive Y)
            PointF pointingVector = new PointF(
                center.X - average.X,
                 -(center.Y - average.Y)
                );

            double theta = (Math.Atan2(pointingVector.Y, pointingVector.X)); //+ 45.0*(Math.PI / 180.0)) % (2.0 * Math.PI);

            theta = (theta / (2.0 * Math.PI)) * 100.0;
            if (theta < 0) theta += 100;

            thetaPercent = (int)theta;

            Console.WriteLine(thetaPercent);



            #endregion

            try
            {
                SendPacket(thetaPercent, 25, 1);
            }
            catch (System.IO.IOException e)
            {
                throw e;
            }
        }


        public void SendPacket(int thetaPercent, int intensityPercent, int durationPercent)
        {
            try
            {
                port.Write(new byte[] { 255, (byte)thetaPercent, (byte)intensityPercent, (byte)durationPercent, 0 }, 0, 5);
                Console.WriteLine("{0} {1} {2}", thetaPercent, intensityPercent, durationPercent);
                //char[] buf = new char[128];
                //int i = port.BytesToRead;
                //while (i-- > 0) Console.Write(port.ReadChar());
            }
            catch (System.IO.IOException e)
            {


                
                throw new System.IO.IOException(string.Format("Could not write to port %s.", port.PortName));
            }
        }

        public void ClosePort()
        {
            try
            {
                port.Close();
            }
            catch (System.IO.IOException)
            {
                throw new System.IO.IOException(string.Format("Port disconnected before proerly closed"));
            }
        }

        private void ReleaseCamera() 
        {
            try
            {
                port.Close();
            }
            catch (System.IO.IOException ioException)
            {
                
                throw new System.IO.IOException(string.Format("Port disconnected before properly closed"));
            }
        }

    }
}
