using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Arcade
{
    /// <summary>
    /// Interaction logic for PauzeMenu.xaml
    /// </summary>
    public partial class PauzeMenu : Window
    {
        GameWindow game;
        public PauzeMenu(GameWindow game)
        {
            InitializeComponent();
            this.game = game;
            newcanvas.Focus();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {

                GameWindow.SpeelVerder();
                this.Close();

            }
        }

        private void VerderSpelenButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            GameWindow.SpeelVerder();
        }

        private void HerstartenButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            game.Close();
            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
            MW.Close();
        }

        private void HoofmenuButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow MW = new MainWindow();
            MW.Visibility = Visibility.Visible;
            game.Close();
        }
    }
}