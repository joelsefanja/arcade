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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arcade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void LeaderBoardButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void CreditsButtonClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// QuitButtonClick sluit het programma zodra de knop 'Quit' is ingedrukt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
