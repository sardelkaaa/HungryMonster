using System.Runtime.CompilerServices;

namespace HungryMonster
{
    public partial class HungryMonster : Form
    {
        Image heroIdle;
        Player player = new Player();

        public HungryMonster()
        {
            InitializeComponent();
            heroIdle = Properties.Resources.Pink_Monster;
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {            
            PinkMonster.Refresh();
            textLabel.Text = "SCORE: " + player.score;
            PinkMonster.Top += player.jumpSpeed;
            var movement = new PlayerMovement(PinkMonster.Left, PinkMonster.Width, ClientSize.Width);


            //if (player.goLeft && PinkMonster.Left > 60)
            //    PinkMonster.Left -= player.playerSpeed;
            //if (player.goRight && PinkMonster.Left + PinkMonster.Width < ClientSize.Width)
            //    PinkMonster.Left += player.playerSpeed;

            movement.PlayerMoveLeft();
            movement.PlayerMoveRight();

            if (player.goLeft && background.Left < 2)
            {
                background.Left += player.backgroundSpeed;
                MoveGameElements("forward");
            }

            if (player.goRight && background.Left > -709)
            {
                background.Left -= player.backgroundSpeed;
                MoveGameElements("back");
            }

            if (player.jumping)
            {
                player.jumpSpeed = -8;
                player.force--;
            }

            else
                player.jumpSpeed = 8;

            if (player.jumping && player.force < 0)
                player.jumping = false;

            foreach (Control x in Controls)
            {
                if (IsPictureBoxItem(x, "platform"))
                {
                    if (IsPlayerCollideItem(x) && !player.jumping)
                    {
                        player.force = 4;
                        PinkMonster.Top = x.Top - PinkMonster.Height;
                        player.jumpSpeed = 0;
                    }
                    x.BringToFront();
                }

                if (IsPictureBoxItem(x, "fruit"))
                {
                    if (IsPlayerCollideItem(x))
                    {
                        Controls.Remove(x);
                        player.score++;
                    }
                }
            }

            if (IsPlayerCollideItem(melon))
            {
                melon.Visible = false;
                player.hasMelon = true;
            }

            if (IsPlayerCollideItem(finishPoint) && player.hasMelon)
            {
                finishPoint.Image = Properties.Resources.finishPoint;
                GameTimer.Stop();
                MessageBox.Show("You Completed the level!");
            }
            if (IsPlayerFell())
            {
                GameTimer.Stop();
                MessageBox.Show("You Died!");

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                player.goLeft = true;
            if (e.KeyCode == Keys.Right)
                player.goRight = true;
            if (e.KeyCode == Keys.Space && player.jumping == false)
                player.jumping = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                player.goLeft = false;
            if (e.KeyCode == Keys.Right)
                player.goRight = false;
            if (!player.jumping)
                player.jumping = false;
        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {

        }

        private void RestartGame()
        {

        }

        private void MoveGameElements(string direction)
        {
            foreach (Control x in Controls)
            {
                if (IsPictureBoxItem(x, "platform") ||
                    IsPictureBoxItem(x, "fruit") ||
                    IsPictureBoxItem(x, "melon") ||
                    IsPictureBoxItem(x, "finishPoint") ||
                    IsPictureBoxItem(x, "startPoint"))
                {
                    if (direction == "back")
                        x.Left -= player.backgroundSpeed;
                    if (direction == "forward")
                        x.Left += player.backgroundSpeed;
                }
            }
        }

        private bool IsPlayerFell()
        {
            return PinkMonster.Top + PinkMonster.Height > ClientSize.Height + 60;
        }

        private bool IsPlayerCollideItem(Control x)
        {
            return PinkMonster.Bounds.IntersectsWith(x.Bounds);
        }

        private bool IsPictureBoxItem(Control x, string item)
        {
            return x is PictureBox && (string)x.Tag == item;
        }
    }
}