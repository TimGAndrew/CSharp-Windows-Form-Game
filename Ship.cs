using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.TimAndrew.W0212032
{
    /// <summary>
    /// A Class to handle Ships
    /// </summary>
    public class Ship
    {

        public Rectangle displayArea;

        private int shipWidth = 50;
        private int shipHeight = 70;

        private Image image;

        

        //public LaserTurrent1()

       
        /// <summary>
        /// Method to get and set the number of lives:
        /// </summary>
        public int lives { get; set; }

        /// <summary>
        /// Method to get and set the score:
        /// </summary>
        public int score {get; set;}

        public int level { get; set; }

        public int subLevel { get; set; }


		public Ship(Rectangle gameplayarea)
        {

            image = Image.FromFile("Images/ship.png");


            level = 0;
            subLevel = 0;
            score = 0;
            lives = 3;

            displayArea.Height = shipHeight;
            displayArea.Width = shipWidth;

            //center the ship:
            displayArea.Y = gameplayarea.Bottom - 100;
            displayArea.X = gameplayarea.Right / 2;
        }


		///move controlled by mouse:
		public void Move(Point mouseLocation, Rectangle gameplayarea)
        {

            //handel x position:

            displayArea.X = mouseLocation.X - (shipWidth / 2);

            if (displayArea.X >= gameplayarea.Right - shipWidth)
            {
                displayArea.X = gameplayarea.Right - shipWidth;
            }
            else if (displayArea.X <= gameplayarea.Left)
            {
                displayArea.X = gameplayarea.X;
            }

            // handle y position:

            displayArea.Y = mouseLocation.Y - (shipHeight / 2);

            if (displayArea.Y >= gameplayarea.Bottom - shipHeight)
            {
                displayArea.Y = gameplayarea.Bottom - shipHeight;
            }
            else if (displayArea.Y <= gameplayarea.Top)
            {
                displayArea.Y = gameplayarea.Y;
            }
            
        }


        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, displayArea);
            //SolidBrush brush = new SolidBrush(Color.Aquamarine);
               
				//fill the rectangle:
            //graphics.FillRectangle(brush, displayArea);
        }



    }
}
