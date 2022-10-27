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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arcade
{
    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// StartButtonClick sluit de Main Window en opent het NamenInvoeren scherm zodra de knop "Start" is ingedrukt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event StartButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
           
            NamenInvoeren ni = new NamenInvoeren();
            ni.Visibility = Visibility.Visible;
            this.Close();
            
        }
        /// <summary>
        /// LeaderBoardButtonClick sluit de Main Window en opent het Scores scherm zodra de knop "Scorebord" is ingedrukt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event LeaderBoardButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void LeaderBoardButtonClick(object sender, RoutedEventArgs e)
        {
            Leaderboard lb = new Leaderboard();
            lb.Visibility = Visibility.Visible;
            this.Close();

        }

        /// <summary>
        /// CreditsButtonClick sluit de Main Window en opent het credits scherm zodra de knop "Credits" is ingedrukt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event CreditsButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void CreditsButtonClick(object sender, RoutedEventArgs e)
        {
            credits cr = new credits();
            cr.Visibility = Visibility.Visible;
            this.Close();
        }

        /// <summary>
        /// QuitButtonClick sluit het programma zodra de knop 'Quit' is ingedrukt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event QuitButtonClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
