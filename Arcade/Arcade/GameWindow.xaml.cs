using System;
using System.Collections.Generic;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace Arcade
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public static DispatcherTimer timer = new DispatcherTimer(); // TIMER VOOR DE MECHANICA VAN HET SPEL
        public static DispatcherTimer spelTimer = new DispatcherTimer(); // TIMER VOOR DE SCORE VAN DE SPELERS

        int snelheid = 8; // SPELERSNELHEID
        int zwaartekracht = 10; // ZWAARTEKRACHT
        int springSnelheid = 20; // SPRING SNELHEID / SPRING HOOGTE
        bool speler1NaarLinks, speler1NaarRechts, speler1Springt = false; // BEWEGING SPELER 1
        bool speler2NaarLinks, speler2NaarRechts, speler2Springt = false; // BEWEGING SPELER 2
        int score = 0, score2 = 0; // SCORE SPELER 1 & 2
        int tijdSeconden = 0, tijdMinuten = 0; // SECONDEN & MINUTEN TELLERS VOOR TIJDENS HET SPEL
        string seconden, minuten; // PREFIX VOOR DE TIMER ONDER DE 10 SECONDEN & PREFIX VOOR DE TIMER ONDER DE 60 MINUTEN 
        public static string speler1Naam, speler2Naam; // SPELERS NAMEN; KUNNEN MET EXTERNE .XAML.CS BESTANDEN BENADERD WORDEN
        string spelerGewonnen = "Onbekende Winnaar";

        bool moveEnemyRightOne = true;
        bool moveEnemyRightTwo = true;
        int enemySpeed = 5;


        // VARIABLEN VOOR TIJDENS PAUZE
        public static bool zwaartekrachtDisabled = false;
        public static bool pauze = false;

        //TODO GAME WINDOW METHODE BESCHIJVEN: BIJVOORBEELD: ZET VERSCHILLENDE (BESCHIJF WELKE) ONDERELEN KLAAR VOOR DE GAME
        public GameWindow()
        {
            InitializeComponent();
            newcanvas.Focus(); // FOCUS OP HET SPEL CANVAS ZODAT INGEDRUKTE TOETSEN EN MUISKLIKKEN KUNNEN WORDEN WAARGENOMEN
            updateNames(); // ZET DE INGEVOERDE NAMEN ALS LABELS

            // GAME TIMER AANMAKEN EN AANZETTEN
            timer.Tick += GameTimer;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();


            // SPEL TIMER AANMAKEN EN AANZETTEN
            spelTimer.Tick += spelersTimer;
            spelTimer.Interval = TimeSpan.FromSeconds(1);
            spelTimer.Start();

            // PLAATS DE SPELERS OP HET ONDERSTE PLATFORM.
            spelersNaarBeginpunt();


        }

        private void GameTimer(object sender, EventArgs e) // DEZE METHODE WORD ELKE 20 MILISECONDEN HERHAALDELIJK UITGEVOERD
        {
            // HIER STAAN ALLE METHODEN DIE TIJDENS HET SPEL HERHAALDELIJK WORDEN UITGEVOERD EN GECONTROLEERD
            // OP BASIS VAN DEZE CONTROLE WORD ER ACTIE ONDERNOMEN IN DE VERSCHILLENDE METHODEN
            // BIJVOORBEELD: SPELERS VERPLAATST OF ZWAARTEKRACHT TOEGEPAST / UITGEZET.

            // IN DE METHODEN STAAT DE CODE VOOR ELK VERSCHILLEND ONDERDEEL.
            zwaartekrachtBerekenenSpelers();
            bewegingSpeler1();
            bewegingSpeler2();

            // BEWEGING MONSTERS
            bewegingMonster(enemy1, eiland15, eiland16, ref moveEnemyRightOne);
            bewegingMonster(enemy2, eiland28, eiland29, ref moveEnemyRightTwo);

            // DEZE METHODEN CONTROLEREN DE INTERACTIE TUSSEN EEN SPELER EN DE VERSCHILLENDE OBSTAKELS
            interactieMetMonster();
            interactieMetPlatform();
            interactieMetEiland();
            interactieMetMuur();
            interactieMetMunt();
            interactieMetDeur();


            //
            if (zwaartekrachtDisabled) { zwaartekracht = 0; } else { zwaartekrachtDisabled = false; zwaartekracht = 10; }
        }

        // TODO ALLE METHODEN BESCHRIJVEN
        // TODO UNIT TESTING TOEPASSEN
        public void spelersNaarBeginpunt()
        {
            Canvas.SetBottom(Speler1, Canvas.GetTop(platform1));
            Canvas.SetBottom(Speler2, Canvas.GetTop(platform2));
        }
        public void bewegingSpeler1()
        {
            // BEWEGINGS MECHANISMEN VOOR SPELER 1

            if (speler1NaarLinks == true && Canvas.GetLeft(Speler1) > 0) // BEWEEG NAAR LINKS BEREKENING 
            {

                Canvas.SetLeft(Speler1, Canvas.GetLeft(Speler1) - snelheid);
            }
            if (speler1NaarRechts == true && Canvas.GetLeft(Speler1) + (Speler1.Width) < 1200) // BEWEEG NAAR RECHTS BEREKENING 
            {

                Canvas.SetLeft(Speler1, Canvas.GetLeft(Speler1) + snelheid);
            }

            // TODO SPRING BEREKENING. MISSCHIEN MET EEN TIMER
            if (speler1Springt == true /*&& Canvas.GetLeft(Player) > 0*/) // SPRING OMHOOG BEREKENING 
            {
                Canvas.SetTop(Speler1, Canvas.GetTop(Speler1) - springSnelheid);
            }
        }
        public void bewegingSpeler2()
        {
            // BEWEGINGS MECHANISMEN VOOR SPELER 2
            if (speler2NaarLinks == true && Canvas.GetLeft(Speler2) > 0)  // BEWEEG NAAR LINKS BEREKENING 
            {
                Canvas.SetLeft(Speler2, Canvas.GetLeft(Speler2) - snelheid);
            }
            if (speler2NaarRechts == true && Canvas.GetLeft(Speler2) + (Speler2.Width) < 1200) // BEWEEG NAAR RECHTS BEREKENING 
            {
                Canvas.SetLeft(Speler2, Canvas.GetLeft(Speler2) + snelheid);
            }

            // TODO SPRING BEREKENING. MISSCHIEN MET EEN TIMER
            if (speler2Springt == true /*&& Canvas.GetLeft(Player2) > 0*/)
            {
                Canvas.SetTop(Speler2, Canvas.GetTop(Speler2) - springSnelheid); // SPRING OMHOOG BEREKENING 
            }
        }

        private void bewegingMonster(Rectangle enemy, Rectangle left, Rectangle right, ref bool moveEnemyRight)
        {

            if (moveEnemyRight.Equals(true) && Canvas.GetLeft(right) + right.Width > Canvas.GetLeft(right) + enemy.Width)
            {
                Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) + enemySpeed);
                if (Canvas.GetLeft(right) + right.Width <= Canvas.GetLeft(enemy) + enemy.Width)
                {
                    moveEnemyRight = false;
                }
            }

            if (moveEnemyRight.Equals(false) && Canvas.GetLeft(left) < Canvas.GetLeft(enemy))
            {
                Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) - enemySpeed);
                if (Canvas.GetLeft(left) >= Canvas.GetLeft(enemy))
                {
                    moveEnemyRight = true;
                }
            }
        }



        public void interactieMetPlatform()
        {
            // NATUURKUNDE VOOR DE SPELERS EN DE INTERACTIE MET OBSTAKELS EN PLATFORMEN
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {

                if ((string)x.Tag == "platform") // CONTROLE ALS EEN RECHTHOEK DE TAG PLATFORM HEEFT
                {
                    Rect speler1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // HITBOX AANMAKEN VOOR SPELER 1
                    Rect speler2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // HITBOX AANMAKEN VOOR SPELER 2
                    Rect platformhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // HITBOX AANMAKEN VOOR PLATFORM

                    if (speler1hitbox.IntersectsWith(platformhitbox)) // CONTROLE OF SPELER 1 OP EEN PLATFORM STAAT
                    {
                        Canvas.SetTop(Speler1, Canvas.GetTop(x) - Speler1.Height); // POSTITIE AANPASSEN VAN SPELER 1 NAAR BOVEN OP HET PLATFORM.
                    }
                    if (speler2hitbox.IntersectsWith(platformhitbox)) // CONTROLE OF SPELER 2 OP EEN PLATFORM STAAT
                    {
                        Canvas.SetTop(Speler2, Canvas.GetTop(x) - Speler2.Height); // POSTITIE AANPASSEN VAN SPELER 2 NAAR BOVEN OP HET PLATFORM.
                    }
                }
            }
        }
        public void interactieMetEiland()
        {
            // CONTROLE VAN SPELER 1 EN 2 OF ZE MET EEN ZWEVEND EILAND INTERACTIE HEBBEN.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "eiland") // CONTROLEREN VOOR RECHTHOEKEN MET DE TAG EILAND
                {
                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1) + 5, Canvas.GetTop(Speler1), Speler1.Width - 10, Speler1.Height); // HITBOX AANMAKEN VOOR SPELER 1
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2) + 25, Canvas.GetTop(Speler2), Speler2.Width - 30, Speler2.Height); // HITBOX AANMAKEN VOOR SPELER 2
                    Rect eilandhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // HITBOX AANMAKEN VOOR RECHTHOEKEN MET TAG EILAND

                    // SPELER 1 HITBOXEN

                    // EERST WORDT ER GECONTROLEERD OF SPELER 1 ZICH TEGEN DE ONDER KANT VAN EEN EILAND BEVIND
                    if (eilandhitbox.Bottom >= player1hitbox.Top && eilandhitbox.Bottom < player1hitbox.Bottom && player1hitbox.IntersectsWith(eilandhitbox))
                    { Canvas.SetTop(Speler1, eilandhitbox.Bottom); } // ALS STATEMENT WAAR IS DAN WORDT SPELER 1 VERPLAATST NAAR DE BODEM VAN HET EILAND

                    // DAARNA WORDT ER GECONTROLEERD OF SPELER 1 INTERACTIE MET HET EILAND HEEFT
                    else if (player1hitbox.IntersectsWith(eilandhitbox))
                    {
                        Canvas.SetTop(Speler1, Canvas.GetTop(x) - Speler1.Height); // ALS STATEMENT WAAR IS DAN WORDT SPELER 1 VERPLAATST NAAR DE BOVENKANT VAN HET EILAND
                    }


                    // SPELER 2 HITBOXEN 

                    // EERST WORDT ER GECONTROLEERD OF SPELER 2 ZICH TEGEN DE ONDER KANT VAN EEN EILAND BEVIND
                    if (eilandhitbox.Bottom >= player2hitbox.Top && eilandhitbox.Bottom < player2hitbox.Bottom && player2hitbox.IntersectsWith(eilandhitbox))
                    { Canvas.SetTop(Speler2, eilandhitbox.Bottom); } // ALS STATEMENT WAAR IS DAN WORDT SPELER 2 VERPLAATST NAAR DE BODEM VAN HET EILAND

                    // DAARNA WORDT ER GECONTROLEERD OF SPELER 1 INTERACTIE MET HET EILAND HEEFT
                    else if (player2hitbox.IntersectsWith(eilandhitbox))
                    { Canvas.SetTop(Speler2, Canvas.GetTop(x) - Speler2.Height); } // ALS STATEMENT WAAR IS DAN WORDT SPELER 2 VERPLAATST NAAR DE BOVENKANT VAN HET EILAND
                }
            }
        }

        public void interactieMetMonster()
        {
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "enemy")

                {

                    Rect player1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height);
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height);
                    Rect monsterhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    

                    if (player1hitbox.IntersectsWith(monsterhitbox))
                    {
                        Canvas.SetTop(Speler1, Canvas.GetTop(platform1));

                    }
                    if (player2hitbox.IntersectsWith(monsterhitbox))
                    {
                        
                        Canvas.SetTop(Speler2, Canvas.GetTop(platform2));

                    }
                }
            }
    }

         public void interactieMetMuur()
        {
            // CONTROLE VAN SPELER 1 EN 2 OF ZE MET EEN MUUR INTERACTIE HEBBEN.
            // TODO WERKEN MET METHODE(CASE) IPV IF ELSE
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "muur")
                {
                    Rect speler1muurhitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height); // HITBOX AANMAKEN VOOR SPELER 1
                    Rect speler2muurhitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height); // HITBOX AANMAKEN VOOR SPELER 2
                    Rect muurhitbox = new Rect(Canvas.GetLeft(x) - snelheid, Canvas.GetTop(x), x.Width + snelheid, x.Height); // HITBOX AANMAKEN VOOR MUUR
                    Rect deurhitbox = new Rect(Canvas.GetLeft(deur), Canvas.GetTop(deur), deur.Width, deur.Height);

                    // CONTROLEREN OF SPELER 1 DE MUUR RAAKT AAN DE LINKERKANT
                    if (muurhitbox.Left <= speler1muurhitbox.Right
                        && muurhitbox.Right > speler1muurhitbox.Right
                        && speler1muurhitbox.Top >= deurhitbox.Bottom // CHECK OF EEN SPELER BOVEN OP EEN MUUR STAAT
                        && speler1muurhitbox.IntersectsWith(muurhitbox))
                    { Canvas.SetLeft(Speler1, muurhitbox.Left - speler1muurhitbox.Width); }

                    // CONTROLEREN OF SPELER 1 DE MUUR RAAKT AAN DE RECHTERKANT
                    else if (muurhitbox.Right >= speler1muurhitbox.Left
                        && muurhitbox.Left < speler1muurhitbox.Left
                        && speler1muurhitbox.Top >= deurhitbox.Bottom // CHECK OF EEN SPELER BOVEN OP EEN MUUR STAAT
                        && speler1muurhitbox.IntersectsWith(muurhitbox))
                    { Canvas.SetLeft(Speler1, muurhitbox.Right); }

                    // CONTROLEREN OF SPELER 2 DE MUUR RAAKT AAN DE LINKERKANT
                    if (muurhitbox.Left <= speler2muurhitbox.Right
                        && muurhitbox.Right > speler2muurhitbox.Right
                        && speler2muurhitbox.Top >= deurhitbox.Bottom // CHECK OF EEN SPELER BOVEN OP EEN MUUR STAAT
                        && speler2muurhitbox.IntersectsWith(muurhitbox))
                    { Canvas.SetLeft(Speler2, muurhitbox.Left - speler2muurhitbox.Width); }

                    // CONTROLEREN OF SPELER 2 DE MUUR RAAKT AAN DE RECHTERKANT
                    else if (muurhitbox.Right >= speler2muurhitbox.Left
                        && muurhitbox.Left < speler2muurhitbox.Left
                        && speler2muurhitbox.Top >= deurhitbox.Bottom // CHECK OF EEN SPELER BOVEN OP EEN MUUR STAAT
                        && speler2muurhitbox.IntersectsWith(muurhitbox))
                    { Canvas.SetLeft(Speler2, muurhitbox.Right); }
                }
            }
        }
        public void interactieMetMunt()
        {
            // MUNTEN OPPAKKEN SPELERS
            //todo speler 2 munten oppakken.
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "coin")
                {
                    Rect speler1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height);
                    Rect speler2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height);
                    Rect coinhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if ((speler1hitbox.IntersectsWith(coinhitbox)) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        score++;

                        scoreSpeler1Label.Content = "Munten: " + score;

                    }
                    if ((speler2hitbox.IntersectsWith(coinhitbox)) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        score2++;

                        scoreSpeler2Label.Content = "Munten: " + score2;

                    }
                }
            }
        }
        public void interactieMetDeur()
        {
            // INSTELLEN WELKE SPELER HEEFT GEWONNEN AAN DE HAND VAN DE DEUR//
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "deur")
                {
                    Rect speler1hitbox = new Rect(Canvas.GetLeft(Speler1), Canvas.GetTop(Speler1), Speler1.Width, Speler1.Height);
                    Rect speler2hitbox = new Rect(Canvas.GetLeft(Speler2), Canvas.GetTop(Speler2), Speler2.Width, Speler2.Height);
                    Rect deurhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (speler1hitbox.IntersectsWith(deurhitbox))
                    {
                        if (spelerGewonnen != speler2Naam)
                        { spelerGewonnen = speler1Naam; }
                    }

                    if (speler2hitbox.IntersectsWith(deurhitbox))
                    {
                        if (spelerGewonnen != speler1Naam)
                        { spelerGewonnen = speler2Naam; }
                    }
                    // LAAT ZIEN WELKE SPELER GEWONNEN HEEFT ALS BEIDE SPELERS DE DEUR BEREIKT HEBBEN
                    if (speler1hitbox.IntersectsWith(deurhitbox) && speler2hitbox.IntersectsWith(deurhitbox))
                    {

                        // MessageBox.Show(spelerGewonnen + " heeft gewonnen!");
                        // MESSAGE BOX GEEFT EEN ERROR -> POPPETJES ZAKKEN NAAR BENEDEN ERDOOR... 
                        // OPLOSSING: EEN NEW WINDOW POP-UP
                        //TODO POPUP MET WIE GEWONNEN HEEFT -> SOORT MENU
                        timer.Stop();
                        spelTimer.Stop();

                        StripEind SE = new StripEind();
                        SE.Visibility = Visibility.Visible;
                        this.Close();
                    }

                }
            }
        }
        public void zwaartekrachtBerekenenSpelers()
        {
            Canvas.SetTop(Speler1, Canvas.GetTop(Speler1) + zwaartekracht); // ZWAARTEKRACHT BEREKENING
            Canvas.SetTop(Speler2, Canvas.GetTop(Speler2) + zwaartekracht); // ZWAARTEKRACHT BEREKENING
        }

        /// <summary>
        /// updateNames-methode geeft de ingevoerde spelernamen weer op het spelscherm.
        /// </summary>
        public void updateNames()
        {
            speler1label.Content = speler1Naam; // lAAT DE EERSTE SPELERS NAAM ZIEN IN HET EERSTE LABEL
            speler2label.Content = speler2Naam; // lAAT DE TWEEDE SPELERS NAAM ZIEN IN HET TWEEDE LABEL

        }

        /// <summary>
        /// spelerTimer-Methode berekent de timer voor in het spel. En geeft deze in het juiste tijdsformat weer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spelersTimer(object sender, EventArgs e)
        {
            tijdSeconden++;

            if (tijdSeconden == 60) // ELKE MINUUT EEN MINUUUT ERBIJ TELLEN
            {
                tijdMinuten++;
                tijdSeconden = 0;
            }

            // TIJD FORMAT INSTELLEN (MM:SS)
            if (tijdSeconden < 10) // PREFIX BIJ SECONDEN ONDER DE 10 (00, 01, 02 ETC.)
            {
                seconden = "0" + tijdSeconden.ToString();
            }
            else { seconden = tijdSeconden.ToString(); }
            // PREFIX BIJ MINUTEN ONDER DE 60 (00:00, 01:00, 02:00 ETC.)
            if (tijdMinuten < 60)
            {
                minuten = "0" + tijdMinuten.ToString();
            }
            else if (tijdMinuten > 60)
            {
                minuten = tijdMinuten.ToString();
            }
            tijd.Content = "tijd: " + minuten + ":" + seconden;

        }

        //TODO KEYDOWN METHODE BESCHRIJVEN
        private void keydown(object sender, KeyEventArgs e) // BEWEGINGEN VOOR SPELER 1 & 2
        {
            // BEWEGING VOOR SPELER 1 (KEYS: AWSD)
            if (e.Key == Key.A) // BEWEGING NAAR LINKS
            {
                speler1NaarLinks = true;
                // Player2.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2); //roteert de speler als de conditie true is//

            }
            if (e.Key == Key.D)
            {
                speler1NaarRechts = true; // BEWEGING NAAR RECHTS
                // Player2.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2);
            }

            // TODO SPELR 1 SPRINGANIMATIE / SPRINGEN NA AANTAL SECONDEN UITZETTEN EN PAS AANZETTEN ALS SPELER OP EEN PLATORM STAAT
            if (e.Key == Key.W && !speler1Springt) // SPRINGEN AANZETTEN 
            {
                speler1Springt = true; // SPRINGEN AANZETTEN
            }

            // KEYDOWN VOOR SPELER 2 (KEYS: PIJLTJES-TOETSEN)
            if (e.Key == Key.Left)
            {
                speler2NaarLinks = true; // BEWEGING NAAR LINKS
                //Player.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2); //roteert de speler als de conditie true is//
            }
            if (e.Key == Key.Right)
            {
                speler2NaarRechts = true; // BEWEGING NAAR RECHTS
                //Player.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2);
            }
            // TODO SPELER 2 SPRINGANIMATIE / SPRINGEN NA AANTAL SECONDEN UITZETTEN EN PAS AANZETTEN ALS SPELER OP EEN PLATORM STAAT
            if (e.Key == Key.Up && !speler2Springt)
            {
                speler2Springt = true; // SPRINGEN AANZETTEN 
            }
            //

            if (e.Key == Key.Escape)
            {
                zwaartekrachtDisabled = true;
                speler1NaarLinks = speler1NaarRechts = speler1Springt = false;
                speler2NaarLinks = speler2NaarRechts = speler2Springt = false;


                pauze = true;
                PauzeMenu pm = new PauzeMenu(this);
                pm.Visibility = Visibility.Visible;
                spelTimer.Stop();
                timer.Stop();
            }
        }

        //TODO KEYUP METHODE BESCHRIJVEN
        private void keyup(object sender, KeyEventArgs e)
        {
            // BEWEGING SPELER 1 UITSCHAKELEN
            if (e.Key == Key.A)
            {
                speler1NaarLinks = false; // BEWEGING NAAR LINKS UITSCHAKELEN 
            }
            if (e.Key == Key.D)
            {
                speler1NaarRechts = false; // BEWEGING NAAR RECHTS UITSCHAKELEN
            }
            if (speler1Springt)
            {
                speler1Springt = false; // SPRINGEN UITSCHAKELEN
            }


            // STOPPEN BEWEGING SPELER 2
            if (e.Key == Key.Left)
            {
                speler2NaarLinks = false; // BEWEGING NAAR LINKS UITSCHAKELEN 
            }
            if (e.Key == Key.Right)
            {
                speler2NaarRechts = false; // BEWEGING NAAR RECHTS UITSCHAKELEN
            }
            if (speler2Springt)
            {
                speler2Springt = false;  // SPRINGEN UITSCHAKELEN
            }

            // GAME PAUZEREN
            if (e.Key == Key.Escape)
            {
                zwaartekrachtDisabled = false;

                pauze = false;
                PauzeMenu pm = new PauzeMenu(this);


            }

        }

        public static void SpeelVerder()
        {
            pauze = false;
            zwaartekrachtDisabled = false;
            timer.Start();
            spelTimer.Start();
        }


    }
}
