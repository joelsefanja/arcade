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
    /// Interaction logic for Strip1.xaml
    /// </summary>
    public partial class Strip1 : Window
    {
        public Strip1()
        {
            InitializeComponent();
        }

        private void VerderClick(object sender, RoutedEventArgs e)
        {
            Strip2 S2 = new Strip2();
            S2.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
