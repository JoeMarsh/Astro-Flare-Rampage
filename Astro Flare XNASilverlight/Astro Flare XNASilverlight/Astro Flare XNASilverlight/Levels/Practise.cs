using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class Practise
    {
        List<LevelItem_1> levelItems = new List<LevelItem_1>();
        double timeCursor = 0;
        int itemIndex = 0;
        int ViewCenter { get { return Config.WorldBoundsX / 2; } }
        //static TimeSpan levelTime;
        double levelTime = 0;
        //double levelSpeed = 0;

        Random rand = new Random();

        int side;
        Vector2 entryPosition;
        Vector2 direction;

        int difficulty = 1;

        public T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        public Practise()
        {
            generateLevel();
//            //for (int i = 0; i < 1 + difficulty; i++)
//            //{
//            //    //AddItem((ItemType_1)Enum.ToObject(typeof(ItemType_1), 8));
//            //    AddItem(NumToEnum<ItemType_1>(4));
//            //}
//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyInterceptor);
//            }
//            timeCursor += 5;

//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyInterceptor);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyInterceptor);
//            }
//            timeCursor += 5;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyInterceptor);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 10 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 5;

//            for (int i = 0; i < 2 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);
//            }
//            timeCursor += 6;

//            for (int i = 0; i < 2 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);
//            }
//            timeCursor += 6;

//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }
//            timeCursor += 10;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }
//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 5;

//            for (int i = 0; i < 15 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyInterceptor);
//            }

//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 5;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }
//            timeCursor += 5;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.boss1, 1600, 400, Config.BossSpeed, new Vector2(-1, 0));

//            timeCursor += 15;

//            AddItem(ItemType_1.EnemyDasher, 50, 300, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 600, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));

//            timeCursor += 5;

//            AddItem(ItemType_1.EnemyDasher, 50, 83, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 249, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 415, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 518, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 747, Config.EnemyDasherSpeed, new Vector2(1, 0));

//            AddItem(ItemType_1.EnemyDasher, 1550, 166, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 332, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 498, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 664, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 830, Config.EnemyDasherSpeed, new Vector2(-1, 0));

//            timeCursor += 5;

//            //increase difficulty

//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//                AddItem(ItemType_1.EnemyShooter);
//            }
//            timeCursor += 10;


//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 2 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);
//            }
//            timeCursor += 6;

//            for (int i = 0; i < 2 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);
//            }

//            AddItem(ItemType_1.EnemyDasher, 50, 83, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 249, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 415, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 518, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 747, Config.EnemyDasherSpeed, new Vector2(1, 0));

//            AddItem(ItemType_1.EnemyDasher, 1550, 166, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 332, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 498, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 664, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 830, Config.EnemyDasherSpeed, new Vector2(-1, 0));

//            timeCursor += 6;

//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }

//            timeCursor += 10;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }
//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 5;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }

//            timeCursor += 5;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }

//            timeCursor += 5;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.boss1, 1600, 400, Config.BossSpeed, new Vector2(-1, 0));

//            timeCursor += 15;

//            AddItem(ItemType_1.EnemyDasher, 50, 300, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 600, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));

//            timeCursor += 5;

//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//                AddItem(ItemType_1.EnemyShooter);

//            }
//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }

//            timeCursor += 10;

//            for (int i = 0; i < 2 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);

//            }
//            timeCursor += 6;

//            for (int i = 0; i < 2 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//                AddItem(ItemType_1.EnemyShooter);

//            }

//            AddItem(ItemType_1.EnemyDasher, 50, 300, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 600, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));

//            timeCursor += 6;

//            for (int i = 0; i < 4 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }
//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }

//            timeCursor += 10;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));

//            AddItem(ItemType_1.EnemyDasher, 50, 300, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 600, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));

//            timeCursor += 10;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }
//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//            }
//            timeCursor += 5;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            timeCursor += 10;

//            for (int i = 0; i < 6 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyAvoider);
//                AddItem(ItemType_1.EnemyShooter);
//            }

//            AddItem(ItemType_1.EnemyDasher, 50, 83, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 249, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 415, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 518, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 747, Config.EnemyDasherSpeed, new Vector2(1, 0));


//            AddItem(ItemType_1.EnemyDasher, 1550, 166, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 332, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 498, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 664, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 830, Config.EnemyDasherSpeed, new Vector2(-1, 0));

//            timeCursor += 5;

//            for (int i = 0; i < 20 + difficulty; i++)
//            {
//                AddItem(ItemType_1.EnemyChaser);
//            }

//            timeCursor += 5;

//            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.boss1, 1600, 400, Config.BossSpeed, new Vector2(-1, 0));

//            timeCursor += 15;

//            AddItem(ItemType_1.EnemyDasher, 50, 83, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 249, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 415, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 518, Config.EnemyDasherSpeed, new Vector2(1, 0));
//            AddItem(ItemType_1.EnemyDasher, 50, 747, Config.EnemyDasherSpeed, new Vector2(1, 0));


//            AddItem(ItemType_1.EnemyDasher, 1550, 166, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 332, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 498, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 664, Config.EnemyDasherSpeed, new Vector2(-1, 0));
//            AddItem(ItemType_1.EnemyDasher, 1550, 830, Config.EnemyDasherSpeed, new Vector2(-1, 0));

//            timeCursor += 5;

//            //AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            //AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            //AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            //AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
//            //AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));

//            //AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));
//            //AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));
//            //AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));
//            //AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));
//            //AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));

////////////////////////////////////////////
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);
//            //AddItem(ItemType_1.PowerupWeaponAutoBurst, 1000, 350, 0f);
//            //AddItem(ItemType_1.PowerupDamageAll, 500, 200, 0f);
//            //AddItem(ItemType_1.PowerupFreeze, 500, 500, 0f);

//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);
//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);
//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.PowerupDamageAll, 500, 200, 0f);
//            //AddItem(ItemType_1.PowerupFreeze, 500, 500, 0f);
//            //AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));

//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);

//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);
//            //AddItem(ItemType_1.PowerupWeaponAutoBurst, 1000, 350, 0f);
//            //AddItem(ItemType_1.PowerupDamageAll, 500, 200, 0f);
//            //AddItem(ItemType_1.PowerupFreeze, 500, 500, 0f);
//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);
//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}
//            //AddItem(ItemType_1.EnemyChaser);
//            //timeCursor += 5;
//            //for (int i = 0; i < 4; i++)
//            //{
//            //    AddItem();
//            //}




//            //AddItem(ItemType.EnemyInterceptor, ViewCenter, 2f);

//            //timeCursor += 5;
//            //AddItem(ItemType.PowerupHealth, ViewCenter - 100, 2f);
//            //AddItem(ItemType.PowerupMissiles, ViewCenter, 2f);
//            //AddItem(ItemType.Asteroid, ViewCenter + 100, 2f);

//            //timeCursor += 4;
//            //AddItem(ItemType.Asteroid, ViewCenter - 100, 2f);
//            //AddItem(ItemType.Asteroid, ViewCenter - 150, 3f);
//            //AddItem(ItemType.Asteroid, ViewCenter + 100, 2f);
//            //AddItem(ItemType.Asteroid, ViewCenter + 150, 3f);

//            //timeCursor += 4;
//            //AddItem(ItemType.Asteroid, 0, 3f, Vector2.Normalize(new Vector2(1, 1)));
//            //AddItem(ItemType.Asteroid, Config.WorldBoundsX, 3f, Vector2.Normalize(new Vector2(-1, 1)));

//            //Random random = new Random();
//            //for (int i = 0; i < 100000; i++)
//            //{
//            //    timeCursor += 1;
//            //    AddItem((ItemType)random.Next(4), random.Next(Config.ScreenWidth), 3f);
//            //}
//            levelTime = 0;
//            itemIndex = 0;
        }

        private void generateLevel()
        {
            levelItems.Clear();
            timeCursor = 0;

            for (int i = 0; i < 3 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
            }
            timeCursor += 5;

            for (int i = 0; i < 3 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
            }
            timeCursor += 10;

            for (int i = 0; i < 4 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
            }
            timeCursor += 5;

            for (int i = 0; i < 4 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
            }
            timeCursor += 10;

            for (int i = 0; i < 5 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyAvoider);
            }
            timeCursor += 5;

            for (int i = 0; i < 2 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
                AddItem(ItemType_1.EnemyShooter);
            }
            timeCursor += 6;

            for (int i = 0; i < 2 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyAvoider);
                AddItem(ItemType_1.EnemyShooter);
            }
            timeCursor += 6;

            for (int i = 0; i < 2 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
                AddItem(ItemType_1.EnemyShooter);
            }
            timeCursor += 10;

            for (int i = 0; i < 10 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyChaser);
            }
            timeCursor += 10;

            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
            timeCursor += 10;

            for (int i = 0; i < 10 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyChaser);
            }
            for (int i = 0; i < 5 + difficulty; i++)
            {
                AddItem(ItemType_1.EnemyAvoider);
            }
            timeCursor += 5;

            for (int i = 0; i < 10 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyInterceptor);
            }

            timeCursor += 10;

            for (int i = 0; i < 5 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyAvoider);
            }
            timeCursor += 5;

            for (int i = 0; i < 10 * difficulty; i++)
            {
                AddItem(ItemType_1.EnemyChaser);
            }
            timeCursor += 5;

            AddItem(ItemType_1.boss1, 0, 400, Config.BossSpeed, new Vector2(1, 0));
            AddItem(ItemType_1.boss1, 1600, 400, Config.BossSpeed, new Vector2(-1, 0));

            timeCursor += 15;

            AddItem(ItemType_1.EnemyDasher, 50, 300, Config.EnemyDasherSpeed, new Vector2(1, 0));
            AddItem(ItemType_1.EnemyDasher, 1550, 600, Config.EnemyDasherSpeed, new Vector2(-1, 0));
            AddItem(ItemType_1.EnemyDasher, 600, 50, Config.EnemyDasherSpeed, new Vector2(0, 1));
            AddItem(ItemType_1.EnemyDasher, 1000, 910, Config.EnemyDasherSpeed, new Vector2(0, -1));

            timeCursor += 5;

            AddItem(ItemType_1.EnemyDasher, 50, 83, Config.EnemyDasherSpeed, new Vector2(1, 0));
            AddItem(ItemType_1.EnemyDasher, 50, 249, Config.EnemyDasherSpeed, new Vector2(1, 0));
            AddItem(ItemType_1.EnemyDasher, 50, 415, Config.EnemyDasherSpeed, new Vector2(1, 0));
            AddItem(ItemType_1.EnemyDasher, 50, 518, Config.EnemyDasherSpeed, new Vector2(1, 0));
            AddItem(ItemType_1.EnemyDasher, 50, 747, Config.EnemyDasherSpeed, new Vector2(1, 0));

            AddItem(ItemType_1.EnemyDasher, 1550, 166, Config.EnemyDasherSpeed, new Vector2(-1, 0));
            AddItem(ItemType_1.EnemyDasher, 1550, 332, Config.EnemyDasherSpeed, new Vector2(-1, 0));
            AddItem(ItemType_1.EnemyDasher, 1550, 498, Config.EnemyDasherSpeed, new Vector2(-1, 0));
            AddItem(ItemType_1.EnemyDasher, 1550, 664, Config.EnemyDasherSpeed, new Vector2(-1, 0));
            AddItem(ItemType_1.EnemyDasher, 1550, 830, Config.EnemyDasherSpeed, new Vector2(-1, 0));

            timeCursor += 5;

            levelTime = 0;
            itemIndex = 0;
        }


        public Vector2 randomStartLocation(int side)
        {
            switch (side)
            {
                //left, top, right, bottom
                case 0:
                    return entryPosition = new Vector2(50, rand.Next(910));
                case 1:
                    return entryPosition = new Vector2(rand.Next(1550), 50);
                case 2:
                    return entryPosition = new Vector2(1550, rand.Next(910));
                case 3:
                    return entryPosition = new Vector2(rand.Next(1550), 910);
            }

            return Vector2.Zero;
        }

        void AddItem(ItemType_1 type, float positionX, float positionY, float speed, Vector2 direction)
        {
            LevelItem_1 item;
            item.Time = timeCursor;
            item.Type = type;
            item.PositionX = positionX;
            item.PositionY = positionY;
            item.Speed = speed;
            item.Direction = direction;

            levelItems.Add(item);
        }

        void AddItem(ItemType_1 type, float positionX, float positionY, float speed)
        {
            AddItem(type, positionX, positionY, speed, new Vector2(0, 1));
        }

        void AddItem(ItemType_1 type)
        {
            //random item, random side, direction, random speed
            side = rand.Next(0, 4);


            switch (side)
            {
                //left, top, right, bottom
                case 0:
                    //direction = new Vector2(1,0);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
                case 1:
                    //direction = new Vector2(0,1);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
                case 2:
                    //direction = new Vector2(-1,0);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
                case 3:
                    //direction = new Vector2(0,-1);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
            }

            AddItem(type, entryPosition.X, entryPosition.Y, Config.EnemySpeed, direction);
        }

        void AddItem()
        {
            //random item, random side, direction, random speed
            side = rand.Next(0, 4);
            
            
            switch (side)
            {
               //left, top, right, bottom
                case 0:
                    //direction = new Vector2(1,0);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
                case 1:
                    //direction = new Vector2(0,1);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
                case 2:
                    //direction = new Vector2(-1,0);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
                case 3:
                    //direction = new Vector2(0,-1);
                    direction = Vector2.Zero;
                    entryPosition = randomStartLocation(side);
                    break;
            }

            AddItem(ItemType_1.EnemyAvoider, entryPosition.X, entryPosition.Y, Config.EnemySpeed, direction);
        }

        public void Update(TimeSpan gameTime)
        {
            levelTime += gameTime.TotalSeconds;

            for (int i = itemIndex; i < levelItems.Count; i++)
            {
                if (levelItems[i].Time > levelTime)
                    break;
                //TODO: all enemies dead, show end level screen with score.

                SpawnItem(levelItems[i]);
                itemIndex++;
            }

            //if (levelTime > 381)
            if (levelTime > 127)
            {
                levelTime = 0;
                itemIndex = 0;
                //difficulty += 1;
                //Config.EnemySpeed += 1;
                //if (Config.EnemySpeed > 9)
                //    Config.EnemySpeed = 9;
                generateLevel();
            }

            //if (Enemy.Enemies.Count == 0)
            //    levelTime += 10;

            //levelSpeed += gameTime.ElapsedGameTime.TotalSeconds;
            //if (Node.Fps >= 30 && levelSpeed > 10)
            //    Node.Fps /= 2;

            //infinite enemies for testing
            //if (Enemy.Enemies.Count <= 10)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        SpawnItem(levelItems[i]);
            //    }
            //}
        }

        void SpawnItem(LevelItem_1 item)
        {
            GameNode node = null;

            if (item.Type == ItemType_1.EnemyInterceptor) node = new EnemyInterceptor(Config.EnemyInterceptorSpriteSheet);
            else if (item.Type == ItemType_1.EnemyAvoider) node = new EnemyAvoider(Config.EnemyAvoiderSpriteSheet);
            else if (item.Type == ItemType_1.EnemyChaser) node = new EnemyChaser(Config.EnemyChaserSpriteSheet);
            else if (item.Type == ItemType_1.EnemyDasher) node = new EnemyDasher(Config.EnemyDasherSpriteSheet);
            else if (item.Type == ItemType_1.EnemyShooter) node = new EnemyShooter(Config.EnemyShooterSpriteSheet);
            else if (item.Type == ItemType_1.PowerupHealth) node = new PowerupHealth(Config.PowerupSlowAllSpriteSheet);
            else if (item.Type == ItemType_1.PowerupMissiles) node = new PowerupMissiles(Config.PowerupMissileSpriteSheet);
            else if (item.Type == ItemType_1.PowerupWeaponAutoBurst) node = new PowerupWeaponAutoBurst(Config.PowerupSlowAllSpriteSheet);
            else if (item.Type == ItemType_1.boss1) node = new boss1(Config.BossSpriteSheet);
            else if (item.Type == ItemType_1.PowerupDamageAll) node = new PowerupDamageAll(Config.PowerupSlowAllSpriteSheet);
            else if (item.Type == ItemType_1.PowerupFreeze) node = new PowerupFreeze(Config.PowerupMissileSpriteSheet);
            else if (item.Type == ItemType_1.PowerupDoubleShot) node = new PowerupDoubleShot(Config.PowerupSlowAllSpriteSheet);
            else if (item.Type == ItemType_1.PowerupTripleShot) node = new PowerupTripleShot(Config.PowerupSlowAllSpriteSheet);
            else if (item.Type == ItemType_1.PowerupAddBullet) node = new PowerupAddBullet(Config.PowerupAddProjectileSpriteSheet);
            else if (item.Type == ItemType_1.PowerupShotSpeed) node = new PowerupShotSpeed(Config.PowerupProjectileSpeedSpriteSheet);
            else if (item.Type == ItemType_1.Coin) node = new Coin(Config.CoinSpriteSheet);

            if (node != null)
            {
                //TODO: suppy levelItem Y position from item
                //node.Position = new Vector2(item.PositionX, -node.Sprite.Origin.Y);
                node.Position = new Vector2(item.PositionX, item.PositionY);
                node.Speed = item.Speed;
                node.Direction = item.Direction;
            }

        }
    }
}
