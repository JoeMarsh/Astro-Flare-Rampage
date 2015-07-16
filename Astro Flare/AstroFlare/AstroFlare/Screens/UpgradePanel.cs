using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class UpgradePanel : ScrollingPanelControl
    {
        SpriteFont titleFont;
        SpriteFont buttonFont;
        SpriteFont buttonFontSmall;

        Texture2D Button;
        Color buttonSelectedColor = Color.Black;

        ButtonControl bHealth5;
        ButtonControl bHealth10;
        ButtonControl bHealth15;
        ButtonControl bShields10;
        ButtonControl bShields20;
        ButtonControl bShields30;
        ButtonControl bDamage1;
        ButtonControl bDamage2;
        ButtonControl bDamage4;
        ButtonControl bRange10;
        ButtonControl bRange20;
        ButtonControl bRange50;
        ButtonControl bPowerup5;
        ButtonControl bPowerup10;
        ButtonControl bPowerup20;
        //ButtonControl bColorBlue;
        //ButtonControl bColorPurple;

        public UpgradePanel(ContentManager content)
        {
            // load activeUpgrades

            titleFont = content.Load<SpriteFont>("Fonts\\Andy22");
            //buttonFont = content.Load<SpriteFont>("Fonts\\Tahoma14");
            buttonFont = content.Load<SpriteFont>("menufont");
            buttonFontSmall = content.Load<SpriteFont>("menufontsmall");

            Button = content.Load<Texture2D>("GameScreens\\Buttons\\B_Transparent");

            bHealth5 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 1,000", buttonFont, "Reinforced Hull: Health + 5", buttonFontSmall, false);
            bHealth5.Tapped += new EventHandler<EventArgs>(bHealth5_Tapped);

            bHealth10 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 3,000", buttonFont, "Reinforced Hull: Health + 10", buttonFontSmall, false);
            bHealth10.Tapped += new EventHandler<EventArgs>(bHealth10_Tapped);

            bHealth15 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 7,000", buttonFont, "Reinforced Hull: Health + 15", buttonFontSmall, false);
            bHealth15.Tapped += new EventHandler<EventArgs>(bHealth15_Tapped);

            bShields10 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 1,000", buttonFont, "Shield Booster: Shields + 10", buttonFontSmall, false);
            bShields10.Tapped += new EventHandler<EventArgs>(bShields10_Tapped);

            bShields20 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 3,000", buttonFont, "Shield Booster: Shields + 20", buttonFontSmall, false);
            bShields20.Tapped += new EventHandler<EventArgs>(bShields20_Tapped);

            bShields30 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 7,000", buttonFont, "Shield Booster: Shields + 30", buttonFontSmall, false);
            bShields30.Tapped += new EventHandler<EventArgs>(bShields30_Tapped);

            bDamage1 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 1,000", buttonFont, "Weapon Refit: Damage + 10%", buttonFontSmall, false);
            bDamage1.Tapped += new EventHandler<EventArgs>(bDamage1_Tapped);

            bDamage2 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 3,000", buttonFont, "Weapon Refit: Damage + 20%", buttonFontSmall, false);
            bDamage2.Tapped += new EventHandler<EventArgs>(bDamage2_Tapped);

            bDamage4 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 7,000", buttonFont, "Weapon Refit: Damage + 40%", buttonFontSmall, false);
            bDamage4.Tapped += new EventHandler<EventArgs>(bDamage4_Tapped);

            bRange10 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 1,000", buttonFont, "Extender: Weapon Range + 10", buttonFontSmall, false);
            bRange10.Tapped += new EventHandler<EventArgs>(bRange10_Tapped);

            bRange20 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 3,000", buttonFont, "Extender: Weapon Range + 20", buttonFontSmall, false);
            bRange20.Tapped += new EventHandler<EventArgs>(bRange20_Tapped);

            bRange50 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 7,000", buttonFont, "Extender: Weapon Range + 50", buttonFontSmall, false);
            bRange50.Tapped += new EventHandler<EventArgs>(bRange50_Tapped);

            bPowerup5 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 1,000", buttonFont, "Extractor: Powerup Drop +5%", buttonFontSmall, false);
            bPowerup5.Tapped += new EventHandler<EventArgs>(bPowerup5_Tapped);

            bPowerup10 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 3,000", buttonFont, "Extractor: Powerup Drop +10%", buttonFontSmall, false);
            bPowerup10.Tapped += new EventHandler<EventArgs>(bPowerup10_Tapped);

            bPowerup20 = new ButtonControl(Button, new Vector2(0, 0), "Unlock - 7,000", buttonFont, "Extractor: Powerup Drop +20%", buttonFontSmall, false);
            bPowerup20.Tapped += new EventHandler<EventArgs>(bPowerup20_Tapped);

            //bColorBlue = new ButtonControl(Button, new Vector2(0, 0), "Color Blue \n Unlock - 1,000", buttonFont);
            //bColorBlue.Tapped += new EventHandler<EventArgs>(bColorBlue_Tapped);

            //bColorPurple = new ButtonControl(Button, new Vector2(0, 0), "Color Purple \n Unlock - 1,000", buttonFont);
            //bColorPurple.Tapped += new EventHandler<EventArgs>(bColorPurple_Tapped);

            //AddChild(new TextControl("UPGRADES", titleFont));

            AddChild(bHealth5);
            AddChild(bHealth10);
            AddChild(bHealth15);
            AddChild(bShields10);
            AddChild(bShields20);
            AddChild(bShields30);
            AddChild(bDamage1);
            AddChild(bDamage2);
            AddChild(bDamage4);
            AddChild(bRange10);
            AddChild(bRange20);
            AddChild(bRange50);
            AddChild(bPowerup5);
            AddChild(bPowerup10);
            AddChild(bPowerup20);
            //AddChild(bColorBlue);
            //AddChild(bColorPurple);

            LayoutColumn(200, 40, 40);
        }

        public override void Update(GameTime gametime)
        {
            #region Update Buttons

            if (Config.bHealth5Active && !Config.bHealth5Locked)
            {
                bHealth5.Text = "Active";
                bHealth5.Color = buttonSelectedColor;
            }
            else if (!Config.bHealth5Locked)
            {
                bHealth5.Text = "Select";
                bHealth5.Color = Color.White;
            }

            if (Config.bHealth10Active && !Config.bHealth10Locked)
            {
                bHealth10.Text = "Active";
                bHealth10.Color = buttonSelectedColor;
            }
            else if (!Config.bHealth10Locked)
            {
                bHealth10.Text = "Select";
                bHealth10.Color = Color.White;
            }

            if (Config.bHealth15Active && !Config.bHealth15Locked)
            {
                bHealth15.Text = "Active";
                bHealth15.Color = buttonSelectedColor;
            }
            else if (!Config.bHealth15Locked)
            {
                bHealth15.Text = "Select";
                bHealth15.Color = Color.White;
            }

            if (Config.bShields10Active && !Config.bShields10Locked)
            {
                bShields10.Text = "Active";
                bShields10.Color = buttonSelectedColor;
            }
            else if (!Config.bShields10Locked)
            {
                bShields10.Text = "Select";
                bShields10.Color = Color.White;
            }

            if (Config.bShields20Active && !Config.bShields20Locked)
            {
                bShields20.Text = "Active";
                bShields20.Color = buttonSelectedColor;
            }
            else if (!Config.bShields20Locked)
            {
                bShields20.Text = "Select";
                bShields20.Color = Color.White;
            }

            if (Config.bShields30Active && !Config.bShields30Locked)
            {
                bShields30.Text = "Active";
                bShields30.Color = buttonSelectedColor;
            }
            else if (!Config.bShields30Locked)
            {
                bShields30.Text = "Select";
                bShields30.Color = Color.White;
            }

            if (Config.bDamage1Active && !Config.bDamage1Locked)
            {
                bDamage1.Text = "Active";
                bDamage1.Color = buttonSelectedColor;
            }
            else if (!Config.bDamage1Locked)
            {
                bDamage1.Text = "Select";
                bDamage1.Color = Color.White;
            }

            if (Config.bDamage2Active && !Config.bDamage2Locked)
            {
                bDamage2.Text = "Active";
                bDamage2.Color = buttonSelectedColor;
            }
            else if (!Config.bDamage2Locked)
            {
                bDamage2.Text = "Select";
                bDamage2.Color = Color.White;
            }

            if (Config.bDamage4Active && !Config.bDamage4Locked)
            {
                bDamage4.Text = "Active";
                bDamage4.Color = buttonSelectedColor;
            }
            else if (!Config.bDamage4Locked)
            {
                bDamage4.Text = "Select";
                bDamage4.Color = Color.White;
            }

            if (Config.bRange10Active && !Config.bRange10Locked)
            {
                bRange10.Text = "Active";
                bRange10.Color = buttonSelectedColor;
            }
            else if (!Config.bRange10Locked)
            {
                bRange10.Text = "Select";
                bRange10.Color = Color.White;
            }

            if (Config.bRange20Active && !Config.bRange20Locked)
            {
                bRange20.Text = "Active";
                bRange20.Color = buttonSelectedColor;
            }
            else if (!Config.bRange20Locked)
            {
                bRange20.Text = "Select";
                bRange20.Color = Color.White;
            }

            if (Config.bRange50Active && !Config.bRange50Locked)
            {
                bRange50.Text = "Active";
                bRange50.Color = buttonSelectedColor;
            }
            else if (!Config.bRange50Locked)
            {
                bRange50.Text = "Select";
                bRange50.Color = Color.White;
            }

            if (Config.bPowerup5Active && !Config.bPowerup5Locked)
            {
                bPowerup5.Text = "Active";
                bPowerup5.Color = buttonSelectedColor;
            }
            else if (!Config.bPowerup5Locked)
            {
                bPowerup5.Text = "Select";
                bPowerup5.Color = Color.White;
            }

            if (Config.bPowerup10Active && !Config.bPowerup10Locked)
            {
                bPowerup10.Text = "Active";
                bPowerup10.Color = buttonSelectedColor;
            }
            else if (!Config.bPowerup10Locked)
            {
                bPowerup10.Text = "Select";
                bPowerup10.Color = Color.White;
            }

            if (Config.bPowerup20Active && !Config.bPowerup20Locked)
            {
                bPowerup20.Text = "Active";
                bPowerup20.Color = buttonSelectedColor;
            }
            else if (!Config.bPowerup20Locked)
            {
                bPowerup20.Text = "Select";
                bPowerup20.Color = Color.White;
            }

            //if (Config.bColorBlueActive && !Config.bColorBlueLocked)
            //{
            //    bColorBlue.Text = "Color Blue \n Active";
            //    bColorBlue.Color = buttonSelectedColor;
            //}
            //else if (!Config.bColorBlueLocked)
            //{
            //    bColorBlue.Text = "Color Blue";
            //    bColorBlue.Color = Color.White;
            //}

            //if (Config.bColorPurpleActive && !Config.bColorPurpleLocked)
            //{
            //    bColorPurple.Text = "Color Purple \n Active";
            //    bColorPurple.Color = buttonSelectedColor;
            //}
            //else if (!Config.bColorPurpleLocked)
            //{
            //    bColorPurple.Text = "Color Purple";
            //    bColorPurple.Color = Color.White;
            //}

            #endregion

            base.Update(gametime);
        }

        public override void Draw(DrawContext context)
        {

            base.Draw(context);
        }

        #region Button Tapped Event

        void bHealth5_Tapped(object sender, EventArgs e)
        {
            if (Config.bHealth5Locked)
            {
                if (Config.Coins >= 1000)
                {
                    Config.bHealth5Locked = false;
                    Config.Coins -= 1000;
                }
            }
            else if (!Config.bHealth5Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ShipHealth += 5;
                bHealth5.Color = buttonSelectedColor;
                Config.activeUpgrades += 1;
                Config.bHealth5Active = true;
            }
            else if (Config.bHealth5Active)
            {
                Config.ShipHealth -= 5;
                bHealth5.Color = Color.White;
                Config.activeUpgrades -= 1;
                Config.bHealth5Active = false;
            }
        }

        void bHealth10_Tapped(object sender, EventArgs e)
        {
            if (Config.bHealth10Locked)
            {
                if (Config.Coins >= 3000)
                {
                    Config.bHealth10Locked = false;
                    Config.Coins -= 3000;
                }
            }
            else if (!Config.bHealth10Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ShipHealth += 10;
                bHealth10.Color = buttonSelectedColor;
                Config.bHealth10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bHealth10Active)
            {
                Config.ShipHealth -= 10;
                bHealth10.Color = Color.White;
                Config.bHealth10Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bHealth15_Tapped(object sender, EventArgs e)
        {
            if (Config.bHealth15Locked)
            {
                if (Config.Coins >= 7000)
                {
                    Config.bHealth15Locked = false;
                    Config.Coins -= 7000;
                }
            }
            else if (!Config.bHealth15Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ShipHealth += 15;
                bHealth15.Color = buttonSelectedColor;
                Config.bHealth15Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bHealth15Active)
            {
                Config.ShipHealth -= 15;
                bHealth15.Color = Color.White;
                Config.bHealth15Active = false;
                Config.activeUpgrades -= 1;
            }
        }


        void bShields10_Tapped(object sender, EventArgs e)
        {
            if (Config.bShields10Locked)
            {
                if (Config.Coins >= 1000)
                {
                    Config.bShields10Locked = false;
                    Config.Coins -= 1000;
                }
            }
            else if (!Config.bShields10Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ShieldHealth += 10;
                bShields10.Color = buttonSelectedColor;
                Config.bShields10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bShields10Active)
            {
                Config.ShieldHealth -= 10;
                bShields10.Color = Color.White;
                Config.bShields10Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bShields20_Tapped(object sender, EventArgs e)
        {
            if (Config.bShields20Locked)
            {
                if (Config.Coins >= 3000)
                {
                    Config.bShields20Locked = false;
                    Config.Coins -= 3000;
                }
            }
            else if (!Config.bShields20Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ShieldHealth += 20;
                bShields20.Color = buttonSelectedColor;
                Config.bShields20Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bShields20Active)
            {
                Config.ShieldHealth -= 20;
                bShields20.Color = Color.White;
                Config.bShields20Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bShields30_Tapped(object sender, EventArgs e)
        {
            if (Config.bShields30Locked)
            {
                if (Config.Coins >= 7000)
                {
                    Config.bShields30Locked = false;
                    Config.Coins -= 7000;
                }
            }
            else if (!Config.bShields30Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ShieldHealth += 30;
                bShields30.Color = buttonSelectedColor;
                Config.bShields30Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bShields30Active)
            {
                Config.ShieldHealth -= 30;
                bShields30.Color = Color.White;
                Config.bShields30Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bDamage1_Tapped(object sender, EventArgs e)
        {
            if (Config.bDamage1Locked)
            {
                if (Config.Coins >= 1000)
                {
                    Config.bDamage1Locked = false;
                    Config.Coins -= 1000;
                }
            }
            else if (!Config.bDamage1Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.PlayerBulletDamage += 1;
                Config.MissileDamage += 1;
                bDamage1.Color = buttonSelectedColor;
                Config.bDamage1Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bDamage1Active)
            {
                Config.PlayerBulletDamage -= 1;
                Config.MissileDamage -= 1;
                bDamage1.Color = Color.White;
                Config.bDamage1Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bDamage2_Tapped(object sender, EventArgs e)
        {
            if (Config.bDamage2Locked)
            {
                if (Config.Coins >= 3000)
                {
                    Config.bDamage2Locked = false;
                    Config.Coins -= 3000;
                }
            }
            else if (!Config.bDamage2Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.PlayerBulletDamage += 2;
                Config.MissileDamage += 2;
                bDamage2.Color = buttonSelectedColor;
                Config.bDamage2Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bDamage2Active)
            {
                Config.PlayerBulletDamage -= 2;
                Config.MissileDamage -= 2;
                bDamage2.Color = Color.White;
                Config.bDamage2Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bDamage4_Tapped(object sender, EventArgs e)
        {
            if (Config.bDamage4Locked)
            {
                if (Config.Coins >= 7000)
                {
                    Config.bDamage4Locked = false;
                    Config.Coins -= 7000;
                }
            }
            else if (!Config.bDamage4Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.PlayerBulletDamage += 4;
                Config.MissileDamage += 4;
                bDamage4.Color = buttonSelectedColor;
                Config.bDamage4Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bDamage4Active)
            {
                Config.PlayerBulletDamage -= 4;
                Config.MissileDamage -= 4;
                bDamage4.Color = Color.White;
                Config.bDamage4Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bRange10_Tapped(object sender, EventArgs e)
        {
            if (Config.bRange10Locked)
            {
                if (Config.Coins >= 1000)
                {
                    Config.bRange10Locked = false;
                    Config.Coins -= 1000;
                }
            }
            else if (!Config.bRange10Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ProjectileRange += 10;
                bRange10.Color = buttonSelectedColor;
                Config.bRange10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bRange10Active)
            {
                Config.ProjectileRange -= 10;
                bRange10.Color = Color.White;
                Config.bRange10Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bRange20_Tapped(object sender, EventArgs e)
        {
            if (Config.bRange20Locked)
            {
                if (Config.Coins >= 3000)
                {
                    Config.bRange20Locked = false;
                    Config.Coins -= 3000;
                }
            }
            else if (!Config.bRange20Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ProjectileRange += 20;
                bRange20.Color = buttonSelectedColor;
                Config.bRange20Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bRange20Active)
            {
                Config.ProjectileRange -= 20;
                bRange20.Color = Color.White;
                Config.bRange20Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bRange50_Tapped(object sender, EventArgs e)
        {
            if (Config.bRange50Locked)
            {
                if (Config.Coins >= 7000)
                {
                    Config.bRange50Locked = false;
                    Config.Coins -= 7000;
                }
            }
            else if (!Config.bRange50Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.ProjectileRange += 50;
                bRange50.Color = buttonSelectedColor;
                Config.bRange50Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bRange50Active)
            {
                Config.ProjectileRange -= 50;
                bRange50.Color = Color.White;
                Config.bRange50Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bPowerup5_Tapped(object sender, EventArgs e)
        {
            if (Config.bPowerup5Locked)
            {
                if (Config.Coins >= 1000)
                {
                    Config.bPowerup5Locked = false;
                    Config.Coins -= 1000;
                }
            }
            else if (!Config.bPowerup5Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.PowerupDropChance += 5;
                bPowerup5.Color = buttonSelectedColor;
                Config.bPowerup5Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bPowerup5Active)
            {
                Config.PowerupDropChance -= 5;
                bPowerup5.Color = Color.White;
                Config.bPowerup5Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bPowerup10_Tapped(object sender, EventArgs e)
        {
            if (Config.bPowerup10Locked)
            {
                if (Config.Coins >= 3000)
                {
                    Config.bPowerup10Locked = false;
                    Config.Coins -= 3000;
                }
            }
            else if (!Config.bPowerup10Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.PowerupDropChance += 10;
                bPowerup10.Color = buttonSelectedColor;
                Config.bPowerup10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bPowerup10Active)
            {
                Config.PowerupDropChance -= 10;
                bPowerup10.Color = Color.White;
                Config.bPowerup10Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        void bPowerup20_Tapped(object sender, EventArgs e)
        {
            if (Config.bPowerup20Locked)
            {
                if (Config.Coins >= 7000)
                {
                    Config.bPowerup20Locked = false;
                    Config.Coins -= 7000;
                }
            }
            else if (!Config.bPowerup20Active && Config.activeUpgrades < Config.maxActiveUpgrades)
            {
                Config.PowerupDropChance += 20;
                bPowerup20.Color = buttonSelectedColor;
                Config.bPowerup20Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bPowerup20Active)
            {
                Config.PowerupDropChance -= 20;
                bPowerup20.Color = Color.White;
                Config.bPowerup20Active = false;
                Config.activeUpgrades -= 1;
            }
        }

        //void bColorBlue_Tapped(object sender, EventArgs e)
        //{
        //    if (Config.bColorBlueLocked)
        //    {
        //        if (Config.Coins >= 1000)
        //        {
        //            Config.bColorBlueLocked = false;
        //            Config.Coins -= 1000;
        //        }
        //    }
        //    else if (!Config.bColorBlueActive)
        //    {
        //        bColorBlue.Color = buttonSelectedColor;
        //        Config.bColorBlueActive = true;
        //        Config.activeUpgrades += 1;
        //    }
        //    else if (Config.bColorBlueActive)
        //    {
        //        bColorBlue.Color = Color.White;
        //        Config.bColorBlueActive = false;
        //        Config.activeUpgrades -= 1;
        //    }
        //}

        //void bColorPurple_Tapped(object sender, EventArgs e)
        //{
        //    if (Config.bColorPurpleLocked)
        //    {
        //        if (Config.Coins >= 1000)
        //        {
        //            Config.bColorPurpleLocked = false;
        //            Config.Coins -= 1000;
        //        }
        //    }
        //    else if (!Config.bColorPurpleActive)
        //    {
        //        bColorPurple.Color = buttonSelectedColor;
        //        Config.bColorPurpleActive = true;
        //        Config.activeUpgrades += 1;
        //    }
        //    else if (Config.bColorPurpleActive)
        //    {
        //        bColorPurple.Color = Color.White;
        //        Config.bColorPurpleActive = false;
        //        Config.activeUpgrades -= 1;
        //    }
        //}

        #endregion


    }
}
