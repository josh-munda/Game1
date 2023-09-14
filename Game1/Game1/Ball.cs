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

        private Vector2 position;

        private Vector2 velocity;

        /// <summary>
        /// The constructor for the ball's position and velocity
        /// </summary>
        public Ball()
        {
            position = new Vector2(180, 150);
            velocity = new Vector2(5, 5);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ball");
        }


        public void Update(GameTime gameTime)
        {
            //Update the position based on the velocity
            position += velocity;

            //Check for collision with the top and bottom of the screen
            if(position.Y < 0 || position.Y + texture.Height > 360)
            {
                velocity.Y *= -1;
            }
        }

        /// <summary>
        /// Draw the ball on the screen
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to draw the sprite</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
