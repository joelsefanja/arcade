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

    public partial class Strip1 : Window
    {
        /// <summary>
        /// Strip1 scherm laten beginnen.
        /// </summary>
        public Strip1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Verder gaan naar de volgende strip pagina.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event VerderClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void VerderClick(object sender, RoutedEventArgs e)
        {
            Strip2 S2 = new Strip2();
            S2.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
