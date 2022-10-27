using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
    /// interactie logica voor PauzeMenu.xaml
    /// </summary>
    public partial class PauzeMenu : Window
    {
        // Alias aanmaken voor de GameWindow.
        GameWindow game;

        /// <summary>
        /// Pauzemenu openen en focus verleggen op het pauze scherm.
        /// </summary>
        /// <param name="game">GameWindow properties</param>
        public PauzeMenu(GameWindow game)
        {
            InitializeComponent();
            this.game = game; 
            newcanvas.Focus();
        }

        /// <summary>
        /// Als de escape toets is ingedrukt wordt sluit het PauzeMenu en gaat het spel verder.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event OnKeyDown</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
                GameWindow.SpeelVerder();
            }
        }

        /// <summary>
        /// Sluit het PauzeMenu en het spel gaat verder.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event VerderSpelenButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void VerderSpelenButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            GameWindow.SpeelVerder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Aanroepende object van het event HerstartenButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void HerstartenButtonClick(object sender, RoutedEventArgs e)
        {
            // Zwaartekracht wordt aangezet voor het nieuwe spel.
            GameWindow.zwaartekrachtDisabled = false;

            // Nieuw MainWindow wordt aangemaakt.
            MainWindow MW = new MainWindow();
            game.Close();

            // Nieuw GameWindow wordt aangemaakt.
            GameWindow gw = new GameWindow();

            // GameWindow wordt geopend.
            gw.Visibility = Visibility.Visible;

            // Pauze menu sluiten.
            this.Close();

            // MainWindow sluiten.
            MW.Close();
        }

        /// <summary>
        /// MainWindow openen en het huidige scherm afsluiten.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event HoofmenuButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        public void HoofmenuButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow MW = new MainWindow();
            MW.Visibility = Visibility.Visible;
            game.Close();
        }
    }
}