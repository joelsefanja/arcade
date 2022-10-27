using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Policy;

namespace Arcade
{
    public partial class GameWindow : Window
    {
        public static DispatcherTimer timer = new DispatcherTimer(); // Timer voor de mechanica van het spel.
        public static DispatcherTimer spelTimer = new DispatcherTimer(); // Timer voor de score van de spelers.
        int snelheid = 8; // Spelersnelheid van de spelers.
        int zwaartekracht = 10; // Zwaartekracht van de spelers.
        int vliegSnelheid = 20; // Vlieg snelheid.
        bool speler1NaarLinks, speler1NaarRechts, speler1Springt = false; // Beweging speler 1.
        bool speler2NaarLinks, speler2NaarRechts, speler2Springt = false; // Beweging speler 2.
        public static int muntenSpeler1 = 0, muntenSpeler2 = 0; // Score speler 1 & 2.
        int tijdSeconden = 0, tijdMinuten = 0; // Seconden & minuten tellers voor tijdens het spel.
        string seconden, minuten; // Prefix voor de timer onder de 10 seconden & prefix voor de timer onder de 60 minuten.
        public static string speler1Naam, speler2Naam; // Spelers namen; kunnen met externe .xaml.cs bestanden benaderd worden.
        public static string tijdSpeler1, tijdSpeler2; // Spelertijd die wordt opgeslagen in de database.
        bool moveEnemyRightOne = true, moveEnemyRightTwo = true; // Beweging van de enemies.
        int enemySpeed = 3; // Snelheid van de enemies.
        bool scoresZijnOpgeslagen; // Zorgt ervoor dat scores niet dubbel worden opgeslagen.
        bool buttonActive = false; // Laat zien of een knop actief is.
        bool sleutelOpgepakt = false; // Laat zien of de sleutel opgepakt is.
        public static string TijdMetNul = "0"; // Losse seconden en minuten worden hierin samengevoegd tot een string 00:00.
        public static string MeesteMunten; // Variable met de speler met de meeste munten.

        // Variablen die gebruikt worden tijdens pauze.
        public static bool zwaartekrachtDisabled = false;
        public static bool pauze = false;



        /// <summary>
        /// GameWindow bevat alle game logica die tijdens het spel gebruikt wordt.
        /// </summary>
        public GameWindow()
        {
            InitializeComponent();
            newcanvas.Focus(); // Focused op het spel canvas zodat ingedrukte toetsen en muisklikken kunnen worden waargenomen.
            updateNames();
            
            // Spelmuziek toegevoegd.
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\joels\Source\Repos\joelsefanja\arcade\Arcade\Arcade\audio\Fairy_Spring_Tune.wav");
            player.Play();


            // Game timer aanmaken en aanzetten.
            timer.Tick += GameTimer;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();


            // Spel timer aanmaken en aanzetten.
            spelTimer.Tick += spelersTimer;
            spelTimer.Interval = TimeSpan.FromSeconds(1);
            spelTimer.Start();

            // Plaats de spelers op het onderste platform.
            spelersNaarBeginpunt();
        }

        /// <summary>
        /// Roept bij elke 20ms alle methoden op voor het spel.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event GameTimer</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void GameTimer(object sender, EventArgs e)
        {
            // Controleert of de spelers zwaartekracht moeten krijgen.
            zwaartekrachtBerekenenSpelers();

            // Controleert of spelers en monsters moeten bewegen.
            bewegingSpeler1();
            bewegingSpeler2();
            bewegingMonster(enemy1, eiland15, eiland16, ref moveEnemyRightOne);
            bewegingMonster(enemy2, eiland28, eiland29, ref moveEnemyRightTwo);

            // Controleert de interactie tussen elke speler en de verschillende obstakels.
            interactieMetMonster();
            interactieMetPlatform();
            interactieMetEiland();
            interactieMetMuur();
            interactieMetMunt();
            interactieMetDeur();
            interactieMetSleutel();
            interactieMetKnoppen();
           
            // Controleert of zwaartekracht aan of uit moet worden gezet.
            if (zwaartekrachtDisabled) { zwaartekracht = 0; } else { zwaartekrachtDisabled = false; zwaartekracht = 10; }
        }

