using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace first_game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, inTouch, lvlup;

        int force;
        int score ;
        int playerSpeed = 7;
        int restart = 0;
        

        int horizontalSpeed = 5;
        

        int enemyOneSpeed = 2;
        int enemyTwoSpeed = 1;
        int enemyThreeSpeed = 3;
      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {

            if (lvlup)
            {
                Player.Height = 50;
                txtScore.Text = "Score:" + score + Environment.NewLine + "Nice  ";
            }
            else
            {
                Player.Height = 25;
            }

            if (restart >= 5)
            {
                txtScore.Text = "Score:" + score + Environment.NewLine + "NOOB :( ";
            }
            else   txtScore.Text = "Score:" + score + Environment.NewLine + "Colect nine coins and take them to the door "; ;
            if (goLeft == true)
            {
                Player.Left -= playerSpeed;
            }
            if(goRight == true)
            {
                Player.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
             

                Player.Top += force;
                force++;

            }
            if (jumping == false )
            {
                force++;
                Player.Top += force;
            }
            foreach (Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    

                    if ((string)x.Tag =="platform")
                    {
                        if(Player.Bounds.IntersectsWith(x.Bounds) )
                        {
                            Player.Height = Player.Height -3 ;
                          inTouch = true;
                            
                                Player.Top = x.Top - Player.Height;
                                inTouch = true;
                            force = 3;
                            jumping = false;
                          
                        } 
               
                     
                        x.BringToFront();
                    }
                    else if ((string)x.Tag == "Door"&& Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (lvlup == true) { RestartGame(); }
                        else if (score == 9) { lvlup = true; RestartGame(); }
                        
                    }
                    else if ((string)x.Tag == "Coin" && Player.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                    {
                        x.Visible = false;
                        score++;

                    }
                    else if ((string)x.Tag == "Spikes" ||   (string)x.Tag == "Enemy")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            RestartGame();
                        }
                      
                    }
                }

            }
         
                Enemy1.Left = Enemy1.Left + enemyOneSpeed;
            if (Enemy1.Left < 477 || Enemy1.Left > 555) enemyOneSpeed = enemyOneSpeed * (-1);

            MovingPlatform.Left = MovingPlatform.Left + horizontalSpeed;
            if (MovingPlatform.Left < 35 || MovingPlatform.Left > 522) horizontalSpeed = horizontalSpeed * (-1);

            Enemy2.Left = Enemy2.Left + enemyTwoSpeed;
            if (Enemy2.Left < 20 || Enemy2.Left > 141) enemyTwoSpeed = enemyTwoSpeed * (-1);

            Enemy3.Top = Enemy3.Top + enemyThreeSpeed;
            if (Enemy3.Top < 101 || Enemy3.Top > 236) enemyThreeSpeed = enemyThreeSpeed * (-1);
        }


        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                inTouch = false;
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter )
            {
                RestartGame();

            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if( e.KeyCode == Keys.Space && jumping == false && inTouch == true )
            {
                jumping = true;
                force = -15;
                inTouch = false;

            }
     


        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            
            score = 0;
            restart++;

            txtScore.Text = "Score" + score ;

            foreach (Control x in this.Controls)
            {
                if(x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }

            }
            // Reset posision of  player, platforms and enemys
         
            Player.Left = 30;
            Player.Top = 677;
            Enemy1.Left = 478;
            Enemy1.Top =  576;
            Enemy2.Left = 141; 
            Enemy2.Top = 155;
            Enemy3.Left = 537; 
            Enemy3.Top = 101;
            PlatformTop.Top = 274;
            MovingPlatform.Top = 456;
            MovingPlatform.Left = 522; 
            gameTimer.Start();
        }
    }
}
