using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    class Ball
    {
        public Ellipse Elli { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Vx { get; set; }
        public Double Vy { get; set; }
        public Double Radius { get; set; }
        public int Punkte_L { get; set; }
        public int Punkte_R { get; set; }

        public Ball(Double X = 100, Double Y = 100, Double Vx = 20, Double Vy = 20, Double Radius = 10)
        {
            this.X = X;
            this.Y = Y;
            this.Vx = Vx;
            this.Vy = Vy;
            this.Radius = Radius;

            Elli = new Ellipse();
            Elli.Width = 2 * Radius;
            Elli.Height = 2 * Radius;
            Elli.Fill = Brushes.Blue;

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Elli))
            {
                c.Children.Add(Elli);
            }
        }

        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(Elli))
            {
                c.Children.Remove(Elli);
            }
        }

        public void Resize(double sx, double sy)
        {
            X *= sx;
            Y *= sy;

            Vx *= sx;
            Vy *= sy;

            Radius *= (sx + sy) / 2;

            Elli.Width *= (sx + sy) / 2;
            Elli.Height *= (sx + sy) / 2;

            Canvas.SetLeft(Elli, sx * Canvas.GetLeft(Elli));
            Canvas.SetTop(Elli, sy * Canvas.GetTop(Elli));
        }

        public void Move(Double dt)
        {
            X = X + Vx * dt / 1000;
            Y = Y + Vy * dt / 1000;
        }

        public void Collision(Paddle p)
        {
            if ((X - Radius <= p.X + p.Width) &&
                (X - Radius > p.X) &&
                (Y + Radius > p.Y) &&
                (Y + Radius < p.Y + p.Height))
            {
                Vx = -Vx;
            }
            else if ((X + Radius >= p.X) &&
                (X + Radius < p.X + p.Width) &&
                (Y + Radius > p.Y) &&
                (Y + Radius < p.Y + p.Height))
            {
                Vx = -Vx;
            }
        }

        public void Collision(Rectangle r, bool ccde)
        {
            // Obere oder untere Bande
            if (Y - Radius <= Canvas.GetTop(r))
            {
                Vy = -Vy;                                       // Reflexion and der Bande
                if (ccde)
                {
                    Y = Y + 2 * (Canvas.GetTop(r) - (Y - Radius));  // Korrektur des Detektionsfehlers
                }
            }
            if (Y + Radius >= Canvas.GetTop(r) + r.Height)
            {
                Vy = -Vy;
                if (ccde)
                {
                    Y = Y - 2 * (Y + Radius - Canvas.GetTop(r) - r.Height);
                }
            }

            // Linke oder rechte Bande
            if (X - Radius <= Canvas.GetLeft(r))
            {
                Punkte_R++;
                Vx = -Vx;
                if (ccde)
                {
                    X = X + 2 * (Canvas.GetLeft(r) - (X - Radius));
                }
            }
            if (X + Radius >= Canvas.GetLeft(r) + r.Width)
            {
                Punkte_L++;
                Vx = -Vx;
                if (ccde)
                {
                    X = X - 2 * (X + Radius - Canvas.GetLeft(r) - r.Width);
                }
            }

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }
    }
}
