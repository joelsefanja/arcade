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
    /// Interaction logic for StripEind.xaml
    /// </summary>
    public partial class StripEind : Window
    {
        public StripEind()
        {
            InitializeComponent();
        }

        private void ScoreBekijkenClick(object sender, RoutedEventArgs e)
        {
            Scores Sc = new Scores();
            Sc.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
