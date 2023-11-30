using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace VPGame
{
    public class Hexagon : Shape
    {
        public int Side { get; set; }
        public Hexagon(Point center) {
            Center = center;
            Side = 30;
            Radius = 30;
            Health = 300;
            MaxHealth = 300;
            ScorePoints = 500;
            double angle = 2 * Math.PI / 6;
            Points = new List<Point>();

            for (int i = 0; i < 6; i++)
            {
                double currentAngle = angle * i;
                int x = center.X + (int)(Side * Math.Cos(currentAngle));
                int y = center.Y + (int)(Side * Math.Sin(currentAngle));
                Points.Add(new Point(x, y));
            }
        }
        public override void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.FromArgb(88, 105, 189), 8);
            Brush b = new SolidBrush(Color.FromArgb(118, 141, 252));

            g.DrawPolygon(p, Points.ToArray());
            g.FillPolygon(b, Points.ToArray());

            if (Health < MaxHealth && Health > 0)
            {
                Pen health = new Pen(Color.FromArgb(133, 227, 125), 6);
                Brush healthBackground = new SolidBrush(Color.FromArgb(85, 85, 85));
                float percent = (float)(Health * 100) / MaxHealth;
                float x = (60 * (percent/100)) - 30;
                g.FillRectangle(healthBackground, Center.X - 31, Center.Y + 36, 62, 8);
                g.DrawLine(health, Center.X - 30, Center.Y + 40, Center.X + x, Center.Y + 40);
                health.Dispose();
            }
            

            p.Dispose();
            b.Dispose();
            
        }
    }
}
