
namespace Insta_scrape
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_start = new System.Windows.Forms.Button();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_stop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.interval_input = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.interval_input)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(12, 115);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(84, 29);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(70, 12);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(122, 20);
            this.textBox_Username.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(108, 115);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(84, 29);
            this.button_stop.TabIndex = 3;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Interval (minutes)";
            // 
            // interval_input
            // 
            this.interval_input.Location = new System.Drawing.Point(147, 44);
            this.interval_input.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.interval_input.Name = "interval_input";
            this.interval_input.Size = new System.Drawing.Size(45, 20);
            this.interval_input.TabIndex = 6;
            this.interval_input.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Insta Scrape";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 156);
            this.Controls.Add(this.interval_input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.button_start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Insta Scrape";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.interval_input)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown interval_input;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

