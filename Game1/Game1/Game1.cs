using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        private int player1Score = 0;
        private int player2Score = 0;

        private Texture2D backgroundImage;

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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!gameLogic.IsGameOver)
            {
                player1Paddle.Update(gameTime);
                player2Paddle.Update(gameTime);
                ball.Update(gameTime, player1Paddle, player2Paddle);
                gameLogic.Update(gameTime);

                /*
                bool player1JustScored = false;
                bool player2JustScored = false;

                if (ball.Position.X < 0)
                {
                    player2JustScored = true;
                }
                else if (ball.Position.X + ball.Width > GraphicsDevice.Viewport.Width)
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

                    gameLogic.ResetBall();
                }
                */
            }
            else if (gameLogic.IsGameOver == true) Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // TODO: Add your drawing code here
            if (!gameLogic.IsGameOver)
            {
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
                spriteBatch.DrawString(spriteFont, $"How long can you keep the ball in play? {gameTime.TotalGameTime:c}", new Vector2(10, 450), Color.White);
                
            }

            //cube.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}