using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
// Mercuring usings
using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;

namespace AstroFlare
{
    static class ParticleEffects
    {
        //private AbstractRenderer myRenderer;
        static private SpriteBatchRenderer myRenderer;

        static private Matrix World;
        static public Matrix View;
        static public Matrix Projection;
        static private Vector3 CameraPosition = Vector3.Zero;
        static ParticleEffect ExplosionSquaresSmall;
        //static ParticleEffect laserBeam;
        static ParticleEffect ExplosionSquaresLarge;
        //static ParticleEffect megaExplosion;
        static ParticleEffect ship1Trail;
        static ParticleEffect ship2Trail;
        static ParticleEffect ship3Trail;
        //static ParticleEffect shipTrailBlue;
        static ParticleEffect shipTrailRed;
        static ParticleEffect missileTrail;
        static ParticleEffect enemyTrail;
        static float SecondsPassed;
        static ParticleEffect menuShipTrailGreen;
        static ParticleEffect menuShipTrailPurple;
        static ParticleEffect menuShipTrailBlue;

        //static GameTime gameTimer;

        //static public ParticleEffects(GraphicsDeviceManager graphics, Camera2D cam, GraphicsDevice graphicsDevice)
        //{
        //    myRenderer = new SpriteBatchRenderer
        //    {
        //        GraphicsDeviceService = graphics,
        //        Transformation = Matrix.CreateTranslation(0f, 0f, 0f)
        //        //Transformation = cam.get_transformation(graphicsDevice)                
        //    };

        //    World = Matrix.Identity;
        //    Projection = Matrix.Identity;
        //    View = Matrix.Identity;
        //}

        //static public ParticleEffects()
        //{

        //}

        static public void Initialize(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
        {
            myRenderer = new SpriteBatchRenderer
            {
                GraphicsDeviceService = graphics,
                
                //Transformation = Matrix.CreateTranslation(0f, 0f, 0f)    
                
            };


            View = Matrix.Identity;
            World = Matrix.Identity;
            Projection = Matrix.Identity;
            //Projection = Matrix.CreateOrthographicOffCenter(0, 800, 480, 0, 0, 2000);

            //View = new Matrix(1.0f, 0.0f, 0.0f, 0.0f,
            //            0.0f, -1.0f, 0.0f, 0.0f,
            //            0.0f, 0.0f, -1.0f, 0.0f,
            //            0.0f, 0.0f, 0.0f, 1.0f);
            
        }



        static public void LoadContent(ContentManager Content)
        {

           ExplosionSquaresSmall = new ParticleEffect
           {

               Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Name = "ExplosionSquaresSmall",
                        Budget = 1000,
                        Term = 0.5f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3 (0f, 0.5019608f, 0f),
                        ReleaseOpacity = 0.5f,
                        ReleaseQuantity = 25,
                        ReleaseScale = new Range(4f, 2f),
                        ReleaseSpeed = new Range(64f, 32),
                        ReleaseRotation = new RotationRange { Roll = new Range(-MathHelper.Pi, MathHelper.Pi), Pitch = new Range(-MathHelper.Pi, MathHelper.Pi), Yaw = new Range(-MathHelper.Pi, MathHelper.Pi) },
                        

                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator3
                            {
                                InitialOpacity = 0.5f,
                                MedianOpacity = 0.2f,
                                FinalOpacity = 0.0f,
                                Median = 0.75f,
                            },

                            new ColourInterpolator2
                            {
                                InitialColour = new Vector3(0f, 0.5019608f, 0f),
                                FinalColour = new Vector3((float)27 / 255, (float)53 / 255, (float)31 / 255),
                            },

                        },
                    },
                },
           };

           //laserBeam = new ParticleEffect
           //{

           //    Emitters = new EmitterCollection
           //     {

