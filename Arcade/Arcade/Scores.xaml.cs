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
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Window
    {
        public Scores()
        {
            InitializeComponent();

            // Tijd label aanpassen naar de gezamelijke tijd + de tijd die opgeslagen is in de GameWindow.
            Tijd.Content = "Gezamelijke tijd:" + GameWindow.TijdMetNul;

            // Score label van speler 1 en speler 2 aanpassen naar het aantal munten dat opgeslagen is in de GameWindow.
            ScoreSpeler1.Content = "Munten " + GameWindow.speler1Naam + ": " + GameWindow.muntenSpeler1;
            ScoreSpeler2.Content = "Munten " + GameWindow.speler2Naam + ": " + GameWindow.muntenSpeler2;

            // Label content aanpassen zodat wordt weergegeven wie de meeste munten heeft.
            MeesteMunten.Content = GameWindow.MeesteMunten;

            GameWindow.muntenSpeler1 = 0;
            GameWindow.muntenSpeler2 = 0;
        }

        /// <summary>
        /// MainWindow openen en het huidige scherm afsluiten.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event TerugHoofdMenuClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void TerugHoofdMenuClick(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
