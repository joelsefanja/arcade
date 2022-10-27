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
    public partial class Strip2 : Window
    {
        /// <summary>
        /// Strip2 scherm laten beginnen.
        /// </summary>
        public Strip2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Beginnen met het spel.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event SpelBeginnenClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void SpelBeginnenClick(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow();
            gw.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
