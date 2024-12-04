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

			// If player won, update the score for the specific difficulty level
			if (playerWon)
			{
				if (_game.HighScores.ContainsKey(difficultyLevel) && _game.HighScores[difficultyLevel] > timeUsed)
				{
					_game.HighScores[difficultyLevel] = timeUsed;

                    // Save new high scores to file
                    _game.ScoresScene.SaveHighScores();
                }
			}
		}

		// Update method to check for input and handle returning to the start menu
		public override void Update(GameTime gameTime)
		{
			var keyboardState = Keyboard.GetState();
			if (keyboardState.IsKeyDown(Keys.Enter))
			{
				MediaPlayer.Stop(); // Stop the song when leaving the Game Over scene
				_game.ChangeScene(new StartMenuScene(_game));
			}
		}

		// Draw method to render the Game Over scene, showing the final score and messages
		public override void Draw(GameTime gameTime)
		{
			_game.SpriteBatch.Begin();

			// Display win or loss message
			string message = playerWon ? "You Won!" : "Game Over";
			Vector2 messageSize = normalFont.MeasureString(message);
			Vector2 messagePosition = new Vector2(
				(_game.GraphicsDevice.Viewport.Width - messageSize.X) / 2,
				100
			);
			_game.SpriteBatch.DrawString(normalFont, message, messagePosition, Color.White);

			if (playerWon)
			{
				// Display the time taken to win the game
				string timeText = $"Time: {(int)timeUsed} seconds";
				Vector2 timePosition = new Vector2((_game.GraphicsDevice.Viewport.Width - normalFont.MeasureString(timeText).X) / 2, 200);
				_game.SpriteBatch.DrawString(normalFont, timeText, timePosition, Color.White);
			}
			else
			{
				// Display score and time if player lost
				string scoreText = $"Score: {finalScore}";
				string timeText = $"Time: {(int)timeUsed} seconds";

				Vector2 scorePosition = new Vector2((_game.GraphicsDevice.Viewport.Width - normalFont.MeasureString(scoreText).X) / 2, 200);
				Vector2 timePosition = new Vector2((_game.GraphicsDevice.Viewport.Width - normalFont.MeasureString(timeText).X) / 2, 250);

				_game.SpriteBatch.DrawString(normalFont, scoreText, scorePosition, Color.White);
				_game.SpriteBatch.DrawString(normalFont, timeText, timePosition, Color.White);
			}

			// Display instruction to restart or return to start menu
			string instructionText = "Press Enter to return to the main menu";
			Vector2 instructionPosition = new Vector2(
				(_game.GraphicsDevice.Viewport.Width - normalFont.MeasureString(instructionText).X) / 2,
				350
			);
			_game.SpriteBatch.DrawString(normalFont, instructionText, instructionPosition, Color.White);

			_game.SpriteBatch.End();
		}
	}
}
