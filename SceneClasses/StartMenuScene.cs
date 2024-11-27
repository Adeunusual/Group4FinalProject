/*
* StartMenuScene.cs
* 
* Description: Implements the start menu for the GhostHunter game, allowing players to navigate between different options including Play Game, Help, Scores, About, and Quit.
* 
* Revision History:
* Faruq A. Atanda : 15th November, 2024
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Xml.Linq;

namespace GhostHunter
{
    public class StartMenuScene : GameScene
    {
        //declaration of variables

        public StartMenuScene(Game1 game) : base(game)
        {
        }

        // Load content specific to the Start Menu Scene
        public override void LoadContent()
        {

        }

        // Update method to handle player input and change scenes accordingly
        public override void Update(GameTime gameTime)
        {

        }

        // Draw method to display the menu and background
        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin(); // Begin drawing


            _game.SpriteBatch.End(); // End drawing
        }
    }
}
