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
            SentenceInput = new TextBox();
            StopButton = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            delayTrackBar = new TrackBar();
            label6 = new Label();
            label7 = new Label();
            errorTrackBar = new TrackBar();
            UpdateDataButton = new Button();
            PhrasePosX = new TextBox();
            PhrasePosY = new TextBox();
            label1 = new Label();
            SetPos1Button = new Button();
            pictureBox1 = new PictureBox();
            SetPos2Button = new Button();
            label8 = new Label();
            BoutonPosY = new TextBox();
            BoutonPosX = new TextBox();
            SetPos3Button = new Button();
            label9 = new Label();
            ResultatPosY = new TextBox();
            ResultatPosX = new TextBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label10 = new Label();
            DelayLabel = new Label();
            MistakesLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)delayTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // FindAwnserButton
            // 
            FindAwnserButton.Location = new Point(12, 74);
            FindAwnserButton.Name = "FindAwnserButton";
            FindAwnserButton.Size = new Size(189, 23);
            FindAwnserButton.TabIndex = 0;
            FindAwnserButton.Text = "Trouver la réponse";
            FindAwnserButton.UseVisualStyleBackColor = true;
            FindAwnserButton.Click += FindButtonClicked;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(12, 160);
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
            AwnserLabel.Location = new Point(12, 100);
            AwnserLabel.Name = "AwnserLabel";
            AwnserLabel.Size = new Size(52, 15);
            AwnserLabel.TabIndex = 2;
            AwnserLabel.Text = "Réponse";
            // 
            // SentenceInput
            // 
            SentenceInput.Location = new Point(12, 45);
            SentenceInput.Name = "SentenceInput";
            SentenceInput.PlaceholderText = "Entrez la phrase ici";
            SentenceInput.Size = new Size(562, 23);
            SentenceInput.TabIndex = 3;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(129, 160);
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
            label3.Location = new Point(12, 142);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 6;
            label3.Text = "Mode Automatique";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 265);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 7;
            label4.Text = "Instructions";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 290);
            label5.Name = "label5";
            label5.Size = new Size(496, 105);
            label5.TabIndex = 8;
            label5.Text = resources.GetString("label5.Text");
            // 
            // delayTrackBar
            // 
            delayTrackBar.LargeChange = 1;
            delayTrackBar.Location = new Point(12, 217);
            delayTrackBar.Name = "delayTrackBar";
            delayTrackBar.Size = new Size(147, 45);
            delayTrackBar.TabIndex = 9;
            delayTrackBar.Value = 1;
            delayTrackBar.Scroll += delayTrackBar_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(61, 199);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 10;
            label6.Text = "Delais";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(204, 199);
            label7.Name = "label7";
            label7.Size = new Size(65, 15);
            label7.TabIndex = 12;
            label7.Text = "Taux Erreur";
            // 
            // errorTrackBar
            // 
            errorTrackBar.Location = new Point(165, 217);
            errorTrackBar.Maximum = 100;
            errorTrackBar.Name = "errorTrackBar";
            errorTrackBar.Size = new Size(147, 45);
            errorTrackBar.SmallChange = 10;
            errorTrackBar.TabIndex = 11;
            errorTrackBar.TickFrequency = 10;
            errorTrackBar.Scroll += errorTrackBar_Scroll;
            // 
            // UpdateDataButton
            // 
            UpdateDataButton.Location = new Point(12, 408);
            UpdateDataButton.Name = "UpdateDataButton";
            UpdateDataButton.Size = new Size(189, 23);
            UpdateDataButton.TabIndex = 13;
            UpdateDataButton.Text = "Mettre à jour";
            UpdateDataButton.UseVisualStyleBackColor = true;
            UpdateDataButton.Click += UpdateDataButton_Click;
            // 
            // PhrasePosX
            // 
            PhrasePosX.Location = new Point(448, 160);
            PhrasePosX.MaxLength = 5;
            PhrasePosX.Name = "PhrasePosX";
            PhrasePosX.PlaceholderText = "x";
            PhrasePosX.Size = new Size(60, 23);
            PhrasePosX.TabIndex = 14;
            // 
            // PhrasePosY
            // 
            PhrasePosY.Location = new Point(514, 160);
            PhrasePosY.MaxLength = 5;
            PhrasePosY.Name = "PhrasePosY";
            PhrasePosY.PlaceholderText = "y";
            PhrasePosY.Size = new Size(60, 23);
            PhrasePosY.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(331, 164);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 16;
            label1.Text = "Debut de la Phrase";
            // 
            // SetPos1Button
            // 
            SetPos1Button.Location = new Point(580, 160);
            SetPos1Button.Name = "SetPos1Button";
            SetPos1Button.Size = new Size(61, 23);
            SetPos1Button.TabIndex = 17;
            SetPos1Button.Text = "Set";
            SetPos1Button.UseVisualStyleBackColor = true;
            SetPos1Button.Click += Set1Clicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(704, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 95);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 18;
            pictureBox1.TabStop = false;
            // 
            // SetPos2Button
            // 
            SetPos2Button.Location = new Point(580, 196);
            SetPos2Button.Name = "SetPos2Button";
            SetPos2Button.Size = new Size(61, 23);
            SetPos2Button.TabIndex = 22;
            SetPos2Button.Text = "Set";
            SetPos2Button.UseVisualStyleBackColor = true;
            SetPos2Button.Click += Set2Clicked;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(325, 200);
            label8.Name = "label8";
            label8.Size = new Size(111, 15);
            label8.TabIndex = 21;
            label8.Text = "Bouton pas d'erreur";
            // 
            // BoutonPosY
            // 
            BoutonPosY.Location = new Point(514, 196);
            BoutonPosY.MaxLength = 5;
            BoutonPosY.Name = "BoutonPosY";
            BoutonPosY.PlaceholderText = "y";
            BoutonPosY.Size = new Size(60, 23);
            BoutonPosY.TabIndex = 20;
            // 
            // BoutonPosX
            // 
            BoutonPosX.Location = new Point(448, 196);
            BoutonPosX.MaxLength = 5;
            BoutonPosX.Name = "BoutonPosX";
            BoutonPosX.PlaceholderText = "x";
            BoutonPosX.Size = new Size(60, 23);
            BoutonPosX.TabIndex = 19;
            // 
            // SetPos3Button
            // 
            SetPos3Button.Location = new Point(580, 232);
            SetPos3Button.Name = "SetPos3Button";
            SetPos3Button.Size = new Size(61, 23);
            SetPos3Button.TabIndex = 26;
            SetPos3Button.Text = "Set";
            SetPos3Button.UseVisualStyleBackColor = true;
            SetPos3Button.Click += Set3Clicked;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(351, 236);
            label9.Name = "label9";
            label9.Size = new Size(85, 15);
            label9.TabIndex = 25;
            label9.Text = "Bande Résultat";
            // 
            // ResultatPosY
            // 
            ResultatPosY.Location = new Point(514, 232);
            ResultatPosY.MaxLength = 5;
            ResultatPosY.Name = "ResultatPosY";
            ResultatPosY.PlaceholderText = "y";
            ResultatPosY.Size = new Size(60, 23);
            ResultatPosY.TabIndex = 24;
            // 
            // ResultatPosX
            // 
            ResultatPosX.Location = new Point(448, 232);
            ResultatPosX.MaxLength = 5;
            ResultatPosX.Name = "ResultatPosX";
            ResultatPosX.PlaceholderText = "x";
            ResultatPosX.Size = new Size(60, 23);
            ResultatPosX.TabIndex = 23;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(675, 175);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(154, 80);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 27;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(675, 261);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(154, 120);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 28;
            pictureBox3.TabStop = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(364, 265);
            label10.Name = "label10";
            label10.Size = new Size(263, 45);
            label10.TabIndex = 29;
            label10.Text = "Pour insérer une coordonnée:\r\n- Clicker sur Set\r\n- Click gauche sur la position encerclée en rouge\r\n";
            // 
            // DelayLabel
            // 
            DelayLabel.AutoSize = true;
            DelayLabel.Location = new Point(12, 247);
            DelayLabel.Name = "DelayLabel";
            DelayLabel.Size = new Size(111, 15);
            DelayLabel.TabIndex = 30;
            DelayLabel.Text = "Mode Automatique";
            DelayLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // MistakesLabel
            // 
            MistakesLabel.AutoSize = true;
            MistakesLabel.Location = new Point(165, 247);
            MistakesLabel.Name = "MistakesLabel";
            MistakesLabel.Size = new Size(111, 15);
            MistakesLabel.TabIndex = 31;
            MistakesLabel.Text = "Mode Automatique";
            MistakesLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(841, 448);
            Controls.Add(MistakesLabel);
            Controls.Add(DelayLabel);
            Controls.Add(label10);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(SetPos3Button);
            Controls.Add(label9);
            Controls.Add(ResultatPosY);
            Controls.Add(ResultatPosX);
            Controls.Add(SetPos2Button);
            Controls.Add(label8);
            Controls.Add(BoutonPosY);
            Controls.Add(BoutonPosX);
            Controls.Add(pictureBox1);
            Controls.Add(SetPos1Button);
            Controls.Add(label1);
            Controls.Add(PhrasePosY);
            Controls.Add(PhrasePosX);
            Controls.Add(UpdateDataButton);
            Controls.Add(label7);
            Controls.Add(errorTrackBar);
            Controls.Add(label6);
            Controls.Add(delayTrackBar);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(StopButton);
            Controls.Add(SentenceInput);
            Controls.Add(AwnserLabel);
            Controls.Add(StartButton);
            Controls.Add(FindAwnserButton);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)delayTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button FindAwnserButton;
        private Button StartButton;
        private Label AwnserLabel;
        private TextBox SentenceInput;
        private Button StopButton;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TrackBar delayTrackBar;
        private Label label6;
        private Label label7;
        private TrackBar errorTrackBar;
        private Button UpdateDataButton;
        private TextBox PhrasePosX;
        private TextBox PhrasePosY;
        private Label label1;
        private Button SetPos1Button;
        private PictureBox pictureBox1;
        private Button SetPos2Button;
        private Label label8;
        private TextBox BoutonPosY;
        private TextBox BoutonPosX;
        private Button SetPos3Button;
        private Label label9;
        private TextBox ResultatPosY;
        private TextBox ResultatPosX;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label10;
        private Label DelayLabel;
        private Label MistakesLabel;
    }
}