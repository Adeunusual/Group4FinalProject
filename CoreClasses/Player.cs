using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GhostHunter
{
    internal class Player
    {
        public Vector2 position = new Vector2(100, 100); // player starts at screen top-left
        private int speed;
        Texture2D playerSprite;
        private int screenWidth;
        private int screenHeight;

        // Constructor to initialize player properties
        public Player(Texture2D playerSprite, Vector2 startPosition, int screenWidth, int screenHeight)
        {
            this.playerSprite = playerSprite;
            this.position = startPosition;
            this.speed = 2;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        // Bounds property for collision detection
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, playerSprite.Width, playerSprite.Height);
            }
        }

        // Update method to handle player movement
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 movement = Vector2.Zero;

            // Handle player movement based on keyboard input
            if (keyboardState.IsKeyDown(Keys.Up))
                movement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.Down))
                movement.Y += 1;
            if (keyboardState.IsKeyDown(Keys.Left))
                movement.X -= 1;
            if (keyboardState.IsKeyDown(Keys.Right))
                movement.X += 1;

            // Normalize movement vector for consistent speed
            if (movement != Vector2.Zero)
                movement.Normalize();

            // Update the position based on movement and speed
            position += movement * speed;

            // Keep the player within screen boundaries
            position = new Vector2(
                MathHelper.Clamp(position.X, 0, screenWidth - playerSprite.Width),
                MathHelper.Clamp(position.Y, 0, screenHeight - playerSprite.Height)
            );
        }

        // Draw method to render the player
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerSprite, position, Color.White);
        }
    }
}