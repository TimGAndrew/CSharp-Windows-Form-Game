using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.TimAndrew.W0212032
{
    /// <summary>
    /// A Class to handle Enemies
    /// </summary>
    class Enemy
    {
        private Image image;
        /// <summary>
        /// Public access to Enemy Display Rectangle
        /// </summary>
        public Rectangle displayArea;

        private int enemyDimension { get; set; }

        /// <summary>
        /// Target enemy will move towards:
        /// </summary>
        private Point target { get; set; }

        /// <summary>
        /// Speed enemy will move at:
        /// </summary>
        private int speed { get; set; }

        /// <summary>
        /// Enemy constructor
        /// </summary>
        /// <param name="gameplayarea">gameplay area or screen dimensions</param>
        public Enemy(Rectangle gameplayarea, Ship ship)
        {
                //Enemy Constructor uses random numbers:
            Random random = new Random();

            //pick image:
            int imageChoice = random.Next(1, 6);
            switch (imageChoice)
            {
                case 1:
                    image = Image.FromFile("Images/enemy1.png");
                    break;
                case 2:
                    image = Image.FromFile("Images/enemy2.png");
                    break;
                case 3:
                    image = Image.FromFile("Images/enemy3.png");
                    break;
                case 4:
                    image = Image.FromFile("Images/enemy4.png");
                    break;
                case 5:
                    image = Image.FromFile("Images/enemy5.png");
                    break;
            }


            //randomize enemies speed:
            speed = random.Next(1, 5) * ((ship.level + 1));
                //randomize enemies size:
            enemyDimension = random.Next(75, 100);

                //set enemyDisplayArea:
            displayArea.Height = enemyDimension;
            displayArea.Width = enemyDimension;

                //Pick a side for the enemy to enter from:
            int side = random.Next(1, 4);

                //set the enemies initial position based on side generated:
            switch (side)
            {
                case 1: //left side:
                    {
                        displayArea.X = gameplayarea.Left - enemyDimension;
                        displayArea.Y = random.Next(gameplayarea.Top, gameplayarea.Bottom - enemyDimension);
                        break;
                    }
                case 2: //right side:
                    {
                        displayArea.X = gameplayarea.Right;
                        displayArea.Y = random.Next(gameplayarea.Top, gameplayarea.Bottom - enemyDimension);
                        break;
                    }
                case 3: //top side:
                    {
                        displayArea.Y = gameplayarea.Top - enemyDimension;
                        displayArea.X = random.Next(gameplayarea.Left, gameplayarea.Right - enemyDimension);
                        break;
                    }
                case 4: //bottom side:
                    {
                        displayArea.Y = gameplayarea.Bottom;
                        displayArea.X = random.Next(gameplayarea.Left, gameplayarea.Right - enemyDimension);
                        break;
                    }
            }
        }

        public void Move(Point shipLocation)
        {
                //set the enemy's 
            target = shipLocation;
            
                //move the x:
            if (target.X < displayArea.X)
            {
                displayArea.X -= speed;
            }
            else if(target.X > displayArea.X)
            {
                displayArea.X += speed;
            }

                //move the y:
            if (target.Y < displayArea.Y)
            {
                displayArea.Y -= speed;
            }
            else if (target.Y > displayArea.Y)
            {
                displayArea.Y += speed;
            }




        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, displayArea);

            //SolidBrush brush = new SolidBrush(Color.Orange);

            //fill the rectangle:
           //graphics.FillRectangle(brush, displayArea);
        }

    }
}
