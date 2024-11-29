/*
 * HelpScene.cs
 * 
 * Description: Displays the help page with instructions and controls for the player.
 * 
 * Revision History:
 * Chiayin Yang : 24th November, 2024 - "Added HelpScene and Coin classes with basic structure."
 * Chiayin Yang : 25th November, 2024 - "Implemented LoadContent for HelpScene with Help instructions image."
 * Chiayin Yang : 29th November, 2024 - "Updated HelpScene with additional game instructions and player guidance."
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostHunter
{
    public class HelpScene : GameScene
    {
        private Texture2D HelpSceneImage; // Texture for the HelpScene image

        public HelpScene(Game1 game) : base(game)
        {
        }

        public override void LoadContent()
        {
            // Load the help image
            HelpSceneImage = _game.Content.Load<Texture2D>("HelpSceneImg"); // load Help Image
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();

            // Clear the screen with a background color
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the help image to fill the entire screen
            _game.SpriteBatch.Draw(HelpSceneImage, new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);

            _game.SpriteBatch.End();
        }
    }
}