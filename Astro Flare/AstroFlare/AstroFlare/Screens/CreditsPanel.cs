using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class CreditsPanel : ScrollingPanelControl
    {
        SpriteFont titleFont;
        SpriteFont descriptionFont18;
        float offset = 100;

        public CreditsPanel(ContentManager content)
        {
            titleFont = content.Load<SpriteFont>("Fonts\\Pericles22");
            descriptionFont18 = content.Load<SpriteFont>("Fonts\\Andy18");

            AddChild(new TextControl("Developer:", titleFont, Color.White, new Vector2(0, offset), true, true));
            offset += 60;
            AddChild(new TextControl("Digital Marsh - digitalmarsh@gmail.com", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 60;

            AddChild(new TextControl("Music:", titleFont, Color.White, new Vector2(0, offset), true, true));
            offset += 60;

            AddChild(new TextControl("Intro: Before the Storm", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("Rampage: Through the Stars", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("Rampage - Timed: Galactic Struggle", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("Extermination: Against the Odds", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("www.brian-doyle.com", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 60;

            AddChild(new TextControl("Alter Ego: Survival", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("Alter Ego - Timed: Holocaust", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("www.playonloop.com", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 60;

            AddChild(new TextControl("Time Bandit: Giant Robots Fighting", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("www.opengameart.org", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 60;

            AddChild(new TextControl("Additional Graphics:", titleFont, Color.White, new Vector2(0, offset), true, true));
            offset += 60;
            AddChild(new TextControl("www.opengameart.org", descriptionFont18, Color.White, new Vector2(0, offset), true, true));
            offset += 40;
            AddChild(new TextControl("Philippe Chabot - www.arcengames.com", descriptionFont18, Color.White, new Vector2(0, offset), true, true));



            //LayoutColumn(400, 40, 40);
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
        }
    }
}
