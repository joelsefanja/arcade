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
    /// Interaction logic for credits.xaml
    /// </summary>
    public partial class credits : Window
    {
        public credits()
        {
            InitializeComponent();
        }

        private void TerugHoofmenuButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
