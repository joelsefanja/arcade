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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Arcade;

namespace Arcade
{
   
    public partial class NamenInvoeren : Window
    {
        /// <summary>
        /// Open het NamenInvoeren scherm en focus daarop.
        /// </summary>
        public NamenInvoeren()
        {
            InitializeComponent();
            namenGrid.Focus();
            
        }

        /// <summary>
        /// Als er op het eerste invoerveld wordt gefocust wordt het veld vervangen met een lege string.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event OnFocus1</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void OnFocus1(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer1.Text == "Vul speler naam 1 in")
            {
            invoerPlayer1.Text = "";
            }
        }

        /// <summary>
        /// Als er op het tweede invoerveld wordt gefocust wordt het veld vervangen met een lege string.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event OnFocus2</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void OnFocus2(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer2.Text == "Vul speler naam 2 in")
            {
                invoerPlayer2.Text = "";
            }
        }

        /// <summary>
        /// Geeft een aanwijzing om de spelernaam in te vullen weer als de focus verloren wordt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event FocusLostInvoer1</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void FocusLostInvoer1(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer1.Text == "")
            {
                invoerPlayer1.Text = "Vul speler naam 1 in";
            }
        }

        /// <summary>
        ///  Geeft een aanwijzing om de spelernaam in te vullen weer als de focus verloren wordt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event FocusLostInvoer2</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void FocusLostInvoer2(object sender, RoutedEventArgs e)
        {
            if (invoerPlayer2.Text == "")
            {
                invoerPlayer2.Text = "Vul speler naam 2 in";
            }
        }

        /// <summary>
        /// Als deze knop is ingedrukt sluit dit scherm en start het spel verder.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event startSpel</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void startSpel(object sender, RoutedEventArgs e)
        {
            // Standaard namen opslaan in de GameWindow als de plaatshoudende tekst niet is vervangen.
            if (invoerPlayer1.Text == "Vul speler naam 1 in")
            { GameWindow.speler1Naam = "Speler 1"; }

            // Ingevoerde namen opslaan in de GameWindow.
            else
            { GameWindow.speler1Naam = invoerPlayer1.Text; }

            // Standaard namen opslaan in de GameWindow als de plaatshoudende tekst niet is vervangen.
            if (invoerPlayer2.Text == "Vul speler naam 2 in")
            { GameWindow.speler2Naam = "Speler 2"; }

            // Ingevoerde namen opslaan in de GameWindow.
            else
            { GameWindow.speler2Naam = invoerPlayer2.Text; }

            // Strip1 scherm openen en het huidige scherm sluiten.
            Strip1 S1 = new Strip1();
            S1.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
