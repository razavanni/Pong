using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    internal class Uhr
    {
        public Ellipse UhrenRand { get; set; }

        public Zeiger Sec { get; set; }
        public Zeiger Min { get; set; }
        public Zeiger Std { get; set; }

        public Uhr(double x, double y, double r, Canvas c)
        {
            double x1, x2, y1, y2, angle, length;

            // Uhrenrand
            UhrenRand = new Ellipse
            {
                Width = r * 2,
                Height = r * 2,
                Stroke = Brushes.Green,
                StrokeThickness = 1.5
            };
            Canvas.SetLeft(UhrenRand, x);
            Canvas.SetTop(UhrenRand, y);

            // Zeiger
            x1 = Canvas.GetLeft(UhrenRand) + UhrenRand.Width / 2;
            y1 = Canvas.GetTop(UhrenRand) + UhrenRand.Height / 2;
            // Sekunden Zeiger
            angle = 0;
            length = 0.8 * (UhrenRand.Height / 2);
            x2 = x1 + (length * Math.Sin(angle));
            y2 = y1 - (length * Math.Cos(angle));
            Sec = new Zeiger(length, angle, 30, x1, x2, y1, y2, 1, Brushes.Red);

            // Minuten Zeiger
            length = 0.8 * (UhrenRand.Height / 2);
            angle = 0;
            x2 = x1 + (length * Math.Sin(angle));
            y2 = y1 - length * Math.Cos(angle);
            Min = new Zeiger(length, angle, 1800, x1, x2, y1, y2, 1.5, Brushes.Black);

            // Stunden Zeiger
            length = 0.55 * (UhrenRand.Height / 2);
            angle = 0;
            x2 = x1 + (length * Math.Sin(angle));
            y2 = y1 - (length * Math.Cos(angle));
            Std = new Zeiger(length, angle, 21600, x1, x2, y1, y2, 2, Brushes.Black);

            Std.Draw(c);
            Min.Draw(c);
            Sec.Draw(c);

        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(UhrenRand))
            {
                c.Children.Add(UhrenRand);
            }
        }

        public void Resize(double sx, double sy)
        {

            UhrenRand.Width *= (sx + sy) / 2;
            UhrenRand.Height *= (sx + sy) / 2;

            Canvas.SetLeft(UhrenRand, sx * Canvas.GetLeft(UhrenRand));
            Canvas.SetTop(UhrenRand, sy * Canvas.GetTop(UhrenRand));

            Sec.Resize(sx, sy);
            Min.Resize(sx, sy);
            Std.Resize(sx, sy);
        }

        public void UpdateUhr(double t_sec, Canvas c)
        {
            Sec.UpdateZeiger(t_sec);
            Min.UpdateZeiger(t_sec);
            Std.UpdateZeiger(t_sec);

            Sec.Draw(c);
            Min.Draw(c);
            Std.Draw(c);
        }
    }
}
