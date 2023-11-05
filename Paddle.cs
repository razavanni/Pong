using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input.StylusWisp;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    internal class Paddle
    {
        public Rectangle pdl { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Speed { get; set; }

        public Paddle(double x = 100, double y = 100, double w = 100, double h = 100, double s = 100)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Speed = s;

            pdl = new Rectangle();

            pdl.Width = Width;
            pdl.Height = Height;
            pdl.Fill = Brushes.Blue;

            Canvas.SetLeft(pdl, X);
            Canvas.SetTop(pdl, Y);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(pdl))
                c.Children.Add(pdl);
        }

        public void Undraw(Canvas c)
        {
            if (c.Children.Contains(pdl))
                c.Children.Remove(pdl);
        }

        public void Move(double s, int direction, Rectangle r)
        {
            double newY = Y + ((Speed * 2) * direction * s / 1000);
            double rTop = Canvas.GetTop(r);
            double rBottom = rTop + r.ActualHeight;

            if (newY < rTop)
            {
                newY = rTop;
            }
            else if (newY + Height > rBottom)
            {
                newY = rBottom;
            }
            else
            {
                Y = newY;
            }

            Canvas.SetTop(pdl, Y);
        }

        public void Resize(double sx, double sy)
        {
            X = X * sx;
            Y = Y * sy;

            Width = Width * sx;
            Height = Height * sy;

            pdl.Width = pdl.Width * sx;
            pdl.Height = pdl.Height * sy;

            Canvas.SetLeft(pdl, sx * Canvas.GetLeft(pdl));
            Canvas.SetTop(pdl, sy * Canvas.GetTop(pdl));
        }
    }
}
