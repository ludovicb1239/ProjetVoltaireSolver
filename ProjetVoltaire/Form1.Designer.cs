namespace ProjetVoltaire
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            FindAwnserButton = new Button();
            StartButton = new Button();
            AwnserLabel = new Label();
            StopButton = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            delayTrackBar = new TrackBar();
            label6 = new Label();
            label7 = new Label();
            errorTrackBar = new TrackBar();
            DelayLabel = new Label();
            MistakesLabel = new Label();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)delayTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorTrackBar).BeginInit();
            SuspendLayout();
            // 
            // FindAwnserButton
            // 
            FindAwnserButton.Location = new Point(12, 45);
            FindAwnserButton.Name = "FindAwnserButton";
            FindAwnserButton.Size = new Size(189, 23);
            FindAwnserButton.TabIndex = 0;
            FindAwnserButton.Text = "Trouver la réponse";
            FindAwnserButton.UseVisualStyleBackColor = true;
            FindAwnserButton.Click += FindButtonClicked;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(13, 118);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(111, 23);
            StartButton.TabIndex = 1;
            StartButton.Text = "Start Auto";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButtonClicked;
            // 
            // AwnserLabel
            // 
            AwnserLabel.AutoSize = true;
            AwnserLabel.Location = new Point(12, 74);
            AwnserLabel.Name = "AwnserLabel";
            AwnserLabel.Size = new Size(52, 15);
            AwnserLabel.TabIndex = 2;
            AwnserLabel.Text = "Réponse";
            // 
            // StopButton
            // 
            StopButton.Location = new Point(130, 118);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(111, 23);
            StopButton.TabIndex = 4;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButtonClicked;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 27);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 5;
            label2.Text = "Mode Manuel";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 100);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 6;
            label3.Text = "Mode Automatique";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 244);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 7;
            label4.Text = "Instructions";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 261);
            label5.Name = "label5";
            label5.Size = new Size(202, 30);
            label5.TabIndex = 8;
            label5.Text = "- Se connecter sur la nouvelle fenêtre\r\n- Cliquer sur le niveau voulu";
            // 
            // delayTrackBar
            // 
            delayTrackBar.LargeChange = 1;
            delayTrackBar.Location = new Point(13, 175);
            delayTrackBar.Name = "delayTrackBar";
            delayTrackBar.Size = new Size(147, 45);
            delayTrackBar.TabIndex = 9;
            delayTrackBar.Value = 1;
            delayTrackBar.Scroll += delayTrackBar_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(62, 157);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 10;
            label6.Text = "Delais";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(205, 157);
            label7.Name = "label7";
            label7.Size = new Size(65, 15);
            label7.TabIndex = 12;
            label7.Text = "Taux Erreur";
            // 
            // errorTrackBar
            // 
            errorTrackBar.Location = new Point(166, 175);
            errorTrackBar.Maximum = 100;
            errorTrackBar.Name = "errorTrackBar";
            errorTrackBar.Size = new Size(147, 45);
            errorTrackBar.SmallChange = 10;
            errorTrackBar.TabIndex = 11;
            errorTrackBar.TickFrequency = 10;
            errorTrackBar.Scroll += errorTrackBar_Scroll;
            // 
            // DelayLabel
            // 
            DelayLabel.AutoSize = true;
            DelayLabel.Location = new Point(13, 205);
            DelayLabel.Name = "DelayLabel";
            DelayLabel.Size = new Size(40, 15);
            DelayLabel.TabIndex = 30;
            DelayLabel.Text = "temps";
            DelayLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // MistakesLabel
            // 
            MistakesLabel.AutoSize = true;
            MistakesLabel.Location = new Point(166, 205);
            MistakesLabel.Name = "MistakesLabel";
            MistakesLabel.Size = new Size(55, 15);
            MistakesLabel.TabIndex = 31;
            MistakesLabel.Text = "pourcent";
            MistakesLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 308);
            label11.Name = "label11";
            label11.Size = new Size(541, 60);
            label11.TabIndex = 32;
            label11.Text = resources.GetString("label11.Text");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 377);
            Controls.Add(label11);
            Controls.Add(MistakesLabel);
            Controls.Add(DelayLabel);
            Controls.Add(label7);
            Controls.Add(errorTrackBar);
            Controls.Add(label6);
            Controls.Add(delayTrackBar);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(StopButton);
            Controls.Add(AwnserLabel);
            Controls.Add(StartButton);
            Controls.Add(FindAwnserButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Projet Voltaire";
            ((System.ComponentModel.ISupportInitialize)delayTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button FindAwnserButton;
        private Button StartButton;
        private Label AwnserLabel;
        private Button StopButton;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TrackBar delayTrackBar;
        private Label label6;
        private Label label7;
        private TrackBar errorTrackBar;
        private Label DelayLabel;
        private Label MistakesLabel;
        private Label label11;
    }
}