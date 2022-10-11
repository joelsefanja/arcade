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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Arcade;

namespace Arcade
{
    /// <summary>
    /// Interaction logic for NamenInvoeren.xaml
    /// </summary>
    public partial class NamenInvoeren : Window
    {
        string spelerNaam1, spelerNaam2;
        public NamenInvoeren()
        {
            InitializeComponent();
        }

        private void startSpel(object sender, RoutedEventArgs e)
        {
            // namen opslaan in de GameWindow voor in het level
            GameWindow.playerName1 = inputPlayer1.Text;
            GameWindow.playerName2 = inputPlayer2.Text;

            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
