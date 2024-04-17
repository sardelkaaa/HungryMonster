using System.Numerics;

namespace HungryMonster
{
    internal class PlayerMovement : Player
    {
        private int PinkMonsterLeft;
        private int PinkMonsterWidth;
        private int ClientSizeWidth;
        private Player player;

        public PlayerMovement(int PinkMonsterLeft, int PinkMonsterWidth, int ClientSizeWidth)
        {
            this.PinkMonsterLeft = PinkMonsterLeft;
            this.PinkMonsterWidth = PinkMonsterWidth;
            this.ClientSizeWidth = ClientSizeWidth;
            player = this;
        }

        public void PlayerMoveLeft()
        {
            if (player.goLeft && PinkMonsterLeft > 60)
                PinkMonsterLeft -= player.playerSpeed;
        }

        public void PlayerMoveRight()
        {
            if (player.goRight && PinkMonsterLeft + PinkMonsterWidth < ClientSizeWidth)
                PinkMonsterLeft += player.playerSpeed;
        }
    }
}