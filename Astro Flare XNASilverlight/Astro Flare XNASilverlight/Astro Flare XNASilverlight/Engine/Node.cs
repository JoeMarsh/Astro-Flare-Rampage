using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class Node
    {
        public static List<Node> Nodes = new List<Node>();

        public static float Fps = 30;
        static Vector2 line;

        private Node parent;
        private List<Node> children = new List<Node>();
        public Node Parent { get { return this.parent; } }

        private float rotation = 0.0f;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value % MathHelper.TwoPi; }
        }

        public Vector2 WeaponOffset = Vector2.Zero;

        public Vector2 Velocity;

        public Vector2 Position;
        //public Vector2 Position
        //{
        //    get { return this.position; }
        //    set { this.Move(value - this.position); }
        //}
        //public Vector2 Position
        //{
        //    get { return this.Position; }
        //    set { this.Position = value; }
        //}

        public Vector2 Direction;
        public float Speed;
        public bool Invulnerable = false;

        public Sprite Sprite;

        private bool dead;
        public bool Dead { get { return this.dead; } }

        public Node(SpriteSheet spriteSheet)
        {
            Nodes.Add(this);
            this.Sprite = new Sprite(spriteSheet);
        }

        public void SetParent(Node parent)
        {
            if (this.parent != null)
                this.parent.children.Remove(this);

            this.parent = parent;

            if (parent != null)
                parent.children.Add(this);
        }

        //public void Move(Vector2 amount)
        //{
        //    this.position += amount;
        //    foreach (Node node in this.children)
        //    {
        //        node.Move(amount);
        //    }
        //}

        public static void UpdateNodes(TimeSpan gameTime)
        {
            for (int i = Nodes.Count - 1; i >= 0; i--)
                Nodes[i].Update(gameTime);
        }

        //public virtual void Update(GameTime gameTime)
        //{
        //    this.Move(this.Direction * this.Speed);
        //    this.Sprite.Update(gameTime);
        //}

        public virtual void Update(TimeSpan gameTime)
        {
            //Position += Direction * Speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 30);

            //if (this is PlayerShip)
            //    Position += Direction * Speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 30);
            //else
                Velocity = Direction * Speed * ((float)gameTime.TotalMilliseconds / Fps);

            Position += Velocity;

            this.Sprite.Update(gameTime);
        }

        public void Bounce(Node node1, Node node2)
        {

            Vector2 cOfMass = (node1.Velocity + node2.Velocity) / 2;

            Vector2 normal1 = node2.Position - node1.Position;
            normal1.Normalize();
            Vector2 normal2 = node1.Position - node2.Position;
            normal2.Normalize();

            node1.Velocity -= cOfMass;
            node1.Velocity = Vector2.Reflect(node1.Velocity, normal1);
            node1.Velocity += cOfMass;

            node2.Velocity -= cOfMass;
            node2.Velocity = Vector2.Reflect(node2.Velocity, normal2);

            node2.Velocity += cOfMass;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this.Sprite.Draw(spriteBatch, this.Position, rotation);
        }

        public virtual void Remove()
        {
            this.dead = true;

            int count = this.children.Count;
            for (int i = 0; i < count; i++)
            {
                this.children[i].Remove();
            }

            //foreach (Node node in this.children)
            //    node.Remove();
        }

        public static void RemoveDead()
        {
            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                if (Nodes[i].dead)
                {
                    if (Nodes[i].Parent != null)
                        Nodes[i].SetParent(null);
                    Nodes.RemoveAt(i);
                }
            }
        }

        //public void Perform(EventHandler action, EventArgs e)
        //{
        //    action(this, e);
        //    for (int i = this.children.Count - 1; i >= 0; i--)
        //        this.children[i].Perform(action, e);
        //}

        public Node GetRoot()
        {
            Node node = this;
            while (node.parent != null)
                node = node.Parent;

            return node;
        }

        #region Collision

        const int ALPHA_THRESHOLD = 48;
        //public static GraphicsDevice GraphicsDevice;

        //public Rectangle Bounds
        //{
        //    get
        //    {
        //        return new Rectangle((int)Position.X - (int)this.Sprite.Origin.X,
        //                             (int)Position.Y - (int)this.Sprite.Origin.Y,
        //                             Sprite.Width,
        //                             Sprite.Height);
        //    }
        //}

        public Rectangle NormalizeBounds(Rectangle rect)
        {
            return new Rectangle(rect.X - (int)this.Position.X + this.Sprite.FrameBounds.X + (int)this.Sprite.Origin.X,
                                 rect.Y - (int)this.Position.Y + this.Sprite.FrameBounds.Y + (int)this.Sprite.Origin.Y,
                                 rect.Width, rect.Height);
        }

        public static Rectangle Intersect(Rectangle boundsA, Rectangle boundsB)
        {
            int x1 = Math.Max(boundsA.Left, boundsB.Left);
            int y1 = Math.Max(boundsA.Top, boundsB.Top);
            int x2 = Math.Min(boundsA.Right, boundsB.Right);
            int y2 = Math.Min(boundsA.Bottom, boundsB.Bottom);

            if ((x2 - x1 > 0) && (y2 - y1 > 0))
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            else
                return Rectangle.Empty;
        }

        public static bool CheckQuad(Node nodeA, Node nodeB)
        {
            if ((nodeA.Position.X + nodeA.Sprite.Width < 800 && nodeB.Position.X < 800
                || nodeB.Position.X + nodeB.Sprite.Width < 800 && nodeA.Position.X < 800) ||
                (nodeA.Position.X + nodeA.Sprite.Width >= 800 && nodeB.Position.X >= 800
                || nodeB.Position.X + nodeB.Sprite.Width >= 800 && nodeA.Position.X >= 800))
            {
                if ((nodeA.Position.Y + nodeA.Sprite.Height < 480 && nodeB.Position.Y < 480
                || nodeB.Position.Y + nodeB.Sprite.Height < 480 && nodeA.Position.Y < 480) ||
                (nodeA.Position.Y + nodeA.Sprite.Height >= 480 && nodeB.Position.Y >= 480
                || nodeB.Position.Y + nodeB.Sprite.Height >= 480 && nodeA.Position.Y >= 480))
                {

                    if (Vector2.Distance(nodeA.Position, nodeB.Position) < (nodeA.Sprite.Width / 2 + nodeB.Sprite.Width / 2))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }


        public static bool CheckCollision(Node nodeA, Node nodeB)
        {
            line = nodeB.Position - nodeA.Position;

            // we use LengthSquared to avoid a costly square-root call
            return (line.LengthSquared() <= (nodeA.Sprite.Width / 2 + nodeB.Sprite.Width / 2) * (nodeA.Sprite.Width / 2 + nodeB.Sprite.Width / 2));


            //if(CheckQuad(nodeA, nodeB))
            //{

             //if (Vector2.Distance(nodeA.Position, nodeB.Position) < (nodeA.Sprite.Width / 2 + nodeB.Sprite.Width / 2))
             //    return true;
             // else
             //    return false;

            //}
            //else
            //    return false;

            //if ((nodeA.position.X + nodeA.Sprite.Width < 800 && nodeB.position.X < 800
            //    || nodeB.position.X + nodeB.Sprite.Width < 800 && nodeA.position.X < 800) ||
            //    (nodeA.position.X + nodeA.Sprite.Width >= 800 && nodeB.position.X >= 800
            //    || nodeB.position.X + nodeB.Sprite.Width >= 800 && nodeA.position.X >= 800))
            //{
            //    if ((nodeA.position.Y + nodeA.Sprite.Height < 480 && nodeB.position.Y < 480
            //    || nodeB.position.Y + nodeB.Sprite.Height < 480 && nodeA.position.Y < 480) ||
            //    (nodeA.position.Y + nodeA.Sprite.Height >= 480 && nodeB.position.Y >= 480
            //    || nodeB.position.Y + nodeB.Sprite.Height >= 480 && nodeA.position.Y >= 480))
            //    {

            //        if (Vector2.Distance(nodeA.position, nodeB.position) < (nodeA.Sprite.Width / 2 + nodeB.Sprite.Width / 2))
            //            return true;
            //        else
            //            return false;
            //    }
            //    else
            //        return false;
            //}
            //else
            //    return false;




            //if (CheckQuad(nodeA, nodeB))
            //{

            //    Rectangle collisionRect = Intersect(nodeA.Bounds, nodeB.Bounds);

            //    if (collisionRect == Rectangle.Empty)
            //        return false;

            //    int pixelCount = collisionRect.Width * collisionRect.Height;

            //    Color[] pixelsA = new Color[pixelCount];
            //    Color[] pixelsB = new Color[pixelCount];

            //    //GraphicsDevice.Textures[0] = null;
            //    nodeA.Sprite.Texture.GetData<Color>(0, nodeA.NormalizeBounds(collisionRect), pixelsA, 0, pixelCount);
            //    nodeB.Sprite.Texture.GetData<Color>(0, nodeB.NormalizeBounds(collisionRect), pixelsB, 0, pixelCount);

            //    for (int i = 0; i < pixelCount; i++)
            //    {
            //        if (pixelsA[i].A > ALPHA_THRESHOLD && pixelsB[i].A > ALPHA_THRESHOLD)
            //            return true;
            //    }
            //    return false;
            //}
            //else
            //    return false;
        }

        #endregion
    }

    ///// <summary>
    ///// The result of a collision query
    ///// </summary>
    //struct CollisionResult
    //{
    //    /// <summary>
    //    /// How far away did the collision occur down the ray
    //    /// </summary>
    //    public float Distance;

    //    /// <summary>
    //    /// The collision "direction"
    //    /// </summary>
    //    public Vector2 Normal;

    //    /// <summary>
    //    /// What caused the collison (what the source ran into)
    //    /// </summary>
    //    //public Actor Actor;

    //    public static int Compare(CollisionResult a, CollisionResult b)
    //    {
    //        return a.Distance.CompareTo(b.Distance);
    //    }
    //}

    ///// <summary>
    ///// A code container for collision-related mathematical functions.
    ///// </summary>
    //static class Collision
    //{
    //    /// <summary>
    //    /// Determines the point of intersection between two line segments, 
    //    /// as defined by four points.
    //    /// </summary>
    //    /// <param name="a">The first point on the first line segment.</param>
    //    /// <param name="b">The second point on the first line segment.</param>
    //    /// <param name="c">The first point on the second line segment.</param>
    //    /// <param name="d">The second point on the second line segment.</param>
    //    /// <param name="point">The output value with the interesection, if any.</param>
    //    /// <remarks>The output parameter "point" is only valid
    //    /// when the return value is true.</remarks>
    //    /// <returns>True if intersecting, false otherwise.</returns>
    //    public static bool LineLineIntersect(Vector2 a, Vector2 b, Vector2 c,
    //        Vector2 d, out Vector2 point)
    //    {
    //        point = Vector2.Zero;

    //        double r, s;
    //        double denominator = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);

    //        // If the denominator in above is zero, AB & CD are colinear
    //        if (denominator == 0)
    //        {
    //            return false;
    //        }

    //        double numeratorR = (a.Y - c.Y) * (d.X - c.X) - (a.X - c.X) * (d.Y - c.Y);
    //        r = numeratorR / denominator;

    //        double numeratorS = (a.Y - c.Y) * (b.X - a.X) - (a.X - c.X) * (b.Y - a.Y);
    //        s = numeratorS / denominator;

    //        // non-intersecting
    //        if (r < 0 || r > 1 || s < 0 || s > 1)
    //        {
    //            return false;
    //        }

    //        // find intersection point
    //        point.X = (float)(a.X + (r * (b.X - a.X)));
    //        point.Y = (float)(a.Y + (r * (b.Y - a.Y)));

    //        return true;
    //    }


    //    /// <summary>
    //    /// Determine if two circles intersect or contain each other.
    //    /// </summary>
    //    /// <param name="center1">The center of the first circle.</param>
    //    /// <param name="radius1">The radius of the first circle.</param>
    //    /// <param name="center2">The center of the second circle.</param>
    //    /// <param name="radius2">The radius of the second circle.</param>
    //    /// <returns>True if the circles intersect or contain one another.</returns>
    //    public static bool CircleCircleIntersect(Vector2 center1, float radius1,
    //        Vector2 center2, float radius2)
    //    {
    //        Vector2 line = center2 - center1;
    //        // we use LengthSquared to avoid a costly square-root call
    //        return (line.LengthSquared() <= (radius1 + radius2) * (radius1 + radius2));
    //    }


    //    /// <summary>
    //    /// Data defining a circle/line collision result.
    //    /// </summary>
    //    public struct CircleLineCollisionResult
    //    {
    //        public bool Collision;
    //        public Vector2 Point;
    //        public Vector2 Normal;
    //        public float Distance;
    //    }


    //    /// <summary>
    //    /// Determines if a circle and line segment intersect, and if so, how they do.
    //    /// </summary>
    //    /// <param name="position">The center of the circle.</param>
    //    /// <param name="radius">The radius of the circle.</param>
    //    /// <param name="lineStart">The first point on the line segment.</param>
    //    /// <param name="lineEnd">The second point on the line segment.</param>
    //    /// <param name="result">The result data for the collision.</param>
    //    /// <returns>True if a collision occurs, provided for convenience.</returns>
    //    public static bool CircleLineCollide(Vector2 center, float radius,
    //        Vector2 lineStart, Vector2 lineEnd, ref CircleLineCollisionResult result)
    //    {
    //        Vector2 AC = center - lineStart;
    //        Vector2 AB = lineEnd - lineStart;
    //        float ab2 = AB.LengthSquared();
    //        if (ab2 <= 0f)
    //        {
    //            return false;
    //        }
    //        float acab = Vector2.Dot(AC, AB);
    //        float t = acab / ab2;

    //        if (t < 0.0f)
    //            t = 0.0f;
    //        else if (t > 1.0f)
    //            t = 1.0f;

    //        result.Point = lineStart + t * AB;
    //        result.Normal = center - result.Point;

    //        float h2 = result.Normal.LengthSquared();
    //        float r2 = radius * radius;

    //        if (h2 > r2)
    //        {
    //            result.Collision = false;
    //        }
    //        else
    //        {
    //            result.Normal.Normalize();
    //            result.Distance = (radius - (center - result.Point).Length());
    //            result.Collision = true;
    //        }

    //        return result.Collision;
    //    }
    //}
}
