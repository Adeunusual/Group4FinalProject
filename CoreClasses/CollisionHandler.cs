using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GhostHunter
{
    internal class CollisionHandler
    {
        private Player player;
        private List<Enemy> enemies;
        private int lives;
        private int score;
        private int level;
        private bool isPlayerBlinking;
        private double blinkTimer;
        private const double blinkDuration = 0.2; // Time in seconds for the player to blink

        private SpriteFont gameFont; // Font for displaying status

        public CollisionHandler(Player player, List<Enemy> enemies, SpriteFont gamefont)
        {
            this.player = player;
            this.enemies = enemies;
            this.lives = 3; // Initial number of lives
            this.score = 0; // Initial score
            this.level = 1; // Start at level 1
            this.isPlayerBlinking = false;
            this.blinkTimer = 0;
            this.gameFont = gamefont; // Load the font
        }

        // Update the collision handler logic
        public void Update(GameTime gameTime)
        {
            // Check for collisions between player and each enemy
            foreach (Enemy enemy in enemies)
            {
                //       if (player.Bounds.Intersects(enemy.Bounds))      // on hold for enemy bound.......
                {
                    HandlePlayerCollision();
                    break; // Stop checking further enemies after first collision
                }
            }

            // Handle blinking effect when the player collides
            if (isPlayerBlinking)
            {
                blinkTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (blinkTimer > blinkDuration)
                {
                    isPlayerBlinking = false; // Stop blinking after duration
                    blinkTimer = 0;
                }
            }
        }
        // Draw the player and status
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw player only if not blinking
            if (!isPlayerBlinking)
            {
                player.Draw(spriteBatch); //only draw if not blinking
            }
        }

        // Draw the status (lives, score, level) on the screen
        public void DrawStatus(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameFont, $"Lives: {lives}", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(gameFont, $"Level: {level}", new Vector2(10, 70), Color.White);
        }

        // Handle collision logic when the player hits an enemy
        private void HandlePlayerCollision()
        {
            lives--;

            if (lives > 0)
            {
                // Trigger blinking effect when the player gets hit
                isPlayerBlinking = true;
            }
            else
            {
                // Handle game over logic if the player has no lives left
                HandleGameOver();
            }
        }
        // Increase score (e.g., when collecting coins or defeating enemies)
        public void IncreaseScore(int points)
        {
            score += points;
        }

        // Proceed to the next level
        public void NextLevel()
        {
            level++;
        }

        // Handle the game over logic (showing Game Over screen...)
        private void HandleGameOver()
        {
            Console.WriteLine("Game over!");
        }

        //Get current number of lives
        public int GetLives()
        {
            return lives;
        }
    }
}
