using System;
namespace WristbandCsharp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.intensityLabel = new System.Windows.Forms.Label();
            this.intensitySlider = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SpokenFeedback = new System.Windows.Forms.CheckBox();
            this.TonalFeedback = new System.Windows.Forms.CheckBox();
            this.vibrationSlider = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.vibLabel = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button36 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SkipResearch = new System.Windows.Forms.Button();
            this.yesResearch = new System.Windows.Forms.Button();
            this.WrongItem = new System.Windows.Forms.Button();
            this.button42 = new System.Windows.Forms.Button();
            this.button41 = new System.Windows.Forms.Button();
            this.button40 = new System.Windows.Forms.Button();
            this.button39 = new System.Windows.Forms.Button();
            this.button38 = new System.Windows.Forms.Button();
            this.button37 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.FAIL = new System.Windows.Forms.Button();
            this.nextItem = new System.Windows.Forms.Button();
            this.motorActive = new System.Windows.Forms.Label();
            this.CantHear = new System.Windows.Forms.Button();
            this.CanHear = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intensitySlider)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vibrationSlider)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(960, 585);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.intensityLabel);
            this.groupBox1.Controls.Add(this.intensitySlider);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(7, 590);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(307, 79);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feedback Settings";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // intensityLabel
            // 
            this.intensityLabel.AutoSize = true;
            this.intensityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intensityLabel.ForeColor = System.Drawing.Color.Red;
            this.intensityLabel.Location = new System.Drawing.Point(259, 14);
            this.intensityLabel.Name = "intensityLabel";
            this.intensityLabel.Size = new System.Drawing.Size(21, 13);
            this.intensityLabel.TabIndex = 14;
            this.intensityLabel.Text = "50";
            // 
            // intensitySlider
            // 
            this.intensitySlider.Location = new System.Drawing.Point(165, 29);
            this.intensitySlider.Name = "intensitySlider";
            this.intensitySlider.Size = new System.Drawing.Size(122, 45);
            this.intensitySlider.TabIndex = 10;
            this.intensitySlider.Value = 5;
            this.intensitySlider.Scroll += new System.EventHandler(this.intensitySlider_Scroll);
            this.intensitySlider.ValueChanged += new System.EventHandler(this.intensitySlider_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Motor Duration :";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // button6
            // 
            this.button6.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button6.Location = new System.Drawing.Point(80, 26);
            this.button6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 23);
            this.button6.TabIndex = 12;
            this.button6.Text = "Refresh";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button4.Location = new System.Drawing.Point(80, 52);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 22);
            this.button4.TabIndex = 11;
            this.button4.Text = "Disconnect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button5.Location = new System.Drawing.Point(4, 52);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(65, 22);
            this.button5.TabIndex = 10;
            this.button5.Text = "Connect";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(4, 29);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(65, 21);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.Text = "OFF";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Arudino Status";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(121, 266);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "←";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(809, 266);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 36);
            this.button2.TabIndex = 4;
            this.button2.Text = "→";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(452, 539);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 36);
            this.button3.TabIndex = 5;
            this.button3.Text = "↓";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(452, 8);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(34, 36);
            this.button7.TabIndex = 6;
            this.button7.Text = "↑";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.Location = new System.Drawing.Point(452, 266);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(34, 36);
            this.button8.TabIndex = 7;
            this.button8.Text = "⇈";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SpokenFeedback);
            this.groupBox2.Controls.Add(this.TonalFeedback);
            this.groupBox2.Location = new System.Drawing.Point(381, 591);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 79);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Audio Feedback";
            // 
            // SpokenFeedback
            // 
            this.SpokenFeedback.AutoSize = true;
            this.SpokenFeedback.Location = new System.Drawing.Point(7, 32);
            this.SpokenFeedback.Name = "SpokenFeedback";
            this.SpokenFeedback.Size = new System.Drawing.Size(114, 17);
            this.SpokenFeedback.TabIndex = 1;
            this.SpokenFeedback.Text = "Spoken Feedback";
            this.SpokenFeedback.UseVisualStyleBackColor = true;
            // 
            // TonalFeedback
            // 
            this.TonalFeedback.AutoSize = true;
            this.TonalFeedback.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TonalFeedback.Location = new System.Drawing.Point(7, 13);
            this.TonalFeedback.Name = "TonalFeedback";
            this.TonalFeedback.Size = new System.Drawing.Size(104, 17);
            this.TonalFeedback.TabIndex = 0;
            this.TonalFeedback.Text = "Tonal Feedback";
            this.TonalFeedback.UseVisualStyleBackColor = true;
            // 
            // vibrationSlider
            // 
            this.vibrationSlider.LargeChange = 1;
            this.vibrationSlider.Location = new System.Drawing.Point(325, 616);
            this.vibrationSlider.Name = "vibrationSlider";
            this.vibrationSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.vibrationSlider.Size = new System.Drawing.Size(45, 54);
            this.vibrationSlider.TabIndex = 10;
            this.vibrationSlider.Value = 5;
            this.vibrationSlider.Scroll += new System.EventHandler(this.vibrationSlider_Scroll);
            this.vibrationSlider.ValueChanged += new System.EventHandler(this.vibrationSlider_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 590);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Vibration";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // vibLabel
            // 
            this.vibLabel.AutoSize = true;
            this.vibLabel.ForeColor = System.Drawing.Color.Red;
            this.vibLabel.Location = new System.Drawing.Point(336, 604);
            this.vibLabel.Name = "vibLabel";
            this.vibLabel.Size = new System.Drawing.Size(19, 13);
            this.vibLabel.TabIndex = 13;
            this.vibLabel.Text = "50";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1400, 12);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(167, 49);
            this.button10.TabIndex = 17;
            this.button10.Text = "Introduction";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1473, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 26);
            this.label5.TabIndex = 18;
            this.label5.Text = " |\r\n V\r\n";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click_2);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button36);
            this.groupBox3.Controls.Add(this.button35);
            this.groupBox3.Controls.Add(this.button18);
            this.groupBox3.Controls.Add(this.button9);
            this.groupBox3.Location = new System.Drawing.Point(1499, 558);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shelf Search Functions";
            // 
            // button36
            // 
            this.button36.Location = new System.Drawing.Point(10, 76);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(96, 23);
            this.button36.TabIndex = 3;
            this.button36.Text = "Can\'t find twice";
            this.button36.UseVisualStyleBackColor = true;
            this.button36.Click += new System.EventHandler(this.button36_Click);
            // 
            // button35
            // 
            this.button35.Location = new System.Drawing.Point(109, 50);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(75, 23);
            this.button35.TabIndex = 2;
            this.button35.Text = "Scan Again";
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Click += new System.EventHandler(this.button35_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(10, 50);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(86, 23);
            this.button18.TabIndex = 1;
            this.button18.Text = "Skip Item";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(10, 21);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(145, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "Search Failed to Find Item";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click_2);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SkipResearch);
            this.groupBox4.Controls.Add(this.yesResearch);
            this.groupBox4.Controls.Add(this.WrongItem);
            this.groupBox4.Controls.Add(this.button42);
            this.groupBox4.Controls.Add(this.button41);
            this.groupBox4.Controls.Add(this.button40);
            this.groupBox4.Controls.Add(this.button39);
            this.groupBox4.Controls.Add(this.button38);
            this.groupBox4.Controls.Add(this.button37);
            this.groupBox4.Controls.Add(this.button21);
            this.groupBox4.Controls.Add(this.button20);
            this.groupBox4.Controls.Add(this.button19);
            this.groupBox4.Location = new System.Drawing.Point(988, 485);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(502, 173);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Barcode Verification";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // SkipResearch
            // 
            this.SkipResearch.Location = new System.Drawing.Point(241, 100);
            this.SkipResearch.Name = "SkipResearch";
            this.SkipResearch.Size = new System.Drawing.Size(75, 23);
            this.SkipResearch.TabIndex = 91;
            this.SkipResearch.Text = "No, skip";
            this.SkipResearch.UseVisualStyleBackColor = true;
            this.SkipResearch.Click += new System.EventHandler(this.SkipResearch_Click);
            // 
            // yesResearch
            // 
            this.yesResearch.Location = new System.Drawing.Point(140, 100);
            this.yesResearch.Name = "yesResearch";
            this.yesResearch.Size = new System.Drawing.Size(95, 23);
            this.yesResearch.TabIndex = 90;
            this.yesResearch.Text = "Yes, Re-search";
            this.yesResearch.UseVisualStyleBackColor = true;
            this.yesResearch.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // WrongItem
            // 
            this.WrongItem.Location = new System.Drawing.Point(199, 71);
            this.WrongItem.Name = "WrongItem";
            this.WrongItem.Size = new System.Drawing.Size(75, 23);
            this.WrongItem.TabIndex = 89;
            this.WrongItem.Text = "Wrong Item";
            this.WrongItem.UseVisualStyleBackColor = true;
            this.WrongItem.Click += new System.EventHandler(this.WrongItem_Click);
            // 
            // button42
            // 
            this.button42.Location = new System.Drawing.Point(421, 42);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(75, 23);
            this.button42.TabIndex = 88;
            this.button42.Text = "Re-Do No";
            this.button42.UseVisualStyleBackColor = true;
            this.button42.Click += new System.EventHandler(this.button42_Click);
            // 
            // button41
            // 
            this.button41.Location = new System.Drawing.Point(341, 42);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(74, 23);
            this.button41.TabIndex = 7;
            this.button41.Text = "Re-Do Yes";
            this.button41.UseVisualStyleBackColor = true;
            this.button41.Click += new System.EventHandler(this.button41_Click);
            // 
            // button40
            // 
            this.button40.Location = new System.Drawing.Point(183, 10);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(107, 23);
            this.button40.TabIndex = 6;
            this.button40.Text = "Start Verification";
            this.button40.UseVisualStyleBackColor = true;
            this.button40.Click += new System.EventHandler(this.button40_Click);
            // 
            // button39
            // 
            this.button39.Location = new System.Drawing.Point(8, 71);
            this.button39.Name = "button39";
            this.button39.Size = new System.Drawing.Size(121, 23);
            this.button39.TabIndex = 5;
            this.button39.Text = "No. don\'t use feature";
            this.button39.UseVisualStyleBackColor = true;
            this.button39.Click += new System.EventHandler(this.button39_Click);
            // 
            // button38
            // 
            this.button38.Location = new System.Drawing.Point(8, 42);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(106, 23);
            this.button38.TabIndex = 4;
            this.button38.Text = "Yes, use feature";
            this.button38.UseVisualStyleBackColor = true;
            this.button38.Click += new System.EventHandler(this.button38_Click);
            // 
            // button37
            // 
            this.button37.Location = new System.Drawing.Point(8, 15);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(75, 23);
            this.button37.TabIndex = 3;
            this.button37.Text = "Use verify?";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(379, 71);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(117, 23);
            this.button21.TabIndex = 2;
            this.button21.Text = "Verify Failed Twice";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(421, 13);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 23);
            this.button20.TabIndex = 1;
            this.button20.Text = "Verify Failed";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(199, 39);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 0;
            this.button19.Text = "Verified";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(1003, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(136, 31);
            this.label18.TabIndex = 42;
            this.label18.Text = "ALEX 1.0";
            // 
            // FAIL
            // 
            this.FAIL.BackColor = System.Drawing.Color.Red;
            this.FAIL.Location = new System.Drawing.Point(1238, 25);
            this.FAIL.Name = "FAIL";
            this.FAIL.Size = new System.Drawing.Size(75, 23);
            this.FAIL.TabIndex = 43;
            this.FAIL.Text = "FAIL";
            this.FAIL.UseVisualStyleBackColor = false;
            this.FAIL.Click += new System.EventHandler(this.FAIL_Click);
            // 
            // nextItem
            // 
            this.nextItem.Location = new System.Drawing.Point(1530, 495);
            this.nextItem.Name = "nextItem";
            this.nextItem.Size = new System.Drawing.Size(124, 50);
            this.nextItem.TabIndex = 44;
            this.nextItem.Text = "Next Item";
            this.nextItem.UseVisualStyleBackColor = true;
            this.nextItem.Click += new System.EventHandler(this.nextItem_Click);
            // 
            // motorActive
            // 
            this.motorActive.AutoSize = true;
            this.motorActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motorActive.ForeColor = System.Drawing.Color.Red;
            this.motorActive.Location = new System.Drawing.Point(587, 611);
            this.motorActive.Name = "motorActive";
            this.motorActive.Size = new System.Drawing.Size(309, 37);
            this.motorActive.TabIndex = 46;
            this.motorActive.Text = "Motor is NOT active.";
            this.motorActive.Click += new System.EventHandler(this.motorActive_Click);
            // 
            // CantHear
            // 
            this.CantHear.Location = new System.Drawing.Point(1319, 38);
            this.CantHear.Name = "CantHear";
            this.CantHear.Size = new System.Drawing.Size(75, 23);
            this.CantHear.TabIndex = 47;
            this.CantHear.Text = "Can\'t Hear";
            this.CantHear.UseVisualStyleBackColor = true;
            this.CantHear.Click += new System.EventHandler(this.CantHear_Click);
            // 
            // CanHear
            // 
            this.CanHear.Location = new System.Drawing.Point(1319, 9);
            this.CanHear.Name = "CanHear";
            this.CanHear.Size = new System.Drawing.Size(75, 23);
            this.CanHear.TabIndex = 48;
            this.CanHear.Text = "Can Hear";
            this.CanHear.UseVisualStyleBackColor = true;
            this.CanHear.Click += new System.EventHandler(this.CanHear_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button17);
            this.groupBox5.Controls.Add(this.button16);
            this.groupBox5.Controls.Add(this.button15);
            this.groupBox5.Controls.Add(this.button14);
            this.groupBox5.Controls.Add(this.button13);
            this.groupBox5.Controls.Add(this.button12);
            this.groupBox5.Controls.Add(this.button11);
            this.groupBox5.Location = new System.Drawing.Point(1705, 485);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 172);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Demo/Training";
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(119, 139);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 6;
            this.button17.Text = "Let\'s Start";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(7, 139);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 5;
            this.button16.Text = "Try Again";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(7, 110);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(187, 23);
            this.button15.TabIndex = 4;
            this.button15.Text = "Spoken and Haptic";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(7, 86);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(187, 23);
            this.button14.TabIndex = 3;
            this.button14.Text = "Tone and Haptic";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(7, 62);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(187, 23);
            this.button13.TabIndex = 2;
            this.button13.Text = "Haptic ONLY";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click_1);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(7, 38);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(187, 23);
            this.button12.TabIndex = 1;
            this.button12.Text = "Spoken ONLY";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click_1);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(7, 14);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(187, 23);
            this.button11.TabIndex = 0;
            this.button11.Text = "Tone ONLY";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click_2);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(1573, 12);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(159, 49);
            this.button22.TabIndex = 50;
            this.button22.Text = "Introduction, Pt. 2";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(1739, 13);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(160, 48);
            this.button23.TabIndex = 51;
            this.button23.Text = "Introduction, Pt. 3 Tap and Grab)";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1966, 679);
            this.Controls.Add(this.button23);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.CanHear);
            this.Controls.Add(this.CantHear);
            this.Controls.Add(this.motorActive);
            this.Controls.Add(this.nextItem);
            this.Controls.Add(this.FAIL);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.vibLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vibrationSlider);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Tonal vs. Haptic - Wizard of Oz";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intensitySlider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vibrationSlider)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
        }

        private void radioButton4_CheckedChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void radioButton5_CheckedChanged_1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button10_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button9_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button3_Click_1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label1_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label2_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void radioButton1_CheckedChanged_1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void radioButton2_CheckedChanged_1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void radioButton3_CheckedChanged_1(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void groupBox2_Enter(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label3_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label4_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            arduino = new Arduino((string)comboBox2.SelectedItem);
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            //This is the disconnect button.
            arduino.ClosePort();
            arduino = null;
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {
        }

        private void label5_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {

            double h = pictureBox1.Height;//cap.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT);
            double w = pictureBox1.Width;//cap.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH);

            Console.WriteLine(MousePosition.X/w);
            Console.WriteLine(MousePosition.Y/h);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox SpokenFeedback;
        private System.Windows.Forms.CheckBox TonalFeedback;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar intensitySlider;
        private System.Windows.Forms.Label intensityLabel;
        private System.Windows.Forms.TrackBar vibrationSlider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label vibLabel;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button35;
        private System.Windows.Forms.Button button36;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button39;
        private System.Windows.Forms.Button button38;
        private System.Windows.Forms.Button button40;
        private System.Windows.Forms.Button button42;
        private System.Windows.Forms.Button button41;
        private System.Windows.Forms.Button FAIL;
        private System.Windows.Forms.Button nextItem;
        private System.Windows.Forms.Label motorActive;
        private System.Windows.Forms.Button CantHear;
        private System.Windows.Forms.Button CanHear;
        private System.Windows.Forms.Button WrongItem;
        private System.Windows.Forms.Button SkipResearch;
        private System.Windows.Forms.Button yesResearch;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
    }
}