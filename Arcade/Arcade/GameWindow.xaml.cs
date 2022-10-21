using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace Arcade
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer spelTimer = new DispatcherTimer();

        int speed = 10; //spelersnelheid//
        int dropSpeed = 10; //zwaartekracht//
        bool goLeft, goRight;
        bool goLeft2, goRight2;
        bool jumping = false;
        bool jumping2 = false;
        int horizontalspeed = 5;
        int verticalspeed = 3;
        int score = 0;
        int score2 = 0;
        int tijdSeconden = 0; //speler timer seconden
        int tijdMinuten = 0; //speler timer minuten
        int SpelerTimerTick = 0; // tick om na 50 ticks van 20 miliseconden de timer te updaten
        string seconden; // variabel voor de prefix 0 bij de tijd onder 10 seconden.
        string minuten; // variabel voor de prefix 0 bij de minuten onder 60 minuten.
        public static string playerName1, playerName2;
        bool moveEnemyRightOne = true;
        bool moveEnemyRightTwo = true;
        int enemySpeed = 5;
        
        private void handleEnemy(Rectangle enemy, Rectangle left, Rectangle right, ref bool moveEnemyRight)
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


        private void handlePlayerControl(Rectangle player, bool goRight, bool goLeft, bool jumping)
        {
            if (jumping == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - 20); // speler 1 jumpcode, jank af jumpcode 20 is de snelheid moet wss een timer bij//
            }

            if (goLeft == true && Canvas.GetLeft(player) > 0) // defineren van de looprichtingen + de zwaartekracht etc//
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - this.speed);
            }

            if (goRight == true && Canvas.GetLeft(player) + (player.Width + 15) < Application.Current.MainWindow.Width) //rechts voor speler 2//
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + this.speed);
            }
        }

        public GameWindow() // game engine
        {

            InitializeComponent();

            newcanvas.Focus(); // focus op het spel canvas.

            // zet de ingevoerde namen als labels
            updateNames();

            //Start game engine timer
            timer.Tick += MainTimerEvent;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();

            // start level timer
            spelTimer.Tick += spelersTimer;
            spelTimer.Interval = TimeSpan.FromSeconds(1);
            spelTimer.Start();
        } 

        private void MainTimerEvent(object sender, EventArgs e) //main timer events met de werking van de mechanics van de spelers en hopelijk later de munten en trapdoors later//
        {
            
            Canvas.SetTop(Player, Canvas.GetTop(Player) + dropSpeed);
            Canvas.SetTop(Player2, Canvas.GetTop(Player2) + dropSpeed);

            // Handles actions for the players
            this.handlePlayerControl(Player, goRight, goLeft, jumping);
            this.handlePlayerControl(Player2, goRight2, goLeft2, jumping2);

            // Handle enemy movement
            this.handleEnemy(enemy2, eiland11, eiland10, ref moveEnemyRightOne);
            this.handleEnemy(enemy1, eiland15, eiland16, ref moveEnemyRightTwo);


            //enemy shit//
            //physics voor de spelers en colliders met de rectangles//
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    x.Stroke = Brushes.Black;
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height); // hitboxberekening voor speler 2//
                    Rect platformhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (player2hitbox.IntersectsWith(platformhitbox))
                    {
                        Canvas.SetTop(Player2, Canvas.GetTop(x) - Player2.Height);
                    }
                }
                //System.Windows.Application.Current.Shutdown(); (handig voor later//
                //SPELER 2//
                if ((string)x.Tag == "eiland") //instellen van de borders en acties wanneer speler2 in aanraking komt met de platformen met de tag eiland//
                {
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height); // hitboxberekening voor speler 2//
                    Rect eilandhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); //hitboxberekeningen voor de platformen met de naam eiland//
                    if (eilandhitbox.Bottom >= player2hitbox.Top && eilandhitbox.Bottom < player2hitbox.Bottom && player2hitbox.IntersectsWith(eilandhitbox))
                    {
                        Canvas.SetTop(Player2, eilandhitbox.Bottom); //instellen van onderkant van de platformen zodat de speler er niet langs kan//
                    }

                    else if (player2hitbox.IntersectsWith(eilandhitbox))
                    {
                        Canvas.SetTop(Player2, eilandhitbox.Top - Player.Height); //instellen van de bovenkant van het platform zodat speler2 er op kan lopen//
                    }


                } //SPELER 1//
                if ((string)x.Tag == "eiland") //instellen van de borders en acties wanneer speler 1 in aanraking komt met de platformen met de tag eiland//
                {
                    Rect playerhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height); // hitboxberekening voor speler 1//
                    Rect eilandhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); //hitboxberekeningen voor de platformen met de naam eiland//
                    if (eilandhitbox.Bottom >= playerhitbox.Top && eilandhitbox.Bottom < playerhitbox.Bottom && playerhitbox.IntersectsWith(eilandhitbox))
                    {
                        Canvas.SetTop(Player, eilandhitbox.Bottom); //instellen van onderkant van de platformen zodat de speler er niet langs kan//
                    }

                    else if (playerhitbox.IntersectsWith(eilandhitbox))
                    {
                        Canvas.SetTop(Player, eilandhitbox.Top - Player.Height); //instellen van de bovenkant van het platform zodat speler er op kan lopen//
                    }


                }
                //SPELER 2 WALL//
                if ((string)x.Tag == "wall")
                {
                    Rect player2wallhitbox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), 50, 50); // hitboxberekening voor speler 2//
                    Rect wallhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), 100, 196);


                    // hit rechts
                    if (wallhitbox.Right >= player2wallhitbox.Left && wallhitbox.Left < player2wallhitbox.Left && player2wallhitbox.IntersectsWith(wallhitbox))
                    {
                        Canvas.SetLeft(Player2, wallhitbox.Right);
                    }

                    // hit links
                    else if (wallhitbox.Left <= player2wallhitbox.Right && wallhitbox.Right > player2wallhitbox.Right && player2wallhitbox.IntersectsWith(wallhitbox))
                    {
                        Canvas.SetLeft(Player2, wallhitbox.Left - player2wallhitbox.Width);
                    }

                }
                //SPELER 1 WALL//
                if ((string)x.Tag == "wall")
                {
                    Rect playerwallhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), 50, 50); // hitboxberekening voor speler 2 VERANDER DE 50  en de 50 later voor player.width en player.height//
                    Rect wallhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), 100, 196);


                    // hit rechts
                    if (wallhitbox.Right >= playerwallhitbox.Left && wallhitbox.Left < playerwallhitbox.Left && playerwallhitbox.IntersectsWith(wallhitbox))
                    {
                        Canvas.SetLeft(Player, wallhitbox.Right);
                    }

                    // hit links
                    else if (wallhitbox.Left <= playerwallhitbox.Right && wallhitbox.Right > playerwallhitbox.Right && playerwallhitbox.IntersectsWith(wallhitbox))
                    {
                        Canvas.SetLeft(Player, wallhitbox.Left - playerwallhitbox.Width);
                    }

                }
                //SPELER 2 DEUR MECHANICS//
                if ((string)x.Tag == "deur")
                {
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);
                    Rect deurhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (player2hitbox.IntersectsWith(deurhitbox))
                    {
                        spelTimer.Stop();
                        timer.Stop();
                        MessageBox.Show(playerName1 + ", Je hebt gewonnen!!");
                    }

                }
                //SPELER 1 DEUR MECHANICS//
                if ((string)x.Tag == "deur")
                {
                    Rect playerhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
                    Rect deurhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerhitbox.IntersectsWith(deurhitbox))
                    {
                        timer.Stop();
                        MessageBox.Show(playerName2 + ", Je hebt gewonnen!!");
                    }

                }
                //MUNTEN VERZAMELEN CODE NIET AF//
                //TODO MUNTEN VERZAMELEN
                if ((string)x.Tag == "coin")
                {
                    Rect playerhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
                    Rect coinhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerhitbox.IntersectsWith(coinhitbox))
                    {

                    }

                }
                if ((string)x.Tag== "enemy")//ENEMY CODE FIRST ATTEMPT//

                {
   
                    Rect playerhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
                    Rect player2hitbox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);
                    Rect Enemyhitbox= new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if(playerhitbox.IntersectsWith(Enemyhitbox) || player2hitbox.IntersectsWith(Enemyhitbox))
                    {
                        Canvas.SetTop(Player, 716);

                    }
                    

                    
                }


            }

            if (Canvas.GetTop(Player) + (Player.Height * 2) > Application.Current.MainWindow.Height) //hitboxes instellen//
            {
                Canvas.SetTop(Player, -80);
            }
            foreach (var x in newcanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    x.Stroke = Brushes.Black;
                    Rect playerhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
                    Rect platformhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerhitbox.IntersectsWith(platformhitbox))
                    {
                        //dropSpeed = 0;
                        Canvas.SetTop(Player, Canvas.GetTop(x) - Player.Height);
                    }
                }
            }

        }

        public void updateNames() 
        {
            speler1label.Content = playerName1;
            speler2label.Content = playerName2;

        }

        /// <summary>
        /// Methode voor de timer in het spelscherm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spelersTimer(object sender, EventArgs e) {
            tijdSeconden++;
            
            if (tijdSeconden == 60)
            {
                tijdMinuten++;
                tijdSeconden = 0;
            }
            
            // tijd format instellen op basis van de timer
            if (tijdSeconden < 10) // prefix bij seconden onder de 10.
            {
                 seconden = "0" + tijdSeconden.ToString();
            }
            else { seconden = tijdSeconden.ToString();}
            // prefix bij minuten onder de 60
            if(tijdMinuten < 60)
            {
                minuten = "0" + tijdMinuten.ToString();
            }
            else if (tijdMinuten > 60)
            {
                minuten = tijdMinuten.ToString();
            }
            tijd.Content = "tijd: " + minuten + ":" + seconden;

        }
        private void keydown(object sender, KeyEventArgs e) //keybinds voor speler 1 en 2//
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
                Player.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2); //roteert de speler als de conditie true is//
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
                Player.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2);
            }
            if (e.Key == Key.A)
            {
                goLeft2 = true;
                Player2.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2); //roteert de speler als de conditie true is//

            }
            if (e.Key == Key.D)
            {
                goRight2 = true;
                Player2.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2);
            }
            if (e.Key == Key.Up && !jumping) //toetsenbordinput voor ghet springen,keydown//
            {
                jumping = true;
            }
            if (e.Key == Key.W && !jumping2) //toetsenbordinput voor het springen, keydown//
            {
                jumping2 = true;
            }
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.A)
            {
                goLeft2 = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (e.Key == Key.D)
            {
                goRight2 = false;
            }
            if (jumping)
            {
                jumping = false; //attemps were made stopt hopelijk het springen//
            }
            if (jumping2)
            {
                jumping2 = false;
            }

        }
    }
}
