/*
* Enemy.cs
* 
* Description: This class manages enemy properties, movement logic, and patrolling behavior within the maze.
*
* Revision History:
* Maria Fernanda: 28th, November, 2024 - Created enemy movement logic and patrolling behavior.
* 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostHunter
{
    public class Enemy
    {
        //declaration of variables
        private Texture2D texture;
        public Vector2 Position;
        private float speed;
        private int[,] maze;
        private bool moveHorizontally;
        private Vector2 initialPosition;
        private int tileSize = 32;
        private int patrolDistance = 3; // Distance in tiles to patrol back and forth
        private Vector2 direction;

        // Constructor to initialize enemy properties
        public Enemy(Texture2D texture, Vector2 startPosition, float speed, int[,] maze, bool moveHorizontally)
        {
            this.texture = texture;
            Position = startPosition;
            initialPosition = startPosition;
            this.speed = speed;
            this.maze = maze;
            this.moveHorizontally = moveHorizontally;

            // Set initial direction of movement based on whether the enemy moves horizontally or vertically
            direction = moveHorizontally ? new Vector2(1, 0) : new Vector2(0, 1);
        }

        // Update method to handle enemy movement
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 potentialPosition = Position + direction * speed * deltaTime;

            // Check if the new position is within the patrol range
            if (IsValidPatrolPosition(potentialPosition))
            {
                // Move to the new position if it's valid
                Position = potentialPosition;
            }
            else
            {
                // Reverse direction if the patrol range limit is reached
                direction *= -1;
            }
        }

        // Checks if the enemy can patrol to the new position without exceeding patrol distance
        private bool IsValidPatrolPosition(Vector2 newPosition)
        {
            int col = (int)(newPosition.X / tileSize);
            int row = (int)(newPosition.Y / tileSize);

            // Ensure the enemy stays within the patrol distance from its initial position
            if (moveHorizontally)
            {
                //get the distance traveled in terms of tiles
                float distanceFromStart = Math.Abs(newPosition.X - initialPosition.X) / tileSize;
                if (distanceFromStart > patrolDistance)
                {
                    return false;
                }
            }
            else
            {
                //get the distance traveled in terms of tiles
                float distanceFromStart = Math.Abs(newPosition.Y - initialPosition.Y) / tileSize;
                if (distanceFromStart > patrolDistance)
                {
                    return false;
                }
            }

            // Ensure the new position is within maze boundaries 
            if (row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1))
            {
                return maze[row, col] == 0; // Return true if the tile is walkable
            }

            return false; // Return false if the position is invalid
        }

        // Draw the enemy using the given SpriteBatch
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

    }
}
