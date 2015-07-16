using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;

namespace AstroFlare
{   //TODO: reorganise with catagory titles
    public enum LevelSelect
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Practise = 20,
    }

    static class Config
    {
        public static void Initialize() 
        {
       
        }

        public static bool Vibrate = true;
        public static bool MusicOn = true;
        public static bool SoundFXOn = true;
        public static bool ThumbsticksOn = true;
        public static int ControlOption = 0;

        public static LevelSelect Level = LevelSelect.One;
        public static LevelSelect tempLevel = LevelSelect.One;

        public static bool AIControlled = true;

        //ShipUpgradesMenuScreen
        public static bool ship2Locked = true;
        public static bool ship3Locked = true;
        public static bool ship1Active = true;
        public static bool ship2Active = false;
        public static bool ship3Active = false;

        //UpgradesMenuScreen
        public static int activeUpgrades = 0;
        public static int maxActiveUpgrades = 5;
        public static bool bHealth5Locked = true;
        public static bool bHealth10Locked = true;
        public static bool bHealth15Locked = true;
        public static bool bShields10Locked = true;
        public static bool bShields20Locked = true;
        public static bool bShields30Locked = true;
        public static bool bDamage1Locked = true;
        public static bool bDamage2Locked = true;
        public static bool bDamage4Locked = true;
        public static bool bRange10Locked = true;
        public static bool bRange20Locked = true;
        public static bool bRange50Locked = true;
        public static bool bPowerup5Locked = true;
        public static bool bPowerup10Locked = true;
        public static bool bPowerup20Locked = true;
        public static bool bColorBlueLocked = true;
        public static bool bColorPurpleLocked = true;

        public static bool bHealth5Active = false;
        public static bool bHealth10Active = false;
        public static bool bHealth15Active = false;
        public static bool bShields10Active = false;
        public static bool bShields20Active = false;
        public static bool bShields30Active = false;
        public static bool bDamage1Active = false;
        public static bool bDamage2Active = false;
        public static bool bDamage4Active = false;
        public static bool bRange10Active = false;
        public static bool bRange20Active = false;
        public static bool bRange50Active = false;
        public static bool bPowerup5Active = false;
        public static bool bPowerup10Active = false;
        public static bool bPowerup20Active = false;
        public static bool bColorBlueActive = false;
        public static bool bColorPurpleActive = false;

        public static int ProjectileRange = 500;
        public static int PowerupDropChance = 0;

        public static Random Rand = new Random();

        public static double Score;
        public static double Multi;
        public static int CoinsCollected = 0;
        public static double Coins = 0;

        public static double AIScore;
        public static double AIMulti;

        public static int shieldDropChance = 0;
        public static int projectileDropChance = 0;
        public static int missileDropChance = 0;
        public static int firerateDropChance = 0;
        public static int laserDropChance = 0;
        public static int timeDropChance = 0;

        public static int WorldBoundsX = 1600;
        public static int WorldBoundsY = 960;
        public static Rectangle WorldRect = new Rectangle(0, 0, WorldBoundsX, WorldBoundsY);

        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        public static float EmemiesKilled = 0;

        public static float KillStreak = 0;
        public static float KillStreakBuildpoints = 0;

        public static double DamageEffectTimeout = 0.2;
        //public static SpriteSheet SmallExplosionSpriteSheet = new SpriteSheet("explosion_small", 3, 5, 30, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet ProjectileExplosion = new SpriteSheet("projectile_explosion", 4, 4, 60, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet PlayerShield = new SpriteSheet("player_shield", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet Heal = new SpriteSheet("heal", 6, 1, 12, SpriteSheetMode.NoDamageTexture);

        //public static SpriteSheet AsteroidSpriteSheet = new SpriteSheet("asteroid", 5, 6, 15);
        //public static int AsteroidHealth = 50;
        public static SpriteSheet MultiHud = new SpriteSheet(@"GameScreens\MultiHud", 10, 1, 0, Color.White, SpriteSheetMode.NoDamageTexture);

        public static SpriteSheet ShipSpriteSheetBase = new SpriteSheet(@"Ships\player3_base", 1, 1, 1, ShipColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet ShipSpriteSheet = new SpriteSheet(@"Ships\player3_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        //public static SpriteSheet ShipSpriteSheet = new SpriteSheet("ship1", 4, 3, 12);
        public static float ShipSpeed = 0.5f;
        public static int ShipHealth = 100;
        public static Color ShipColor = new Color(63, 252, 0);
        public static Vector2 ShipSpawnOffset = new Vector2(0, -20);
        public static int PlayerLives = 1;
        public static int PlayerSpecialAbilityUses = 1;

        //public static SpriteSheet PlayerBulletSheet = new SpriteSheet("laser_green", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet PlayerBulletSheet = new SpriteSheet("ship_projectile2", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BulletSheetGreenLaser = new SpriteSheet("laser_green", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BulletSheetShip2Laser = new SpriteSheet("laser_ship2", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BulletSheetShip3Laser = new SpriteSheet("laser_ship3", 1, 1, 1, SpriteSheetMode.NoDamageTexture);

        //public static SpriteSheet BulletSheetBlueLaserDouble = new SpriteSheet("laser_blue_double", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet BulletSheetEnergyPurple = new SpriteSheet("energy_purple", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet BulletSheetMelter = new SpriteSheet("double_flame", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet BulletSheetWave = new SpriteSheet("energy_wave", 1, 1, 1, SpriteSheetMode.NoDamageTexture);

        public static SpriteSheet BulletSheetLaser = new SpriteSheet("phaser", 1, 1, 1, SpriteSheetMode.NoDamageTexture);

        //public static int PlayerBulletDamage = 4;
        public static int PlayerBulletDamage = 10;
        public static float PlayerBulletSpeed = 20f;
        public static float PlayerFireInterval = 0.15f;

        public static int WaveFireTotal = 4;
        public static double WaveFireInterval = 0.05;
        public static float WaveFireSpread = 0.28f;

        public static SpriteSheet EnemyInterceptorSpriteSheetBase = new SpriteSheet(@"Ships\enemy_interceptor_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet EnemyInterceptorSpriteSheet = new SpriteSheet(@"Ships\enemy_interceptor_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet EnemyChaserSpriteSheetBase = new SpriteSheet(@"Ships\enemy_claw_bottom", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet EnemyChaserSpriteSheet = new SpriteSheet(@"Ships\enemy_claw_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet EnemySpikeSpriteSheetBase = new SpriteSheet(@"Ships\graypinkship_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet EnemySpikeSpriteSheet = new SpriteSheet(@"Ships\graypinkship_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet EnemyDasherSpriteSheetBase = new SpriteSheet(@"Ships\blueship2_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet EnemyDasherSpriteSheet = new SpriteSheet(@"Ships\blueship2_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);
        public static int EnemyDasherSpeed = 8;

        public static SpriteSheet EnemyAvoiderSpriteSheetBase = new SpriteSheet(@"Ships\enemy_avoider_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet EnemyAvoiderSpriteSheet = new SpriteSheet(@"Ships\enemy_avoider_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet EnemyShooterSpriteSheetBase = new SpriteSheet(@"Ships\enemy_shooter_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet EnemyShooterSpriteSheet = new SpriteSheet(@"Ships\enemy_shooter_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);
        public static int EnemyShooterFireInterval = 4;
        public static int EnemyShooterHealth = 100;

        public static SpriteSheet Bug1SpriteSheetBase = new SpriteSheet(@"Ships\bugship1_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Bug1SpriteSheet = new SpriteSheet(@"Ships\bugship1_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet Bug2SpriteSheetBase = new SpriteSheet(@"Ships\bugship2_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Bug2SpriteSheet = new SpriteSheet(@"Ships\bugship2_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet Bug3SpriteSheetBase = new SpriteSheet(@"Ships\bugship3_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Bug3SpriteSheet = new SpriteSheet(@"Ships\bugship3_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet Bug4SpriteSheetBase = new SpriteSheet(@"Ships\bugship4_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Bug4SpriteSheet = new SpriteSheet(@"Ships\bugship4_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet BugDagger1SpriteSheetBase = new SpriteSheet(@"Ships\bugdagger1_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BugDagger1SpriteSheet = new SpriteSheet(@"Ships\bugdagger1_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet BugDagger2SpriteSheetBase = new SpriteSheet(@"Ships\bugdagger2_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BugDagger2SpriteSheet = new SpriteSheet(@"Ships\bugdagger2_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static SpriteSheet BugDagger3SpriteSheetBase = new SpriteSheet(@"Ships\bugdagger3_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BugDagger3SpriteSheet = new SpriteSheet(@"Ships\bugdagger3_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);

        public static Color DamageColor = new Color(255, 0, 0);
        //public static Color EnemyColor = new Color(0, 138, 255);
        public static Color EnemyColor = new Color(59, 59, 231);
        public static int EnemyHealth = 20;
        public static int EnemySpeed = 6;
        public static double EnemyFireInterval = 8;
        public static double EnemyBurstFireInterval = 0.15;
        public static int EnemyBurstFireTotal = 4;
        public static float EnemyAvoidanceRadius = 300f;
        public static float EnemyAvoidanceWeight = 0.4f;

        public static SpriteSheet EnemyBulletSheet = new SpriteSheet("energy_bomb", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static int EnemyBulletDamage = 5;
        public static float EnemyBulletSpeed = 6f;

        public static SpriteSheet EnemyPlayerBulletSheet = new SpriteSheet("laser_red", 1, 1, 1, SpriteSheetMode.NoDamageTexture);


        //Background
        public static int StarfieldResolutionX = WorldBoundsX / 3;
        public static int StarfieldResolutionY = WorldBoundsY / 3;
        public static float StarfieldDensity = 0.002f;
        public static float StarfieldSpeed = 2f;
        public static Color StarfieldColor = new Color(200, 200, 255);
        public static int StarfieldLayerCount = 3;

        //public static SpriteSheet MissileSpriteSheet = new SpriteSheet("missile1", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static int MissileDamage = 2;
        public static int MissileDamage = 10;
        public static float MissileSpeed = 10;
        //public static float MissileTurnIncrement = 0.07f;
        public static float MissileTurnIncrement = 0.1f;
        public static float MissileOnPlayerTurnIncrement = 0.3f;

        //Powerups
        public static SpriteSheet PowerupMissileSpriteSheet = new SpriteSheet(@"Powerup\powerup_missile", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet PowerupWeaponBurstWaveSpriteSheet = new SpriteSheet(@"Powerup\burstwave_powerup", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet PowerupHealthSpriteSheet = new SpriteSheet(@"Powerup\powerup_health", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static int HealthPowerupValue = 25;
        public static SpriteSheet PowerupShieldsSpriteSheet = new SpriteSheet(@"Powerup\powerup_shields", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static int ShieldsPowerupValue = 25;
        public static SpriteSheet PowerupProjectileSpeedSpriteSheet = new SpriteSheet(@"Powerup\powerup_speed", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet PowerupAddProjectileSpriteSheet = new SpriteSheet(@"Powerup\powerup_add_projectile", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet PowerupLightningSpriteSheet = new SpriteSheet(@"Powerup\powerup_green", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet PowerupSlowAllSpriteSheet = new SpriteSheet(@"Powerup\powerup_time", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet PowerupLaserDefenceSpriteSheet = new SpriteSheet(@"Powerup\powerup_laser", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        //public static SpriteSheet LightningSpriteSheet = new SpriteSheet(@"Powerup\lightning", 2, 2, 16, SpriteSheetMode.NoDamageTexture);

        //public static SpriteSheet SidearmSpriteSheet = new SpriteSheet("sidearm", 1, 1, 1);
        public static int SidearmHealth = 5;
        public static double SidearmFireInterval = 0.5;

        //Buddies
        //public static SpriteSheet BuddySpriteSheet = new SpriteSheet("buddy2", 4, 1, 15);
        public static int BuddySpeed = 3;

        //Bosses
        //boss1
        public static SpriteSheet BossSpriteSheetBase = new SpriteSheet(@"Ships\boss1_base", 1, 1, 1, EnemyColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet BossSpriteSheet = new SpriteSheet(@"Ships\boss1_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);
        public static Color BossColor = new Color(59, 59, 231);
        public static int BossHealth = 750;
        public static int BossSpeed = 2;
        public static double BossFireInterval = 8;
        public static double BossBurstFireInterval = 0.15;
        public static int BossBurstFireTotal = 4;
        public static float BossAvoidanceRadius = 300f;
        public static float BossAvoidanceWeight = 0.3f;

        public static SpriteSheet BossBulletSheet = new SpriteSheet("energy_bomb", 1, 1, 1, SpriteSheetMode.NoDamageTexture);
        public static int BossBulletDamage = 10;
        public static float BossBulletSpeed = 4f;

        public static SpriteSheet CoinSpriteSheet = new SpriteSheet("coin5", 4, 4, 10, SpriteSheetMode.NoDamageTexture);

        //
        public static SpriteSheet Ship1SpriteSheetBase = new SpriteSheet(@"Ships\player1_base", 1, 1, 1, ShipColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Ship1SpriteSheet = new SpriteSheet(@"Ships\player1_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);
        public static SpriteSheet ShipShield = new SpriteSheet("Shield", 1, 1, 1, new Color(0, 0, 0, 0), 0.7f, SpriteSheetMode.Normal);
        public static int ShieldHealth = 200;
        public static Color ShieldColor = new Color(0, 0, 0, 0);
        public static Color Ship1Color = Color.Green;
        public static SpriteSheet Ship2SpriteSheetBase = new SpriteSheet(@"Ships\player2_base", 1, 1, 1, ShipColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Ship2SpriteSheet = new SpriteSheet(@"Ships\player2_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);
        public static Color Ship2Color = Color.Purple;
        public static SpriteSheet Ship3SpriteSheetBase = new SpriteSheet(@"Ships\player3_base", 1, 1, 1, ShipColor, SpriteSheetMode.NoDamageTexture);
        public static SpriteSheet Ship3SpriteSheet = new SpriteSheet(@"Ships\player3_top", 1, 1, 1, Color.White, 0.6f, SpriteSheetMode.Normal);
        public static Color Ship3Color = new Color(173, 216, 230);
        
        public static SpriteSheet CurrentShipBase = Ship1SpriteSheetBase;
        public static SpriteSheet CurrentShipTop = Ship1SpriteSheet;
        //public static SpriteSheet CurrentProjectile = BulletSheetGreenLaser;
        public static SpriteSheet CurrentProjectile = BulletSheetGreenLaser;
        public static Color CurrentShipColor = Ship1Color;

        public static int level = 0;

        public static List<float> RampageScores = new List<float> { 0, 0, 0, 0, 0, 0 };
        public static List<float> RampageTimedScores = new List<float> { 0, 0, 0, 0, 0, 0 };
        public static List<float> AlterEgoScores = new List<float> { 0, 0, 0, 0, 0, 0 };
        public static List<float> AlterEgoTimedScores = new List<float> { 0, 0, 0, 0, 0, 0 };
        public static List<float> TimeBanditScores = new List<float> { 0, 0, 0, 0, 0, 0 };
        public static List<float> ExterminationScores = new List<float> { 0, 0, 0, 0, 0, 0 };

        public static void loadSavedData()
        {
            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_rampage_scores))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_rampage_scores,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                RampageScores.Insert(i, float.Parse(reader.ReadLine()));
                            }
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_rampagetimed_scores))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_rampagetimed_scores,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                RampageTimedScores.Insert(i, float.Parse(reader.ReadLine()));
                            }
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_alterego_scores))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_alterego_scores,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                AlterEgoScores.Insert(i, float.Parse(reader.ReadLine()));
                            }
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_alteregotimed_scores))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_alteregotimed_scores,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                AlterEgoTimedScores.Insert(i, float.Parse(reader.ReadLine()));
                            }
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_timebandit_scores))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_timebandit_scores,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                TimeBanditScores.Insert(i, float.Parse(reader.ReadLine()));
                            }
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_extermination_scores))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_extermination_scores,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                ExterminationScores.Insert(i, float.Parse(reader.ReadLine()));
                            }
                        }
                    });
            }

            RampageScores.Sort();
            RampageScores.Reverse();
            RampageTimedScores.Sort();
            RampageTimedScores.Reverse();
            AlterEgoScores.Sort();
            AlterEgoScores.Reverse();
            AlterEgoTimedScores.Sort();
            AlterEgoTimedScores.Reverse();
            TimeBanditScores.Sort();
            TimeBanditScores.Reverse();
            ExterminationScores.Sort();
            ExterminationScores.Reverse();

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_options))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_options,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            Vibrate = bool.Parse(reader.ReadLine());
                            MusicOn = bool.Parse(reader.ReadLine());
                            SoundFXOn = bool.Parse(reader.ReadLine());
                            ControlOption = int.Parse(reader.ReadLine());
                            ThumbsticksOn = bool.Parse(reader.ReadLine());
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_autofire))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_autofire,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            AIControlled = bool.Parse(reader.ReadLine());
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_level))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_level,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            level = int.Parse(reader.ReadLine());
                        }
                    });
            }

            Config.Level = (LevelSelect)level + 1;

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_ships))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_ships,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            ship2Locked = bool.Parse(reader.ReadLine());
                            ship3Locked = bool.Parse(reader.ReadLine());

                            ship1Active = bool.Parse(reader.ReadLine());
                            ship2Active = bool.Parse(reader.ReadLine());
                            ship3Active = bool.Parse(reader.ReadLine());
                        }
                    });
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_coins))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_coins,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            Config.Coins = double.Parse(reader.ReadLine());
                        }
                    });
            }

            if (Config.ship1Active)
            {
                Config.CurrentProjectile = Config.BulletSheetGreenLaser;
                Config.CurrentShipTop = Config.Ship1SpriteSheet;
                Config.CurrentShipBase = Config.Ship1SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship1Color;
            }

            if (Config.ship2Active)
            {
                Config.CurrentProjectile = Config.BulletSheetShip2Laser;
                Config.CurrentShipTop = Config.Ship2SpriteSheet;
                Config.CurrentShipBase = Config.Ship2SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship2Color;
            }

            if (Config.ship3Active)
            {
                Config.CurrentProjectile = Config.BulletSheetShip3Laser;
                Config.CurrentShipTop = Config.Ship3SpriteSheet;
                Config.CurrentShipBase = Config.Ship3SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship3Color;
            }

            if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_upgrades))
            {
                GlobalSave.SaveDevice.Load(
                    GlobalSave.containerName,
                    GlobalSave.fileName_upgrades,
                    stream =>
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            Config.activeUpgrades = int.Parse(reader.ReadLine());

                            Config.bHealth5Locked = bool.Parse(reader.ReadLine());
                            Config.bHealth10Locked = bool.Parse(reader.ReadLine());
                            Config.bHealth15Locked = bool.Parse(reader.ReadLine());
                            Config.bShields10Locked = bool.Parse(reader.ReadLine());
                            Config.bShields20Locked = bool.Parse(reader.ReadLine());
                            Config.bShields30Locked = bool.Parse(reader.ReadLine());
                            Config.bDamage1Locked = bool.Parse(reader.ReadLine());
                            Config.bDamage2Locked = bool.Parse(reader.ReadLine());
                            Config.bDamage4Locked = bool.Parse(reader.ReadLine());
                            Config.bRange10Locked = bool.Parse(reader.ReadLine());
                            Config.bRange20Locked = bool.Parse(reader.ReadLine());
                            Config.bRange50Locked = bool.Parse(reader.ReadLine());
                            Config.bPowerup5Locked = bool.Parse(reader.ReadLine());
                            Config.bPowerup10Locked = bool.Parse(reader.ReadLine());
                            Config.bPowerup20Locked = bool.Parse(reader.ReadLine());

                            Config.bHealth5Active = bool.Parse(reader.ReadLine());
                            Config.bHealth10Active = bool.Parse(reader.ReadLine());
                            Config.bHealth15Active = bool.Parse(reader.ReadLine());
                            Config.bShields10Active = bool.Parse(reader.ReadLine());
                            Config.bShields20Active = bool.Parse(reader.ReadLine());
                            Config.bShields30Active = bool.Parse(reader.ReadLine());
                            Config.bDamage1Active = bool.Parse(reader.ReadLine());
                            Config.bDamage2Active = bool.Parse(reader.ReadLine());
                            Config.bDamage4Active = bool.Parse(reader.ReadLine());
                            Config.bRange10Active = bool.Parse(reader.ReadLine());
                            Config.bRange20Active = bool.Parse(reader.ReadLine());
                            Config.bRange50Active = bool.Parse(reader.ReadLine());
                            Config.bPowerup5Active = bool.Parse(reader.ReadLine());
                            Config.bPowerup10Active = bool.Parse(reader.ReadLine());
                            Config.bPowerup20Active = bool.Parse(reader.ReadLine());
                        }
                    });
            }


            if (Config.bPowerup5Active)
                Config.PowerupDropChance += 5;
            if (Config.bPowerup10Active)
                Config.PowerupDropChance += 10;
            if (Config.bPowerup20Active)
                Config.PowerupDropChance += 20;

            if (Config.bHealth5Active)
                Config.ShipHealth += 5;
            if (Config.bHealth10Active)
                Config.ShipHealth += 10;
            if (Config.bHealth15Active)
                Config.ShipHealth += 15;

            if (Config.bShields10Active)
                Config.ShieldHealth += 10;
            if (Config.bShields20Active)
                Config.ShieldHealth += 20;
            if (Config.bShields30Active)
                Config.ShieldHealth += 30;

            if (Config.bRange10Active)
                Config.ProjectileRange += 10;
            if (Config.bRange20Active)
                Config.ProjectileRange += 20;
            if (Config.bRange50Active)
                Config.ProjectileRange += 50;

            if (Config.bDamage1Active)
            {
                Config.PlayerBulletDamage += 1;
                Config.MissileDamage += 1;
            }
            if (Config.bDamage2Active)
            {
                Config.PlayerBulletDamage += 2;
                Config.MissileDamage += 2;
            }
            if (Config.bDamage4Active)
            {
                Config.PlayerBulletDamage += 4;
                Config.MissileDamage += 4;
            }

        }
    }
}
