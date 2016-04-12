using System;
using System.IO;
using System.Diagnostics;

namespace WristbandCsharp
{

    class logWriter
    {
        TextWriter log;
        Stopwatch timer;
        string asterisks = "***********************************************";
        string filename;

        public logWriter(string filename)
        {
            this.filename = filename;
            timer = new Stopwatch();

        }

        //public ~logWriter()
        //{
        //    log.Close();
        //}

        public void newExperiment(string description)
        {
            log = new StreamWriter(filename, true);
            log.WriteLine();
            log.WriteLine();
            log.WriteLine(asterisks);
            log.WriteLine(description);
            log.WriteLine(DateTime.Now);
            log.WriteLine(asterisks);
            timer.Start();
            log.Close();
        }

        public void writeTimeAction(string action)
        {
            log = new StreamWriter(filename, true);
            log.WriteLine(timer.ElapsedMilliseconds.ToString() + ": " + action);
            log.Close();
        }

        public void close()
        {
            log.WriteLine();
            log.WriteLine();
            log.Close();
        }

    }
}