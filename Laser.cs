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
    /// A Class to handle Lasers
    /// </summary>
    class Laser
    {
        /// <summary>
        /// Public access to Laser's Display Rectangle
        /// </summary>
        public Rectangle displayArea;

            //Display area dimensions:
        private int laserWidth = 2;
        private int laserHeight = 50;

            //speed of lasers ( -1 = down, 0 = doesn't move 1 = slowly up)
        private int speed = 40;

  

        /// <summary>
        /// Laser Constructor
        /// </summary>
        /// <param name="origin">A screen point where the laser begins</param>
        public Laser(Point origin)
        {
                //create a new laser:
            displayArea.Width = laserWidth;
            displayArea.Height = laserHeight;

                //Set its XY point:
            displayArea.X = origin.X;
            displayArea.Y = origin.Y;
        }

        /// <summary>
        /// Laser Move Behaviour
        /// </summary>
        public void Move()
        {
                //Goes to the top of the screen at (speed)/tick
            displayArea.Y -= speed;
        }

        /// <summary>
        /// Draw the Laser
        /// </summary>
        /// <param name="graphics">Graphics to use</param>
        public void Draw(Graphics graphics)
        {
                //Create the laser brush:
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(this.displayArea.X, this.displayArea.Y),
                new Point(this.displayArea.X, this.displayArea.Y + laserHeight),
                Color.FromArgb(255, 0, 255, 255),
                Color.FromArgb(0, 0, 255, 255));

                //Draw the rectangle:
            graphics.FillRectangle(linGrBrush, displayArea);
        }

    }
}
