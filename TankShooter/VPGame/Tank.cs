using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VPGame
{
    public class Tank
    {
        public int Radius { get; set; }
        public int GunLength { get; set; }
        public float Angle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int BodyDamage { get; set; }
        public int Regen { get; set; }
        public Tank(int width, int height)
        {
            Radius = 28;
            GunLength = 55;
            Regen = 20;
            Width = width;
            Height = height;
            Angle = 0;
            Health = 500;
            MaxHealth = 500;
            BodyDamage = 50;
        }

        public void Draw (Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int x = Width / 2;
            int y = Height / 2;

            int gunEndX = (int)(x + GunLength * Math.Cos(Angle));
            int gunEndY = (int)(y + GunLength * Math.Sin(Angle));


            Pen p = new Pen(Color.FromArgb(183, 183, 183), 28);
            g.DrawLine(p, x, y, gunEndX, gunEndY);

            p.Dispose();

            Brush b = new SolidBrush(Color.FromArgb(76, 201, 234));
            Pen p2 = new Pen(Color.FromArgb(3, 134, 168), 4);
           
            g.FillEllipse(b, x - Radius, y - Radius, Radius * 2, Radius * 2);
            g.DrawEllipse(p2, x - Radius, y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
            p2.Dispose();

            Point Center = new Point(x, y);
            if (Health < MaxHealth && Health > 0)
            {
                Pen health = new Pen(Color.FromArgb(133, 227, 125), 6);
                Brush healthBackground = new SolidBrush(Color.FromArgb(85, 85, 85));
                float percent = (float)(Health * 100) / MaxHealth;
                float k = (60 * (percent / 100)) - 30;
                g.FillRectangle(healthBackground, Center.X - 31, Center.Y + 36, 62, 8);
                g.DrawLine(health, Center.X - 30, Center.Y + 40, Center.X + k, Center.Y + 40);
                health.Dispose();
            }

        }


    }
}
