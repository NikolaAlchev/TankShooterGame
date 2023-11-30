namespace VPGame
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
            this.MoveBulltesTimer = new System.Windows.Forms.Timer(this.components);
            this.AutoFireTimer = new System.Windows.Forms.Timer(this.components);
            this.SpawnShapesTimer = new System.Windows.Forms.Timer(this.components);
            this.MovementTimer = new System.Windows.Forms.Timer(this.components);
            this.HealthRegenTimer = new System.Windows.Forms.Timer(this.components);
            this.ShootingTimer = new System.Windows.Forms.Timer(this.components);
            this.TimeTimer = new System.Windows.Forms.Timer(this.components);
            this.BossMoveTimer = new System.Windows.Forms.Timer(this.components);
            this.BossShootingTimer = new System.Windows.Forms.Timer(this.components);
            this.VisibleLableTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // MoveBulltesTimer
            // 
            this.MoveBulltesTimer.Interval = 10;
            this.MoveBulltesTimer.Tick += new System.EventHandler(this.MoveBulltesTimer_Tick);
            // 
            // AutoFireTimer
            // 
            this.AutoFireTimer.Interval = 400;
            this.AutoFireTimer.Tick += new System.EventHandler(this.AutoFireTimer_Tick);
            // 
            // SpawnShapesTimer
            // 
            this.SpawnShapesTimer.Interval = 500;
            this.SpawnShapesTimer.Tick += new System.EventHandler(this.SpawnShapesTimer_Tick);
            // 
            // MovementTimer
            // 
            this.MovementTimer.Interval = 10;
            this.MovementTimer.Tick += new System.EventHandler(this.MovementTimer_Tick);
            // 
            // HealthRegenTimer
            // 
            this.HealthRegenTimer.Interval = 1000;
            this.HealthRegenTimer.Tick += new System.EventHandler(this.HealthRegenTimer_Tick);
            // 
            // ShootingTimer
            // 
            this.ShootingTimer.Interval = 400;
            this.ShootingTimer.Tick += new System.EventHandler(this.ShootingTimer_Tick);
            // 
            // TimeTimer
            // 
            this.TimeTimer.Interval = 1000;
            this.TimeTimer.Tick += new System.EventHandler(this.TimeTimer_Tick);
            // 
            // BossMoveTimer
            // 
            this.BossMoveTimer.Interval = 10;
            this.BossMoveTimer.Tick += new System.EventHandler(this.BossMoveTimer_Tick);
            // 
            // BossShootingTimer
            // 
            this.BossShootingTimer.Interval = 1500;
            this.BossShootingTimer.Tick += new System.EventHandler(this.BossShootingTimer_Tick);
            // 
            // VisibleLableTimer
            // 
            this.VisibleLableTimer.Interval = 1000;
            this.VisibleLableTimer.Tick += new System.EventHandler(this.VisibleLable_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 474);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diep.io";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer MoveBulltesTimer;
        private System.Windows.Forms.Timer AutoFireTimer;
        private System.Windows.Forms.Timer SpawnShapesTimer;
        private System.Windows.Forms.Timer MovementTimer;
        private System.Windows.Forms.Timer HealthRegenTimer;
        private System.Windows.Forms.Timer ShootingTimer;
        private System.Windows.Forms.Timer TimeTimer;
        private System.Windows.Forms.Timer BossMoveTimer;
        private System.Windows.Forms.Timer BossShootingTimer;
        private System.Windows.Forms.Timer VisibleLableTimer;
    }
}

