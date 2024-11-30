/*
* GameOverScene.cs
* 
* Description: Manages the Game Over scene, displaying whether the player won or lost, along with their final score and time taken. Handles transitioning back to the start menu.
*
* Revision History:
* Maria Fernanda: 15th, November, 2024 - Implemented Game Over scene, score updates, and navigation back to the Start Menu.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GhostHunter
{
	public class GameOverScene : GameScene
	{
		//declaration of Variables
		private bool playerWon;
		private SpriteFont normalFont;
		private int finalScore;
		private float timeUsed;
		private string difficultyLevel;

		// Constructor to initialize the Game Over scene with relevant game results
		public GameOverScene(Game1 game, bool won, int score = 0, float time = 0, string level = "Easy") : base(game)
		{
			playerWon = won;
			finalScore = score;
			timeUsed = time;
			difficultyLevel = level;
		}

		// Load content for the Game Over scene, such as fonts and background music
		public override void LoadContent()
		{
			normalFont = _game.Content.Load<SpriteFont>("normalFont");

			// Load the game over song and play it
			var gameOverSong = _game.Content.Load<Song>("gameOverSound");
			MediaPlayer.Play(gameOverSong);
			MediaPlayer.IsRepeating = false; // Play the song once, not repeating
			MediaPlayer.Volume = 0.7f; // Set the volume to a reasonable level


		}

		// Update method to check for input and handle returning to the start menu
		public override void Update(GameTime gameTime)
		{
			
		}

		// Draw method to render the Game Over scene, showing the final score and messages
		public override void Draw(GameTime gameTime)
		{
			_game.SpriteBatch.Begin();



			_game.SpriteBatch.End();
		}
	}
}
