using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Paddle
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private Vector2 position;

        private int initialXPositionForPlayer1 = 0;
        private int initialYPositionForPlayer1 = 180;

        private int initialXPositionForPlayer2 = 715;
        private int initialYPositionForPlayer2 = 180;

        public static readonly int screenHeight = 360;

        /// <summary>
        /// The color to blend with the paddle
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The constructor for the paddles and its positions
        /// </summary>
        /// <param name="isPlayer1">If it is Player1</param>
        public Paddle(bool isPlayer1)
        {
            if (isPlayer1)
            {
                position = new Vector2(initialXPositionForPlayer1, initialYPositionForPlayer1);
            }
            else
            {
                position = new Vector2(initialXPositionForPlayer2, initialYPositionForPlayer2);
            }
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("paddle");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            //Update keyabord state
            keyboardState = Keyboard.GetState();

            //Player 1 controls with gamepad
            gamePadState = GamePad.GetState(PlayerIndex.One);
            position.Y += gamePadState.ThumbSticks.Left.Y * 5;

            //Player 2 contols with arrow keys
            if (keyboardState.IsKeyDown(Keys.Up)) position.Y -= 5;
            else if (keyboardState.IsKeyDown(Keys.Down)) position.Y += 5;

            position.Y = MathHelper.Clamp(position.Y, 0, screenHeight - texture.Height);
        }
    }
}
