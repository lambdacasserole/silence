namespace Silence
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveControlButton = new Silence.ControlButton();
            this.openControlButton = new Silence.ControlButton();
            this.clearControlButton = new Silence.ControlButton();
            this.loopControlButton = new Silence.ControlButton();
            this.playControlButton = new Silence.ControlButton();
            this.stopControlButton = new Silence.ControlButton();
            this.recordControlButton = new Silence.ControlButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveControlButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openControlButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearControlButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopControlButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playControlButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopControlButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordControlButton)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveControlButton);
            this.panel1.Controls.Add(this.openControlButton);
            this.panel1.Controls.Add(this.clearControlButton);
            this.panel1.Controls.Add(this.loopControlButton);
            this.panel1.Controls.Add(this.playControlButton);
            this.panel1.Controls.Add(this.stopControlButton);
            this.panel1.Controls.Add(this.recordControlButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 64);
            this.panel1.TabIndex = 2;
            // 
            // saveControlButton
            // 
            this.saveControlButton.Image = ((System.Drawing.Image)(resources.GetObject("saveControlButton.Image")));
            this.saveControlButton.Location = new System.Drawing.Point(384, 0);
            this.saveControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.saveControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.saveControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.saveControlButton.Name = "saveControlButton";
            this.saveControlButton.ShapeColor = System.Drawing.Color.White;
            this.saveControlButton.ShapeImage = global::Silence.Properties.Resources.save;
            this.saveControlButton.Size = new System.Drawing.Size(64, 64);
            this.saveControlButton.TabIndex = 7;
            this.saveControlButton.TabStop = false;
            this.saveControlButton.Click += new System.EventHandler(this.saveControlButton_Click);
            // 
            // openControlButton
            // 
            this.openControlButton.Image = ((System.Drawing.Image)(resources.GetObject("openControlButton.Image")));
            this.openControlButton.Location = new System.Drawing.Point(320, 0);
            this.openControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.openControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.openControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.openControlButton.Name = "openControlButton";
            this.openControlButton.ShapeColor = System.Drawing.Color.White;
            this.openControlButton.ShapeImage = global::Silence.Properties.Resources.open;
            this.openControlButton.Size = new System.Drawing.Size(64, 64);
            this.openControlButton.TabIndex = 6;
            this.openControlButton.TabStop = false;
            this.openControlButton.Click += new System.EventHandler(this.openControlButton_Click);
            // 
            // clearControlButton
            // 
            this.clearControlButton.Image = ((System.Drawing.Image)(resources.GetObject("clearControlButton.Image")));
            this.clearControlButton.Location = new System.Drawing.Point(256, 0);
            this.clearControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.clearControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.clearControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.clearControlButton.Name = "clearControlButton";
            this.clearControlButton.ShapeColor = System.Drawing.Color.White;
            this.clearControlButton.ShapeImage = global::Silence.Properties.Resources.clear;
            this.clearControlButton.Size = new System.Drawing.Size(64, 64);
            this.clearControlButton.TabIndex = 5;
            this.clearControlButton.TabStop = false;
            this.clearControlButton.Click += new System.EventHandler(this.clearControlButton_Click);
            // 
            // loopControlButton
            // 
            this.loopControlButton.Image = ((System.Drawing.Image)(resources.GetObject("loopControlButton.Image")));
            this.loopControlButton.Location = new System.Drawing.Point(192, 0);
            this.loopControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.loopControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.loopControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.loopControlButton.Name = "loopControlButton";
            this.loopControlButton.ShapeColor = System.Drawing.Color.White;
            this.loopControlButton.ShapeImage = global::Silence.Properties.Resources.loop;
            this.loopControlButton.Size = new System.Drawing.Size(64, 64);
            this.loopControlButton.TabIndex = 4;
            this.loopControlButton.TabStop = false;
            this.loopControlButton.Click += new System.EventHandler(this.loopControlButton_Click);
            // 
            // playControlButton
            // 
            this.playControlButton.Image = ((System.Drawing.Image)(resources.GetObject("playControlButton.Image")));
            this.playControlButton.Location = new System.Drawing.Point(128, 0);
            this.playControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.playControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.playControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.playControlButton.Name = "playControlButton";
            this.playControlButton.ShapeColor = System.Drawing.Color.White;
            this.playControlButton.ShapeImage = global::Silence.Properties.Resources.play;
            this.playControlButton.Size = new System.Drawing.Size(64, 64);
            this.playControlButton.TabIndex = 3;
            this.playControlButton.TabStop = false;
            this.playControlButton.Click += new System.EventHandler(this.playControlButton_Click);
            // 
            // stopControlButton
            // 
            this.stopControlButton.Image = ((System.Drawing.Image)(resources.GetObject("stopControlButton.Image")));
            this.stopControlButton.Location = new System.Drawing.Point(64, 0);
            this.stopControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.stopControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.stopControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.stopControlButton.Name = "stopControlButton";
            this.stopControlButton.ShapeColor = System.Drawing.Color.White;
            this.stopControlButton.ShapeImage = global::Silence.Properties.Resources.stop;
            this.stopControlButton.Size = new System.Drawing.Size(64, 64);
            this.stopControlButton.TabIndex = 2;
            this.stopControlButton.TabStop = false;
            this.stopControlButton.Click += new System.EventHandler(this.stopControlButton_Click);
            // 
            // recordControlButton
            // 
            this.recordControlButton.Image = ((System.Drawing.Image)(resources.GetObject("recordControlButton.Image")));
            this.recordControlButton.Location = new System.Drawing.Point(0, 0);
            this.recordControlButton.MouseDownBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(23)))), ((int)(((byte)(61)))));
            this.recordControlButton.MouseOutBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.recordControlButton.MouseOverBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(33)))), ((int)(((byte)(89)))));
            this.recordControlButton.Name = "recordControlButton";
            this.recordControlButton.ShapeColor = System.Drawing.Color.White;
            this.recordControlButton.ShapeImage = global::Silence.Properties.Resources.record;
            this.recordControlButton.Size = new System.Drawing.Size(64, 64);
            this.recordControlButton.TabIndex = 1;
            this.recordControlButton.TabStop = false;
            this.recordControlButton.Click += new System.EventHandler(this.recordControlButton_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 64);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Silence";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.saveControlButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openControlButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clearControlButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopControlButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playControlButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopControlButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordControlButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ControlButton saveControlButton;
        private ControlButton openControlButton;
        private ControlButton clearControlButton;
        private ControlButton loopControlButton;
        private ControlButton playControlButton;
        private ControlButton stopControlButton;
        private ControlButton recordControlButton;

    }
}

