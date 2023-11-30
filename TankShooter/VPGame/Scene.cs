using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VPGame
{
    public class Scene
    {
        public Tank Tank { get; set; }
        public List<Bullet> Bullets { get; set; }
        public List<Shape> Shapes { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Points { get; set; }
        public bool AutoFire { get; set; } = false;
        public Point BorderCenter { get; set; }
        public int XBorderLeft { get; set; }
        public int XBorderRight { get; set; }
        public int YBorderUp { get; set; } 
        public int YBorderDown { get; set; }
        public int Speed { get; set; }
        public int BulletSpeed { get; set; } = 4;
        public int BulletDamage { get; set; } = 50;
        public int MaxCurrentLevelPoints { get; set; }
        public int CurrentLevelPoints { get; set; }
        public int CurrentLevel { get; set; }
        public int SquareCount { get; set; } = 0;
        public int TriangleCount { get; set; } = 0;
        public int HexagonCount { get; set; } = 0;
        public List<int> SpeedIntegers = new List<int>() { 3, 3, 3, 3};
        public int CurrentIndex { get; set; } = 0;
        public int UpdateIndex { get; set; } = 0;
        public int MaxPixelsTraveled { get; set; } = 600;

        public bool timeOver { get; set; } = false;
        public BossTank Boss { get; set; }
        public List<BossBullet> BossBullets { get; set; }
        public int Difficulty { get; set; } = 1;

        Random random = new Random();

        public Scene(int width, int height, int difficulty) {
            Difficulty = difficulty;
            Tank = new Tank(width, height);
            Width = width;
            Height = height;
            Points = 0;
            Bullets = new List<Bullet>();
            Shapes = new List<Shape>();
            Speed = 3;
            CurrentLevelPoints = 0;
            MaxCurrentLevelPoints = 100;
            CurrentLevel = 0;
            BorderCenter = new Point(Width/2,Height/2);
            XBorderLeft = -2970 + Width/2 + 1000;
            XBorderRight = 2970 + Width/2 - 1000;
            YBorderUp = -2970 + Height/2 + 1000;
            YBorderDown = 2970 + Height/2 - 1000;

            Boss = new BossTank(width, height, difficulty);
            BossBullets = new List<BossBullet>();

        }

        public void UpdateList()
        {
            SpeedIntegers[UpdateIndex] += 1;
            if(UpdateIndex == 3)
            {
                UpdateIndex = 0;
            }
            else
            {
                UpdateIndex++;
            }
            
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen border = new Pen(Color.FromArgb(200, 200, 200), 2000);
            g.DrawRectangle(border, BorderCenter.X - 3000, BorderCenter.Y - 3000, 6000, 6000);
            border.Dispose();
            foreach (Bullet b in Bullets)
            {
                b.Draw(g);
            }
            if (timeOver)
            {
                foreach (BossBullet b in BossBullets)
                {
                    b.Draw(g);
                }
            }
            
            foreach (Shape s in Shapes)
            {
                s.Draw(g);
            }
            if (timeOver)
            {
                Boss.Draw(g);
            }
            

            Tank.Draw(g);

            if(CurrentLevelPoints >= MaxCurrentLevelPoints)
            {
                CurrentLevelPoints = CurrentLevelPoints - MaxCurrentLevelPoints;
                MaxCurrentLevelPoints = (int)(MaxCurrentLevelPoints * 1.1);
                CurrentLevel++;
            }

            Brush scoreBackground = new SolidBrush(Color.FromArgb(61, 61, 61));
            g.FillPie(scoreBackground, Width / 2 - 204 - 16, Height - 87, 34,34,90,180);
            g.FillPie(scoreBackground, Width / 2 + 204 - 18, Height - 87, 34,34,270,180);
            g.FillRectangle(scoreBackground, Width / 2 - 204, Height - 87, 408, 34);
            float percent = (float)CurrentLevelPoints / MaxCurrentLevelPoints;
            int x = (int)(400 * percent) - 200;
            if(x <= 200)
            {
                Brush greenCircles = new SolidBrush(Color.FromArgb(133, 227, 125));
                if (CurrentLevelPoints > 0)
                {
                    g.FillPie(greenCircles, Width / 2 - 200 - 12, Height - 83, 26, 26, 90, 180);
                }

                Pen score = new Pen(Color.FromArgb(133, 227, 125), 26);
                g.DrawLine(score, new Point(Width / 2 - 200, Height - 70), new Point(Width / 2 + x, Height - 70));
                score.Dispose();
            }

            scoreBackground.Dispose();

            

        }

        public void AddBullet(Point location)
        {
            Bullets.Add(new Bullet((float)Math.Atan2(location.Y - this.Height / 2, location.X - this.Width / 2), Width, Height, BulletSpeed, BulletDamage));
        }

        public void MoveBullets()
        {
            foreach (Bullet bullet in Bullets)
            {
                bullet.Move();
            }
        }
        public double Distance(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }
        public void DeleteBullets()
        {
            for(int i= Bullets.Count-1;i>=0; i--)
            {
                if (Bullets[i].PixelsTraveled > MaxPixelsTraveled)
                {
                    Bullets.RemoveAt(i);
                }
            }
            
        }
        public bool NotOverlapping(Shape newShape)
        {
            foreach (Shape s in Shapes)
            {
                if (Distance(s.Center, newShape.Center) < (s.Radius+newShape.Radius + 10))
                {
                    return false;
                }
            }
            return true;
        }
        public void AddShape()
        {
            int x = random.Next(0, 100);
            bool flag = false;
            Shape newShape = null;
            switch (x)
            {
                case int n when n < 65 && SquareCount<=60: newShape = new Square(new Point(random.Next(XBorderLeft, XBorderRight), random.Next(YBorderUp, YBorderDown)));
                    while (true)
                    {
                        if (NotOverlapping(newShape))
                            break;
                        else
                            newShape = new Square(new Point(random.Next(XBorderLeft, XBorderRight), random.Next(YBorderUp, YBorderDown)));
                    }
                    flag = true;
                    SquareCount++;
                    break;
                case int n when n >= 65 && n < 90 && TriangleCount <= 40:
                    newShape = new Triangle(new Point(random.Next(XBorderLeft, XBorderRight), random.Next(YBorderUp, YBorderDown)));
                    while (true)
                    {
                        if (NotOverlapping(newShape))
                            break;
                        else
                            newShape = new Triangle(new Point(random.Next(XBorderLeft, XBorderRight), random.Next(YBorderUp, YBorderDown)));
                    }
                    flag = true;
                    TriangleCount++;
                    break;
                default:
                    if (HexagonCount <= 30)
                    {
                        newShape = new Hexagon(new Point(random.Next(XBorderLeft, XBorderRight), random.Next(YBorderUp, YBorderDown)));
                        while (true)
                        {
                            if (NotOverlapping(newShape))
                                break;
                            else
                                newShape = new Hexagon(new Point(random.Next(XBorderLeft, XBorderRight), random.Next(YBorderUp, YBorderDown)));
                        }
                        flag = true;
                        HexagonCount++;
                    }
                    
                    break;

            }
            if (flag)
            {
                Shapes.Add(newShape);
            }
            
        }

        internal void Hit()
        {
            if (timeOver)
            {
                for (int i = Bullets.Count - 1; i >= 0; i--)
                {
                    for (int j = BossBullets.Count - 1; j >= 0; j--)
                    {
                        if (Distance(BossBullets[j].Center, Bullets[i].Center) < (BossBullets[j].Radius + Bullets[i].Radius))
                        {
                            int x = BossBullets[j].Damage;
                            BossBullets[j].Damage-= Bullets[i].Damage;
                            Bullets[i].Damage -= x;
                            if (BossBullets[j].Damage <= 0)
                            {
                                BossBullets.RemoveAt(j);
                            }
                            if (Bullets[i].Damage <= 0)
                            {
                                Bullets.RemoveAt(i);
                                break;
                            }
                            
                        }
                    }
                    
                }
            }
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                if(Distance(Boss.Center, Bullets[i].Center) < (Boss.Radius + Bullets[i].Radius))
                {
                    Boss.CurrentHealth -= Bullets[i].Damage;
                    Bullets.RemoveAt(i);
                }
            }
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                for (int j = Bullets.Count - 1; j >= 0; j--)
                {
                    if(Distance(Shapes[i].Center, Bullets[j].Center) < (Shapes[i].Radius + Bullets[j].Radius)) {
                        Shapes[i].Health -= Bullets[j].Damage;
                        if (Shapes[i].Health <= 0)
                        {
                            Bullets[j].Damage = Math.Abs(Shapes[i].Health);
                            Points += Shapes[i].ScorePoints;
                            CurrentLevelPoints += Shapes[i].ScorePoints;
                            if (Shapes[i] is Square)
                            {
                                SquareCount--;
                            }
                            else if (Shapes[i] is Triangle)
                            {
                                TriangleCount--;
                            }
                            else
                            {
                                HexagonCount--;
                            }
                            Shapes.RemoveAt(i);
                            if (i == 0)
                            {
                                if (Bullets[j].Damage == 0)
                                {
                                    Bullets.RemoveAt(j);
                                }
                                break;
                            }
                            else
                                i--;

                        }
                        else
                        {
                            Bullets[j].Damage = 0;
                        }

                        if (Bullets[j].Damage == 0)
                        {
                            Bullets.RemoveAt(j);
                        }
                    }
                }
            }
        }

        internal void moveUp()
        {
            if (YBorderUp < Height / 2 + 200)
            {
                YBorderUp += SpeedIntegers[CurrentIndex];
                YBorderDown += SpeedIntegers[CurrentIndex];

                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullets[i].Center = new Point(Bullets[i].Center.X, Bullets[i].Center.Y + SpeedIntegers[CurrentIndex]);
                }
                for (int i = 0; i < BossBullets.Count; i++)
                {
                    BossBullets[i].Center = new Point(BossBullets[i].Center.X, BossBullets[i].Center.Y + SpeedIntegers[CurrentIndex]);
                }
                foreach (Shape s in Shapes)
                {
                    s.Center = new Point(s.Center.X, s.Center.Y + SpeedIntegers[CurrentIndex]);
                    if (!(s is Square))
                    {
                        for (int i = 0; i < s.Points.Count; i++)
                        {
                            s.Points[i] = new Point(s.Points[i].X, s.Points[i].Y + SpeedIntegers[CurrentIndex]);
                        }
                    }

                }
                BorderCenter = new Point(BorderCenter.X, BorderCenter.Y + SpeedIntegers[CurrentIndex]);
                if (CurrentIndex == 3)
                {
                    CurrentIndex = 0;
                }
                else
                {
                    CurrentIndex++;
                }
                Boss.CalculatePlayerPosition();
                Boss.Center = new Point(Boss.Center.X, Boss.Center.Y + SpeedIntegers[CurrentIndex]);
            }

            
            
        }

        internal void moveDown()
        {
            if (YBorderDown > Height / 2 - 200)
            {
                YBorderUp -= SpeedIntegers[CurrentIndex];
                YBorderDown -= SpeedIntegers[CurrentIndex];
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullets[i].Center = new Point(Bullets[i].Center.X, Bullets[i].Center.Y - SpeedIntegers[CurrentIndex]);
                }
                for (int i = 0; i < BossBullets.Count; i++)
                {
                    BossBullets[i].Center = new Point(BossBullets[i].Center.X, BossBullets[i].Center.Y - SpeedIntegers[CurrentIndex]);
                }
                foreach (Shape s in Shapes)
                {
                    s.Center = new Point(s.Center.X, s.Center.Y - SpeedIntegers[CurrentIndex]);
                    if (!(s is Square))
                    {
                        for (int i = 0; i < s.Points.Count; i++)
                        {
                            s.Points[i] = new Point(s.Points[i].X, s.Points[i].Y - SpeedIntegers[CurrentIndex]);
                        }
                    }
                }
                BorderCenter = new Point(BorderCenter.X, BorderCenter.Y - SpeedIntegers[CurrentIndex]);
                if (CurrentIndex == 3)
                {
                    CurrentIndex = 0;
                }
                else
                {
                    CurrentIndex++;
                }
                Boss.CalculatePlayerPosition();
                Boss.Center = new Point(Boss.Center.X, Boss.Center.Y - SpeedIntegers[CurrentIndex]);
            }
        }

        internal void moveLeft()
        {
            if (XBorderLeft <  Width / 2 + 200)
            {
                XBorderLeft += SpeedIntegers[CurrentIndex];
                XBorderRight += SpeedIntegers[CurrentIndex];
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullets[i].Center = new Point(Bullets[i].Center.X + SpeedIntegers[CurrentIndex], Bullets[i].Center.Y);
                }
                for (int i = 0; i < BossBullets.Count; i++)
                {
                    BossBullets[i].Center = new Point(BossBullets[i].Center.X + SpeedIntegers[CurrentIndex], BossBullets[i].Center.Y);
                }
                foreach (Shape s in Shapes)
                {
                    s.Center = new Point(s.Center.X + SpeedIntegers[CurrentIndex], s.Center.Y);
                    if (!(s is Square))
                    {
                        for (int i = 0; i < s.Points.Count; i++)
                        {
                            s.Points[i] = new Point(s.Points[i].X + SpeedIntegers[CurrentIndex], s.Points[i].Y);
                        }
                    }
                }

                BorderCenter = new Point(BorderCenter.X + SpeedIntegers[CurrentIndex], BorderCenter.Y);
                if (CurrentIndex == 3)
                {
                    CurrentIndex = 0;
                }
                else
                {
                    CurrentIndex++;
                }
                Boss.CalculatePlayerPosition();
                Boss.Center = new Point(Boss.Center.X + SpeedIntegers[CurrentIndex], Boss.Center.Y);
            }
        }

        internal void moveRight()
        {
            if (XBorderRight > Width / 2 - 200)
            {
                XBorderLeft -= SpeedIntegers[CurrentIndex];
                XBorderRight -= SpeedIntegers[CurrentIndex];
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullets[i].Center = new Point(Bullets[i].Center.X - SpeedIntegers[CurrentIndex], Bullets[i].Center.Y);
                }
                for (int i = 0; i < BossBullets.Count; i++)
                {
                    BossBullets[i].Center = new Point(BossBullets[i].Center.X - SpeedIntegers[CurrentIndex], BossBullets[i].Center.Y);
                }
                foreach (Shape s in Shapes)
                {
                    s.Center = new Point(s.Center.X - SpeedIntegers[CurrentIndex], s.Center.Y);
                    if (!(s is Square))
                    {
                        for (int i = 0; i < s.Points.Count; i++)
                        {
                            s.Points[i] = new Point(s.Points[i].X - SpeedIntegers[CurrentIndex], s.Points[i].Y);
                        }
                    }
                }
                BorderCenter = new Point(BorderCenter.X - SpeedIntegers[CurrentIndex], BorderCenter.Y);
                if (CurrentIndex == 3)
                {
                    CurrentIndex = 0;
                }
                else
                {
                    CurrentIndex++;
                }
                Boss.CalculatePlayerPosition();
                Boss.Center = new Point(Boss.Center.X - SpeedIntegers[CurrentIndex], Boss.Center.Y);
            }
        }

        internal void moveUpRight()
        {
            moveUp();
            moveRight();

        }

        internal void moveUpLeft()
        {
            moveUp();
            moveLeft();
        }

        internal void moveDownRight()
        {
            moveDown();
            moveRight();
        }

        internal void moveDownLeft()
        {
            moveDown();
            moveLeft();
        }

        internal void bodyHit()
        {
            Point Center = new Point(Width/2, Height/2);
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                while(Distance(Center, Shapes[i].Center) < (Tank.Radius + Shapes[i].Radius))
                {
                    Shapes[i].Health -= Tank.BodyDamage;
                    Tank.Health -= 50;
                    if (Shapes[i].Health <= 0)
                    {
                        Points += Shapes[i].ScorePoints;
                        CurrentLevelPoints += Shapes[i].ScorePoints;
                        if (Shapes[i] is Square)
                        {
                            SquareCount--;
                        }
                        else if (Shapes[i] is Triangle)
                        {
                            TriangleCount--;
                        }
                        else
                        {
                            HexagonCount--;
                        }
                        Shapes.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        internal void GenerateStartingShapes()
        {
            for (int i=0; i < 150; i++)
            {
                AddShape();
            }
        }

        internal void moveBoss()
        {
            Boss.Move();
            Point Center = new Point(Width / 2, Height / 2);
            if (Distance(Center, Boss.Center) < Tank.Radius + Boss.Radius)
            {
                Tank.Health = 0;
            }
        }

        internal void AddBossBullet()
        {
            BossBullets.Add(new BossBullet(Boss.Angle, Boss.Center, Difficulty, 0));
            BossBullets.Add(new BossBullet(Boss.Angle, Boss.Center, Difficulty, - 49.5));
            BossBullets.Add(new BossBullet(Boss.Angle, Boss.Center, Difficulty, 49.5));
        }

        internal void moveBossBullets()
        {
            foreach (BossBullet bullet in BossBullets)
            {
                bullet.Move();
            }
        }

        internal void DeleteBossBullets()
        {
            for (int i = BossBullets.Count - 1; i >= 0; i--)
            {
                if (BossBullets[i].PixelsTraveled > 1000)
                {
                    BossBullets.RemoveAt(i);
                }
            }
        }

        internal void BossBulletsHit()
        {
            Point PlayerCenter = new Point(Width/2,Height/2);
            for (int i = BossBullets.Count - 1; i >= 0; i--)
            {
                if (Distance(PlayerCenter, BossBullets[i].Center) < (Tank.Radius + BossBullets[i].Radius))
                {
                    Tank.Health -= BossBullets[i].Damage;
                    BossBullets.RemoveAt(i);
                }
            }
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                for (int j = BossBullets.Count - 1; j >= 0; j--)
                {
                    if (Distance(Shapes[i].Center, BossBullets[j].Center) < (Shapes[i].Radius + BossBullets[j].Radius))
                    {
                        Shapes[i].Health -= BossBullets[j].Damage;
                        if (Shapes[i].Health <= 0)
                        {
                            BossBullets[j].Damage = Math.Abs(Shapes[i].Health);
                            if (Shapes[i] is Square)
                            {
                                SquareCount--;
                            }
                            else if (Shapes[i] is Triangle)
                            {
                                TriangleCount--;
                            }
                            else
                            {
                                HexagonCount--;
                            }
                            Shapes.RemoveAt(i);
                            if (i == 0)
                            {
                                if (BossBullets[j].Damage == 0)
                                {
                                    BossBullets.RemoveAt(j);
                                }
                                break;
                            }
                            else
                                i--;

                        }
                        else
                        {
                            BossBullets[j].Damage = 0;
                        }

                        if (BossBullets[j].Damage == 0)
                        {
                            BossBullets.RemoveAt(j);
                        }
                    }
                }
            }
        }

        internal void BossBodyHit()
        {
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                while (Distance(Boss.Center, Shapes[i].Center) < (Boss.Radius + Shapes[i].Radius))
                {
                    Shapes[i].Health = 0;
                    if (Shapes[i] is Square)
                    {
                        SquareCount--;
                    }
                    else if (Shapes[i] is Triangle)
                    {
                        TriangleCount--;
                    }
                    else
                    {
                        HexagonCount--;
                    }
                    Shapes.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
