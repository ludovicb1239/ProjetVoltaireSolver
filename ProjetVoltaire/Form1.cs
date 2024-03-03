namespace ProjetVoltaire
{
    public partial class Form1 : Form
    {
        Solver solver;
        Driver driver;
        Thread workerThread;

        bool stop = false;
        bool running = false;

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
            workerThread = new Thread(ThreadMain);
            workerThread.IsBackground = true;

            Task.Run(() =>
            {
                driver = new Driver(@"chromedriver-win64/chromedriver.exe");
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
            Thread.Sleep(500);

            if (driver.HasExercice())
            {
                Console.WriteLine("Warning -> Popup exercice found");
                driver.ClickSkipExercice();
                Console.WriteLine("Info -> Popup exercice Done");
            }
            if (!driver.IsExercice())
            {
                Console.WriteLine("Warning -> No exercice found");
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
                }
                else
                {
                    Console.WriteLine("Error -> No error found in matching data");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Info -> No match found");

                driver.ClickNoMistakes();
            }

            Thread.Sleep(500);
            CheckResult();
            if (driver.HasExercice())
            {
                Console.WriteLine("Warning -> Popup exercice found");
                driver.ClickSkipExercice();
                Console.WriteLine("Info -> Popup exercice Done");
            }
            else
            {
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

                    //stop = !makingMistake;
                    break;
                case 2: //dont know
                    Console.WriteLine("Return -> Cant get state");

                    stop = true;
                    break;
            }
        }
        private void ThreadMain()
        {
            running = true;
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread threw an exception: {ex.Message}");
            }
            finally
            {
                running = false;
                this.Invoke(new Action(() =>
                {
                    UpdateButtonState();
                }));
            }
            Console.WriteLine("Info -> Stopped");
        }
        private void StartButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Info -> Starting");
            if (running)
                return;
            running = true;
            stop = false;
            UpdateButtonState();
            workerThread.Start();
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
            StartButton.Enabled = solver.isOK && !running;
            StopButton.Enabled = solver.isOK && running;
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