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
    /// Interaction logic for Strip2.xaml
    /// </summary>
    public partial class Strip2 : Window
    {
        public Strip2()
        {
            InitializeComponent();
        }

        private void SpelBeginnenClick(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
