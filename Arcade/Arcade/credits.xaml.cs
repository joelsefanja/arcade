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
    /// Interactie logica voor credits.xaml
    /// </summary>
    public partial class credits : Window
    {
        /// <summary>
        /// Credits scherm laten beginnen.
        /// </summary>
        public credits()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Open hoofdmenu window en sluit het huidige window.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event TerugHoofdmenuButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>

        private void TerugHoofdmenuButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