        /// <summary>
        /// Verplaats de spelers naar het beginpunt van het spel.
        /// </summary>
        public void spelersNaarBeginpunt()
        {
            Canvas.SetBottom(Speler1, Canvas.GetTop(platform1));
            Canvas.SetBottom(Speler2, Canvas.GetTop(platform2));
        }

        /// <summary>
        /// Controleert of speler 1 moet bewegen en beweegt speler 1.
        /// </summary>
        public void bewegingSpeler1()
        {
            // Beweeg speler 1 naar links als de conditie waar is. 
            if (speler1NaarLinks == true && Canvas.GetLeft(Speler1) > 0) 
            {
                Canvas.SetLeft(Speler1, Canvas.GetLeft(Speler1) - snelheid);
            }

            // Beweeg speler 1 naar rechts als de conditie waar is. 
            if (speler1NaarRechts == true && Canvas.GetLeft(Speler1) + (Speler1.Width) < 1200)
            {

                Canvas.SetLeft(Speler1, Canvas.GetLeft(Speler1) + snelheid);
            }

            // Beweeg speler 1 naar omhoog als de conditie waar is. 
            if (speler1Springt == true)
            {
                Canvas.SetTop(Speler1, Canvas.GetTop(Speler1) - vliegSnelheid);
            }
        }

        /// <summary>
        /// Controleert of speler 2 moet bewegen en beweegt speler 2.
        /// </summary>
        public void bewegingSpeler2()
        {
            // Beweeg speler 2 naar links als de conditie waar is. 
            if (speler2NaarLinks == true && Canvas.GetLeft(Speler2) > 0) 
            {
                Canvas.SetLeft(Speler2, Canvas.GetLeft(Speler2) - snelheid);
            }

            // Beweeg speler 2 naar rechts als de conditie waar is.
            if (speler2NaarRechts == true && Canvas.GetLeft(Speler2) + (Speler2.Width) < 1200) 
            {
                Canvas.SetLeft(Speler2, Canvas.GetLeft(Speler2) + snelheid);
            }

            // Beweeg speler 2 naar omhoog als de conditie waar is.
            if (speler2Springt == true) 
            {
                Canvas.SetTop(Speler2, Canvas.GetTop(Speler2) - vliegSnelheid);  
            }
        }

        /// <summary>
        /// Beweegt de monsters heen en weer op de platformen waar zij op staan.
        /// </summary>
        /// <param name="enemy">De eigenschappen van de enemy.</param>
        /// <param name="left">De eigenschappen van het linker platform.</param>
        /// <param name="right">De eigenschappen van het rechter platform.</param>
        /// <param name="moveEnemyRight">Geeft aan of een enemy naar rechts moet bewegen of niet.</param>
        private void bewegingMonster(Rectangle enemy, Rectangle left, Rectangle right, ref bool moveEnemyRight)
        {
            // Beweeg enemy naar rechts als de conditie waar is. 
            if (moveEnemyRight.Equals(true) && Canvas.GetLeft(right) + right.Width > Canvas.GetLeft(right) + enemy.Width)
            {
                Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) + enemySpeed);

                    // Als het einde van het eiland is bereikt wissel dan van beweegrichting.
                    if (Canvas.GetLeft(right) + right.Width <= Canvas.GetLeft(enemy) + enemy.Width)
                    {
                        moveEnemyRight = false;
                    }
            }

