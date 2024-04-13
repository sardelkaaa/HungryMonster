namespace HungryMonster
{
    public partial class HungryMonster : Form
    {
        Image heroIdle;
        bool goLeft, goRight, jumping, hasMelon;
        int jumpSpeed = 10;
        int force = 0;
        int score = 0;
        int playerSpeed = 1;
        int backgroundSpeed = 3;

        public HungryMonster()
        {
            InitializeComponent();
            heroIdle = Properties.Resources.Pink_Monster;
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            textLabel.Text = "SCORE: " + score;
            PinkMonster.Top += jumpSpeed;

            if (goLeft && PinkMonster.Left > 60)
                PinkMonster.Left -= playerSpeed;
            if (goRight && PinkMonster.Left + (PinkMonster.Width) < ClientSize.Width)
                PinkMonster.Left += playerSpeed;

            if (goLeft && background.Left < 0)
            {
                background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }

            if (goRight && background.Left > -709)
            {
                background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }

            if (jumping)
            {
                jumpSpeed = -8;
                force--;
            }

            else
            {
                jumpSpeed = 8;
            }

            if (jumping && force < 0)
                jumping = false;

            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (PinkMonster.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 4;
                        PinkMonster.Top = x.Top - PinkMonster.Height;
                        jumpSpeed = 0;
                    }
                    x.BringToFront();
                }

                if (x is PictureBox && (string)x.Tag == "fruit")
                {
                    if (PinkMonster.Bounds.IntersectsWith(x.Bounds))
                    {
                        Controls.Remove(x);
                        score++;
                    }
                }
            }

            if (PinkMonster.Bounds.IntersectsWith(melon.Bounds))
            {
                melon.Visible = false;
                hasMelon = true;
            }

                if (PinkMonster.Bounds.IntersectsWith(finishPoint.Bounds) && hasMelon)
                {
                    finishPoint.Image = Properties.Resources.finishPoint;
                    GameTimer.Stop();
                    MessageBox.Show("You Completed the level!");
                }
            if (PinkMonster.Top + PinkMonster.Height > ClientSize.Height + 60)
            {
                GameTimer.Stop();
                MessageBox.Show("You Died!");

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                goLeft = true;
            if (e.KeyCode == Keys.Right)
                goRight = true;
            if (e.KeyCode == Keys.Space && jumping == false)
                jumping = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                goLeft = false;
            if (e.KeyCode == Keys.Right)
                goRight = false;
            if (!jumping)
                jumping = false;
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
                if (x is PictureBox && (string)x.Tag == "platform" ||
                    x is PictureBox && (string)x.Tag == "fruit" ||
                    x is PictureBox && (string)x.Tag == "melon" ||
                    x is PictureBox && (string)x.Tag == "finishPoint" ||
                    x is PictureBox && (string)x.Tag == "startPoint")
                {
                    if (direction == "back")
                        x.Left -= backgroundSpeed;
                    if (direction == "forward")
                        x.Left += backgroundSpeed;
                }
            }
        }
    }
}