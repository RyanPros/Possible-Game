using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {

        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        //create a list to hold a column of boxes   
        List<Box> boxes = new List<Box>();

        SoundPlayer MainMusic = new SoundPlayer(Properties.Resources.Main);
        SoundPlayer HitM = new SoundPlayer(Properties.Resources.Hit);
        SoundPlayer Winn = new SoundPlayer(Properties.Resources.Win);

        int xLeft = 200;
        int gap = 400;
        int newBoxCounter = 0;
        Box hallbox1;
        Box hallbox2;
        Box hallbox3;
        Box hallbox4;

        Box startBox;
        Box endBox;
        Box middBox;
        Box middBox2;
        Box middBox3;

        Box wallBox;
        Box wallBox2;
        Box wallBox3;
        Box wallBox4;

        Box hero;


        Random randGen = new Random();

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            MainMusic.Play();
            CreateBox(xLeft);
            CreateBox(xLeft + gap);

            hallbox1 = new Box(this.Width / 9, this.Height - 100, 150, 75, new SolidBrush(Color.Gray));
            hallbox2 = new Box(725, this.Height - 450, 100, 75, new SolidBrush(Color.Gray));
            hallbox3 = new Box(725, this.Height - 450, 100, 75, new SolidBrush(Color.DarkGray));
            hallbox4 = new Box(this.Width / 6, this.Height / 300, 400, 15, new SolidBrush(Color.DarkGray));

            middBox = new Box(this.Width / 6, this.Height / -450, 400, 255, new SolidBrush(Color.DarkGray));
            middBox2 = new Box(725, this.Height - 350, 65, 75, new SolidBrush(Color.DarkGray));
            middBox3 = new Box(625, this.Height - 450, 165, 75, new SolidBrush(Color.DarkGray));
            startBox = new Box(-450, this.Height - 550, 600, -50, new SolidBrush(Color.Green));
            endBox = new Box(790, 0, 500, 500, new SolidBrush(Color.Green));

            wallBox = new Box(150, 495, 600, 100, new SolidBrush(Color.DarkGray));
            wallBox2 = new Box(0, 495, 150, 150, new SolidBrush(Color.Green));
            wallBox3 = new Box(0, -148, 150, 100, new SolidBrush(Color.Green));
            wallBox4 = new Box(-497, 0, 500, 100, new SolidBrush(Color.Green));

            hero = new Box(this.Width / 11, this.Height - 450, 30, 6, new SolidBrush(Color.Goldenrod));
        }

        public void CreateBox(int x)
        {
            SolidBrush ballBrush = new SolidBrush(Color.Blue);
            SolidBrush boxBrush = new SolidBrush(Color.White);
            //Box(x, y, size, speed, brush)
            Box b = new Box(x, 0, 36, 10, ballBrush);
            boxes.Add(b);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;

            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;

            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            Rectangle middbox = new Rectangle(middBox.x, middBox.y, middBox.size, middBox.size);
            Rectangle middbox2 = new Rectangle(middBox2.x, middBox2.y, middBox2.size, middBox2.size);
            Rectangle endbox = new Rectangle(endBox.x, endBox.y, endBox.size, endBox.size);
            Rectangle wallbox = new Rectangle(wallBox.x, wallBox.y, wallBox.size, wallBox.size);
            Rectangle wallbox2 = new Rectangle(wallBox2.x, wallBox2.y, wallBox2.size, wallBox2.size);
            Rectangle wallbox3 = new Rectangle(wallBox3.x, wallBox3.y, wallBox3.size, wallBox3.size);
            Rectangle wallbox4 = new Rectangle(wallBox4.x, wallBox4.y, wallBox4.size, wallBox4.size);

            newBoxCounter++;
            //move Hero
            if (leftArrowDown)
            {
                hero.x -= hero.speed;
            }

            else if (rightArrowDown)
            {
                hero.x += hero.speed;
            }
            else if (upArrowDown)
            {
                hero.y -= hero.speed;
            }

            else if (downArrowDown)
            {
                hero.y += hero.speed;
            }

            //update location of all boxes (drop down screen)
            foreach (Box b in boxes)
            {
                b.Move();
            }

            //remove box if it has gone of screen
            if (boxes[0].y > this.Height)
            {
                boxes.RemoveAt(0);
            }
         
            if (newBoxCounter == 18)
            {
                CreateBox(xLeft);
                CreateBox(xLeft + gap);

                newBoxCounter = 0;
            }

            //Look for conllisoin
            Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);

            foreach (Box b in boxes)
            {
                Rectangle boxRec = new Rectangle(b.x, b.y, b.size, b.size);
                if (heroRec.IntersectsWith(boxRec))
                {
                    HitM.Play();
                    gameLoop.Enabled = false;
                    Form1 f = new Form1();
                    f.Show();
                }
                else if (heroRec.IntersectsWith(middbox))
                {
                    hero.x = hero.x - hero.size;
                }
                else if (heroRec.IntersectsWith(middbox2))
                {
                    hero.x = hero.x - hero.size;
                    hero.y = hero.y - hero.size;
                }
                else if (heroRec.IntersectsWith(endbox))
                {
                    Winn.Play();
                    gameLoop.Enabled = false;
                    winScreen ws = new winScreen();
                    this.Controls.Add(ws);
                }
                else if (heroRec.IntersectsWith(wallbox))
                {
                    hero.y = hero.y - hero.size;
                }
                else if (heroRec.IntersectsWith(wallbox2))
                {
                    hero.y = hero.y - hero.size;
                }
                else if (heroRec.IntersectsWith(wallbox3))
                {
                    hero.y = hero.y + hero.size;
                }
                else if (heroRec.IntersectsWith(wallbox4))
                {
                    hero.x = hero.x + hero.size;
                }
            }

            Refresh();
        }



        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.FillRectangle(middBox.brushColour, middBox.x, middBox.y, 475, 400);
            e.Graphics.FillRectangle(hallbox2.brushColour, hallbox2.x, hallbox2.y, hallbox2.size, hallbox2.size);
            e.Graphics.FillRectangle(hallbox1.brushColour, hallbox1.x, hallbox1.y, hallbox1.size, hallbox1.size);
            e.Graphics.FillRectangle(startBox.brushColour, startBox.x, startBox.y, startBox.size, startBox.size);
            e.Graphics.FillRectangle(startBox.brushColour, 790, 0, 500, 500);
            e.Graphics.FillRectangle(hallbox1.brushColour, 250, middBox.size, 450, 105);
            e.Graphics.FillRectangle(hallbox1.brushColour, 625, hallbox2.y, 100, 450);
            e.Graphics.FillRectangle(middBox2.brushColour, middBox2.x, middBox2.y, middBox2.size, 425);
            e.Graphics.FillRectangle(middBox2.brushColour, middBox3.x, 0, middBox3.size, 52);
            e.Graphics.FillEllipse(hero.brushColour, hero.x, hero.y, hero.size, hero.size);
            e.Graphics.FillRectangle(wallBox.brushColour, wallBox.x, wallBox.y, wallBox.size, wallBox.size);
            e.Graphics.FillRectangle(wallBox2.brushColour, wallBox2.x, wallBox2.y, wallBox2.size, wallBox2.size);
            e.Graphics.FillRectangle(wallBox3.brushColour,wallBox3.x, wallBox3.y,wallBox3.size, wallBox3.size);
            e.Graphics.FillRectangle(wallBox4.brushColour, wallBox4.x, wallBox4.y, wallBox4.size,wallBox4.size);
            foreach (Box b in boxes)
            {
                e.Graphics.FillEllipse(b.brushColour, b.x, b.y, b.size, b.size);

            }





        }
    }
}
