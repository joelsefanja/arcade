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
        
        public NamenInvoeren()
        {
            InitializeComponent();
            namenGrid.Focus();
            
        }

    
        private void inputPlayer1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnFocus1(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer1.Text == "Vul speler naam 1 in")
            {
            invoerPlayer1.Text = "";
            }
        }

        private void OnFocus2(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer2.Text == "Vul speler naam 2 in")
            {
                invoerPlayer2.Text = "";
            }
        }

        private void FocusLostInvoer1(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer1.Text == "")
            {
                invoerPlayer1.Text = "Vul speler naam 1 in";
            }
        }

        private void FocusLostInvoer2(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer2.Text == "")
            {
                invoerPlayer2.Text = "Vul speler naam 2 in";
            }
        }

        private void startSpel(object sender, RoutedEventArgs e)
        {
            // namen opslaan in de GameWindow voor in het level
            if (invoerPlayer1.Text == "Vul speler naam 1 in")
            { GameWindow.speler1Naam = "Speler 1"; }
            else
            { GameWindow.speler1Naam = invoerPlayer1.Text; }

            if (invoerPlayer2.Text == "Vul speler naam 2 in")
            { GameWindow.speler2Naam = "Speler 2"; }
            else
            { GameWindow.speler2Naam = invoerPlayer2.Text; }

            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
