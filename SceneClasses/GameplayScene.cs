/*
* GamePlayScene.cs
* 
* Description: Manages the main gameplay logic including player, enemies, coins, collisions, sounds, and game state transitions.
*
* Revision History:
* Faruq A. Atanda: 15th November, 2024 - Implemented core game logic, including player movement, enemy behaviors, collisions, and scene transitions.
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GhostHunter
{
    public class GamePlayScene : GameScene
    {
		//declartion of variables
		private string difficultyLevel;
		private Player player;
		private List<Enemy> enemies;
		private List<Coin> coins;
		private int playerLives = 3;
		private float countdownTimer = 60f;
		private SpriteFont normalFont;
		private float collisionCooldown = 1.5f; // Time in seconds to avoid multiple collisions
		private float timeSinceLastCollision = 0f;
		private int coinsCollected = 0; // Track coins collected
		private SoundEffectInstance gamePlay_sound; // Instance of the background music
		private SoundEffect catchingCoinSound; // Instance of the background music
		private SoundEffect collisionSound; // Add this line at the top with other fields

		// Maze representation: 0 = Path, 1 = Wall
		private int[,] maze = new int[,]
		{
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
			{ 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
			{ 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
			{ 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
			{ 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
			{ 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
			{ 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			{ 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			{ 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1 },
			{ 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
			{ 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
			{ 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
			{ 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
			{ 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
			{ 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
			{ 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
		};

		private Texture2D wallTexture;
		private Texture2D pathTexture;
		private Random random;

		public GamePlayScene(Game1 game, string level) : base(game)
        {
			difficultyLevel = level;
			random = new Random();
		}

		public override void LoadContent()
		{
			normalFont = _game.Content.Load<SpriteFont>("normalFont");
			wallTexture = _game.Content.Load<Texture2D>("wallTexture");
			pathTexture = _game.Content.Load<Texture2D>("pathTexture");
			catchingCoinSound = _game.Content.Load<SoundEffect>("catchingCoin_sound"); // Load coin collection sound
			collisionSound = _game.Content.Load<SoundEffect>("collision_sound"); // Load the collision sound

			// Load background music and create an instance
			var backgroundMusic = _game.Content.Load<SoundEffect>("gamePlay_sound");
			gamePlay_sound = backgroundMusic.CreateInstance();
			gamePlay_sound.IsLooped = true; // Enable looping
			gamePlay_sound.Volume = 0.7f; // Set volume
			gamePlay_sound.Play(); // Play the sound

			// Initialize player, enemies, and coins based on difficulty level
			player = new Player(_game.Content.Load<Texture2D>("player"), new Vector2(64, 64), maze); // Start at (64, 64)
			enemies = new List<Enemy>();
			coins = new List<Coin>();

			int enemyCount = 0;
			int coinCount = 0;
			float enemySpeed = 20f; // Default speed for enemies guarding coins

			switch (difficultyLevel)
			{
				case "Easy":
					enemyCount = 3;
					coinCount = 5;
					enemySpeed = 20f;
					break;
				case "Medium":
					enemyCount = 5;
					coinCount = 8;
					enemySpeed = 30f;
					break;
				case "Hard":
					enemyCount = 6;
					coinCount = 10;
					enemySpeed = 40f;
					break;
			}

			// Add coins with valid initial positions within the maze paths
			for (int i = 0; i < coinCount; i++)
			{
				Vector2 coinPosition = GetRandomPositionWithinMaze();
				coins.Add(new Coin(_game.Content.Load<Texture2D>("coin"), coinPosition));
				// If there are still enemies available, assign an enemy to guard this coin
				if (enemies.Count < enemyCount)
				{
					bool moveHorizontally = random.Next(0, 2) == 0; // Randomly decide if the enemy should move horizontally or vertically
					enemies.Add(new Enemy(_game.Content.Load<Texture2D>("sprite_1"), coinPosition, enemySpeed, maze, moveHorizontally));
				}
			}

			// Add additional enemies randomly in the maze, if there are more enemies than coins
			for (int i = coinCount; i < enemyCount; i++)
			{
				Vector2 enemyPosition = GetRandomPositionWithinMaze();
				enemies.Add(new Enemy(_game.Content.Load<Texture2D>("sprite_1"), enemyPosition, enemySpeed, maze, random.Next(0, 2) == 0));
			}
		}

		public override void Update(GameTime gameTime)
		{
			var keyboardState = Keyboard.GetState();
			// Update player position with maze boundaries
			player.Update(gameTime, keyboardState);

			// Check if the Escape key is pressed to go back to the Start Menu
			if (keyboardState.IsKeyDown(Keys.Escape))
			{
				gamePlay_sound.Stop(); // Stop gameplay sound using a dedicated method
				_game.ChangeScene(new StartMenuScene(_game));
			}


			// Update enemies
			foreach (var enemy in enemies)
			{
				enemy.Update(gameTime);
			}

			// Update cooldown timer for collision
			timeSinceLastCollision += (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Check for collisions between player and coins and play sound if a coin is collected
			if (CollisionHandler.CheckCoinCollisions(player, coins, ref coinsCollected))
			{
				catchingCoinSound.Play(); // Play sound effect when collecting a coin
			}

			// Check for collisions between player and enemies, with a cooldown to avoid multiple instant hits
			if (timeSinceLastCollision >= collisionCooldown && CollisionHandler.CheckEnemyCollisions(player, enemies))
			{
				playerLives--;
				timeSinceLastCollision = 0f; // Reset the cooldown timer

				collisionSound.Play(); // Play collision sound effect on enemy collision

				// If lives reach 0, trigger game over
				if (playerLives <= 0)
				{
					gamePlay_sound.Stop();
					_game.ChangeScene(new GameOverScene(_game, false, coinsCollected, 60f - countdownTimer, difficultyLevel)); // Player loses
				}
			}

			// Countdown timer
			countdownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (countdownTimer <= 0)
			{
				gamePlay_sound.Stop();
				_game.ChangeScene(new GameOverScene(_game, false, coinsCollected, 60f, difficultyLevel)); // Time out, player loses
			}

			// Check if all coins are collected
			if (coins.Count == 0)
			{
				gamePlay_sound.Stop();
				_game.ChangeScene(new GameOverScene(_game, true, coinsCollected, 60f - countdownTimer, difficultyLevel)); // Player wins
			}
		}



		// Helper method to get a random position within the maze's valid paths and inside the screen boundaries
		private Vector2 GetRandomPositionWithinMaze()
		{
			int row, col;

			do
			{
				// Randomly select a row and column within the maze dimensions
				row = random.Next(1, maze.GetLength(0) - 1); // Exclude boundary walls
				col = random.Next(1, maze.GetLength(1) - 1); // Exclude boundary walls
			}
			while (maze[row, col] != 0 || IsOccupied(row, col));

			// Calculate the position based on tile size
			int tileSize = 32; // Tile size used for the maze
			float x = col * tileSize;
			float y = row * tileSize;

			// Ensure position is within the game screen boundaries
			float screenWidth = _game.GraphicsDevice.Viewport.Width;
			float screenHeight = _game.GraphicsDevice.Viewport.Height;

			if (x >= screenWidth - tileSize) x = screenWidth - tileSize * 2;
			if (y >= screenHeight - tileSize) y = screenHeight - tileSize * 2;

			return new Vector2(x, y);
		}

		private bool IsOccupied(int row, int col)
		{
			int tileSize = 32;
			float x = col * tileSize;
			float y = row * tileSize;

			foreach (var coin in coins)
			{
				if (coin.Position.X == x && coin.Position.Y == y)
				{
					return true;
				}
			}

			foreach (var enemy in enemies)
			{
				if (enemy.Position.X == x && enemy.Position.Y == y)
				{
					return true;
				}
			}

			return false;
		}
		public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();

            _game.SpriteBatch.End();
        }
    }
}