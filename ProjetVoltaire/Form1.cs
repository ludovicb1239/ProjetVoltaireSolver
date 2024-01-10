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

        int waitingForPos = 0;

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENTF_LEFTDOWN = 0x02;
        const int MOUSEEVENTF_LEFTUP = 0x04;

        bool stop = false;

        int mistakes = 0;
        int rightAwns = 0;
        bool makingMistake = false;

        public Form1()
        {
            InitializeComponent();

            solver = new();
            UpdateButtonState();

            DelayLabel.Text = delayTrackBar.Value.ToString() + "s";
            MistakesLabel.Text = errorTrackBar.Value.ToString() + "%";
        }

        private void FindButtonClicked(object sender, EventArgs e)
        {
            if (solver.GetBestMatch(SentenceInput.Text, out string match))
                AwnserLabel.Text = match;
            else
                AwnserLabel.Text = "Il n'y a pas de faute";
        }
        void DoTheThing()
        {
            Thread.Sleep(50);

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
                this.Invoke(new Action(() =>
                {
                    if (string.IsNullOrEmpty(PhrasePosX.Text) || string.IsNullOrEmpty(PhrasePosY.Text))
                    {
                        Console.WriteLine("Warning -> No input values found, using default");
                        PhrasePosX.Text = "494";
                        PhrasePosY.Text = "280";
                    }
                    startX = int.Parse(PhrasePosX.Text);
                    startY = int.Parse(PhrasePosY.Text);
                }));

                int distanceToMove = 1200;

                SetCursorPos(startX, startY);

                // Simulate holding down the left mouse button
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                Thread.Sleep(20);

                // Move the mouse to the right
                SetCursorPos(startX + distanceToMove, startY);

                Thread.Sleep(50);

                // Release the left mouse button
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                Thread.Sleep(50);

                // Send Ctrl+C to copy the selected text to the clipboard
                SendKeys.SendWait("^(c)");
                Thread.Sleep(50);

                string capturedText = "";

                this.Invoke(new Action(() =>
                {
                    // Retrieve the text from the clipboard
                    capturedText = Clipboard.GetText();
                }));

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

                    string text = p.Substring(0, firstIndex);
                    Font font = new Font("Century Gothic", 18f);

                    int width = GetStringWidth(text, font);

                    if (width > 906) {
                        width -= 906;
                        startY += 30;
                    }

                    ClickCursor(startX + width + 5, startY);

                    Thread.Sleep(1400);

                    CheckResult();

                    SendKeys.SendWait("{ENTER}");

                }
                else
                {
                    Console.WriteLine("Error -> No error found in matching data");
                }
            }
            else
            {
                Console.WriteLine("Info -> No match found");

                int bttX = 0, bttY = 0;
                this.Invoke(new Action(() =>
                {
                    if (string.IsNullOrEmpty(BoutonPosX.Text) || string.IsNullOrEmpty(BoutonPosY.Text))
                    {
                        Console.WriteLine("Warning -> No input values found, using default");
                        BoutonPosX.Text = "963";
                        BoutonPosY.Text = "417";
                    }
                    bttX = int.Parse(BoutonPosX.Text);
                    bttY = int.Parse(BoutonPosY.Text);
                }));
                ClickCursor(bttX, bttY);

                Thread.Sleep(1400);

                CheckResult();

                SendKeys.SendWait("{ENTER}");
            }
        }
        private void CheckResult()
        {
            Point pixelPosition = new();
            this.Invoke(new Action(() =>
            {
                if (string.IsNullOrEmpty(ResultatPosX.Text) || string.IsNullOrEmpty(ResultatPosY.Text))
                {
                    Console.WriteLine("Warning -> No input values found, using default");
                    ResultatPosX.Text = "648";
                    ResultatPosY.Text = "189";
                }
                pixelPosition = new Point(int.Parse(ResultatPosX.Text), int.Parse(ResultatPosY.Text));
            }));

            Color pixelColor = GetPixelColor(pixelPosition);
            if (pixelColor == Color.FromArgb(255, 143, 31))
            {
                // orange color
                Console.WriteLine("Return -> Awnser was wrong !");
                mistakes++;

                Console.WriteLine($"Info -> mistakes:{mistakes} right awnsers:{rightAwns}");

                stop = !makingMistake;
            }
            else if (pixelColor == Color.FromArgb(162, 212, 23) || pixelColor == Color.FromArgb(248, 212, 29))
            {
                // green or yellow color
                Console.WriteLine("Return -> Awnser was right !");
                rightAwns++;

                Console.WriteLine($"Info -> mistakes:{mistakes} right awnsers:{rightAwns}");
            }
            else
            {
                Console.WriteLine("Return -> Not an expected color");

                stop = true;
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
        void ClickCursor(int x, int y)
        {
            // Move the mouse to the right
            SetCursorPos(x, y);

            Thread.Sleep(20);
            // Simulate holding down the left mouse button
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(20);
            // Release the left mouse button
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            Thread.Sleep(20);
        }
        static int GetStringWidth(string text, Font font)
        {
            Size size = TextRenderer.MeasureText(text, font);
            return size.Width;
        }

        private void StopButtonClicked(object sender, EventArgs e)
        {
            stop = true;
        }

        private void UpdateDataButton_Click(object sender, EventArgs e)
        {
            solver = new();
            UpdateButtonState();
        }
        private void UpdateButtonState()
        {
            FindAwnserButton.Enabled = solver.isOK;
            StartButton.Enabled = solver.isOK;
            StopButton.Enabled = solver.isOK;
        }

        static Color GetPixelColor(Point position)
        {
            // Create a bitmap of the screen
            using (Bitmap screenBitmap = new Bitmap(1, 1))
            {
                // Create a graphics object from the bitmap
                using (Graphics g = Graphics.FromImage(screenBitmap))
                {
                    // Capture the screen at the specified position
                    g.CopyFromScreen(position, Point.Empty, new Size(1, 1));
                }

                // Get the color of the pixel at position (0, 0)
                return screenBitmap.GetPixel(0, 0);
            }
        }

        private void Set1Clicked(object sender, EventArgs e)
        {
            if (waitingForPos == 0)
            {
                waitingForPos = 1;

                // Set up the mouse click event handler
                MouseHook.Start();
                MouseHook.OnMouseClick += MouseClickHandler;
            }
        }

        private void Set2Clicked(object sender, EventArgs e)
        {
            if (waitingForPos == 0)
            {
                waitingForPos = 2;

                // Set up the mouse click event handler
                MouseHook.Start();
                MouseHook.OnMouseClick += MouseClickHandler;
            }
        }

        private void Set3Clicked(object sender, EventArgs e)
        {
            if (waitingForPos == 0)
            {
                waitingForPos = 3;

                // Set up the mouse click event handler
                MouseHook.Start();
                MouseHook.OnMouseClick += MouseClickHandler;
            }
        }
        private void MouseClickHandler(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"Info -> Mouse Clicked at X: {e.X}, Y: {e.Y}");
            MouseHook.OnMouseClick -= MouseClickHandler;

            switch (waitingForPos)
            {
                case 1:
                    PhrasePosX.Text = e.X.ToString();
                    PhrasePosY.Text = e.Y.ToString();
                    break;
                case 2:
                    BoutonPosX.Text = e.X.ToString();
                    BoutonPosY.Text = e.Y.ToString();
                    break;
                case 3:
                    ResultatPosX.Text = e.X.ToString();
                    ResultatPosY.Text = e.Y.ToString();
                    break;
            }
            waitingForPos = 0;

            // Clean up and exit
            MouseHook.Stop();
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

    // Mouse hook class to capture mouse events
    public static class MouseHook
    {
        private static IntPtr hookId = IntPtr.Zero;
        private static LowLevelMouseProc proc;

        public static event EventHandler<MouseEventArgs> OnMouseClick;

        public static void Start()
        {
            proc = HookCallback;
            hookId = SetHook(proc);
        }

        public static void Stop()
        {
            UnhookWindowsHookEx(hookId);
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(14, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)0x0201) // 0x0201 is WM_LBUTTONDOWN
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                OnMouseClick?.Invoke(null, new MouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }
    }
}