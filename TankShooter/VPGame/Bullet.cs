using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPGame
{
    public class Bullet
    {
        public int Radius { get; set; }
        public int Speed { get; set; }
        public double Angle { get; set; }
        public int Damage { get; set; }
        public Point Center { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public int PixelsTraveled { get; set; } = 0;

        public Bullet(float angle, int width, int height, int speed=4,int damage=50)
        {
            Angle = angle;
            Radius = 10;
            Speed = speed;
            Damage = damage;
            Width = width;
            Height = height;
            Center = new Point((int)(Width / 2 + 50 * Math.Cos(Angle)), (int)(Height / 2 + 50 * Math.Sin(Angle)));
            DeltaX = (double)Speed * Math.Cos(Angle);
            DeltaY = (double)Speed * Math.Sin(Angle);
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = new SolidBrush(Color.FromArgb(76, 201, 234));
            Pen p = new Pen(Color.FromArgb(3, 134, 168), 4);
            g.FillEllipse(b, Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);
            g.DrawEllipse(p, Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
            p.Dispose();
        }
        public void Move()
        {
            double x = DeltaX + Center.X;
            double y = DeltaY + Center.Y;
            PixelsTraveled += (int)Math.Round(Math.Sqrt(Math.Pow(DeltaX, 2) + Math.Pow(DeltaY, 2)));
            Center = new Point((int)Math.Round(x), (int)Math.Round(y));
        }

    }
}
