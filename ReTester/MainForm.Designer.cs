namespace ReTester
{
    partial class MainForm
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
            this.MainViewport = new OpenTK.GLControl();
            this.BtnForceRefresh = new System.Windows.Forms.Button();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // MainViewport
            // 
            this.MainViewport.BackColor = System.Drawing.Color.Gray;
            this.MainViewport.Location = new System.Drawing.Point(16, 15);
            this.MainViewport.Margin = new System.Windows.Forms.Padding(5);
            this.MainViewport.Name = "MainViewport";
            this.MainViewport.Size = new System.Drawing.Size(1175, 768);
            this.MainViewport.TabIndex = 0;
            this.MainViewport.VSync = false;
            this.MainViewport.Paint += new System.Windows.Forms.PaintEventHandler(this.MainViewport_Paint);
            // 
            // BtnForceRefresh
            // 
            this.BtnForceRefresh.Location = new System.Drawing.Point(1199, 753);
            this.BtnForceRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.BtnForceRefresh.Name = "BtnForceRefresh";
            this.BtnForceRefresh.Size = new System.Drawing.Size(207, 28);
            this.BtnForceRefresh.TabIndex = 1;
            this.BtnForceRefresh.Text = "Force viewport refresh";
            this.BtnForceRefresh.UseVisualStyleBackColor = true;
            this.BtnForceRefresh.Click += new System.EventHandler(this.BtnForceRefresh_Click);
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 500;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 798);
            this.Controls.Add(this.BtnForceRefresh);
            this.Controls.Add(this.MainViewport);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl MainViewport;
        private System.Windows.Forms.Button BtnForceRefresh;
        private System.Windows.Forms.Timer MainTimer;
    }
}