            // Beweeg enemy naar links als de conditie waar is.
            if (moveEnemyRight.Equals(false) && Canvas.GetLeft(left) < Canvas.GetLeft(enemy))
            {
                Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) - enemySpeed);

                // Als het einde van het eiland is bereikt wissel dan van beweegrichting.
                if (Canvas.GetLeft(left) >= Canvas.GetLeft(enemy))
                    {
                        moveEnemyRight = true;
                    }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de platformen en plaatst hen er bovenop.
        /// </summary>
        public void interactieMetPlatform()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                // Als tag gelijk is aan platform worden hitboxen voor platformen en spelers gecreërd.
                if ((string)x.Tag == "platform") 
                {
                    Rect speler1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect speler2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect platformhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor platform.

                    // Als speler 1 op platform staat, verplaats de speler naar boven op het platform.
                    if (speler1hitbox.IntersectsWith(platformhitbox)) 
                    {
                        Canvas.SetTop(Speler1, Canvas.GetTop(x) - Speler1.Height); 
                    }

                    // Als speler 2 op platform staat, verplaats de speler naar boven op het platform.
                    if (speler2hitbox.IntersectsWith(platformhitbox)) 
                    {
                        Canvas.SetTop(Speler2, Canvas.GetTop(x) - Speler2.Height);
                    }
                }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de eilanden en plaatst hen er bovenop of eronder.
        /// </summary>
        public void interactieMetEiland()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                // Als tag gelijk is aan "eiland" worden hitboxen voor eilanden en spelers gecreërd.
                if ((string)x.Tag == "eiland") 
                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1) + 5, Canvas.GetTop(Speler1), Speler1.Width - 10, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2) + 25, Canvas.GetTop(Speler2), Speler2.Width - 30, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect eilandhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor rechthoeken met tag eiland.

                    // Als speler 1 tegen de onder kant van een eiland bevind, verplaats de speler naar de onderkant.
                    if (eilandhitbox.Bottom >= player1hitbox.Top && eilandhitbox.Bottom < player1hitbox.Bottom && player1hitbox.IntersectsWith(eilandhitbox))
                    { 
                        Canvas.SetTop(Speler1, eilandhitbox.Bottom); 
                    }
                        // Als speler 1 enkel interactie met het eiland heeft, verplaats speler naar de bovenkant van het eiland.
                        else if (player1hitbox.IntersectsWith(eilandhitbox))
                            {
                                Canvas.SetTop(Speler1, Canvas.GetTop(x) - Speler1.Height); 
                            }

                    // Als speler 2 tegen de onder kant van een eiland bevind, verplaats de speler naar de onderkant.
                    if (eilandhitbox.Bottom >= player2hitbox.Top && eilandhitbox.Bottom < player2hitbox.Bottom && player2hitbox.IntersectsWith(eilandhitbox))
                    { 
                        Canvas.SetTop(Speler2, eilandhitbox.Bottom); 
                    }
                        // Als speler 2 enkel interactie met het eiland heeft, verplaats speler naar de bovenkant van het eiland.
                        else if (player2hitbox.IntersectsWith(eilandhitbox))
                            {
                                Canvas.SetTop(Speler2, Canvas.GetTop(x) - Speler2.Height);
                            } 
                }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de monsters, 
        /// als er interactie is worden de spelers verplaatst naar onder.
        /// </summary>
        public void interactieMetMonster()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                // Als tag gelijk is aan "enemy" worden hitboxen voor enemies en spelers gecreërd.
                if ((string)x.Tag == "enemy")

                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox maken voor speler 1.
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox maken voor speler 2.
                    Rect monsterhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox maken voor enemy.

                    // Als speler 1 interactie heeft met een monster verplaats speler, plaats sleutel terug en sluit de deur.
                    if (player1hitbox.IntersectsWith(monsterhitbox))
                    {
                        // Verplaats de speler naar de bovenkant van platform 1.
                        Canvas.SetTop(Speler1, Canvas.GetTop(platform1));

                        // Geef de sleutel opnieuw weer.
                        sleutelOpgepakt = false;
                        Sleutel1.Visibility = Visibility.Visible;

                        // Doet de deur weer dicht.
                        deurOpen.Visibility = Visibility.Hidden;

                    }

                    // Als speler 2 interactie heeft met een monster verplaats speler, plaats sleutel terug en sluit de deur.
                    if (player2hitbox.IntersectsWith(monsterhitbox))
                    {
                        // Verplaats de speler naar de bovenkant van platform 2.
                        Canvas.SetTop(Speler2, Canvas.GetTop(platform2));

                        // Geef de sleutel opnieuw weer.
                        sleutelOpgepakt = false;
                        Sleutel1.Visibility = Visibility.Visible;

                        // Doet de deur weer dicht.
                        deurOpen.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de muur en zorgt ervoor dat de spelers tegen worden gehouden.
        /// </summary>
        public void interactieMetMuur()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "muur")
                {
                    Rect speler1muurhitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect speler2muurhitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect muurhitbox = new Rect(Canvas.GetLeft(x) - snelheid, Canvas.GetTop(x), x.Width + snelheid, x.Height); // Hitbox aanmaken voor muur.
                    Rect deurhitbox = new Rect(Canvas.GetLeft(deur), Canvas.GetTop(deur), deur.Width, deur.Height); // Hitbox aanmaken voor muur.

                    // Als speler 1 de muur raakt aan de linkerkant en/of boven op de muur staat verplaats de speler naar links.
                    if (muurhitbox.Left <= speler1muurhitbox.Right
                        && muurhitbox.Right > speler1muurhitbox.Right
                        && speler1muurhitbox.Top >= deurhitbox.Bottom
                        && speler1muurhitbox.IntersectsWith(muurhitbox))
                    { 
                        Canvas.SetLeft(Speler1, muurhitbox.Left - speler1muurhitbox.Width); 
                    }

                        // Als speler 1 de muur raakt aan de rechterkant en/of boven op de muur staat verplaats de speler naar rechts.
                        else if (muurhitbox.Right >= speler1muurhitbox.Left
                                    && muurhitbox.Left < speler1muurhitbox.Left
                                    && speler1muurhitbox.Top >= deurhitbox.Bottom 
                                    && speler1muurhitbox.IntersectsWith(muurhitbox))
                                        { 
                                            Canvas.SetLeft(Speler1, muurhitbox.Right); 
                                        }

                    // Als speler 2 de muur raakt aan de linkerkant en/of boven op de muur staat verplaats de speler naar links.
                    if (muurhitbox.Left <= speler2muurhitbox.Right
                        && muurhitbox.Right > speler2muurhitbox.Right
                        && speler2muurhitbox.Top >= deurhitbox.Bottom
                        && speler2muurhitbox.IntersectsWith(muurhitbox))
                            { 
                                Canvas.SetLeft(Speler2, muurhitbox.Left - speler2muurhitbox.Width); 
                            }

                    // Als speler 2 de muur raakt aan de rechterkant en/of boven op de muur staat verplaats de speler naar rechts.
                    else if (muurhitbox.Right >= speler2muurhitbox.Left
                        && muurhitbox.Left < speler2muurhitbox.Left
                        && speler2muurhitbox.Top >= deurhitbox.Bottom 
                        && speler2muurhitbox.IntersectsWith(muurhitbox))
                            { 
                                Canvas.SetLeft(Speler2, muurhitbox.Right); 
                            }
                }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de munt.
        /// </summary>
        public void interactieMetMunt()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "coin")
                {
                    Rect speler1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect speler2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect coinhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor munt.

                    // Als speler 1 een munt raakt en de munt zichtbaar is pas de score van de speler aan.
                    if ((speler1hitbox.IntersectsWith(coinhitbox)) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        muntenSpeler1++;
                        scoreSpeler1Label.Content = "Munten: " + muntenSpeler1;

                    }

                    // Als speler 2 een munt raakt en de munt zichtbaar is pas de score van de speler aan.
                    if ((speler2hitbox.IntersectsWith(coinhitbox)) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        muntenSpeler2++;
                        scoreSpeler2Label.Content = "Munten: " + muntenSpeler2;

                    }
                }
            }

            // Als speler 1 de meeste munten heeft update de MeesteMunten variable.
            if (muntenSpeler1 > muntenSpeler2)
            {
                MeesteMunten = speler1Naam + " heeft gewonnen \n met de meeste munten!";
            }
                // Als speler 2 de meeste munten heeft update de MeesteMunten variable.
                else if (muntenSpeler2 > muntenSpeler1)
                    {
                        MeesteMunten = speler2Naam + " heeft gewonnen \n  met de meeste munten!";
                    }

                    // Als spelers gelijk aantal munten hebben update de MeesteMunten variable.
                    else
                    {
                        MeesteMunten = "Beide spelers hebben evenveel munten, gelijkspel!";
                    }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de deur en beëindigt spel.
        /// </summary>
        public void interactieMetDeur()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "deur")
                {
                    Rect speler1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect speler2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect deurhitbox    = new Rect(Canvas.GetLeft(x),       Canvas.GetTop(x),             x.Width,       x.Height); // Hitbox aanmaken voor de deur.

                    // Als beide spelers de deur aanraken en de sleutel is opgepakt wordt het spel beëindigt.
                    if (speler1hitbox.IntersectsWith(deurhitbox) && speler2hitbox.IntersectsWith(deurhitbox) && sleutelOpgepakt == true)
                    {
                        // Spelers tijd opslaan.
                        tijdOpslaan();
                        
                        // Als de scores nog niet zijn opgeslagen, dan worden de scores opgeslagen en het spel gestopt.
                        if (scoresZijnOpgeslagen == false) { 
                            scoresZijnOpgeslagen = true;

                        // Naam, tijd en aantal munten in de highscores tabel opslaan.
                        AddHighscoreToDatabase(speler1Naam, tijdSpeler1, muntenSpeler1);
                        AddHighscoreToDatabase(speler2Naam, tijdSpeler2, muntenSpeler2);

                            timer.Stop();
                        spelTimer.Stop();

                        // Eindstrip wordt weergegeven.
                        StripEind SE = new StripEind();
                        SE.Visibility = Visibility.Visible;
                        this.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de sleutel.
        /// </summary>
        public void interactieMetSleutel()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                Rect hitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor de sleutel.
                Rect hitboxspeler1 = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                Rect hitboxspeler2 = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.

                // Als een speler de sleutel aanraakt gaat de deur open en verdwijnt de sleutel.
                if ((string)x.Tag == "Sleutel")
                {
                    if (hitboxspeler1.IntersectsWith(hitbox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        sleutelOpgepakt = true;
                        deurOpen.Visibility = Visibility.Visible;

                    }
                    
                    if (hitboxspeler2.IntersectsWith(hitbox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;

                        sleutelOpgepakt = true;
                        deurOpen.Visibility = Visibility.Visible;


                    }
                }
            }
        }

        /// <summary>
        /// Controleert de interactie tussen de spelers en de knoppen.
        /// </summary>
        public void interactieMetKnoppen()
        {
            // Loopt door alle rechthoeken om de tag te controleren.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Name == "Button1")

                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect buttonHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor knop 1.

                    // Als één van beide spelers op knop 1 staat wordt de schuifdeur verplaatst.
                    if (player1hitbox.IntersectsWith(buttonHitbox) || (player2hitbox.IntersectsWith(buttonHitbox)))
                    {
                        buttonActive = true;
                        Canvas.SetLeft(eiland_schuif1, 255);
                        eiland_schuif1.Visibility = Visibility.Hidden;
                    }

                    // Als er niet meer op de knop wordt gestaan wordt de schuifdeur terug geplaatst.
                    else
                    {
                            buttonActive = false;
                            Canvas.SetLeft(eiland_schuif1, 99);
                            eiland_schuif1.Visibility = Visibility.Visible;

                        }

                }
                if ((string)x.Name == "Button2")

                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect buttonHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor knop 2.

                    // Als één van beide spelers op knop 2 staat wordt de schuifdeur verplaatst.
                    if (player1hitbox.IntersectsWith(buttonHitbox) || (player2hitbox.IntersectsWith(buttonHitbox)))
                    {
                        buttonActive = true;
                        Canvas.SetLeft(eiland_schuif2, 1041);
                        eiland_schuif2.Visibility = Visibility.Hidden;

                    }
                        // Als er niet meer op de knop wordt gestaan wordt de schuifdeur terug geplaatst.

                        else
                        {
                                buttonActive = false;
                                Canvas.SetLeft(eiland_schuif2, 893);
                                eiland_schuif2.Visibility = Visibility.Visible;
                        }

                }

                if ((string)x.Name == "Button3")
                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect buttonHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor knop 3.

                    // Als één van beide spelers op knop 3 staat wordt de schuifdeur verplaatst.
                    if (player1hitbox.IntersectsWith(buttonHitbox) || (player2hitbox.IntersectsWith(buttonHitbox)))
                    {
                        buttonActive = true;
                        Canvas.SetLeft(eiland_schuif3, 845);
                        eiland_schuif3.Visibility = Visibility.Hidden;

                    }
                    
                        // Als er niet meer op de knop wordt gestaan wordt de schuifdeur terug geplaatst.
                        else
                        {
                                buttonActive = false;
                                Canvas.SetLeft(eiland_schuif3, 714);
                                eiland_schuif3.Visibility = Visibility.Visible;

                            }


                }
                if ((string)x.Name == "Button4")

                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // Hitbox aanmaken voor speler 1.
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // Hitbox aanmaken voor speler 2.
                    Rect buttonHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // Hitbox aanmaken voor knop 4.

                    // Als één van beide spelers op knop 4 staat wordt de schuifdeur verplaatst.
                    if (player1hitbox.IntersectsWith(buttonHitbox) || (player2hitbox.IntersectsWith(buttonHitbox)))
                    {
                        buttonActive = true;
                        Canvas.SetLeft(eiland_schuif4, 453);
                        eiland_schuif4.Visibility = Visibility.Hidden;
                    }

                        // Als er niet meer op de knop wordt gestaan wordt de schuifdeur terug geplaatst.
                        else
                        {
                                buttonActive = false;
                                Canvas.SetLeft(eiland_schuif4, 356);
                                eiland_schuif4.Visibility = Visibility.Visible;
                        }

                }
            }
        }

        /// <summary>
        /// Slaat de behaalde tijd op voor de spelers.
        /// </summary>
        private void tijdOpslaan()
        {
            tijdSpeler1 = TijdMetNul;
            tijdSpeler2 = TijdMetNul;
        }
        

        private void AddHighscoreToDatabase(string spelernaam, string tijd, int munten)
        {
            //// informatie over de locatie van de database en hoe er mee verbonden moet worden.
            //string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =\"C:\\Users\\yvonn\\source\\repos\\joelsefanja\\arcade\\Arcade\\Arcade\\data\\GameDatabase.mdf\";Integrated Security=True";

            //// SQL Query voor het invoegen van de naam, tijd en munten van spelers in de highscores tabel.
            //string query = "INSERT INTO [Highscores] ([spelerNaam],[spelerTijd],[spelerMunten]) VALUES ('" + spelernaam + "','" + tijd + "','" + munten + "')";

            //// Maakt een nieuwe connectie aan met de gegeven connectieString.
            //SqlConnection connection = new SqlConnection(connectionString);

            //// Maakt een nieuw SQL Query/commando aan en gebruik daar de gegeven query en connectionString voor.
            //SqlCommand command = new SqlCommand(query, connection);

            //// Probeer eerst highscores tabel op te vragen.
            //try
            //{
            //    // Zet de gegeven query als de tekst voor het commando.
            //    command.CommandText = query;

            //    // Zet het text type voor het commando als text.
            //    command.CommandType = CommandType.Text;

            //    // Zet de connectie van het commando als de gegeven connection.
            //    command.Connection = connection;

            //    // Open de connectie.
            //    connection.Open();

            //    // Voer het commando uit.
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //}

            //// Als connectie met de database niet lukt dan wordt de connectie gesloten.
            //catch (Exception)
            //{
            //    connection.Close();
            //}
        }

        /// <summary>
        /// Bereken zwaartekracht van de spelers.
        /// </summary>
        public void zwaartekrachtBerekenenSpelers()
        {
            Canvas.SetTop(Speler1, Canvas.GetTop(Speler1) + zwaartekracht); 
            Canvas.SetTop(Speler2, Canvas.GetTop(Speler2) + zwaartekracht);
        }

        /// <summary>
        /// UpdateNames-methode geeft de ingevoerde spelernamen weer op het spelscherm.
        /// </summary>
        public void updateNames()
        {
            speler1label.Content = speler1Naam; // Laat de eerste spelers naam zien in het eerste label.
            speler2label.Content = speler2Naam; // Laat de tweede spelers naam zien in het tweede label.

        }

        /// <summary>
        /// SpelerTimer-Methode berekent de timer voor in het spel. En geeft deze in het juiste tijdsformat weer.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event spelersTimer</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        public void spelersTimer(object sender, EventArgs e)
        {
            tijdSeconden++;

            if (tijdSeconden == 60) // Elke minuut wordt er een minuuut erbij tellen.
            {
                tijdMinuten++;
                tijdSeconden = 0;
            }

            // Tijd format instellen (mm:ss).
            if (tijdSeconden < 10) // Prefix aangemaakt bij seconden onder de 10 (00, 01, 02 etc.).
            {
                seconden = "0" + tijdSeconden.ToString();
            }
            else { seconden = tijdSeconden.ToString(); }

            // Prefix aangemaakt bij minuten onder de 60 (00:00, 01:00, 02:00 etc.).
            if (tijdMinuten < 60)
            {
                minuten = "0" + tijdMinuten.ToString();
            }
            else if (tijdMinuten > 60)
            {
                minuten = tijdMinuten.ToString();
            }
            TijdMetNul = minuten + ":" + seconden;
            tijd.Content = "tijd: " + TijdMetNul;

        }

        /// <summary>
        /// Controleert of toetsen worden ingedrukt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event keydown</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void keydown(object sender, KeyEventArgs e)
        {
            // Als toets A is ingedrukt kan speler 1 naar links bewegen.
            if (e.Key == Key.A)
            {
                speler1NaarLinks = true;
            }

            // Als toets D is ingedrukt kan speler 1 naar rechts bewegen.
            if (e.Key == Key.D)
            {
                speler1NaarRechts = true; 
            }

            // Als toets W is ingedrukt kan speler 1 omhoog bewegen.
            if (e.Key == Key.W) 
            {
                speler1Springt = true; 
            }

            // Als toets Left is ingedrukt kan speler naar links bewegen.
            if (e.Key == Key.Left)
            {
                speler2NaarLinks = true;
            }

            // Als toets Right is ingedrukt kan speler 2 naar rechts bewegen.
            if (e.Key == Key.Right)
            {
                speler2NaarRechts = true;
            }

            // Als toets Up is ingedrukt kan speler 2 omhoog bewegen.
            if (e.Key == Key.Up)
            {
                speler2Springt = true; 
            }

            // Als toets Escape is ingedrukt kan speler spel pauzeren.
            if (e.Key == Key.Escape)
            {

               // Zwaartkracht uitschakelen.
                zwaartekrachtDisabled = true;

                // Speler beweging uitzetten.
                speler1NaarLinks = speler1NaarRechts = speler1Springt = false;
                speler2NaarLinks = speler2NaarRechts = speler2Springt = false;

                // Pauze menu openen.
                pauze = true;
                PauzeMenu pm = new PauzeMenu(this);
                pm.Visibility = Visibility.Visible;

                // Timers worden gepauzeerd.
                spelTimer.Stop();
                timer.Stop();
            }
        }

        /// <summary>
        /// Controleert of toetsen niet langer worden ingedrukt.
        /// </summary>
        /// <param name="sender">Aanroepende object van het event keyup</param>
        /// <param name="e">Extra informatie over aanroepend object</param>
        private void keyup(object sender, KeyEventArgs e)
        {
            // Als toets A is losgelaten kan speler 1 niet meer naar links bewegen.
            if (e.Key == Key.A)
            {
                speler1NaarLinks = false; 
            }

            // Als toets D is losgelaten kan speler 1 niet meer naar links bewegen.
            if (e.Key == Key.D)
            {
                speler1NaarRechts = false; 
            }

            // Als toets W is losgelaten kan speler 1 niet meer naar links bewegen.
            if (e.Key == Key.W)
            {
                speler1Springt = false;
            }


            // Als toets Left is losgelaten kan speler 2 niet meer naar links bewegen.
            if (e.Key == Key.Left)
            {
                speler2NaarLinks = false; 
            }

            // Als toets Right is losgelaten kan speler 2 niet meer naar links bewegen.
            if (e.Key == Key.Right)
            {
                speler2NaarRechts = false; 
            }

            // Als toets Up is losgelaten kan speler 2 niet meer naar links bewegen.
            if (e.Key == Key.Up)
            {
                speler2Springt = false;
            }

            // Game van pauze afhalen als toets Escape wordt losgelaten.
            if (e.Key == Key.Escape)
            {
                zwaartekrachtDisabled = false;

                pauze = false;
                PauzeMenu pm = new PauzeMenu(this);


            }

        }

        /// <summary>
        /// Spel verder spelen na het sluiten van het pauze menu.
        /// </summary>
        public static void SpeelVerder()
        {
            pauze = false;
            zwaartekrachtDisabled = false;
            timer.Start();
            spelTimer.Start();
        }
    }
}
