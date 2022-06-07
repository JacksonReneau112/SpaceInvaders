﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        Rectangle player = new Rectangle(290, 450, 30, 10);
        List<Rectangle> row1Aliens = new List<Rectangle>();
        List<Rectangle> row2Aliens = new List<Rectangle>();
        List<Rectangle> row3Aliens = new List<Rectangle>();
        List<Rectangle> playerShot = new List<Rectangle>();
        List<Rectangle> obsticals = new List<Rectangle>();



        int playerSpeed = 5;
        int alienSpeed = -3;
        int alienSpeed2 = -3;
        int alienSpeed3 = -3;

        int projectileSpeed = -10;

        int score = 0;
        int round = 1;

        int counter = 0;

        int x = 0;

        string gameState = "waiting";

        bool aDown = false;
        bool dDown = false;
        bool spaceDown = false;

        SolidBrush greenBrush = new SolidBrush(Color.Chartreuse);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);



        public Form1()
        {
            InitializeComponent();

            AlienInitialzing();

            obsticals.Add(new Rectangle(112, 400, 60, 10));
            obsticals.Add(new Rectangle(275, 400, 60, 10));
            obsticals.Add(new Rectangle(437, 400, 60, 10));

        }

        public void GameInitialize()
        {
            titleLabel.Text = "";
            titleLabel.Visible = false;
            subTitleLabel.Text = "";
            subTitleLabel.Visible = false;
            scoreLabel.Visible = true;
            roundLabel.Visible = true;


            gameEngine.Enabled = true;
            gameState = "running";

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                case Keys.Enter:

                    if (gameState == "waiting" || gameState == "over")

                    {

                        GameInitialize();

                    }

                    break;

                case Keys.Escape:

                    if (gameState == "waiting" || gameState == "over")

                    {

                        Application.Exit();

                    }

                    break;
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {


            // Move Player
            if (aDown == true && player.X > 0)
            {
                //Finding X position to left
                player.X -= playerSpeed;
            }

            if (dDown == true && player.X < this.Width - player.Width)
            {
                //Finding X position to Right
                player.X += playerSpeed;
            }

            //moving row 1 aliens
            for (int i = 0; i < row1Aliens.Count(); i++)
            {
                //find the new postion of x based on speed 
                x = row1Aliens[i].X + alienSpeed;

                //replace the rectangle in the list with updated one using new x 
                row1Aliens[i] = new Rectangle(x, row1Aliens[i].Y, row1Aliens[i].Height, row1Aliens[i].Width);
            }

            //moving row 2 aliens
            for (int i = 0; i < row2Aliens.Count(); i++)
            {
                //find the new postion of x based on speed 
                x = row2Aliens[i].X + alienSpeed2;

                //replace the rectangle in the list with updated one using new x 
                row2Aliens[i] = new Rectangle(x, row2Aliens[i].Y, row2Aliens[i].Height, row2Aliens[i].Width);
            }

            //moving row 3 aliens
            for (int i = 0; i < row3Aliens.Count(); i++)
            {
                //find the new postion of x based on speed 
                x = row3Aliens[i].X + alienSpeed3;

                //replace the rectangle in the list with updated one using new x 
                row3Aliens[i] = new Rectangle(x, row3Aliens[i].Y, row3Aliens[i].Height, row3Aliens[i].Width);
            }

            //check edges

            if (row1Aliens.Count > 0)
            {
                if (row1Aliens[0].X < 0)
                {
                    AlienMovment();
                }
                else if (row1Aliens[row1Aliens.Count - 1].X > this.Width - row1Aliens[0].Width)
                {
                    AlienMovment();
                }
            }

            //check edges

            if (row2Aliens.Count > 0)
            {
                if (row2Aliens[0].X < 0)
                {
                    AlienMovment();
                }
                else if (row2Aliens[row2Aliens.Count - 1].X > this.Width - row2Aliens[0].Width)
                {
                    AlienMovment();
                }
            }

            //check edges

            if (row3Aliens.Count > 0)
            {
                if (row3Aliens[0].X < 0)
                {
                    AlienMovment();
                }
                else if (row3Aliens[row3Aliens.Count - 1].X > this.Width - row3Aliens[0].Width)
                {
                    AlienMovment();
                }
            }

            //Player Shooting
            if (counter < 10)
            {
                counter++;
            }
            if (spaceDown == true && counter == 10)
            {
                int x = player.X + 10;
                int y = player.Y;
                playerShot.Add(new Rectangle(x, y, 10, 10));
                counter = 0;
            }

            // Moving projectiles

            for (int i = 0; i < playerShot.Count(); i++)
            {
                //find the new postion of x based on speed 
                int y = playerShot[i].Y + projectileSpeed;

                //replace the rectangle in the list with updated one using new x 
                playerShot[i] = new Rectangle(playerShot[i].X, y, 10, 10);
            }

            //Removing projectiles that are off screen.
            for (int i = 0; i < playerShot.Count(); i++)
            {
                if (playerShot[i].Y < 0)
                {
                    playerShot.RemoveAt(i);
                }
            }

            //Players shot hits an alien

            for (int i = 0; i < playerShot.Count(); i++)
            {
                for (int j = 0; j < row1Aliens.Count(); j++)
                {
                    if (playerShot[i].IntersectsWith(row1Aliens[j]))
                    {
                        playerShot.RemoveAt(i);
                        row1Aliens.RemoveAt(j);
                        score = score + 25;
                        break;
                    }
                }
            }

            for (int i = 0; i < playerShot.Count(); i++)
            {
                for (int j = 0; j < row2Aliens.Count(); j++)
                {
                    if (playerShot[i].IntersectsWith(row2Aliens[j]))
                    {
                        playerShot.RemoveAt(i);
                        row2Aliens.RemoveAt(j);
                        score = score + 25;
                        break;
                    }
                }
            }

            for (int i = 0; i < playerShot.Count(); i++)
            {
                for (int j = 0; j < row3Aliens.Count(); j++)
                {
                    if (playerShot[i].IntersectsWith(row3Aliens[j]))
                    {
                        playerShot.RemoveAt(i);
                        row3Aliens.RemoveAt(j);
                        score = score + 25;
                        break;
                    }
                }
            }

            //A projectiloe hits the players cover.
            for (int i = 0; i < playerShot.Count(); i++)
            {
                for (int j = 0; j < obsticals.Count(); j++)
                {
                    if (playerShot[i].IntersectsWith(obsticals[j]))
                    {
                        playerShot.RemoveAt(i);
                        break;
                    }
                }
            }
            if (round == 3 && row1Aliens.Count == 0 && row2Aliens.Count == 0 && row3Aliens.Count == 0)
            {
                gameEngine.Enabled = false;
            }
            else if (row1Aliens.Count == 0 && row2Aliens.Count == 0 && row3Aliens.Count == 0)
            {
                round++;
                alienSpeed = -3;
                alienSpeed2 = -3;
                alienSpeed3 = -3;

                AlienInitialzing();
            }



            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Title screen
            if (gameState == "waiting")
            {
                titleLabel.Text = "SPACE INVADERS";
                subTitleLabel.Text = "Press Enter to Start or Escape to Exit \n A, D to move, Space to shoot.";
            }

            //game running 
            else if (gameState == "running")
            {
                //display player
                e.Graphics.FillRectangle(greenBrush, player);

                //display aliens
                for (int i = 0; i < row1Aliens.Count(); i++)
                {
                    e.Graphics.FillRectangle(greenBrush, row1Aliens[i]);
                }
                for (int i = 0; i < row2Aliens.Count(); i++)
                {
                    e.Graphics.FillRectangle(greenBrush, row2Aliens[i]);
                }
                for (int i = 0; i < row3Aliens.Count(); i++)
                {
                    e.Graphics.FillRectangle(greenBrush, row3Aliens[i]);
                }

                //Display projectiles
                for (int i = 0; i < playerShot.Count(); i++)
                {
                    e.Graphics.FillRectangle(redBrush, playerShot[i]);
                }

                //Display obsticals
                for (int i = 0; i < obsticals.Count(); i++)
                {
                    e.Graphics.FillRectangle(whiteBrush, obsticals[i]);
                }

                scoreLabel.Text = $"Score: {score}";
                roundLabel.Text = $"Round: {round}";
            }
        }

        public void AlienMovment()
        {

            for (int i = 0; i < row1Aliens.Count(); i++)
            {
                int y = row1Aliens[i].Y + 5;
                row1Aliens[i] = new Rectangle(row1Aliens[i].X, y, row1Aliens[i].Height, row1Aliens[i].Width);
            }
            for (int i = 0; i < row2Aliens.Count(); i++)
            {
                int y = row2Aliens[i].Y + 5;
                row2Aliens[i] = new Rectangle(row2Aliens[i].X, y, row2Aliens[i].Height, row2Aliens[i].Width);
            }
            for (int i = 0; i < row3Aliens.Count(); i++)
            {
                int y = row3Aliens[i].Y + 5;
                row3Aliens[i] = new Rectangle(row3Aliens[i].X, y, row3Aliens[i].Height, row3Aliens[i].Width);
            }

            alienSpeed = alienSpeed * -1;
            alienSpeed2 = alienSpeed2 * -1;
            alienSpeed3 = alienSpeed3 * -1;

        }

        public void AlienInitialzing()
        {
            row1Aliens.Add(new Rectangle(220, 100, 20, 20));
            row1Aliens.Add(new Rectangle(260, 100, 20, 20));
            row1Aliens.Add(new Rectangle(300, 100, 20, 20));
            row1Aliens.Add(new Rectangle(340, 100, 20, 20));
            row1Aliens.Add(new Rectangle(380, 100, 20, 20));

            row2Aliens.Add(new Rectangle(220, 130, 20, 20));
            row2Aliens.Add(new Rectangle(260, 130, 20, 20));
            row2Aliens.Add(new Rectangle(300, 130, 20, 20));
            row2Aliens.Add(new Rectangle(340, 130, 20, 20));
            row2Aliens.Add(new Rectangle(380, 130, 20, 20));

            row3Aliens.Add(new Rectangle(220, 160, 20, 20));
            row3Aliens.Add(new Rectangle(260, 160, 20, 20));
            row3Aliens.Add(new Rectangle(300, 160, 20, 20));
            row3Aliens.Add(new Rectangle(340, 160, 20, 20));
            row3Aliens.Add(new Rectangle(380, 160, 20, 20));
        }
    }
}
