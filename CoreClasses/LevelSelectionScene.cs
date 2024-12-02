/*
* LevelSelectionScene.cs
* 
* Description: This file manages the level selection screen, allowing the player to choose from different difficulty levels (Easy, Medium, Hard).
*
* Revision History:
* Joonryeon Jang: 15th, November, 2024 - Created logic for level selection and transitioning to the gameplay scene.
*/

using GhostHunter;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class LevelSelectionScene : GameScene
{
	//declaration of Variables
	private SpriteFont highlightFont;
	private SpriteFont normalFont;
	private Texture2D backgroundTexture;
	private int selectedIndex = 0;
	private string[] levelOptions = { "Easy", "Medium", "Hard" };

	private KeyboardState previousKeyboardState;
	private bool isLevelSelected = false; // Flag to ensure level selection is handled
	private float waitTime = 0.5f; // Delay time to prevent immediate key press issues

	public LevelSelectionScene(Game1 game) : base(game)
	{
	}

	// Load fonts and background texture for the level selection scene
	public override void LoadContent()
	{
		highlightFont = _game.Content.Load<SpriteFont>("highlightFont");
		normalFont = _game.Content.Load<SpriteFont>("normalFont");
		backgroundTexture = _game.Content.Load<Texture2D>("levelSelection-Bg");
	}

	// Update method to handle user input and level selection
	public override void Update(GameTime gameTime)
	{
		// Decrease wait time to introduce a delay before taking input
		if (waitTime > 0)
		{
			waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			return;
		}

		var keyboardState = Keyboard.GetState();

		// Allow level selection until the level is explicitly chosen
		if (!isLevelSelected)
		{
			// Navigate level options with Up and Down keys
			if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
			{
				selectedIndex = (selectedIndex - 1 + levelOptions.Length) % levelOptions.Length;
			}
			else if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
			{
				selectedIndex = (selectedIndex + 1) % levelOptions.Length;
			}

			// Confirm selection with Enter key
			if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
			{
				isLevelSelected = true; // Set flag to indicate the level is selected
				_game.ChangeScene(new GamePlayScene(_game, levelOptions[selectedIndex])); // Proceed to the selected level
			}

			previousKeyboardState = keyboardState;
		}
	}

	// Draw method to render level selection screen
	public override void Draw(GameTime gameTime)
	{
		_game.SpriteBatch.Begin();

		// Draw the background to cover the entire screen
		Rectangle fullScreenRectangle = new Rectangle(
			0,
			0,
			_game.GraphicsDevice.Viewport.Width,
			_game.GraphicsDevice.Viewport.Height
		);
		_game.SpriteBatch.Draw(backgroundTexture, fullScreenRectangle, Color.White);

		// Draw the "Select Level" heading
		string headingText = "Select Level";
		Vector2 headingSize = highlightFont.MeasureString(headingText);
		Vector2 headingPosition = new Vector2(
			(_game.GraphicsDevice.Viewport.Width - headingSize.X) / 2,
			50
		);
		_game.SpriteBatch.DrawString(highlightFont, headingText, headingPosition, Color.White);

		// Draw the level options with appropriate highlighting
		for (int i = 0; i < levelOptions.Length; i++)
		{
			Color color = (i == selectedIndex) ? Color.Yellow : Color.White;
			Vector2 position = new Vector2(
				(_game.GraphicsDevice.Viewport.Width - normalFont.MeasureString(levelOptions[i]).X) / 2,
				150 + i * 50
			);
			_game.SpriteBatch.DrawString(normalFont, levelOptions[i], position, color);
		}

		_game.SpriteBatch.End();
	}
}
