
namespace AstroFlare
{
    class Coin : Powerup
    {
        //Vector2 line;

        public Coin(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            //GameStateManagementGame.Instance.soundManager.PlaySound("coin", 0.5f);
            if (Config.Level == LevelSelect.Practise)
            {
                Config.Multi += 0.01;
            }
            else if (ship == Player.Ship)
            {
                Config.Multi += 0.01;
                //Config.Coins += 1;
                Config.CoinsCollected += 1;
                //GameStateManagementGame.Instance.soundManager.PlaySound("coin", 0.5f);   
                //GameStateManagementGame.Instance.soundManager.PlaySound("hat2");       
            }
            else if (ship == Player.EnemyPlayer)
                Config.AIMulti += 0.01;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //if (PlayerShip.PlayerShips.Count == 1)
            //{
            //    line = Player.Ship.Position - this.Position;
            //    // use length squared instead of more costly length(). 40000 = (200 * 200)
            //    if (line.LengthSquared() < (40000))
            //    {
            //        this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 1f);
            //        this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            //        this.Direction.Normalize();
            //        this.Position += Direction * 10;
            //    }
            //}

            base.Update(gameTime);
        }
    }
}