           //         new LineEmitter
           //         {
           //             Name = "LaserBeam",
           //             Budget = 10000,
           //             Term = 2.0f,
           //             BlendMode = EmitterBlendMode.Add,
           //             ReleaseColour = new Vector3 (0f, 0.5019608f, 0f),
           //             //ReleaseColour = new Vector3 (255f, 69f, 0f),
           //             ReleaseOpacity = 0.1f,
           //             ReleaseQuantity = 5,
           //             ReleaseScale = new Range(32f, 64f),
           //             ReleaseSpeed = new Range(5f, 10f),
                        
           //             //ReleaseRotation = new RotationRange { Roll = new Range(-MathHelper.Pi, MathHelper.Pi), Pitch = new Range(-MathHelper.Pi, MathHelper.Pi), Yaw = new Range(-MathHelper.Pi, MathHelper.Pi) },
                        
                       
           //             Modifiers = new ModifierCollection
           //             {
           //                 //new OpacityInterpolator3
           //                 //{
           //                 //    InitialOpacity = 0.5f,
           //                 //    MedianOpacity = 0.2f,
           //                 //    FinalOpacity = 0.0f,
           //                 //    Median = 0.75f,
           //                 //},
           //                 //new RotationModifier
           //                 //{
           //                 //    RotationRate = new Vector3(10f),
           //                 //},

           //                 //new ColourInterpolator2
           //                 //{
           //                 //    InitialColour = new Vector3(0f, 0.5019608f, 0f),
           //                 //    FinalColour = new Vector3((float)27 / 255, (float)53 / 255, (float)31 / 255),
           //                 //},

           //                 new DampingModifier
           //                 {
           //                     DampingCoefficient = 5f,
           //                 },

           //             },
           //         },
           //     },
           //};


