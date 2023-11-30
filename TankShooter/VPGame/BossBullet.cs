using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGame
{
    public class BossBullet
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

        public BossBullet(double angle, Point location, int difficulty,double offset = 0)
        {
            switch (difficulty)
            {
                case 0:
                    Speed = 4;
                    Damage = 400;
                    break;
                case 1:
                    Speed = 5;
                    Damage = 500;
                    break;
                default:
                    Speed = 6;
                    Damage = 700;
                    break;
            }
            Angle = angle;
            Radius = 25;        
            Center = new Point((int)(location.X + 130 * Math.Cos(Angle + offset)), (int)(location.Y + 130 * Math.Sin(Angle + offset)));
            DeltaX = (double)Speed * Math.Cos(Angle + offset);
            DeltaY = (double)Speed * Math.Sin(Angle + offset);

        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.FromArgb(182, 59, 64), 4);
            Brush b = new SolidBrush(Color.FromArgb(241, 78, 84));
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
