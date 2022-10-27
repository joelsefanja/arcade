using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interactie logica voor Leaderboard.xaml.
    /// </summary>
    public partial class Leaderboard : Window
    {
        /// <summary>
        /// Leaderboard scherm laten beginnen.
        /// </summary>
        public Leaderboard()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Hoofdmenu weergeven en huidig scherm afsluiten.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event HoofdmenuClick</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void HoofdmenuClick(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Visibility = Visibility.Visible;
            this.Close();
        }

        /// <summary>
        /// updatePagina methode aanroepen zodra de selectie in de dataGrid wijzigd.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event dataGridScores_SelectionChanged</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void dataGridScores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updatePagina();
        }

        /// <summary>
        /// updatePagina methode aanroepen zodra de pagina geladen is.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event OnLoaded</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            updatePagina();
        }

        /// <summary>
        /// Gegevens opvragen uit de database en het updaten van de DataGrid.
        /// </summary>
        private void updatePagina()
        {
           //// informatie over de locatie van de database en hoe er mee verbonden moet worden.
           // string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =\"C:\\Users\\yvonn\\source\\repos\\joelsefanja\\arcade\\Arcade\\Arcade\\data\\GameDatabase.mdf\";Integrated Security=True";

           // // SQL Query voor het opvragen van de naam, tijd en munten van spelers uit de highscores tabel.
           // // De uitvoer wordt gesorteerd op spelers tijd en vervolgens op munten in aflopende volgorde.
           // string query = "SELECT [spelerNaam],[spelerTijd],[spelerMunten] FROM [Highscores] ORDER BY [spelerTijd],[spelerMunten] DESC";

           // // Maakt een nieuwe connectie aan met de gegeven connectieString.
           // SqlConnection connection = new SqlConnection(connectionString);

           // // Maakt een nieuw SQL Query/commando aan en gebruik daar de gegven query en connectionString voor.
           // SqlCommand command = new SqlCommand(query, connection);

           // // Probeer eerst highscores tabel op te vragen.
           // try
           // {
           //     // Zet de gegeven query als de tekst voor het commando.
           //     command.CommandText = query;

           //     // Zet het text type voor het commando als text.
           //     command.CommandType = CommandType.Text;

           //     // Zet de connectie van het commando als de gegeven connection.
           //     command.Connection = connection;

           //     // Open de connectie.
           //     connection.Open();

           //     // Voer het commando uit.
           //     command.ExecuteNonQuery();

           //     // Data adapter gebruikt de data in de database om de dataGrid in het xaml scherm bij te werken.
           //     SqlDataAdapter dataAdp = new SqlDataAdapter(command);
           //     DataTable dt = new DataTable("Highscores");

           //     // Vul de datatabel in het xaml scherm met de highscores tabel.
           //     dataAdp.Fill(dt);

           //     // Gebruik de gevulde data tabel om de dataGridScores tabel in te vullen.
           //     dataGridScores.ItemsSource = dt.DefaultView; 

           //     dataAdp.Update(dt);
           //     connection.Close();
           // }

           // // Als connectie met de database niet lukt laat dan de foutmeldingen zien in een berichtbox.
           // catch (Exception ex)
           // {
           //     connection.Close();
           //     MessageBox.Show(ex.Message);
           // }
        }
    }
    
}
