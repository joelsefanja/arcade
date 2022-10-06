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
using System.Windows.Shell;
using System.Windows.Threading;

namespace Arcade
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private bool moveLeftPlayer1, moveRightPlayer1, JumpPlayer1; // beweging link en rechts van speler 1
        private bool moveLeftPlayer2, moveRightPlayer2, JumpPlayer2; // beweging link en rechts van speler 2
        private DispatcherTimer gameTimer = new DispatcherTimer(); // game timer
        private List<Rectangle> itemsToRemove = new List<Rectangle>(); // items om te verwijderen zoals muntjes die opgepakt worden.
        private const int playerSpeed = 5; // snelheid van een speler
        private const int monsterSpeed = 5; // snelheid van een monster
        private int dropSpeed = 10; // Zwaartekracht 
        
        private Random rand = new Random(); // random nummer generator
        private int scoreSpeler1 = 0; 
        private int scoreSpeler2 = 0;  

        private const int enemyPassesDamage = 10;
        private const int enemyCrashesDamage = 5;

        public GameWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameEngine;
            gameTimer.Start();

            MyCanvas.Focus();
        }
        private void GameEngine(object sender, EventArgs e)
        {
            // TODO beweging geschikt maken voor een 2e speler.

            // beweeg speler 1 naar links
            if (moveLeftPlayer1 && Canvas.GetLeft(Player1) > 0)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - playerSpeed);
            
            // beweeg speler 1 naar rechts
            if (moveRightPlayer1 && Canvas.GetLeft(Player1) + Player1.Width < Application.Current.MainWindow.Width)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + playerSpeed);

            // teleporteer speler 1 naar boven als de speler is gevallen uit het beeld.
            if (Canvas.GetTop(Player1) + (Player1.Height * 2) > Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(Player1, -128);
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    x.Stroke = Brushes.Black;

                    Rect playerHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);
                    // todo playerhitbox fixen -> hij is rood onderstreept...
                }
            }

            if (Canvas.GetBottom(Player1) != Canvas.GetTop(platform1))
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + dropSpeed);
            }
            else
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1));
            }
        }

        //    foreach (Rectangle r in itemsToRemove)
        //    {
        //        MyCanvas.Children.Remove(r);
        //    }

           
                
        //        LabelScore.Content = "Score: " + score;
        //        LabelDamage.Content = "Schade " + damage;
        //        if (score > 5)
        //            enemySpawnLimit = 20;
        //        if (damage > 99)
        //        {
        //            gameTimer.Stop(); // stopt de game timer.
        //            LabelDamage.Content = "Damaged: 100";
        //            LabelDamage.Foreground = Brushes.Red;
        //            MessageBox.Show("Je hebt " + score + " punten gehaald", "Spel verloren");
        //        }

        //        /*
        //        // Code voor het handelen van de damage 

        //        if ((string)x.Tag == "Enemy")
        //        {
        //            Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);
        //            Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
        //            if (Canvas.GetTop(x) + 50 > 900)
        //            {
        //                itemsToRemove.Add(x);
        //                damage += enemyPassesDamage;
        //            }
        //            if (playerHitBox.IntersectsWith(enemy))
        //            {
        //                damage += enemyCrashesDamage;
        //                itemsToRemove.Add(x);
        //            }
        //        } */
        //    }

        //}
        
        //private void makeEnemies()
        //{
        //    ImageBrush enemySprite = new ImageBrush();
        //    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/sprites/monster_64px.png"));

        //    /*
        //    int enemySpriteCounter = rand.Next(1, 3);
        //    switch (enemySpriteCounter)
        //    {
        //        case 1:
        //            enemySprite.ImageSource =
        //            new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
        //            break;
        //        case 2:
        //            enemySprite.ImageSource =
        //            new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
        //            break;
        //        case 3:
        //            enemySprite.ImageSource =
        //            new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
        //            break;
        //        default:
        //            enemySprite.ImageSource =
        //            new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
        //            break;
        //    } */

        //    // nieuwe vijand maken.
        //    //Rectangle newEnemy = new Rectangle
        //    //{
        //    //    Tag = "Enemy",
        //    //    Height = 50,
        //    //    Width = 50,
        //    //    Fill = enemySprite
        //    //};
        //    // plaats de vijand op een bepaalde postitie

        //    //Canvas.SetTop(newEnemy, 200);
        //    //Canvas.SetLeft(newEnemy, rand.Next(30, 650));
        //    //MyCanvas.Children.Add(newEnemy);
        //    //GC.Collect();
        //}

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // speler 1 besturing voor ingedrukte toetsen
            if (e.Key == Key.Left)
            { moveLeftPlayer1 = true; }
            if (e.Key == Key.Right)
            { moveRightPlayer1 = true; }
            if (e.Key == Key.Up)
            { JumpPlayer1 = true; }

            // speler 2 besturing voor ingedrukte toetsen
            if (e.Key == Key.Left)
            { moveLeftPlayer2 = true; }
            if (e.Key == Key.Right)
            { moveRightPlayer2 = true; }
            if (e.Key == Key.Up)
            { JumpPlayer2 = true; }

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // speler 1 besturing voor losgelaten toetsen
            if (e.Key == Key.Left)
            { moveLeftPlayer1 = false; }
            if (e.Key == Key.Right)
            { moveRightPlayer1 = false; }
            if (e.Key == Key.Up)
            { JumpPlayer1 = false; }

            // speler 2 besturing voor losgelaten toetsen
            if (e.Key == Key.Left)
            { moveLeftPlayer2 = false; }
            if (e.Key == Key.Right)
            { moveRightPlayer2 = false; }
            if (e.Key == Key.Up)
            { JumpPlayer2 = false; }

        }

    }

}
