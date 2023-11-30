using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPGame
{
    public class BossTank
    {
        public Point Center { get; set; }
        public int Radius { get; set; }
        public Point MoveToPlayerPoint { get; set; }
        public int GunsLength { get; set; }
        public double Angle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Speed { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        Random random = new Random();
        public BossTank(int width, int height, int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    CurrentHealth = 3000;
                    MaxHealth = 3000;
                    Speed = 3;
                    break;
                case 1:
                    CurrentHealth = 5000;
                    MaxHealth = 5000;
                    Speed = 4;
                    break;
                default:
                    CurrentHealth = 10000;
                    MaxHealth = 10000;
                    Speed = 5;
                    break;
            }
            Radius = 70;
            Width = width;
            Height = height;
            MoveToPlayerPoint = new Point(452, 256);
            GunsLength = 130;
             
            
            int x = random.Next(0, 4);
            switch (x)
            {
                case 0:
                    Center = new Point(-2700, random.Next(-2700, 2700));
                    break;
                case 1:
                    Center = new Point(random.Next(-2700, 2700), -2700);
                    break;
                case 2:
                    Center = new Point(2700, random.Next(-2700, 2700));
                    break;
                default:
                    Center = new Point(random.Next(-2700, 2700), 2700);
                    break;
            }
            //Center = new Point(452, 256);
            Angle = Math.Atan2(MoveToPlayerPoint.Y - Center.Y, MoveToPlayerPoint.X - Center.X);

        }
        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int mainGunEndX = (int)(Center.X + GunsLength * Math.Cos(Angle));
            int mainGunEndY = (int)(Center.Y + GunsLength * Math.Sin(Angle));

            int leftGunEndX = (int)(Center.X + GunsLength * Math.Cos(Angle - 49.5));
            int leftGunEndY = (int)(Center.Y + GunsLength * Math.Sin(Angle - 49.5));

            int rightGunEndX = (int)(Center.X + GunsLength * Math.Cos(Angle + 49.5));
            int rightGunEndY = (int)(Center.Y + GunsLength * Math.Sin(Angle + 49.5));


            Pen gun = new Pen(Color.FromArgb(183, 183, 183), 55);
            g.DrawLine(gun, Center.X, Center.Y, mainGunEndX, mainGunEndY);
            g.DrawLine(gun, Center.X, Center.Y, leftGunEndX, leftGunEndY);
            g.DrawLine(gun, Center.X, Center.Y, rightGunEndX, rightGunEndY);

            gun.Dispose();

            Pen p = new Pen(Color.FromArgb(182, 59, 64), 8);
            Brush b = new SolidBrush(Color.FromArgb(241, 78, 84));
            g.DrawEllipse(p, Center.X - Radius, Center.Y - Radius,Radius*2,Radius*2);
            g.FillEllipse(b, Center.X - Radius, Center.Y - Radius,Radius*2,Radius*2);
            p.Dispose();
            b.Dispose();

            if (CurrentHealth < MaxHealth && CurrentHealth > 0)
            {
                Pen health = new Pen(Color.FromArgb(133, 227, 125), 6);
                Brush healthBackground = new SolidBrush(Color.FromArgb(85, 85, 85));
                float percent = (float)(CurrentHealth * 100) / MaxHealth;
                float k = (150 * (percent / 100)) - 75;
                g.FillRectangle(healthBackground, Center.X - 76, Center.Y + 86, 152, 8);
                g.DrawLine(health, Center.X - 75, Center.Y + 90, Center.X + k, Center.Y + 90);
                health.Dispose();
            }
        }

        public void CalculatePlayerPosition()
        {
            int x = Width / 2;
            int y = Height / 2;
            MoveToPlayerPoint = new Point(x, y);
            Angle = Math.Atan2(y - Center.Y, x - Center.X);
        }

        internal void Move()
        {
            double deltaX = (double)Speed * Math.Cos(Angle);
            double deltaY = (double)Speed * Math.Sin(Angle);
            double x = deltaX + Center.X;
            double y = deltaY + Center.Y;
            Center = new Point((int)Math.Round(x), (int)Math.Round(y));
        }
    }
}
