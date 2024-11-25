/*
 * HelpScene.cs
 * 
 * Description: Displays the help page with instructions and controls for the player.
 * 
 * Revision History:
 * Chiayin Yang : 24th November, 2024 - "Added HelpScene and Coin classes with basic structure."
 * Chiayin Yang : 25th November, 2024 - "Implemented LoadContent for HelpScene with Help instructions image."
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace GhostHunter
{
    public class HelpScene : GameScene
    {
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
        }
    }
}

