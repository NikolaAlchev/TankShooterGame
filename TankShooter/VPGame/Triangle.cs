using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGame
{
    public class Triangle:Shape
    {
        public int Side { get; set; }
        Random r = new Random();
        
        public Triangle(Point center) {
            Center = center;
            Side = 20;
            Radius = 20;
            Health = 200;
            MaxHealth = 200;
            ScorePoints = 200;

            int halfSideLength = Side / 2;
            double angle =  Math.PI / r.Next(1, 100);
            Points = new List<Point>();

            for (int i = 0; i < 3; i++)
            {
                double currentAngle = 2 * Math.PI * i / 3 + angle;
                int x = Center.X + (int)(Side * Math.Cos(currentAngle));
                int y = Center.Y + (int)(Side * Math.Sin(currentAngle));
                Points.Add(new Point(x, y));
            }
        }

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.FromArgb(189, 88, 89),8);
            Brush b = new SolidBrush(Color.FromArgb(252, 118, 119));

            g.DrawPolygon(p, Points.ToArray());
            g.FillPolygon(b, Points.ToArray());

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
