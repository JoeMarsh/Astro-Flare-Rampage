using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class EffectNode : Node
    {
        public EffectNode(SpriteSheet spriteSheet, Vector2 position)
            : base(spriteSheet)
        {
            this.Sprite.Loop += new NotifyHandler(Sprite_Loop);
            this.Position = position;
        }

        void Sprite_Loop()
        {
            this.Remove();
        }
    }
}
