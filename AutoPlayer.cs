using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    internal class AutoPlayer
    {
        public void AutoPlayerMove(Paddle p, Ball b, Rectangle r, double dt)
        {
            // double T = (p.X - b.X - b.Radius) / b.Vx;
            // double Y_T = b.Y + b.Vy * T;
            // double pVy = (Y_T + p.Height / 2) - p.Y;
            // 
            // p.Move(dt, Convert.ToInt32(pVy), r);


            double newY = b.Y - (p.Height / 2);
            double rTop = Canvas.GetTop(r);
            double rBottom = rTop + r.ActualHeight;
            
            if (newY < rTop)
            {
                newY = rTop;
            }
            else if (newY + p.Height > rBottom)
            {
                newY = rBottom;
            }
            else
            {
                p.Y = newY;
            }

        }
    }
}
