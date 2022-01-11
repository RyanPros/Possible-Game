using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Possiblegame
{
    public partial class Gamescreen : UserControl
    {
        List<Balls> boxes = new List<Balls>();

        int paddle1X = 110;
        int paddle1Y = 180;
        int paddleSpeed = 4;
        int paddleWidth = 40;
        int paddleHeight = 40;

        int newCounter = 0;

        Balls hero;
        Balls Ball;

        bool aDown = false;
        bool dDown = false;
        bool wDown = false;
        bool sDown = false;

        Random randGen = new Random();

        public Gamescreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            Ball = new Balls(this. Width / 2 - 10, this.Height - 100, 30, 8, new SolidBrush(Color.Blue));
            hero = new Balls(this.Width / 2 - 15, this.Height - 100, 30, 8, new SolidBrush(Color.Goldenrod));
        }

        public void CreateBox(int x)
        {
            SolidBrush boxBrush = new SolidBrush(Color.Blue);
            Balls b1 = new Balls (20, 0, 36, 10, boxBrush);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Set All Keys
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;

            }

        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
            }

        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //move player 1 
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && paddle1X > 0)
            {
                paddle1X -= paddleSpeed;
            }
            if (dDown == true && paddle1X < this.Width - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }


            //Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);

            //foreach (Balls b in )
            //{
            //    Rectangle boxRec = new Rectangle(b.x, b.y, b.size, b.size);
            //    if (heroRec.IntersectsWith(boxRec)) ;
            //    {
            //        gameLoop.Enabled = false;
            //    }
            //}

            //Refresh();
        }



        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw boxes to screen
            foreach (Balls b in boxes)
            {
                e.Graphics.FillEllipse(b.brushColour, b.x, b.y, b.size, b.size);

            }

            e.Graphics.FillEllipse(hero.brushColour, hero.x, hero.y, hero.size, hero.size);
        }
    }
}
