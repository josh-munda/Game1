using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Paddle player1Paddle;
        private Paddle player2Paddle;

        private Ball ball;

        private GameLogic gameLogic;

        private SpriteFont spriteFont;

        //private int player1Score = 0;
        //private int player2Score = 0;

        private Texture2D backgroundImage;

        private TimeSpan gameTimeElapsed = TimeSpan.Zero;
        private bool gameRunning = true;

        private int screenWidth = 800;
        private int screenHeight = 360;

        //Cube cube;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            player1Paddle = new Paddle(true);
            player2Paddle = new Paddle(false);

            ball = new Ball();

            gameLogic = new GameLogic(GraphicsDevice);

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player1Paddle.LoadContent(Content);
            player2Paddle.LoadContent(Content);
            ball.LoadContent(Content);
            gameLogic.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("consolas");
            backgroundImage = Content.Load<Texture2D>("background");

            //cube = new Cube(this);
        }

        protected override void Update(GameTime gameTime)
        {
            //cube.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            if (gameRunning)
            {
                player1Paddle.Update(gameTime);
                player2Paddle.Update(gameTime);
                ball.Update(gameTime, player1Paddle, player2Paddle);
                gameLogic.Update(gameTime);

                if (ball.Position.X < 0 || ball.Position.X + ball.Width > GraphicsDevice.Viewport.Width)
                {
                    gameRunning = false;

                    gameTimeElapsed = gameTime.ElapsedGameTime;
                    ShowMessageBox($"Game Over!\nTime Played: {gameTime.TotalGameTime.TotalSeconds:F2} seconds\nThanks for playing!");
                }
            }

            
            //else if (gameLogic.IsGameOver == true) Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // TODO: Add your drawing code here
            
                // Get the center position of the screen
                Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

                // Define a scale factor for the image (e.g., 0.5 for 50% scale)
                float scale = 1.0f;

                // Calculate the position to draw the image at the center
                Vector2 imagePosition = screenCenter - new Vector2(backgroundImage.Width * scale / 2, backgroundImage.Height * scale / 2);

                // Draw the background image with the specified scale and position
                spriteBatch.Draw(backgroundImage, imagePosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                player1Paddle.Draw(spriteBatch);
                player2Paddle.Draw(spriteBatch);
                ball.Draw(gameTime, spriteBatch);
                gameLogic.Draw(spriteBatch);
                spriteBatch.DrawString(spriteFont, $"How long can you keep the ball in play? ", new Vector2(120, 330), Color.White);
                

            //cube.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ShowMessageBox(string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, "Game Over", buttons);

            if(result == DialogResult.OK)
            {
                Exit();
            }
        }
        
    }
}