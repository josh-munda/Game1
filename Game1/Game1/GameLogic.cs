using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class GameLogic
    {
        private Paddle paddle1;
        private Paddle paddle2;

        private Ball ball;

        private int player1Score;
        private int player2Score;

        public bool IsGameOver;

        private GraphicsDevice graphicsDevice;

        /// <summary>
        /// Constructor to initialize properties
        /// </summary>
        public GameLogic(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            //This is player 1 and will be controlled by game pad
            paddle1 = new Paddle(true);
            //This is player 2 and will be controlled by keyboard
            paddle2 = new Paddle(false);
            ball = new Ball();
            player1Score = 0;
            player2Score = 0;
            IsGameOver = false;
        }

        /// <summary>
        /// Loads the paddles and ball
        /// </summary>
        /// <param name="content">The content manager to use</param>
        public void LoadContent(ContentManager content)
        {
            paddle1.LoadContent(content);
            paddle2.LoadContent(content);
            ball.LoadContent(content);
        }

        /// <summary>
        /// Updates the scores and the paddles and ball
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            paddle1.Update(gameTime);
            paddle2.Update(gameTime);

            ball.Update(gameTime, paddle1, paddle2);

            
            bool player1JustScored = false;
            bool player2JustScored = false;

            if (ball.Position.X < 0)
            {
                player2JustScored = true;
            }
            else if (ball.Position.X + ball.Width > graphicsDevice.Viewport.Width)
            {
                player1JustScored = true;
            }

            if (player1JustScored || player2JustScored)
            {
                if (player1JustScored)
                {
                    player1Score++;
                }
                else if (player2JustScored)
                {
                    player2Score++;
                }

                ResetBall();
            }


            if (player1Score >= 5 || player2Score >= 5)
            {
                IsGameOver = true;
            }
            
            
        }

        /// <summary>
        /// Draw the necessary sprites
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw the sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            paddle1.Draw(spriteBatch);
            paddle2.Draw(spriteBatch);

            //ball.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Reset the ball to the middle of the screen
        /// </summary>
        public void ResetBall()
        {
            ball.Position = new Vector2(graphicsDevice.Viewport.Width / 2 - ball.Width / 2, graphicsDevice.Viewport.Height / 2 - ball.Height / 2);
            ball.Velocity = new Vector2(5, 5);
        }
    }
}
