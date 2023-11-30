using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace VPGame
{
    public partial class Form1 : Form
    {
        Scene scene;
        private int PreviousLevel { get; set; } = 0;
        public int PointsToUse { get; set; } = 0;
        public bool Shooting { get; set; } = false;
        public Point LastMousePosition { get; set; } = new Point();
        private HashSet<Keys> KeysPressed = new HashSet<Keys>();
        private bool Movement = false;
        Panel panel;
        List<Label> labelsUpgrades = new List<Label>();
        List<Button> buttonUpgrades = new List<Button>();
        List<String> upgrades = new List<String>() { "Health Regen", "Max Health","Body Damage", "Bullet Speed", "Bullet Damage", "Reload Speed", "Movement Speed"};
        List<String> upgradesColors = new List<String>() { "#f0b494", "#ec6cf0", "#9a6cf0", "#6c96f0", "#f06c6c", "#98f06c", "#6cf0ec" };
        List<int> givenPoints = new List<int>() { 0,0,0,0,0,0,0};
        Label label;
        Label autoFireLabel;
        Label bossSpawnedLabel;
        public int BossLabelVisible { get; set; } = 5;
        Label time;
        public int Difficulty { get; set; } = 1;
        public int Time { get; set; } = 180;
        public Form1(int difficulty)
        {
            Difficulty = difficulty;
            time = new Label();
            switch (Difficulty)
            {
                case 0:
                    Time = 210;
                    time.Text = "03:30";
                    break;
                case 1:
                    Time = 150;
                    time.Text = "02:30";
                    break;
                default:
                    Time = 90;
                    time.Text = "01:30";
                    break;
            }
            scene = new Scene(this.Width, this.Height, difficulty);
            label = new Label();
            
            panel = new Panel();
            autoFireLabel = new Label();
            bossSpawnedLabel = new Label();
            InitializeComponent();

            showUpgrades();
            MoveBulltesTimer.Start();
            SpawnShapesTimer.Start();
            HealthRegenTimer.Start();

            TimeTimer.Start();
            this.DoubleBuffered = true;
            scene.GenerateStartingShapes();
            
            label.Location = new Point(Width/2 - 200, Height - 83);
            label.Font = new Font(Font.FontFamily, 16);
            label.Width = 400;
            label.Height = 26;
            label.Text = "Level 0 Score 0";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.AutoSize = false;
            label.ForeColor = Color.White;
            label.BackColor = Color.Transparent;
            label.BringToFront();
            this.Controls.Add(label);

            time.Location = new Point(Width - 120, 10);
            time.Font = new Font(Font.FontFamily, 22);
            time.Width = 100;
            time.Height = 35;
            
            time.TextAlign = ContentAlignment.MiddleCenter;
            time.AutoSize = false;
            time.ForeColor = Color.Black;
            time.BackColor = Color.Transparent;
            time.BringToFront();
            this.Controls.Add(time);

            autoFireLabel.Location = new Point(Width / 2 - 75, 25);
            autoFireLabel.Width = 150;
            autoFireLabel.Height = 26;
            autoFireLabel.BackColor = Color.FromArgb(220,133, 133, 223);
            autoFireLabel.ForeColor = Color.White;
            autoFireLabel.Text = "Auto Fire:ON";
            autoFireLabel.TextAlign = ContentAlignment.MiddleCenter;
            autoFireLabel.Font = new Font(Font.FontFamily, 14,FontStyle.Bold);
            autoFireLabel.Visible = false;
            autoFireLabel.BringToFront();
            this.Controls.Add(autoFireLabel);

            bossSpawnedLabel.Location = new Point(Width / 2 - 160, 60);
            bossSpawnedLabel.Width = 320;
            bossSpawnedLabel.Height = 26;
            bossSpawnedLabel.BackColor = Color.FromArgb(220, 230, 121, 125);
            bossSpawnedLabel.ForeColor = Color.White;
            bossSpawnedLabel.Text = "The Boss Has Been Spawned";
            bossSpawnedLabel.TextAlign = ContentAlignment.MiddleCenter;
            bossSpawnedLabel.Font = new Font(Font.FontFamily, 14, FontStyle.Bold);
            bossSpawnedLabel.Visible = false;
            bossSpawnedLabel.BringToFront();
            this.Controls.Add(bossSpawnedLabel);

        }
        public void showUpgrades()
        {
            this.SuspendLayout();
            panel.Visible = false;
            panel.Location = new Point(2, this.Height - 220);
            panel.BackColor = ColorTranslator.FromHtml("#3d3d3d");
            panel.Width = 203;
            panel.Height = 168;
            panel.Paint += Panel_Paint;
            this.Controls.Add(panel);
            int beginningHeight = 1;
            for (int i=0;i<upgrades.Count; i++)
            {
                Label label = new Label();
                Button button = new Button();

 
                button.Location = new Point(172, beginningHeight);
                button.Width = 30;
                button.Height = 22;
                button.Text = "+";
                button.Font = new Font(button.Font.FontFamily, button.Font.Size, FontStyle.Bold);
                button.ForeColor = ColorTranslator.FromHtml("#3d3d3d");
                button.BackColor = ColorTranslator.FromHtml(upgradesColors[i]);
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.FlatStyle = FlatStyle.Flat;
                button.MouseUp += (s, args) =>
                {
                    if (args.Button == MouseButtons.Left)
                    {
                        int j = buttonUpgrades.IndexOf(button);
                        givenPoints[j] += 1;
                        PointsToUse--;
                        this.ActiveControl = null;
                        switch (j)
                        {
                            case 0:
                                scene.Tank.Regen = 20 + givenPoints[j] * 5;
                                break;
                            case 1:
                                scene.Tank.MaxHealth = 500 + givenPoints[j] * 100;
                                scene.Tank.Health += 100;
                                break;
                            case 2:
                                scene.Tank.BodyDamage = 50 + givenPoints[j] * 10;
                                break;
                            case 3:
                                scene.BulletSpeed = 4 + givenPoints[j];
                                scene.MaxPixelsTraveled = 600 + givenPoints[j] * 30;
                                break;
                            case 4:
                                scene.BulletDamage = 50 + givenPoints[j] * 10;
                                break;
                            case 5:
                                AutoFireTimer.Interval = 400 - givenPoints[j]*20;
                                ShootingTimer.Interval = 400 - givenPoints[j]*20;
                                break;
                            default:
                                scene.Speed = 3 + givenPoints[j];
                                scene.UpdateList();
                                break;
                        }
                        
                        if (givenPoints[j] == 10)
                        {
                            button.Enabled = false;
                            button.BackColor = ColorTranslator.FromHtml("#9c9c9c");
                        }
                        panel.Invalidate();
                    }
                };

                label.Location = new Point(1, beginningHeight);
                label.Font = new Font(Font.FontFamily, 12);
                label.Width = 171;
                label.Height = 22;
                label.Text = upgrades[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.AutoSize = false;
                label.ForeColor = Color.White;
                label.BackColor = Color.Transparent;
                label.BorderStyle = BorderStyle.FixedSingle;
                
                
                label.TabStop = false;
                button.TabStop = false;

                labelsUpgrades.Add(label);
                buttonUpgrades.Add(button);

                beginningHeight += label.Height + 2;
            }
            panel.Controls.AddRange(labelsUpgrades.ToArray());
            panel.Controls.AddRange(buttonUpgrades.ToArray());

            this.ResumeLayout(true);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            scene.Draw(e.Graphics);
            if (PreviousLevel < scene.CurrentLevel)
            {
                PointsToUse += scene.CurrentLevel - PreviousLevel;
                PreviousLevel = scene.CurrentLevel;
                panel.Visible = true;
            }
            label.Text = $"Level {scene.CurrentLevel} Score {scene.Points}";
            if (PointsToUse == 0)
            {
                panel.Visible = false;
                
            }
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            int beginningHeight = 1;
            for (int i = 0; i < upgradesColors.Count; i++)
            {
                int offset = 1;
                Brush b = new SolidBrush(ColorTranslator.FromHtml(upgradesColors[i]));
                for (int j = 0; j < givenPoints[i]; j++)
                {
                    e.Graphics.FillRectangle(b,offset,beginningHeight,16,20);
                    offset += 17;

                }
                beginningHeight += 24;
                b.Dispose();
            }
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        { 
            scene.Tank.Width = this.Width;
            scene.Tank.Height = this.Height;
            scene.Boss.Width = this.Width;
            scene.Boss.Height = this.Height;
            scene.Width = this.Width;
            scene.Height = this.Height;
            label.Location = new Point(Width / 2 - 200, Height - 83);
            time.Location = new Point(Width - 120, 10);
            autoFireLabel.Location = new Point(Width / 2 - 75, 25);
            bossSpawnedLabel.Location = new Point(Width / 2 - 160, 60);
            panel.Location = new Point(10, this.Height - 220);
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            LastMousePosition = e.Location;
            double angle = Math.Atan2(e.Y - this.Height / 2, e.X - this.Width / 2);
            scene.Tank.Angle = (float)angle;
            
        }

        private void MoveBulltesTimer_Tick(object sender, EventArgs e)
        {
            scene.MoveBullets();
            scene.Hit();
            CheckBossHealth();
            scene.moveBossBullets();
            scene.BossBulletsHit();
            scene.DeleteBullets();
            scene.DeleteBossBullets();
            Invalidate();
        }

        private void CheckBossHealth()
        {
            if (scene.Boss.CurrentHealth <= 0)
            {
                MoveBulltesTimer.Stop();
                AutoFireTimer.Stop();
                ShootingTimer.Stop();
                SpawnShapesTimer.Stop();
                MovementTimer.Stop();
                HealthRegenTimer.Stop();
                TimeTimer.Stop();
                BossShootingTimer.Stop();
                BossMoveTimer.Stop();
                VisibleLableTimer.Stop();
                MessageBox.Show("Congratulations, You Won !!! ", "Victory!", MessageBoxButtons.OK);
                DialogResult s = MessageBox.Show("Do you want to play another game?", "New Game", MessageBoxButtons.YesNo);
                if (s == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    KeysPressed = new HashSet<Keys>();
                    labelsUpgrades = new List<Label>();
                    buttonUpgrades = new List<Button>();
                    autoFireLabel.Visible = false;
                    bossSpawnedLabel.Visible = false;
                    BossLabelVisible = 5;
                    panel.Dispose();
                    panel = new Panel();
                    Shooting = false;
                    AutoFireTimer.Interval = 400;
                    ShootingTimer.Interval = 400;
                    scene = new Scene(this.Width, this.Height, Difficulty);
                    MoveBulltesTimer.Start();
                    SpawnShapesTimer.Start();
                    HealthRegenTimer.Start();
                    TimeTimer.Start();
                    scene.GenerateStartingShapes();
                    PointsToUse = 0;
                    PreviousLevel = 0;
                    givenPoints = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
                    showUpgrades();
                    switch (Difficulty)
                    {
                        case 0:
                            Time = 210;
                            time.Text = "03:30";
                            break;
                        case 1:
                            Time = 150;
                            time.Text = "02:30";
                            break;
                        default:
                            Time = 90;
                            time.Text = "01:30";
                            break;
                    }
                    time.ForeColor = Color.Black;
                    Invalidate();

                }
            }
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E && !scene.AutoFire) {
                Shooting = false;
                ShootingTimer.Stop();
                scene.AutoFire = !scene.AutoFire;
                autoFireLabel.Visible = true;
                AutoFireTimer.Start();
            }
            else if (e.KeyCode == Keys.E){
                scene.AutoFire = !scene.AutoFire;
                AutoFireTimer.Stop();
                autoFireLabel.Visible= false;
            }


            KeysPressed.Add(e.KeyCode);

            if (!Movement)
            {
                MovementTimer.Start();
            }
        }

        private void AutoFireTimer_Tick(object sender, EventArgs e)
        {
            if(scene.AutoFire)
            {
                scene.AddBullet(LastMousePosition);
            }
            Invalidate();
        }

        private void SpawnShapesTimer_Tick(object sender, EventArgs e)
        {
            scene.AddShape();
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            KeysPressed.Remove(e.KeyCode);
            if (KeysPressed.Count == 0)
            {
                MovementTimer.Stop();
            }
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (KeysPressed.Contains(Keys.W) && KeysPressed.Contains(Keys.D))
            {
                scene.moveUpRight();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.W) && KeysPressed.Contains(Keys.A))
            {
                scene.moveUpLeft();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.S) && KeysPressed.Contains(Keys.D))
            {
                scene.moveDownRight();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.S) && KeysPressed.Contains(Keys.A))
            {
                scene.moveDownLeft();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.W))
            {
                scene.moveUp();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.S))
            {
                scene.moveDown();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.A))
            {
                scene.moveLeft();
                scene.bodyHit();
            }
            else if (KeysPressed.Contains(Keys.D))
            {
                scene.moveRight();
                scene.bodyHit();
            }
            checkTankHealth();
            Invalidate();
        }
        public void checkTankHealth()
        {
            if (scene.Tank.Health <= 0)
            {
                MoveBulltesTimer.Stop();
                AutoFireTimer.Stop();
                ShootingTimer.Stop();
                SpawnShapesTimer.Stop();
                MovementTimer.Stop();
                HealthRegenTimer.Stop();
                TimeTimer.Stop();
                BossShootingTimer.Stop();
                BossMoveTimer.Stop();
                VisibleLableTimer.Stop();
                DialogResult s = MessageBox.Show("Do you want to play another game?", "Game Over", MessageBoxButtons.YesNo);
                if (s == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    KeysPressed = new HashSet<Keys>();
                    labelsUpgrades = new List<Label>();
                    buttonUpgrades = new List<Button>();
                    autoFireLabel.Visible = false;
                    bossSpawnedLabel.Visible = false;
                    BossLabelVisible = 5;
                    panel.Dispose();
                    panel = new Panel();
                    Shooting = false;
                    AutoFireTimer.Interval = 400;
                    ShootingTimer.Interval = 400;
                    scene = new Scene(this.Width, this.Height, Difficulty);
                    MoveBulltesTimer.Start();
                    SpawnShapesTimer.Start();
                    HealthRegenTimer.Start();
                    TimeTimer.Start();
                    scene.GenerateStartingShapes();
                    PointsToUse = 0;
                    PreviousLevel = 0;
                    givenPoints = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
                    showUpgrades();
                    switch (Difficulty)
                    {
                        case 0:
                            Time = 210;
                            time.Text = "03:30";
                            break;
                        case 1:
                            Time = 150;
                            time.Text = "02:30";
                            break;
                        default:
                            Time = 90;
                            time.Text = "01:30";
                            break;
                    }
                    time.ForeColor = Color.Black;
                    Invalidate();

                }
            }
        }

        private void HealthRegenTimer_Tick(object sender, EventArgs e)
        {
            if (scene.Tank.Health + scene.Tank.Regen > scene.Tank.MaxHealth)
            {
                scene.Tank.Health = scene.Tank.MaxHealth;
            }
            else
            {
                scene.Tank.Health += scene.Tank.Regen;
            }
            if (scene.timeOver)
            {
                if (scene.Boss.CurrentHealth + 40 > scene.Boss.MaxHealth)
                {
                    scene.Boss.CurrentHealth = scene.Boss.MaxHealth;
                }
                else
                {
                    scene.Boss.CurrentHealth += 40;
                }
            }
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            autoFireLabel.Visible = false;
            scene.AutoFire = false;
            AutoFireTimer.Stop();
            Shooting = true;
            ShootingTimer.Start();
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Shooting = false;
            ShootingTimer.Stop();                
            
        }

        private void ShootingTimer_Tick(object sender, EventArgs e)
        {
            if (Shooting)
            {
                scene.AddBullet(LastMousePosition);
            }
            Invalidate();
        }
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            if(Time > 0)
            {
                time.Text = $"{(Time / 60):D2}:{(Time % 60):D2}";
                Time--;
            }
            else
            {
                time.Text = "00:00";
                bossSpawnedLabel.Visible = true;
                time.ForeColor = Color.Red;
                scene.timeOver = true;
                BossMoveTimer.Start();
                BossShootingTimer.Start();
                VisibleLableTimer.Start();
                TimeTimer.Stop();
            }

            
        }
        private void VisibleLable_Tick(object sender, EventArgs e)
        {
            if(BossLabelVisible >= 0)
            {
                BossLabelVisible--;
            }
            else
            {
                bossSpawnedLabel.Visible = false;
                VisibleLableTimer.Stop();
            }
        }
        private void BossMoveTimer_Tick(object sender, EventArgs e)
        {
            scene.moveBoss();
            scene.BossBodyHit();
            checkTankHealth();
        }

        private void BossShootingTimer_Tick(object sender, EventArgs e)
        {
            scene.AddBossBullet();
        }

        
    }
}
