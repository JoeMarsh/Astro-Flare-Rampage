using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using AstroFlare;
using System.IO;
using SkillerSDK.Operations;
using SkillerSDK.Listeners.Responses;
using SkillerSDK.Listeners;

namespace Astro_Flare_XNASilverlight
{
    public partial class ShipUpgrades : PhoneApplicationPage
    {
        public ShipUpgrades()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SKApplication.Instance.GameManager.GetUserGameItems(new SKListener<SKAppItemsResponse>(OnItemsReceivedSuccess, OnItemsReceivedFailure));

            UpdateShipButtons();
            UpdateCoinText();

            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();

            buttonPrepared.Content = "Prepared: " + Config.boostPrepared;
            buttonShieldsUp.Content = "Shields Up: " + Config.boostShieldsUp;
            buttonLastStand.Content = "Shields Up: " + Config.boostLastStand;
        }

        private void Ship1Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Config.ship1Active)
            {
                Config.ship1Active = true;
                Config.ship2Active = false;
                Config.ship3Active = false;
                Config.CurrentProjectile = Config.BulletSheetGreenLaser;
                Config.CurrentShipTop = Config.Ship1SpriteSheet;
                Config.CurrentShipBase = Config.Ship1SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship1Color;
            }
            UpdateShipButtons();
            UpdateCoinText();
        }

        private void Ship2Button_Click(object sender, RoutedEventArgs e)
        {
            if (Config.ship2Locked)
            {
                if (Config.Coins >= 10000)
                {
                    Config.ship2Locked = false;
                    Config.Coins -= 10000;
                }

            }
            else if (!Config.ship2Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = true;
                Config.ship3Active = false;
                Config.CurrentProjectile = Config.BulletSheetShip2Laser;
                Config.CurrentShipTop = Config.Ship2SpriteSheet;
                Config.CurrentShipBase = Config.Ship2SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship2Color;
            }
            UpdateShipButtons();
            UpdateCoinText();
        }

        private void Ship3Button_Click(object sender, RoutedEventArgs e)
        {
            if (Config.ship3Locked)
            {
                if (Config.Coins >= 10000)
                {
                    Config.ship3Locked = false;
                    Config.Coins -= 10000;
                }
            }
            else if (!Config.ship3Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = false;
                Config.ship3Active = true;
                Config.CurrentProjectile = Config.BulletSheetShip3Laser;
                Config.CurrentShipTop = Config.Ship3SpriteSheet;
                Config.CurrentShipBase = Config.Ship3SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship3Color;
            }
            UpdateShipButtons();
            UpdateCoinText();
        }

        private void UpdateShipButtons()
        {
            if (Config.ship1Active)
                Ship1Button.Content = "Ship 1 - Active";
            else
                Ship1Button.Content = "Ship 1 - Select";

            if (Config.ship2Locked)
                Ship2Button.Content = "Ship 2 - Unlock 10k";
            else if (Config.ship2Active)
                Ship2Button.Content = "Ship 2 - Active";
            else
                Ship2Button.Content = "Ship 2 - Select";

            if (Config.ship3Locked)
                Ship3Button.Content = "Ship 3 - Unlock 10k";
            else if (Config.ship3Active)
                Ship3Button.Content = "Ship 3 - Active";
            else
                Ship3Button.Content = "Ship 3 - Select";
        }

        private void UpdateCoinText()
        {
            CoinText.Text = "Coins: " + Config.Coins;
            CoinText2.Text = "Coins: " + Config.Coins;
            CoinText3.Text = "Coins: " + Config.Coins;
        }

        private void UpdateUpgradeButtons()
        {
             //Upgrades
            if (Config.bHealth5Active && !Config.bHealth5Locked)
            {
                bHealth5.Content = "Active\nReinforced Hull: Health + 5";
            }
            else if (!Config.bHealth5Locked)
            {
                bHealth5.Content = "Select\nReinforced Hull: Health + 5";
            }

            if (Config.bHealth10Active && !Config.bHealth10Locked)
            {
                bHealth10.Content = "Active\nReinforced Hull: Health + 10";
            }
            else if (!Config.bHealth10Locked)
            {
                bHealth10.Content = "Select\nReinforced Hull: Health + 10";
            }

            if (Config.bHealth15Active && !Config.bHealth15Locked)
            {
                bHealth15.Content = "Active\nReinforced Hull: Health + 15";
            }
            else if (!Config.bHealth15Locked)
            {
                bHealth15.Content = "Select\nReinforced Hull: Health + 15";
            }

            if (Config.bShields10Active && !Config.bShields10Locked)
            {
                bShields10.Content = "Active\nShield Booster: Shields + 10";
            }
            else if (!Config.bShields10Locked)
            {
                bShields10.Content = "Select\nShield Booster: Shields + 10";
            }

            if (Config.bShields20Active && !Config.bShields20Locked)
            {
                bShields20.Content = "Active\nShield Booster: Shields + 20";
            }
            else if (!Config.bShields20Locked)
            {
                bShields20.Content = "Select\nShield Booster: Shields + 20";
            }

            if (Config.bShields30Active && !Config.bShields30Locked)
            {
                bShields30.Content = "Active\nShield Booster: Shields + 30";
            }
            else if (!Config.bShields30Locked)
            {
                bShields30.Content = "Select\nShield Booster: Shields + 30";
            }

            if (Config.bDamage1Active && !Config.bDamage1Locked)
            {
                bDamage1.Content = "Active\nWeapon Refit: Damage + 10%";
            }
            else if (!Config.bDamage1Locked)
            {
                bDamage1.Content = "Select\nWeapon Refit: Damage + 10%";
            }

            if (Config.bDamage2Active && !Config.bDamage2Locked)
            {
                bDamage2.Content = "Active\nWeapon Refit: Damage + 20%";
            }
            else if (!Config.bDamage2Locked)
            {
                bDamage2.Content = "Select\nWeapon Refit: Damage + 20%";
            }

            if (Config.bDamage4Active && !Config.bDamage4Locked)
            {
                bDamage4.Content = "Active\nWeapon Refit: Damage + 40%";
            }
            else if (!Config.bDamage4Locked)
            {
                bDamage4.Content = "Select\nWeapon Refit: Damage + 40%";
            }

            if (Config.bRange10Active && !Config.bRange10Locked)
            {
                bRange10.Content = "Active\nExtender: Weapon Range + 10";
            }
            else if (!Config.bRange10Locked)
            {
                bRange10.Content = "Select\nExtender: Weapon Range + 10";
            }

            if (Config.bRange20Active && !Config.bRange20Locked)
            {
                bRange20.Content = "Active\nExtender: Weapon Range + 20";
            }
            else if (!Config.bRange20Locked)
            {
                bRange20.Content = "Select\nExtender: Weapon Range + 20";
            }

            if (Config.bRange50Active && !Config.bRange50Locked)
            {
                bRange50.Content = "Active\nExtender: Weapon Range + 50";
            }
            else if (!Config.bRange50Locked)
            {
                bRange50.Content = "Select\nExtender: Weapon Range + 50";
            }

            if (Config.bPowerup5Active && !Config.bPowerup5Locked)
            {
                bPowerup5.Content = "Active\nExtractor: Powerup Drop +5%";
            }
            else if (!Config.bPowerup5Locked)
            {
                bPowerup5.Content = "Select\nExtractor: Powerup Drop +5%";
            }

            if (Config.bPowerup10Active && !Config.bPowerup10Locked)
            {
                bPowerup10.Content = "Active\nExtractor: Powerup Drop +10%";
            }
            else if (!Config.bPowerup10Locked)
            {
                bPowerup10.Content = "Select\nExtractor: Powerup Drop +10%";
            }

            if (Config.bPowerup20Active && !Config.bPowerup20Locked)
            {
                bPowerup20.Content = "Active\nExtractor: Powerup Drop +20%";
            }
            else if (!Config.bPowerup20Locked)
            {
                bPowerup20.Content = "Select\nExtractor: Powerup Drop +20%";
            }
        }

        private void UpdateEquipedUpgradeNumber()
        {
          UpgradesEquiped.Text = "Equipped: " + Config.activeUpgrades + "/5";          
        }

        private void bHealth5_Click(object sender, RoutedEventArgs e)
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
                Config.activeUpgrades += 1;
                Config.bHealth5Active = true;
            }
            else if (Config.bHealth5Active)
            {
                Config.ShipHealth -= 5;
                Config.activeUpgrades -= 1;
                Config.bHealth5Active = false;
            }

            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bHealth10_Click(object sender, RoutedEventArgs e)
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
                Config.bHealth10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bHealth10Active)
            {
                Config.ShipHealth -= 10;
                Config.bHealth10Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bHealth15_Click(object sender, RoutedEventArgs e)
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
                Config.bHealth15Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bHealth15Active)
            {
                Config.ShipHealth -= 15;
                Config.bHealth15Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bShields10_Click(object sender, RoutedEventArgs e)
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
                Config.bShields10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bShields10Active)
            {
                Config.ShieldHealth -= 10;
                Config.bShields10Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bShields20_Click(object sender, RoutedEventArgs e)
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
                Config.bShields20Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bShields20Active)
            {
                Config.ShieldHealth -= 20;
                Config.bShields20Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bShields30_Click(object sender, RoutedEventArgs e)
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
                Config.bShields30Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bShields30Active)
            {
                Config.ShieldHealth -= 30;
                Config.bShields30Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bDamage1_Click(object sender, RoutedEventArgs e)
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
                Config.bDamage1Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bDamage1Active)
            {
                Config.PlayerBulletDamage -= 1;
                Config.MissileDamage -= 1;
                Config.bDamage1Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bDamage2_Click(object sender, RoutedEventArgs e)
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
                Config.bDamage2Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bDamage2Active)
            {
                Config.PlayerBulletDamage -= 2;
                Config.MissileDamage -= 2;
                Config.bDamage2Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bDamage4_Click(object sender, RoutedEventArgs e)
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
                Config.bDamage4Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bDamage4Active)
            {
                Config.PlayerBulletDamage -= 4;
                Config.MissileDamage -= 4;
                Config.bDamage4Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bRange10_Click(object sender, RoutedEventArgs e)
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
                Config.bRange10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bRange10Active)
            {
                Config.ProjectileRange -= 10;
                Config.bRange10Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bRange20_Click(object sender, RoutedEventArgs e)
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
                Config.bRange20Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bRange20Active)
            {
                Config.ProjectileRange -= 20;
                Config.bRange20Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bRange50_Click(object sender, RoutedEventArgs e)
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
                Config.bRange50Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bRange50Active)
            {
                Config.ProjectileRange -= 50;
                Config.bRange50Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bPowerup5_Click(object sender, RoutedEventArgs e)
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
                Config.bPowerup5Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bPowerup5Active)
            {
                Config.PowerupDropChance -= 5;
                Config.bPowerup5Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bPowerup10_Click(object sender, RoutedEventArgs e)
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
                Config.bPowerup10Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bPowerup10Active)
            {
                Config.PowerupDropChance -= 10;
                Config.bPowerup10Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void bPowerup20_Click(object sender, RoutedEventArgs e)
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
                Config.bPowerup20Active = true;
                Config.activeUpgrades += 1;
            }
            else if (Config.bPowerup20Active)
            {
                Config.PowerupDropChance -= 20;
                Config.bPowerup20Active = false;
                Config.activeUpgrades -= 1;
            }
            UpdateUpgradeButtons();
            UpdateEquipedUpgradeNumber();
            UpdateCoinText();
        }

        private void buttonPrepared_Click(object sender, RoutedEventArgs e)
        {
            if (Config.Coins > 500)
            {
                Config.Coins -= 500;
                Config.boostPrepared++;
                UpdateCoinText();
                buttonPrepared.Content = "Prepared: " + Config.boostPrepared;
            }
        }

        private void buttonShieldsUp_Click(object sender, RoutedEventArgs e)
        {
            if (Config.Coins > 500)
            {
                Config.Coins -= 500;
                Config.boostShieldsUp++;
                UpdateCoinText();
                buttonShieldsUp.Content = "Shields Up: " + Config.boostShieldsUp;
            }
        }

        private void buttonLastStand_Click(object sender, RoutedEventArgs e)
        {
            if (Config.Coins > 500)
            {
                Config.Coins -= 500;
                Config.boostLastStand++;
                UpdateCoinText();
                buttonLastStand.Content = "Last Stand: " + Config.boostLastStand;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SKApplication.Instance.UIManager.ShowScreen(this, SKUIManager.eScreenType.GAME_ITEM_STORE);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SKApplication.Instance.UIManager.ShowScreen(this, SKUIManager.eScreenType.COINS_STORE);
        }

        private void OnItemsReceivedSuccess(SKAppItemsResponse response)
        {
            foreach (SkillerSDK.Listeners.Items.SKGameItem items in response.GameItems)
            {
                if (items.Id == "100175")
                {
                    for (int i = 0; i < items.Amount; i++)
                    {
                        //AstroFlare.Config.Coins += 5000;
                        SKApplication.Instance.GameManager.UseGameItem("100175", 1, new SKListener<SKStatusResponse>(OnItemUsedSuccess, OnItemUsedFailure));
                    }
                }
            }
        }

        private void OnItemUsedSuccess(SKStatusResponse response)
        {
            AstroFlare.Config.Coins += 5000;
        }

        private void OnItemUsedFailure(SKStatusResponse response)
        {
            //your code
        }

        private void OnItemsReceivedFailure(SKStatusResponse response)
        {
            //your code
        }
    }
}