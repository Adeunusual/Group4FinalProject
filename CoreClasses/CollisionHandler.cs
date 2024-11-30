/*
* CollisionHandler.cs
* 
* Description: This class contains methods for managing collisions between game entities such as player, coins, and enemies.
*
* Revision History:
* Joonryeon Jang: 15th, November, 2024 - Created collision handling methods for player-coin and player-enemy interactions.
*/

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GhostHunter
{
	public static class CollisionHandler
	{
		// Check collision between two objects using their positions and size
		public static bool CheckCollision(Vector2 position1, Vector2 position2, float collisionRadius)
		{
			// Returns true if the distance between the two positions is less than the specified collision radius
			return Vector2.Distance(position1, position2) < collisionRadius;
		}

		// Check collisions between player and list of coins
		public static bool CheckCoinCollisions(Player player, List<Coin> coins, ref int coinsCollected)
		{
			bool coinCollected = false;

			// Iterate through coins in reverse to avoid indexing issues while removing collected coins
			for (int i = coins.Count - 1; i >= 0; i--)
			{
				if (Vector2.Distance(player.Position, coins[i].Position) < 30) // Adjust collision radius as needed
				{
					// Remove the collected coin, update coin count, and set flag to true
					coins.RemoveAt(i);
					coinsCollected++;
					coinCollected = true;
				}
			}

			return coinCollected;
		}

		// Check collisions between player and list of enemies
		public static bool CheckEnemyCollisions(Player player, List<Enemy> enemies)
		{
			// Iterate through each enemy and check if it collides with the player
			foreach (var enemy in enemies)
			{
				if (CheckCollision(player.Position, enemy.Position, 32)) // Assuming a tile size of 32
				{
					// Return true if a collision with an enemy is detected
					return true;
				}
			}
			return false;
		}
	}
}
