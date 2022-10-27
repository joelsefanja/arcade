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
        /// <summary>
        /// 3e strip scherm beginnen. 
        /// </summary>
        public StripEind()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Scores scherm wordt gestart en het huidige scherm wordt afgesloten.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event ScoreBekijkenClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void ScoreBekijkenClick(object sender, RoutedEventArgs e)
        {
            Scores Sc = new Scores();
            Sc.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
