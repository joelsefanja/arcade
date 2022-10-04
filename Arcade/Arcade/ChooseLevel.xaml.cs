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

namespace Arcade
{
    /// <summary>
    /// Interaction logic for ChooseLevel.xaml
    /// </summary>
    public partial class ChooseLevel : Window
    {
        public ChooseLevel()
        {
            InitializeComponent();
        }

        private void StartLevel1(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
        }

        private void StartLevel2(object sender, RoutedEventArgs e)
        {
            level2 l2 = new level2();
            l2.Visibility = Visibility.Visible;
        }
    }
}
