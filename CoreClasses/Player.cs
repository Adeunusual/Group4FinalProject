/*
* Player.cs
* 
* Description: This file handles player properties and movements, including collision detection to ensure the player stays within maze boundaries.
*
* Revision History:
* Faruq A. Atanda: 15th, November, 2024 - Implemented player movement, collision detection, and smooth navigation logic.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GhostHunter
{
	public class Player
	{
		private Texture2D texture;
		public Vector2 Position;
		private int[,] maze;
		private int tileSize = 32; // Tile size for movement
		private float moveSpeed = 128f; // Speed for smooth movement
		private KeyboardState previousKeyboardState;

		// Constructor to initialize player properties
		public Player(Texture2D texture, Vector2 startPosition, int[,] maze)
		{
			this.texture = texture;
			Position = startPosition;
			this.maze = maze;
		}

		// Update method to handle player movement based on keyboard input
		public void Update(GameTime gameTime, KeyboardState keyboardState)
		{
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Calculate intended movement based on key presses
			Vector2 newPosition = Position;

			// Handle movement between grid tiles
			if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
			{
				newPosition.Y -= tileSize;
			}
			else if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
			{
				newPosition.Y += tileSize;
			}
			else if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left))
			{
				newPosition.X -= tileSize;
			}
			else if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))
			{
				newPosition.X += tileSize;
			}

			// Smooth movement to target position
			Vector2 smoothMoveDirection = Vector2.Zero;

			// Handle smooth movement based on pressed keys
			if (keyboardState.IsKeyDown(Keys.Up))
			{
				smoothMoveDirection.Y -= moveSpeed * deltaTime;
			}
			else if (keyboardState.IsKeyDown(Keys.Down))
			{
				smoothMoveDirection.Y += moveSpeed * deltaTime;
			}
			else if (keyboardState.IsKeyDown(Keys.Left))
			{
				smoothMoveDirection.X -= moveSpeed * deltaTime;
			}
			else if (keyboardState.IsKeyDown(Keys.Right))
			{
				smoothMoveDirection.X += moveSpeed * deltaTime;
			}

			// Add smooth movement to current position if valid
			Vector2 potentialPosition = Position + smoothMoveDirection;

			// Update position only if the movement is valid
			if (IsValidMove(newPosition))
			{
				Position = newPosition;
			}
			else if (IsValidMove(potentialPosition))
			{
				Position = potentialPosition;
			}

			// Update the previous keyboard state
			previousKeyboardState = keyboardState;
		}

		// Helper method to check if a movement is valid
		private bool IsValidMove(Vector2 newPosition)
		{
			int col = (int)(newPosition.X / tileSize);
			int row = (int)(newPosition.Y / tileSize);

			// Ensure the new position is within maze boundaries and is a path (0)
			if (row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1))
			{
				return maze[row, col] == 0; // Return true if the tile is walkable
			}

			return false;
		}

		// Draw method to render the player
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, Color.White);
		}
	}
}
