using System;
using System.Text; 
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Graphics; 
using System.Diagnostics;

namespace AstroFlare
{
    public static class extensions
    {
        public static Texture2D full_white;
        public static void color_texture(this Texture2D _texture, Color _color)
        {
            int data_size = _texture.Width * _texture.Height;
            uint new_color = _color.PackedValue;
            uint[] data = new uint[data_size];
            for (int i = 0; i < data_size; ++i)
            {
                data[i] = new_color;
            }
            _texture.SetData<uint>(data);
        }
        public static void initialise_texture(this SpriteBatch _sprite_batch)
        {
            full_white = new Texture2D(_sprite_batch.GraphicsDevice, 4, 4);
            full_white.color_texture(Color.White);
        }
        public static Texture2D create_texture(this GraphicsDevice _graphics_device, int _width, int _height, Color _color)
        {
            Texture2D created_texture = new Texture2D(_graphics_device, _width, _height);
            created_texture.color_texture(_color);
            return created_texture;
        }

        public class GARBAGE_COLLECTIONS_COUNTER : DrawableGameComponent
        {
            SpriteBatch sprite_batch;
            SpriteFont sprite_font;

            int gc_total = 0;
            int gc_during_draw = 0;
            int gc_during_update = 0;
            WeakReference gc_tracker = new WeakReference(new object());
            StringBuilder text = new StringBuilder(10);
            bool changed = true;
            Rectangle background_rect = new Rectangle();
            Texture2D background_texture;

            public static bool update = true;
            public static bool display = true;
            public static bool display_draw_count = true;
            public static bool display_update_count = true;
            public static Color color = Color.Black;
            public static Color background_color = Color.White;
            public static Vector2 position = new Vector2(50, 50);

            public GARBAGE_COLLECTIONS_COUNTER(Game _game, SpriteBatch _sprite_batch, SpriteFont _sprite_font)
                : base(_game)
            {
                sprite_batch = _sprite_batch;
                sprite_font = _sprite_font;
                DrawOrder = 0;
                text.Length = 0;
                text.Append(" gc: 0");
                background_texture = sprite_batch.GraphicsDevice.create_texture(4, 4, Color.White);
            }

            public override void Update(GameTime gameTime)
            {
                if (update && !gc_tracker.IsAlive)
                {
                    ++gc_during_update;
                    gc_total = gc_during_update + gc_during_draw;
                    changed = true;
                    gc_tracker = new WeakReference(new object());
                }
            }
            public override void Draw(GameTime gameTime)
            {
                if (update && !gc_tracker.IsAlive)
                {
                    ++gc_during_draw;
                    gc_total = gc_during_update + gc_during_draw;
                    gc_tracker = new WeakReference(new object());
                    changed = true;
                }
                if (display)
                {
                    update_text();
                    sprite_batch.Begin();
                    sprite_batch.Draw(background_texture, background_rect, background_color);
                    sprite_batch.DrawString(sprite_font, text, position, color);
                    sprite_batch.End();
                }
            }
            public void update_text()
            {
                if (changed)
                {
                    text.Length = 5;
                    text.Append(gc_total);
                    if (display_draw_count)
                    {
                        text.Append("  d: ");
                        text.Append(gc_during_draw);
                    }
                    if (display_update_count)
                    {
                        text.Append("  u: ");
                        text.Append(gc_during_update);
                    }
                    text.Append(" ");
                    Vector2 text_size = sprite_font.MeasureString(text);
                    background_rect = new Rectangle((int)position.X, (int)position.Y, (int)text_size.X, (int)text_size.Y);
                    changed = false;
                }
            }

        }
        public class FPS_COUNTER : DrawableGameComponent
        {
            SpriteBatch sprite_batch;
            SpriteFont sprite_font;

            int num_draws = 0;
            int num_draws_last_frame = 0;
            Stopwatch drawTimer = new Stopwatch();

            int num_updates = 0;
            int num_updates_last_frame = 0;
            Stopwatch updateTimer = new Stopwatch();

            StringBuilder text = new StringBuilder(20);
            bool changed = true;
            Rectangle background_rect = new Rectangle();
            Texture2D background_texture;

            public static bool update = true;
            public static bool display = true;
            public static bool display_draw_count = true;
            public static bool display_update_count = true;

            public static Color color = Color.Black;
            public static Color background_color = Color.White;
            public static Vector2 position = new Vector2(50, 100);

            public FPS_COUNTER(Game _game, SpriteBatch _sprite_batch, SpriteFont _sprite_font)
                : base(_game)
            {
                sprite_batch = _sprite_batch;
                sprite_font = _sprite_font;
                DrawOrder = 0;
                text.Length = 0;
                text.Append(" FPS: 0");
                background_texture = sprite_batch.GraphicsDevice.create_texture(4, 4, Color.White);
                updateTimer.Start();
                drawTimer.Start();
            }

            public override void Update(GameTime gameTime)
            {
                if (update)
                {
                    if (updateTimer.ElapsedMilliseconds > 1000)
                    {
                        num_updates_last_frame = num_updates;
                        num_updates = 0;
                        updateTimer.Reset();
                        updateTimer.Start();
                    }
                    else
                    {
                        ++num_updates;
                    };
                    changed = true;
                }
            }
            public override void Draw(GameTime gameTime)
            {
                if (update)
                {
                    if (drawTimer.ElapsedMilliseconds > 1000)
                    {
                        num_draws_last_frame = num_draws;
                        num_draws = 0;
                        drawTimer.Reset();
                        drawTimer.Start();
                    }
                    else
                    {
                        ++num_draws;
                    };
                    changed = true;
                }
                if (display)
                {
                    if (changed)
                    {
                        update_text();
                    }
                    sprite_batch.Begin();
                    sprite_batch.Draw(background_texture, background_rect, background_color);
                    sprite_batch.DrawString(sprite_font, text, position, color);
                    sprite_batch.End();
                }
            }
            public void update_text()
            {
                if (changed)
                {
                    text.Length = 5;
                    if (display_draw_count)
                    {
                        text.Append("  d: ");
                        text.Append(num_draws_last_frame);
                    }
                    if (display_update_count)
                    {
                        text.Append("  u: ");
                        text.Append(num_updates_last_frame);
                    }
                    text.Append(" ");
                    Vector2 text_size = sprite_font.MeasureString(text);
                    background_rect = new Rectangle((int)position.X, (int)position.Y, (int)text_size.X, (int)text_size.Y);
                    changed = false;
                }
            }
        }
    }
}