           ExplosionSquaresLarge = new ParticleEffect
           {

               Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Name = "ExplosionSquaresLarge",
                        Budget = 1000,
                        Term = 0.5f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new ColourRange { Red = new Range(0.8f, 1f), Green = new Range(0.3f, 0.7f), Blue = new Range(0.0f, 0.1f)},
                        ReleaseOpacity = 1f,
                        ReleaseQuantity = 50,
                        ReleaseScale = new Range(16f, 8f),
                        ReleaseSpeed = new Range(256f, 64),
                        ReleaseRotation = new RotationRange { Roll = new Range(-MathHelper.Pi, MathHelper.Pi), Pitch = new Range(-MathHelper.Pi, MathHelper.Pi), Yaw = new Range(-MathHelper.Pi, MathHelper.Pi) },
                        //BillboardStyle = ProjectMercury.BillboardStyle.Spherical,

                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator3
                            {
                                InitialOpacity = 1f,
                                MedianOpacity = 0.5f,
                                FinalOpacity = 0f,
                                //Median = 0.75f,
                            },


                            //new DampingModifier
                            //{
                            //    DampingCoefficient = 1f,
                            //},

                            //new LinearGravityModifier
                            //{
                            //    GravityVector = Vector3.Forward,
                            //    Strength = 0.5f,
                            //},

                            //new ColourInterpolator2
                            //{
                            //    InitialColour = new Vector3((float)234 / 255, (float)255 / 255, (float)0 / 255),
                            //    FinalColour = new Vector3((float)77 / 255, (float)6 / 255, (float)150 / 255),
                            //},

                        },
                    },
                },
           };

           //megaExplosion = new ParticleEffect
           //{

           //    Emitters = new EmitterCollection
           //     {
                    
           //         new PointEmitter
           //         {
           //             Name = "ShockWave",
           //             Budget = 1000,
           //             Term = 3f,
           //             BlendMode = EmitterBlendMode.Add,
           //             ReleaseColour = new Vector3 (1f, 1f, 1f),
           //             ReleaseOpacity = 0.2f,
           //             ReleaseQuantity = 1,
           //             ReleaseRotation = new Vector3 (0f, 0f, 0f),
           //             ReleaseScale = new Range(64f, 64f),
           //             ReleaseSpeed = 0f,

                        
           //             Modifiers = new ModifierCollection
           //             {
           //                 new OpacityInterpolator3
           //                 {
           //                     InitialOpacity = 0.2f,
           //                     MedianOpacity = 0.0f,
           //                     FinalOpacity = 0.0f,
           //                     Median = 0.75f,
           //                 },
           //                 new ScaleInterpolator3
           //                 {
           //                     InitialScale = 32f,
           //                     MedianScale = 128,
           //                     FinalScale = 256,
           //                     Median = 0.75f,
           //                 },
           //             },
           //         },

           //         new CircleEmitter
           //         {
           //             Name = "Flames",
           //             Budget = 2000,
           //             Term = 2f,
           //             Radius = 1,
           //             Radiate = true,
           //             BlendMode = EmitterBlendMode.Add,

           //             ReleaseQuantity = 20,
           //             ReleaseColour = new ColourRange { Red = new Range(0.8f, 1f), Green = new Range(0.3f, 0.7f), Blue = new Range(0.0f, 0.1f)},
           //             ReleaseOpacity = new Range(0.5f, 0.9f),
           //             ReleaseRotation = new RotationRange { Roll = new Range(-MathHelper.Pi, MathHelper.Pi), Pitch = new Range(-MathHelper.Pi, MathHelper.Pi), Yaw = new Range(-MathHelper.Pi, MathHelper.Pi) },
           //             ReleaseScale = new Range(100f, 100f),
           //             ReleaseSpeed = new Range(5f, 15f),
                        
                        


           //             Modifiers = new ModifierCollection
           //             {
           //                 new OpacityInterpolator2
           //                 {
           //                     InitialOpacity = 0.5f,
           //                     FinalOpacity = 0f,
           //                 },
           //                 new RotationModifier
           //                 {
           //                     RotationRate = new Vector3(1f, 1f, 0f),
           //                 },
           //                 new DampingModifier
           //                 {
           //                     DampingCoefficient = 1f,
           //                 },
           //                 new ScaleInterpolator3
           //                 {
           //                     InitialScale = 30f,
           //                     MedianScale = 128,
           //                     FinalScale = 256,
           //                     Median = 0.75f,
           //                 },
           //             },
           //         },

           //         new PointEmitter
           //         {
           //             Name = "Flash",
           //             Budget = 1000,
           //             Term = 0.2f,
           //             BlendMode = EmitterBlendMode.Add,
           //             ReleaseColour = new Vector3 (1f, 1f, 1f),
           //             ReleaseOpacity = 0.5f,
           //             ReleaseQuantity = 1,
           //             ReleaseRotation = new RotationRange(),
           //             ReleaseScale = new Range(128f, 128f),
           //             ReleaseSpeed = 50f,

           //             Modifiers = new ModifierCollection
           //             {
           //                 new OpacityInterpolator2
           //                 {
           //                     InitialOpacity = 1f,
           //                     FinalOpacity = 0f,
           //                 },
           //             },
           //         },                                       
           //     },
           //};


            

            ship1Trail = new ParticleEffect
            {
                
                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        //Name = "Big Stars",
                        //Budget = 1000,
                        //Term = 0.25f,
                        //BlendMode = EmitterBlendMode.Alpha,       
                        
                        //ReleaseColour = new Vector3 (0f, 0.5019608f, 0f), 
                        //ReleaseOpacity = 1f,
                        //ReleaseQuantity = 1,
                        //ReleaseRotation = new Vector3 (0f, 0f, 0f),
                        //ReleaseScale = new Range(48f, 24f),
                        //ReleaseSpeed = new Range(96f, 64f),
                        //Enabled = true,
                        //BillboardStyle = ProjectMercury.BillboardStyle.Spherical,
                        
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3(0f, 0.5019608f, 0f),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,
                        


                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            //new ColourInterpolator2
                            //{
                            //    InitialColour = new Vector3(0f, 0.5019608f, 0f),
                            //    FinalColour = new Vector3((float)27 / 255, (float)53 / 255, (float)31 / 255),
                            //},

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },

                        },
                    },

                },
            };

            ship2Trail = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {                        
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3((float)128 / 255, (float)0 / 255, (float)128 / 255),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,
                        
                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },
                        },
                    },

                },
            };

            ship3Trail = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3((float)173 / 255, (float)216 / 255, (float)230 / 255),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,

                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },
                        },
                    },

                },
            };


            //shipTrailBlue = new ParticleEffect
            //{

            //    Emitters = new EmitterCollection
            //    {
                    
            //        new PointEmitter
            //        {
            //            //Name = "Big Stars",
            //            //Budget = 1000,
            //            //Term = 0.25f,
            //            //BlendMode = EmitterBlendMode.Alpha,       
                        
            //            //ReleaseColour = new Vector3 (0f, 0.5019608f, 0f), 
            //            //ReleaseOpacity = 1f,
            //            //ReleaseQuantity = 1,
            //            //ReleaseRotation = new Vector3 (0f, 0f, 0f),
            //            //ReleaseScale = new Range(48f, 24f),
            //            //ReleaseSpeed = new Range(96f, 64f),
            //            //Enabled = true,
            //            //BillboardStyle = ProjectMercury.BillboardStyle.Spherical,
                        
            //            Budget = 500,
            //            Term = 0.25f,
            //            BlendMode = EmitterBlendMode.Alpha,
            //            ReleaseColour = new Vector3((float)173 / 255, (float)216 / 255, (float)230 / 255),
            //            ReleaseOpacity = 1,
            //            ReleaseQuantity = 1,
            //            ReleaseScale = new Range(32f, 64f),
            //            ReleaseSpeed = 10f,
                        


            //            Modifiers = new ModifierCollection
            //            {
            //                new OpacityInterpolator2
            //                {
            //                    InitialOpacity = 1f,
            //                    FinalOpacity = 0f,
            //                },

            //                //new ColourInterpolator2
            //                //{
            //                //    InitialColour = new Vector3((float)173 / 255, (float)216 / 255, (float)230 / 255),
            //                //    FinalColour = new Vector3((float)173 / 255, (float)216 / 255, (float)230 / 255),
            //                //},

            //                new ScaleInterpolator3
            //                {
            //                    InitialScale = 128f,
            //                    MedianScale = 64f,
            //                    Median = 0.4f,
            //                    FinalScale = 32f,
            //                },

            //                new DampingModifier
            //                {
            //                    DampingCoefficient = 5f,
            //                },
            //            },
            //        },

            //    },
            //};

            shipTrailRed = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        //Name = "Big Stars",
                        //Budget = 1000,
                        //Term = 0.25f,
                        //BlendMode = EmitterBlendMode.Alpha,       
                        
                        //ReleaseColour = new Vector3 (0f, 0.5019608f, 0f), 
                        //ReleaseOpacity = 1f,
                        //ReleaseQuantity = 1,
                        //ReleaseRotation = new Vector3 (0f, 0f, 0f),
                        //ReleaseScale = new Range(48f, 24f),
                        //ReleaseSpeed = new Range(96f, 64f),
                        //Enabled = true,
                        //BillboardStyle = ProjectMercury.BillboardStyle.Spherical,
                        
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3((float)255 / 255, (float)0 / 255, (float)0 / 255),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,


                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            //new ColourInterpolator2
                            //{
                            //    InitialColour = new Vector3((float)255 / 255, (float)0 / 255, (float)0 / 255),
                            //    FinalColour = new Vector3((float)255 / 255, (float)0 / 255, (float)0 / 255),
                            //},

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },
                        },
                    },

                },
            };



            missileTrail = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Name = "Big Stars",
                        Budget = 1000,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,       
                        
                        
                        ReleaseColour = new Vector3 ((float)255 / 255, (float)229 / 255, (float)0 / 255), 
                        ReleaseOpacity = 1f,
                        ReleaseQuantity = 1,
                        ReleaseRotation = new Vector3 (0f, 0f, 0f),
                        ReleaseScale = new Range(24f, 12f),
                        ReleaseSpeed = new Range(96f, 64f),
                        Enabled = true,
                        
                        Modifiers = new ModifierCollection
                        {
                            //new OpacityInterpolator2
                            //{
                            //    InitialOpacity = 1f,
                            //    FinalOpacity = 0f,
                            //},

                            new ColourInterpolator2
                            {
                                InitialColour = new Vector3((float)255 / 255, (float)229 / 255, (float)0 / 255),
                                FinalColour = new Vector3((float)255 / 255, (float)104 / 255, (float)0 / 255),
                            },

                            //new ScaleInterpolator3
                            //{
                            //    InitialScale = 48f,
                            //    MedianScale = 64f,
                            //    Median = 0.4f,
                            //    FinalScale = 16f,
                            //},

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },
                        },
                    },

                },
            };

            enemyTrail = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Name = "Big Stars",
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,       
                        
                        
                        ReleaseColour = new Vector3 ((float)0 / 255, (float)133 / 255, (float)255 / 255),
                        ReleaseOpacity = 1f,
                        ReleaseQuantity = 1,
                        ReleaseRotation = new Vector3 (0f, 0f, 0f),
                        ReleaseScale = new Range(48f, 24f),
                        ReleaseSpeed = new Range(96f, 64f),
                        Enabled = true,
                        

                        Modifiers = new ModifierCollection
                        {
                            //new OpacityInterpolator2
                            //{
                            //    InitialOpacity = 1f,
                            //    FinalOpacity = 0f,
                            //},

                            new ColourInterpolator2
                            {
                                InitialColour = new Vector3((float)0 / 255, (float)133 / 255, (float)255 / 255),
                                FinalColour = new Vector3((float)55 / 255, (float)16 / 255, (float)88 / 255),
                            },

                            //new ScaleInterpolator3
                            //{
                            //    InitialScale = 48f,
                            //    MedianScale = 64f,
                            //    Median = 0.4f,
                            //    FinalScale = 16f,
                            //},

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },
                        },
                    },

                },
            };

            menuShipTrailGreen = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3(0f, 0.5019608f, 0f),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,
                        
                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            //new ColourInterpolator2
                            //{
                            //    InitialColour = new Vector3(0f, 0.5019608f, 0f),
                            //    FinalColour = new Vector3((float)27 / 255, (float)53 / 255, (float)31 / 255),
                            //},

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },

                            new LinearGravityModifier
                            {
                                GravityVector = new Vector3(0, 1, 0),
                                Strength = 3000f,
                            },
                        },
                    },

                },
            };

            menuShipTrailPurple = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {                        
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3((float)128 / 255, (float)0 / 255, (float)128 / 255),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,
                        
                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },

                            new LinearGravityModifier
                            {
                                GravityVector = new Vector3(0, 1, 0),
                                Strength = 3000f,
                            },
                        },
                    },

                },
            };

            menuShipTrailBlue = new ParticleEffect
            {

                Emitters = new EmitterCollection
                {
                    
                    new PointEmitter
                    {
                        Budget = 500,
                        Term = 0.25f,
                        BlendMode = EmitterBlendMode.Alpha,
                        ReleaseColour = new Vector3((float)173 / 255, (float)216 / 255, (float)230 / 255),
                        ReleaseOpacity = 1,
                        ReleaseQuantity = 1,
                        ReleaseScale = new Range(32f, 64f),
                        ReleaseSpeed = 10f,

                        Modifiers = new ModifierCollection
                        {
                            new OpacityInterpolator2
                            {
                                InitialOpacity = 1f,
                                FinalOpacity = 0f,
                            },

                            new ScaleInterpolator3
                            {
                                InitialScale = 128f,
                                MedianScale = 64f,
                                Median = 0.4f,
                                FinalScale = 32f,
                            },

                            new DampingModifier
                            {
                                DampingCoefficient = 5f,
                            },

                            new LinearGravityModifier
                            {
                                GravityVector = new Vector3(0, 1, 0),
                                Strength = 3000f,
                            },
                        },
                    },

                },
            };


            //megaExplosion.Emitters[0].Initialise();
            //megaExplosion.Emitters[1].Initialise();
            //megaExplosion.Emitters[2].Initialise();

            //megaExplosion.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Temp\Ring002");
            //megaExplosion.Emitters[1].ParticleTexture = Content.Load<Texture2D>(@"Particles\Temp\Cloud002");
            //megaExplosion.Emitters[2].ParticleTexture = Content.Load<Texture2D>(@"Particles\Temp\Particle005");

            ExplosionSquaresSmall.Emitters[0].Initialise();
            ExplosionSquaresSmall.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\hitParticle");

            //laserBeam.Emitters[0].Initialise();
            //laserBeam.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Beam");

            ExplosionSquaresLarge.Emitters[0].Initialise();
            ExplosionSquaresLarge.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\hitParticle");

            ship1Trail.Emitters[0].Initialise();
            ship1Trail.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            ship2Trail.Emitters[0].Initialise();
            ship2Trail.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            ship3Trail.Emitters[0].Initialise();
            ship3Trail.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            //shipTrailBlue.Emitters[0].Initialise();
            //shipTrailBlue.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            shipTrailRed.Emitters[0].Initialise();
            shipTrailRed.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            missileTrail.Emitters[0].Initialise();
            missileTrail.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            enemyTrail.Emitters[0].Initialise();
            enemyTrail.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            menuShipTrailGreen.Emitters[0].Initialise();
            menuShipTrailGreen.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            menuShipTrailPurple.Emitters[0].Initialise();
            menuShipTrailPurple.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");

            menuShipTrailBlue.Emitters[0].Initialise();
            menuShipTrailBlue.Emitters[0].ParticleTexture = Content.Load<Texture2D>(@"Particles\Particle004");
            
            myRenderer.LoadContent(Content);
        }

        static public void Update(GameTime gameTime)
        {
            //gameTimer = gameTime;
            SecondsPassed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            myRenderer.Transformation = Matrix.Identity;
            //CameraPosition = new Vector3(cam.Pos.X, cam.Pos.Y, 0);

            //megaExplosion.Update(SecondsPassed);
            ship1Trail.Update(SecondsPassed);
            ship2Trail.Update(SecondsPassed);
            ship3Trail.Update(SecondsPassed);
            //shipTrailBlue.Update(SecondsPassed);
            shipTrailRed.Update(SecondsPassed);
            missileTrail.Update(SecondsPassed);
            enemyTrail.Update(SecondsPassed);
            ExplosionSquaresSmall.Update(SecondsPassed);
            //laserBeam.Update(SecondsPassed);
            ExplosionSquaresLarge.Update(SecondsPassed);

            menuShipTrailGreen.Update(SecondsPassed);
            menuShipTrailPurple.Update(SecondsPassed);
            menuShipTrailBlue.Update(SecondsPassed);
            //World = cam._transform;
            //Projection = cam._transform;
            //View = cam._transform;             
        }



        static public void Update(GameTime gameTime, Camera2D cam, ref Matrix transform)
        {
            //gameTimer = gameTime;
            SecondsPassed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            myRenderer.Transformation = transform;
            //CameraPosition = new Vector3(cam.Pos.X, cam.Pos.Y, 0);

            //megaExplosion.Update(SecondsPassed);
            ship1Trail.Update(SecondsPassed);
            ship2Trail.Update(SecondsPassed);
            ship3Trail.Update(SecondsPassed);
            //shipTrailBlue.Update(SecondsPassed);
            shipTrailRed.Update(SecondsPassed);
            missileTrail.Update(SecondsPassed);
            enemyTrail.Update(SecondsPassed);
            ExplosionSquaresSmall.Update(SecondsPassed);
            //laserBeam.Update(SecondsPassed);
            ExplosionSquaresLarge.Update(SecondsPassed);

            menuShipTrailGreen.Update(SecondsPassed);
            menuShipTrailPurple.Update(SecondsPassed);
            menuShipTrailBlue.Update(SecondsPassed);
            //World = cam._transform;
            //Projection = cam._transform;
            //View = cam._transform;             
        }

        static public void Draw()
        {
            //myRenderer.RenderEffect(megaExplosion, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(ship1Trail, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(ship2Trail, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(ship3Trail, ref World, ref View, ref Projection, ref CameraPosition);
            //myRenderer.RenderEffect(shipTrailBlue, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(shipTrailRed, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(missileTrail, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(enemyTrail, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(ExplosionSquaresSmall, ref World, ref View, ref Projection, ref CameraPosition);
            //myRenderer.RenderEffect(laserBeam, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(ExplosionSquaresLarge, ref World, ref View, ref Projection, ref CameraPosition);

            myRenderer.RenderEffect(menuShipTrailGreen, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(menuShipTrailPurple, ref World, ref View, ref Projection, ref CameraPosition);
            myRenderer.RenderEffect(menuShipTrailBlue, ref World, ref View, ref Projection, ref CameraPosition);
        }

        //static public void TriggerMegaExplosionEffect(Vector2 location)
        //{
        //    //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
        //    Vector3 loc = new Vector3(location.X, location.Y, 0f);
        //    megaExplosion.Trigger(SecondsPassed, ref loc);
        //}

        static public void TriggerShip1Trail(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            ship1Trail.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerShip2Trail(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            ship2Trail.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerShip3Trail(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            ship3Trail.Trigger(SecondsPassed, ref loc);
        }

        //static public void TriggerShipTrailBlue(Vector2 location)
        //{
        //    //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
        //    Vector3 loc = new Vector3(location.X, location.Y, 0f);
        //    shipTrailBlue.Trigger(SecondsPassed, ref loc);
        //}

        static public void TriggerShipTrailRed(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            shipTrailRed.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerMissileSmokeTrail(Vector2 location)
        {
           //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            missileTrail.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerEnemySmokeTrail(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            enemyTrail.Trigger(SecondsPassed, ref loc);
        }

        static public void ParticleExplosion(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            ExplosionSquaresSmall.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerExplosionSquaresSmall(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            ExplosionSquaresSmall.Trigger(SecondsPassed, ref loc);
        }

        //static public void TriggerLaserBeam(Vector2 location, float rotation)
        //{
        //    //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
        //    Vector3 loc = new Vector3(location.X, location.Y, 0f);
        //    laserBeam.Emitters[0].ReleaseRotation = new RotationRange { Roll = new Range(rotation, rotation) };
        //    laserBeam.Trigger(SecondsPassed, ref loc);
        //}

        static public void TriggerExplosionSquaresLarge(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            ExplosionSquaresLarge.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerMenuShipTrailGreen(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            menuShipTrailGreen.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerMenuShipTrailPurple(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            menuShipTrailPurple.Trigger(SecondsPassed, ref loc);
        }

        static public void TriggerMenuShipTrailBlue(Vector2 location)
        {
            //float SecondsPassed = (float)gameTimer.ElapsedGameTime.TotalSeconds;
            Vector3 loc = new Vector3(location.X, location.Y, 0f);
            menuShipTrailBlue.Trigger(SecondsPassed, ref loc);
        }
    }
}
