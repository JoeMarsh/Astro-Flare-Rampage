using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    enum ItemType_6
    {
        Bug1,
        Bug2,
        Bug3,
        Bug4,
        BugDagger1,
        BugDagger2,
        BugDagger3,
        PowerupHealth,
        PowerupMissiles,
        PowerupWeaponAutoBurst,
    }

    struct LevelItem_6
    {
        public double Time;
        public ItemType_6 Type;
        public float PositionX;
        public float PositionY;
        public float Speed;
        public Vector2 Direction;
    }

    class Level6
    {
        List<LevelItem_6> levelItems = new List<LevelItem_6>();
        double timeCursor = 0;
        int itemIndex = 0;
        int ViewCenter { get { return Config.WorldBoundsX / 2; } }
        //static TimeSpan levelTime;
        double levelTime = 0;
        Random rand = new Random();

        int side;
        Vector2 entryPosition;
        Vector2 direction;

        int difficulty = 0;

        public Level6()
        {
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            levelItems.Clear();
            timeCursor = 0;

            for (int i = 0; i < 1 + difficulty; i++)
            {
                AddItem((ItemType_6)rand.Next(4));
            }
            timeCursor += 10;

            //AddItem(ItemType.EnemyInterceptor, ViewCenter, 2f);

            //timeCursor += 5;
            //AddItem(ItemType.PowerupHealth, ViewCenter - 100, 2f);
            //AddItem(ItemType.PowerupMissiles, ViewCenter, 2f);
            //AddItem(ItemType.Asteroid, ViewCenter + 100, 2f);

            //timeCursor += 4;
            //AddItem(ItemType.Asteroid, ViewCenter - 100, 2f);
            //AddItem(ItemType.Asteroid, ViewCenter - 150, 3f);
            //AddItem(ItemType.Asteroid, ViewCenter + 100, 2f);
            //AddItem(ItemType.Asteroid, ViewCenter + 150, 3f);

            //timeCursor += 4;
            //AddItem(ItemType.Asteroid, 0, 3f, Vector2.Normalize(new Vector2(1, 1)));
            //AddItem(ItemType.Asteroid, Config.WorldBoundsX, 3f, Vector2.Normalize(new Vector2(-1, 1)));

            //Random random = new Random();
            //for (int i = 0; i < 100000; i++)
            //{
            //    timeCursor += 1;
            //    AddItem((ItemType)random.Next(4), random.Next(Config.ScreenWidth), 3f);
            //}
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


        void AddItem(ItemType_6 type, float positionX, float positionY, float speed, Vector2 direction)
        {
            LevelItem_6 item;
            item.Time = timeCursor;
            item.Type = type;
            item.PositionX = positionX;
            item.PositionY = positionY;
            item.Speed = speed;
            item.Direction = direction;

            levelItems.Add(item);
        }

        void AddItem(ItemType_6 type, float positionX, float positionY, float speed)
        {
            AddItem(type, positionX, positionY, speed, new Vector2(0, 1));
        }

        void AddItem(ItemType_6 type)
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

            AddItem(type, entryPosition.X, entryPosition.Y, rand.Next(2, 5), direction);
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

            AddItem(ItemType_6.Bug1, entryPosition.X, entryPosition.Y, rand.Next(2, 5), direction);
        }

        int levelCounter = 0;

        public void Update(GameTime gameTime)
        {
            levelTime += gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = itemIndex; i < levelItems.Count; i++)
            {
                if (levelItems[i].Time > levelTime)
                    break;
                //TODO: all enemies dead, show end level screen with score.

                SpawnItem(levelItems[i]);
                itemIndex++;
            }

            if (levelTime > 10)
            {
                //difficulty += 1;
                levelCounter++;

                levelTime = 0;
                itemIndex = 0;

                if (levelCounter > 9)
                {
                    Config.EnemySpeed += 1;
                    difficulty += 1;
                    levelCounter = 0;
                    GenerateLevel();
                }

                if (Config.EnemySpeed > 9)
                    Config.EnemySpeed = 9;

                //GenerateLevel();
            }

            //infinite enemies for testing
            //if (Enemy.Enemies.Count <= 10)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        SpawnItem(levelItems[i]);
            //    }
            //}
        }

        void SpawnItem(LevelItem_6 item)
        {
            GameNode node = null;

            if (item.Type == ItemType_6.Bug1) node = new Bug1(Config.Bug1SpriteSheet);
            else if (item.Type == ItemType_6.Bug2) node = new Bug2(Config.Bug2SpriteSheet);
            else if (item.Type == ItemType_6.Bug3) node = new Bug3(Config.Bug3SpriteSheet);
            else if (item.Type == ItemType_6.Bug4) node = new Bug4(Config.Bug4SpriteSheet);
            else if (item.Type == ItemType_6.BugDagger1) node = new BugDagger1(Config.BugDagger1SpriteSheet);
            else if (item.Type == ItemType_6.BugDagger2) node = new BugDagger2(Config.BugDagger2SpriteSheet);
            else if (item.Type == ItemType_6.BugDagger3) node = new BugDagger3(Config.BugDagger3SpriteSheet);
            else if (item.Type == ItemType_6.PowerupHealth) node = new PowerupHealth(Config.PowerupSlowAllSpriteSheet);
            else if (item.Type == ItemType_6.PowerupMissiles) node = new PowerupMissiles(Config.PowerupMissileSpriteSheet);
            else if (item.Type == ItemType_6.PowerupWeaponAutoBurst) node = new PowerupWeaponAutoBurst(Config.PowerupSlowAllSpriteSheet);

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
