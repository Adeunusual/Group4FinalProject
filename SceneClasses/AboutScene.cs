/*
* AboutScene.cs
* 
* Description: This file manages the "About" scene, displaying information about the game and its developers, including a visual image.
*
* Revision History:
* Maria Fernanda: 15th, November, 2024 - Implemented loading and rendering the About image.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostHunter
{
	public class AboutScene : GameScene
	{
		private Texture2D AboutSceneImage; // Texture for the AboutScene image

		// Constructor to initialize the About scene
		public AboutScene(Game1 game) : base(game)
		{
		}

		// Load content for the About scene
		public override void LoadContent()
		{
			// Load the About image
			AboutSceneImage = _game.Content.Load<Texture2D>("AboutSceneImg"); // Load About image
		}

		// Draw method to render the About scene
		public override void Draw(GameTime gameTime)
		{
			_game.SpriteBatch.Begin();

			// Clear the screen with a background color
			_game.GraphicsDevice.Clear(Color.CornflowerBlue);

			// Draw the about image to fill the entire screen
			_game.SpriteBatch.Draw(AboutSceneImage, new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);

			_game.SpriteBatch.End();
		}
	}
}
