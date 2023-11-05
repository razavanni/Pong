using System;
using System.Windows;
using System.Windows.Controls;

namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für StartDlg.xaml
    /// </summary>
    public partial class StartDlg : Window
    {
        public Double Radius { get; set; }
        public Double Vx { get; set; }
        public Double Vy { get; set; }

        public StartDlg()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Radius = Convert.ToDouble(radius.Text);
                Vx = Convert.ToDouble(vx.Text);
                Vy = Convert.ToDouble(vy.Text);

                if (Radius < 1 || Radius > 100 || Vx < 1 || Vx > 2000 || Vy < 0 || Vy > 2000)
                {
                    throw new Exception("Mindestens ein Wert liegt außerhalb des zulässigen Bereichs.");
                }
                else
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message, "Eingabefehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void 
        GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }
    }
}
