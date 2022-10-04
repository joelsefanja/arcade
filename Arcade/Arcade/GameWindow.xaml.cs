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
        private bool moveLeft, moveRight, playerJump; // beweging link en rechts
        private DispatcherTimer gameTimer = new DispatcherTimer(); // game timer
        private List<Rectangle> itemsToRemove = new List<Rectangle>(); 
        private const int playerSpeed = 5; // snelheid van de speler
        private int dropSpeed = 10;
        private const int bulletSpeed = 20; // snelheid van de kogel

        private Random rand = new Random();
        private int enemySpawnLimit = 50;
        private int score = 0;
        private int damage = 0;

        private int enemySpawnCounter = 100;
        private const int enemySpeed = 10;
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
            if (moveLeft && Canvas.GetLeft(Player) > 0)
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - playerSpeed);
            if (moveRight && Canvas.GetLeft(Player) + Player.Width < Application.Current.MainWindow.Width)
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + playerSpeed);
            
            if (Canvas.GetTop(Player) + (Player.Height * 2) > Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(Player, -80);
            }
            
            


            /* foreach (Rectangle x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "Bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - bulletSpeed);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                        itemsToRemove.Add(x);
                }
            } */

            Canvas.SetTop(Player, Canvas.GetTop(Player) + dropSpeed);

            foreach (Rectangle r in itemsToRemove)
            {
                MyCanvas.Children.Remove(r);
            }

            Rect playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player),
                Player.Width, Player.Height);
            enemySpawnCounter--;
            /* if (enemySpawnCounter < 0)
            {
                makeEnemies(); // run the make enemies function
                enemySpawnCounter = enemySpawnLimit; //reset the enemy counter to the limit integer
            } */

            // verwijderen van tegenstander en van de kogel als ze een bepaalde positie hebben bereikt.

             foreach (Rectangle x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "Bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - bulletSpeed);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                        itemsToRemove.Add(x);
                    foreach (Rectangle y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if ((string)y.Tag == "Enemy")
                        {
                            Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (bullet.IntersectsWith(enemy))
                            {
                                itemsToRemove.Add(x); //bullet
                                itemsToRemove.Add(y); //enemy
                                score++;
                            }
                        }
                    }
                }
                
                LabelScore.Content = "Score: " + score;
                LabelDamage.Content = "Schade " + damage;
                if (score > 5)
                    enemySpawnLimit = 20;
                if (damage > 99)
                {
                    gameTimer.Stop(); // stopt de game timer.
                    LabelDamage.Content = "Damaged: 100";
                    LabelDamage.Foreground = Brushes.Red;
                    MessageBox.Show("Je hebt " + score + " punten gehaald", "Spel verloren");
                }

                /*
                // Code voor het handelen van de damage 

                if ((string)x.Tag == "Enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);
                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) + 50 > 900)
                    {
                        itemsToRemove.Add(x);
                        damage += enemyPassesDamage;
                    }
                    if (playerHitBox.IntersectsWith(enemy))
                    {
                        damage += enemyCrashesDamage;
                        itemsToRemove.Add(x);
                    }
                } */
            }

        }
        
        private void makeEnemies()
        {
            ImageBrush enemySprite = new ImageBrush();
            enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/sprites/monster_64px.png"));

            /*
            int enemySpriteCounter = rand.Next(1, 3);
            switch (enemySpriteCounter)
            {
                case 1:
                    enemySprite.ImageSource =
                    new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
                    break;
                case 2:
                    enemySprite.ImageSource =
                    new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
                    break;
                case 3:
                    enemySprite.ImageSource =
                    new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
                    break;
                default:
                    enemySprite.ImageSource =
                    new BitmapImage(new Uri("pack://application:,,,/img/monster_64px.png"));
                    break;
            } */

            // nieuwe vijand maken.
            Rectangle newEnemy = new Rectangle
            {
                Tag = "Enemy",
                Height = 50,
                Width = 50,
                Fill = enemySprite
            };
            // plaats de vijand op een bepaalde postitie

            Canvas.SetTop(newEnemy, 200);
            Canvas.SetLeft(newEnemy, rand.Next(30, 650));
            MyCanvas.Children.Add(newEnemy);
            GC.Collect();
        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            { moveLeft = true; }
            if (e.Key == Key.Right)
            { moveRight = true; }
            if (e.Key == Key.Space)
            { playerJump = true; }
            
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                moveLeft = false;
            if (e.Key == Key.Right)
                moveRight = false;
           
        }

    }

}
