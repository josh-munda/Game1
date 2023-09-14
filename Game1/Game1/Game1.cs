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
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!gameLogic.IsGameOver)
            {
                player1Paddle.Update(gameTime);
                player2Paddle.Update(gameTime);
                ball.Update(gameTime, player1Paddle, player2Paddle);
                gameLogic.Update(gameTime);

                if (ball.Position.X < 0)
                {
                    player2Score++;
                    gameLogic.ResetBall();
                }
                else if (ball.Position.X + ball.Width > GraphicsDevice.Viewport.Width)
                {
                    player1Score++;
                    gameLogic.ResetBall();
                }
            }
            else Exit();

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
                player1Paddle.Draw(spriteBatch);
                player2Paddle.Draw(spriteBatch);
                ball.Draw(spriteBatch);
                gameLogic.Draw(spriteBatch);
                spriteBatch.DrawString(spriteFont, $"Player 1: {player1Score}", new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(spriteFont, $"Player 2: {player2Score}", new Vector2(600, 10), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}