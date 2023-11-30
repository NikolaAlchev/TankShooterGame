using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGame
{
    public abstract class Shape
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public Point Center { get; set; }
        public float Angle { get; set; }
        public int Radius { get; set; }
        public int ScorePoints { get; set; }
        public List<Point> Points { get; set; }

        public abstract void Draw(Graphics g);
    }
}
