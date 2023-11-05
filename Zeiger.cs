using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    internal class Zeiger
    {
        public Line Linie { get; set; }
        public double Lenght { get; set; }
        public double Angle { get; set; }
        public double Teiler { get; set; }

        public Zeiger(double l, double a, double t, double x1, double x2, double y1, double y2, double thick, Brush f)
        {
            Linie = new Line();
            Lenght = l;
            Angle = a;
            Teiler = t;

            Linie.X1 = x1;
            Linie.Y1 = y1;
            Linie.X2 = x2;
            Linie.Y2 = y2;

            Linie.Stroke = f;
            Linie.StrokeThickness = thick;
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Linie))
            {
                c.Children.Add(Linie);
            }
        }

        public void Resize(double sx, double sy)
        {
            Lenght *= (sx + sy) / 2;

            Linie.StrokeThickness *= (sx + sy) / 2;

            Linie.X1 *= sx;
            Linie.X2 *= sx;
            Linie.Y1 *= sy;
            Linie.Y2 *= sy;
        }

        public void UpdateZeiger(double t_sec)
        {
            double ang = Angle + Math.PI * t_sec / Teiler;

            Linie.X2 = Linie.X1 + (Lenght * Math.Sin(ang));
            Linie.Y2 = Linie.Y1 - (Lenght * Math.Cos(ang));
        }
    }
}
