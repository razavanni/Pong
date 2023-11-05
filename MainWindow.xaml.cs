using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Markup.Localizer;

namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartDlg dlg;
        Ball ball;
        DispatcherTimer timer;
        Double ticks_old;
        Uhr Uhr;
        Paddle p1, p2;
        double ticks_start;
        double t_sec = 0.0;
        AutoPlayer autoPlayer;


        public MainWindow()
        {
            dlg = new StartDlg();

            if ((bool)dlg.ShowDialog())
            {
                InitializeComponent();
            }
            else
            {
                Close();
            }
        }

        private void wnd_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);

            ball = new Ball(250, 150, dlg.Vx, dlg.Vy, dlg.Radius);
            autoPlayer = new AutoPlayer();
            p1 = new Paddle(110, 125, 10, 100, 100);
            p2 = new Paddle(475, 125, 10, 100, 100);
            Uhr = new Uhr((Canvas.GetLeft(Rect) + Rect.Width - 40) / 2, Canvas.GetTop(Rect) - 40, 40, Cvs);

            gameSpeed.Minimum = 0;
            gameSpeed.Maximum = 2000;
            gameSpeed.Value = dlg.Vx;

            ball.Draw(Cvs);
            p1.Draw(Cvs);
            p2.Draw(Cvs);
            Uhr.Draw(Cvs);

            ticks_start = Environment.TickCount;
            ticks_old = Environment.TickCount;
            timer.Start();
        }

        private void UpdateBallSpeed(double newVx, double newVy)
        {
            if(ball != null)
            {
                ball.Vx = newVx;
                ball.Vy = newVy;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;

            dlg.Vx = gameSpeed.Value;
            dlg.Vy = gameSpeed.Value;

            ball.Move(ticks - ticks_old);

            ball.Collision(p1);
            ball.Collision(p2);
            ball.Collision(Rect, true);



            if (Keyboard.IsKeyDown(Key.S))
                p1.Move(ticks - ticks_old, 1, Rect);
            else if (Keyboard.IsKeyDown(Key.W))
                p1.Move(ticks - ticks_old, -1, Rect);

            if ((bool)dlg.autoplayer_check.IsChecked)
            {
                autoPlayer.AutoPlayerMove(p2, ball, Rect, ticks - ticks_old);
                p2.Move(ticks - ticks_old, 0, Rect);
            }
            else if (!(bool)dlg.autoplayer_check.IsChecked)
            {
                if (Keyboard.IsKeyDown(Key.Down))
                    p2.Move(ticks - ticks_old, 1, Rect);
                else if (Keyboard.IsKeyDown(Key.Up))
                    p2.Move(ticks - ticks_old, -1, Rect);
            }

            double elapsedSeconds = (ticks - ticks_old) / 1000.0;
            double fps = 1.0 / elapsedSeconds;
            fpsAnzeige.Content = "FPS: " + fps.ToString("F0");

            ticks_old = ticks;

            punktestand.Content = ball.Punkte_L + " : " + ball.Punkte_R;

            t_sec = (Environment.TickCount - ticks_start) / 1000.0;
            Uhr.UpdateUhr(t_sec, Cvs);
        }

        private void ende_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            ticks_old = Environment.TickCount;
            ticks_start = Environment.TickCount;

            ball.Punkte_L = 0;
            ball.Punkte_R = 0;

            timer.Start();
        }

        private void parameter_Click(object sender, RoutedEventArgs e)
        {
            dlg = new StartDlg();

            if ((bool)dlg.ShowDialog())
            {
                ball.UnDraw(Cvs);
                ball = new Ball(250, 150, dlg.Vx, dlg.Vy, dlg.Radius);
                ticks_start = Environment.TickCount;
                ball.Draw(Cvs);
            }
        }

        private void gameSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateBallSpeed(gameSpeed.Value, gameSpeed.Value);
        }

        private void Cvs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width > 0 && e.PreviousSize.Height > 0)
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                ball.Resize(sx, sy);
                p1.Resize(sx, sy);
                p2.Resize(sx, sy);
                Uhr.Resize(sx, sy);

                Rect.Width *= sx;
                Rect.Height *= sy;
                Canvas.SetLeft(Rect, sx * Canvas.GetLeft(Rect));
                Canvas.SetTop(Rect, sy * Canvas.GetTop(Rect));

                hilfeL.Width *= sx;
                hilfeL.Height *= sy;
                Canvas.SetLeft(hilfeL, sx * Canvas.GetLeft(hilfeL));
                Canvas.SetTop(hilfeL, sy * Canvas.GetTop(hilfeL));

                hilfeR.Width *= sx;
                hilfeR.Height *= sy;
                Canvas.SetLeft(hilfeR, sx * Canvas.GetLeft(hilfeR));
                Canvas.SetTop(hilfeR, sy * Canvas.GetTop(hilfeR));

                punktestand.Width *= sx;
                punktestand.Height *= sy;
                Canvas.SetLeft(punktestand, sx * Canvas.GetLeft(punktestand));
                Canvas.SetTop(punktestand, sy * Canvas.GetTop(punktestand));
            }
        }
    }
}
