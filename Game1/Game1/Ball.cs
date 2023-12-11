using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Ball
    {
        private Texture2D texture;

        private double animationTimer;

        private short animationFrame = 1;

        private float speedIncrement = 1.1f;

        private float maxSpeed = 10.0f;

        /// <summary>
        /// The height of the screen
        /// </summary>
        public static readonly int screenHeight = 360;

        /// <summary>
        /// The velocity of the ball
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// The width of the ball
        /// </summary>
        public int Width = 20;

        /// <summary>
        /// The height of the ball
        /// </summary>
        public int Height = 20;

        /// <summary>
        /// The position of the ball
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The constructor for the ball's position and velocity
        /// </summary>
        public Ball()
        {
            Position = new Vector2(180, 150);
            Velocity = new Vector2(3, 3);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ball");
        }


        public void Update(GameTime gameTime, Paddle paddle1, Paddle paddle2)
        {
            //Update the position based on the velocity
            Position += Velocity;

            //Check for collision with the top and bottom of the screen
            if(Position.Y < 0 || Position.Y + texture.Height > screenHeight)
            {
                Velocity.Y *= -1;
            }

            //Reverse ball's horizontal direction when collision with paddle
            if(IsCollisionWithPaddle(paddle1) || IsCollisionWithPaddle(paddle2))
            {
                Velocity.X *= -1;

                Velocity *= speedIncrement;

                Velocity = Vector2.Clamp(Velocity, new Vector2(-maxSpeed, -maxSpeed), new Vector2(maxSpeed, maxSpeed));
            }

            //Position.Y = MathHelper.Clamp(Position.Y, 0, screenHeight - texture.Height);
        }

        /// <summary>
        /// Draw the ball on the screen
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to draw the sprite</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            
            if(animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 1;
                animationTimer -= 0.3;
            }
            
            var source = new Rectangle(animationFrame * 1, 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }


        public bool IsCollisionWithPaddle(Paddle paddle)
        {
            Rectangle ballBounds = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                texture.Width,
                texture.Height
                );

            Rectangle paddleBounds = new Rectangle(
                (int)paddle.Position.X,
                (int)paddle.Position.Y,
                texture.Width,
                texture.Height
                );

            return ballBounds.Intersects(paddleBounds);
        }

        public void Initialize()
        {
            Position = new Vector2(180, 150);
            Velocity = new Vector2(3, 3);
        }
    }
}
