using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGame
{
    public class Square:Shape
    {
        public int Size { get; set; }
        

        public Square(Point point) {
            Center = point;
            Size = 30;
            Radius = 20;
            Health = 100;
            MaxHealth = 100;
            ScorePoints = 50;
        }

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.FromArgb(191, 174, 78), 4);
            Brush b = new SolidBrush(Color.FromArgb(255, 232, 105));
            g.FillRectangle(b, Center.X - Size / 2, Center.Y - Size / 2, Size, Size);
            g.DrawRectangle(p, Center.X - Size / 2, Center.Y - Size / 2, Size, Size);

            if (Health < MaxHealth && Health > 0)
            {
                Pen health = new Pen(Color.FromArgb(133, 227, 125), 6);
                Brush healthBackground = new SolidBrush(Color.FromArgb(85, 85, 85));
                float percent = (float)(Health * 100) / MaxHealth;
                float x = (50 * (percent / 100)) - 25;
                g.FillRectangle(healthBackground, Center.X - 26, Center.Y + 26, 52, 8);
                g.DrawLine(health, Center.X - 25, Center.Y + 30, Center.X + x, Center.Y + 30);
                health.Dispose();
            }

            p.Dispose();
            b.Dispose();
        }
    }
}
