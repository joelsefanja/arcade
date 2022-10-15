using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Arcade;

namespace Arcade
{
    /// <summary>
    /// Interaction logic for NamenInvoeren.xaml
    /// </summary>
    public partial class NamenInvoeren : Window
    {
        // public DispatcherTimer tekstAnimatieTimer = new DispatcherTimer(); // TIMER VOOR DE ANIMATIE VAN TEXT
        string spelerNaam1, spelerNaam2;
        public NamenInvoeren()
        {
            InitializeComponent();
            //namenGrid.Focus();
            //tekstAnimatieTimer.Tick += tekstAnimatie;
            //tekstAnimatieTimer.Interval = TimeSpan.FromMilliseconds(50);
            //tekstAnimatieTimer.Start();
        }

        public void tekstAnimatie()
        {

        }
        private void inputPlayer1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnFocus1(object sender, RoutedEventArgs e)
        {
            inputPlayer1.Text = "";
        }

        private void OnFocus2(object sender, RoutedEventArgs e)
        {
            inputPlayer2.Text = "";
        }

        private void FocusLostInvoer1(object sender, RoutedEventArgs e)
        {
            if (inputPlayer1.Text == "")
            {
                inputPlayer1.Text = "Vul speler naam 1 in";
            }
        }

        private void FocusLostInvoer2(object sender, RoutedEventArgs e)
        {
            if (inputPlayer2.Text == "")
            {
                inputPlayer2.Text = "Vul speler naam 2 in";
            }
        }

        private void startSpel(object sender, RoutedEventArgs e)
        {
            // namen opslaan in de GameWindow voor in het level
            GameWindow.speler1Naam = inputPlayer1.Text;
            GameWindow.speler2Naam = inputPlayer2.Text;

            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
