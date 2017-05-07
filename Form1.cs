using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//1h21m: timers

namespace Assignment4.TimAndrew.W0212032
{
    public partial class Assignment4 : Form
    {
        private bool awaitClick = true;
        private int laserCost = 10;

        private bool gameOver = false;

        private bool FirstRunFlag = true;

        private bool playGame = false;

        private bool introPlayed = false;

        private bool shipDestroyed = false;

        private Point mouseLocation;

        private Ship ship;

        private Image image;

            //hash set of lasers:
        private HashSet<Laser> lasers = new HashSet<Laser>();
            //HashSet of enemies:
        private HashSet<Enemy> enemies = new HashSet<Enemy>();

        /// <summary>
        /// The primary Start of the program:
        /// </summary>
        public Assignment4()
        {
            InitializeComponent();

            this.MouseMove += new MouseEventHandler(Assignment4_MouseMove);

        }

        /// <summary>
        /// On form load:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assignment4_Load(object sender, EventArgs e)
        {
            //form's initial state:
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //this.Bounds = Screen.PrimaryScreen.Bounds;
            //this.TopMost = true;
            Opacity = 0.00;

            ship = new Ship(this.DisplayRectangle);
            enemies.Add(new Enemy(this.DisplayRectangle, ship));

        }


        /// <summary>
        /// a method to display the intro screen:
        /// </summary>
        /// <param name="graphics"></param>
        private void DisplayIntro(Graphics graphics)
        {
            for (int i =0; i < 100; i++)
            {
                Opacity += .01;
                System.Threading.Thread.Sleep(5);
                Invalidate();
            }
            Opacity = 1.00;
            System.Threading.Thread.Sleep(2000);

        }

        private void DisplayLevelUp(Graphics graphics)
        {
            string message = string.Format("LEVEL COMPLETE!\nCLICK FOR LEVEL {0}!", ship.level);
            Font font = new Font("Impact", 20);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point point = new Point((this.DisplayRectangle.Width /2)-40 , (this.DisplayRectangle.Height / 2)-40);

            graphics.DrawString(message, font, brush, point);
        }

        private void FadeInAndOut(object sender, EventArgs e)
        {
            if (Opacity < 1.00)
            {
                Opacity += .01;
            }
            
            timer2.Enabled = true;
        }

        private void DisplayNewGame(Graphics graphics)
        {
            string message = string.Format("CLICK TO PLAY A NEW GAME!");
            Font font = new Font("Impact", 20);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point point = new Point((this.DisplayRectangle.Width / 2) - 40, (this.DisplayRectangle.Height / 2) - 40);

            graphics.DrawString(message, font, brush, point);
        }

        
         private void DisplayGameOver(Graphics graphics)
        {
            string message = string.Format("GAME OVER!\nCLICK TO PLAY A NEW GAME!");
            Font font = new Font("Impact", 20);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point point = new Point((this.DisplayRectangle.Width / 2) - 40, (this.DisplayRectangle.Height / 2) - 40);

            graphics.DrawString(message, font, brush, point);
        }


        /// <summary>
        /// Painting the screen:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assignment4_Paint(object sender, PaintEventArgs e)
        {
            if (!playGame && !introPlayed && FirstRunFlag)
            {
                image = Image.FromFile("Images/TimAndrew.png");
                e.Graphics.DrawImage(image, this.DisplayRectangle);
                FirstRunFlag = false;
                Invalidate();
            }

            else if (!playGame && !introPlayed)
            {
                DisplayIntro(e.Graphics);
                introPlayed = true;
                Invalidate();
            }
            else if (!playGame)
            {
                DisplayNewGame(e.Graphics);

                timer1.Start();
            }
            else if (shipDestroyed)
            {
                DisplayShipDestroyed(e.Graphics);
            }
            else if (gameOver)
            {
                DisplayGameOver(e.Graphics);
            }

            else
            {
                DisplayScore(e.Graphics);


                ship.Draw(e.Graphics);
                foreach (Laser laser in lasers)
                {
                    laser.Draw(e.Graphics);
                }

                foreach (Enemy enemy in enemies)
                {
                    enemy.Draw(e.Graphics);
                }
            }

        }

        private void DisplayShipDestroyed(Graphics graphics)
        {
            string message = string.Format("SHIP LOST!\nCLICK TO CONTINUE!");
            Font font = new Font("Impact", 20);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point point = new Point((this.DisplayRectangle.Width / 2) - 40, (this.DisplayRectangle.Height / 2) - 40);

            graphics.DrawString(message, font, brush, point);

        }

        /// <summary>
        /// A method to display the score:
        /// </summary>
        /// <param name="graphics"></param>
        private void DisplayScore(Graphics graphics)
        {
            string message = string.Format("\tLevel: {2}/{3}\n\tLives : {0}\n\tScore : {1}", ship.lives, ship.score, ship.level, ship.subLevel);
            Font font = new Font("Impact", 20);
            SolidBrush brush = new SolidBrush(Color.White);
            Point point = new Point(20, 20);

            graphics.DrawString(message, font, brush, point);
        }

