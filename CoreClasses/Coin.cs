/*
* Coin.cs
* 
* Description: This class represents a coin object in the game, including its properties and rendering logic.
*
* Revision History:
* Chiayin Yang: 24th - "Added HelpScene and Coin classes with basic structure."
* Chiayin Yang: 25th - "Added drawing logic for Coin entity."
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostHunter
{
    public class Coin
    {       
        //declaration of variables
        private Texture2D texture;
        public Vector2 Position;

        public Coin(Texture2D texture, Vector2 startPosition)
        {
            // Set the texture of the coin and initialize its position in the maze
            this.texture = texture;
            Position = startPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Render the coin at its current position with a yellow color
            spriteBatch.Draw(texture, Position, Color.Yellow);
        }
    }
}
