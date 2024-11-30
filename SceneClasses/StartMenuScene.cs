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
		private SpriteFont highlightFont; // Font for the heading
		private SpriteFont normalFont; // Font for the menu options
		private Texture2D backgroundTexture; // Background image
		private Texture2D menuBoxTexture; // Texture for menu box
		private SoundEffectInstance backgroundInstance; // Instance of the background music
		private int selectedIndex = 0; // Tracks the currently selected menu option
		private string[] menuOptions = { "Play Game", "Help", "Scores", "About", "Quit" }; // Menu options

		private KeyboardState previousKeyboardState; // Tracks the previous keyboard state

		public StartMenuScene(Game1 game) : base(game)
        {
        }

		// Load content specific to the Start Menu Scene
		// Load content specific to the Start Menu Scene
		public override void LoadContent()
		{
			// Load fonts for heading and menu options
			highlightFont = _game.Content.Load<SpriteFont>("highlightFont");
			normalFont = _game.Content.Load<SpriteFont>("normalFont");

			// Load the background texture for the start menu
			backgroundTexture = _game.Content.Load<Texture2D>("startMenu-Bg");

			// Create a solid color texture for the menu box
			menuBoxTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
			menuBoxTexture.SetData(new[] { Color.DarkSlateGray }); // Set the color of the texture to DarkSlateGray

			// Load background music and create an instance of the sound
			var backgroundMusic = _game.Content.Load<SoundEffect>("startMenu_sound");
			backgroundInstance = backgroundMusic.CreateInstance();
			backgroundInstance.IsLooped = true; // Enable looping to keep the background music playing
			backgroundInstance.Volume = 0.7f; // Set the volume to a moderate level
			backgroundInstance.Play(); // Start playing the background music
		}

		// Update method to handle player input and change scenes accordingly
		public override void Update(GameTime gameTime)
		{
			var keyboardState = Keyboard.GetState(); // Get the current state of the keyboard

			// Navigate through menu options using Up and Down arrow keys
			if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
			{
				// Move up through the menu options and wrap around if needed
				selectedIndex = (selectedIndex - 1 + menuOptions.Length) % menuOptions.Length;
			}
			else if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
			{
				// Move down through the menu options and wrap around if needed
				selectedIndex = (selectedIndex + 1) % menuOptions.Length;
			}

			// Confirm the selected option by pressing Enter
			if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
			{
				// Stop the background music before changing scenes
				backgroundInstance.Stop();

				// Handle the selected menu option
				switch (selectedIndex)
				{
					case 0: // Play Game
						_game.ChangeScene(new LevelSelectionScene(_game)); // Navigate to the Level Selection Scene
						break;
					case 1: // Help
						_game.ChangeScene(new HelpScene(_game)); // Navigate to the Help Scene
						break;
					case 2: // Scores
						_game.ChangeScene(new ScoresScene(_game)); // Navigate to the Scores Scene
						break;
					case 3: // About
						_game.ChangeScene(new AboutScene(_game)); // Navigate to the About Scene
						break;
					case 4: // Quit
						_game.Exit(); // Exit the game
						break;
				}
			}
			// Save the current keyboard state for the next update cycle
			previousKeyboardState = keyboardState;
		}

		// Draw method to display the menu and background
		public override void Draw(GameTime gameTime)
        {
			_game.SpriteBatch.Begin(); // Begin drawing

			// Draw the background image at the origin (top-left corner)
			_game.SpriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

			// Draw the "Game Menu" heading at the top of the screen, centered horizontally
			string headingText = "Game Menu";
			Vector2 headingSize = highlightFont.MeasureString(headingText);
			Vector2 headingPosition = new Vector2(
				(_game.GraphicsDevice.Viewport.Width - headingSize.X) / 2,
				50 // Y-position of the heading
			);
			_game.SpriteBatch.DrawString(highlightFont, headingText, headingPosition, Color.White);

			// Draw the menu box around the options
			Rectangle menuBox = new Rectangle(
				150, // X-position of the box
				150, // Y-position of the box
				_game.GraphicsDevice.Viewport.Width - 300, // Width of the box
				menuOptions.Length * 50 + 20 // Height of the box, based on the number of options
			);
			_game.SpriteBatch.Draw(menuBoxTexture, menuBox, Color.DarkSlateGray);

			// Draw each menu option, highlighting the currently selected one
			for (int i = 0; i < menuOptions.Length; i++)
			{
				Color color = (i == selectedIndex) ? Color.Yellow : Color.White; // Highlight the selected option in yellow
				Vector2 position = new Vector2(
					menuBox.X + (menuBox.Width - normalFont.MeasureString(menuOptions[i]).X) / 2, // Center the option text horizontally within the menu box
					menuBox.Y + 10 + i * 50 // Y-position for each menu option, with some padding
				);
				_game.SpriteBatch.DrawString(normalFont, menuOptions[i], position, color); // Draw the option
			}

			_game.SpriteBatch.End(); // End drawing
		}
    }
}
