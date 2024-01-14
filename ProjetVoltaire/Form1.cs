using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Windows.Forms.AxHost;
using System.Drawing;
using System;

namespace ProjetVoltaire
{
    public partial class Form1 : Form
    {
        Solver solver;
        Driver driver;

        bool stop = false;

        int mistakes = 0;
        int rightAwns = 0;
        bool makingMistake = false;

        public Form1()
        {
            InitializeComponent();
            this.FormClosed += Form1_FormClosed;
            solver = new("");
            UpdateButtonState();

            DelayLabel.Text = delayTrackBar.Value.ToString() + "s";
            MistakesLabel.Text = errorTrackBar.Value.ToString() + "%";

            Task.Run(() =>
            {
                driver = new Driver(@"webdriver/chromedriver.exe");
                driver.DataFound += AwnsersFound;
            });
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Close the ChromeDriver when the form is closed
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        private void FindButtonClicked(object sender, EventArgs e)
        {
            if (solver.GetBestMatch(driver.FindString(), out string match))
                AwnserLabel.Text = match;
            else
                AwnserLabel.Text = "Il n'y a pas de faute";
        }
        void DoTheThing()
        {
            Thread.Sleep(50);

            if (!driver.IsExercice())
            {
                stop = true;
                return;
            }

            int targetMistakes = 0;
            this.Invoke(new Action(() =>
            {
                targetMistakes = errorTrackBar.Value;
            }));

            makingMistake = ((rightAwns + mistakes) != 0) ?
                mistakes * 100 / (rightAwns + mistakes) < targetMistakes :
                false;

            bool foundMatch = false;
            string p = "";
            int startX = 0, startY = 0;


            if (!makingMistake)
            {
                string capturedText = driver.FindString();
                Console.WriteLine("Info -> Captured : " + capturedText);

                if (string.IsNullOrEmpty(capturedText)) return;

                foundMatch = solver.GetBestMatch(capturedText, out p);
            }

            if (foundMatch)
            {
                Console.WriteLine("Info -> Found an : " + p);

                int firstIndex = p.IndexOf("|");
                int lastIndex = p.LastIndexOf("|");

                if (firstIndex != -1 && lastIndex != -1)
                {
                    int averageIndex = (firstIndex + lastIndex) / 2;

                    //string text = p.Substring(0, firstIndex);
                    driver.ClickChar(averageIndex);

                    Thread.Sleep(500);
                    CheckResult();
                    driver.ClickNext();
                }
                else
                {
                    Console.WriteLine("Error -> No error found in matching data");
                }
            }
            else
            {
                Console.WriteLine("Info -> No match found");

                driver.ClickNoMistakes();

                Thread.Sleep(500);
                CheckResult();
                driver.ClickNext();
            }
        }
        private void CheckResult()
        {
            switch (driver.AwnserState())
            {
                case 0: //good
                    Console.WriteLine("Return -> Awnser was right !");
                    rightAwns++;

                    Console.WriteLine($"Info -> mistakes:{mistakes} right awnsers:{rightAwns}");
                    break;
                case 1: //bad
                    Console.WriteLine("Return -> Awnser was wrong !");
                    mistakes++;

                    Console.WriteLine($"Info -> mistakes:{mistakes} right awnsers:{rightAwns}");

                    stop = !makingMistake;
                    break;
                case 2: //dont know
                    Console.WriteLine("Return -> Cant get state");

                    stop = true;
                    break;
            }
        }
        private void StartButtonClicked(object sender, EventArgs e)
        {
            Task workerTask = Task.Run(() =>
            {
                Console.WriteLine("Info -> Starting");
                stop = false;
                while (!stop)
                {
                    DoTheThing();

                    int delayTime = 0;
                    this.Invoke(new Action(() =>
                    {
                        delayTime = delayTrackBar.Value * 1000;
                    }));
                    Thread.Sleep(delayTime);
                }
                Console.WriteLine("Info -> Stopped");
            });
        }
        private void StopButtonClicked(object sender, EventArgs e)
        {
            stop = true;
        }
        private void AwnsersFound(object sender, string data)
        {
            solver = new(data);
            this.Invoke(new Action(() =>
            {
                UpdateButtonState();
            }));
        }

        private void UpdateButtonState()
        {
            FindAwnserButton.Enabled = solver.isOK;
            StartButton.Enabled = solver.isOK;
            StopButton.Enabled = solver.isOK;
        }
        private void delayTrackBar_Scroll(object sender, EventArgs e)
        {
            DelayLabel.Text = delayTrackBar.Value.ToString() + "s";
        }
        private void errorTrackBar_Scroll(object sender, EventArgs e)
        {
            MistakesLabel.Text = errorTrackBar.Value.ToString() + "%";
        }
    }
}