        private void Assignment4_MouseMove(object sender, MouseEventArgs e)
        {
           mouseLocation = e.Location;

           ship.Move(mouseLocation, this.DisplayRectangle);

                //capturing mouse:
           //Console.Out.WriteLine("X: " + mouseLocation.X + ", Y: " +mouseLocation.Y);

        }
        
        private void Assignment4_KeyDown(object sender, KeyEventArgs e)
        {
            //Move the ship:

            //ship.Move(mouseLocation, this.DisplayRectangle);

           /* switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        if (timer1.Enabled)
                            timer1.Stop();
                        else
                            timer1.Start();
                        break;
                    }
                case Keys.Space:
                    {
                        enemies.Add(new Enemy(this.DisplayRectangle, ship));
                        break;
                    }
            }*/
        }

        private void Assignment4_Resize(object sender, EventArgs e)
        {
            //ship.Move(mouseLocation, this.DisplayRectangle);
        }

        /// <summary>
        /// What happens on each Timer Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameOver)
            {
                ship = null;
                enemies = new HashSet<Enemy>();

                ship = new Ship(this.DisplayRectangle);

                AddEnemies();
                gameOver = true;
            }

            else if (shipDestroyed)
            {

                ship.displayArea.X = this.DisplayRectangle.Width / 2 - ship.displayArea.Width / 2;
                ship.displayArea.Y = this.DisplayRectangle.Height / 2 - ship.displayArea.Height / 2;
            }

            else
            {
                CheckCollisions();

                foreach (Laser laser in lasers)
                {
                    laser.Move();
                }

                Point toShip = new Point(ship.displayArea.X + (ship.displayArea.Width / 2),
                                        ship.displayArea.Y + (ship.displayArea.Height / 2));

                foreach (Enemy enemy in enemies)
                {
                    enemy.Move(toShip);
                }
            }

            Invalidate();
        }

        /// <summary>
        /// fires the lasers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assignment4_MouseDown(object sender, MouseEventArgs e)
        {
            if (awaitClick)
            {
                shipDestroyed = false;
                awaitClick = false;
                playGame = true;
                gameOver = false;
            }
            else
            {
                lasers.Add(new Laser(new Point(ship.displayArea.Left + 1, ship.displayArea.Top - 1)));
                lasers.Add(new Laser(new Point(ship.displayArea.Right - 1, ship.displayArea.Top - 1)));
                //subtract laserCost from score:
                ship.score -= laserCost;
                if (ship.score < 0)
                    ship.score = 0;
            }
            
        }

        /// <summary>
        /// A method to check for collisions:
        /// </summary>
        private void CheckCollisions()
        {
                //set up hash sets:
            HashSet<Laser> LasersToRemove = new HashSet<Laser>();
            HashSet<Enemy> EnemiesToRemove = new HashSet<Enemy>();

            //check if a laser has hit an enemy:
            foreach (Laser laser in lasers)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.displayArea.IntersectsWith(laser.displayArea))
                    {
                        LasersToRemove.Add(laser);
                        EnemiesToRemove.Add(enemy);

                    }
                }
            }

                //remove lasers that hit an enemy:
            foreach (Laser laser in LasersToRemove)
            {
                lasers.Remove(laser);

            }

                //remove lasers that hit an enemy:
            foreach (Enemy enemy in EnemiesToRemove)
            {
                enemies.Remove(enemy);
                //increment score
                ship.score += 100 * (ship.level +1);
                if (enemies.Count <= 0)
                {
                    ++ship.subLevel;
                    if (ship.subLevel > 9)
                    {
                        ship.subLevel = 0;
                        ++ship.level;
                        awaitClick = true;
                    }
                    AddEnemies();
                }
            }

            //Remove lasers when they exit the screen:
            lasers.RemoveWhere(LaserExitsScreen);

             //reset enemies to remove:
            EnemiesToRemove = new HashSet<Enemy>();
            
            //Find if an enemy hits the ship:
            foreach (Enemy enemy in enemies)
            {
                if (ship.displayArea.IntersectsWith(enemy.displayArea))
                {
                    EnemiesToRemove.Add(enemy);
                }
            }

            //remove lasers that hit an enemy:
            foreach (Enemy enemy in EnemiesToRemove)
            {
                enemies.Remove(enemy);
                ship.lives -= 1;
                shipDestroyed = true;
                enemies = new HashSet<Enemy>();
                AddEnemies();
                awaitClick = true;
                break;
            }

            if (ship.lives <= 0)
            {
                shipDestroyed = false;
                gameOver = true;
                //ship = new Ship(this.DisplayRectangle);
                //enemies = null;
                //AddEnemies();
            }
                



        }

        private void AddEnemies()
        {
            for (int i = 0; i <= (ship.subLevel) * (ship.level+1);  i++ )
            {
                enemies.Add(new Enemy(this.DisplayRectangle, ship));
            }
        }

        /// <summary>
        /// Method to check if a Laser has exited the top of the screen:
        /// </summary>
        /// <param name="laser">laser</param>
        /// <returns>true/false</returns>
        private bool LaserExitsScreen(Laser laser)
        {
            return laser.displayArea.Bottom <= this.DisplayRectangle.Top;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
