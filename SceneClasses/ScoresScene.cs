/*
* ScoresScene.cs
* 
* Description: Displays the high scores for each difficulty level in the GhostHunter game.
* 
* Revision History:
* Joonryeon Jang : 15th November, 2024
* Updated: Added functionality to read/write high scores from/to a text file.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace GhostHunter
{
    public class ScoresScene : GameScene
    {
        private SpriteFont normalFont;
        private Texture2D backgroundTexture;
        private const string ScoreFilePath = "store.txt";

        public ScoresScene(Game1 game) : base(game)
        {
            // Initialize high scores from file
            LoadHighScores();
        }

        public override void LoadContent()
        {
            normalFont = _game.Content.Load<SpriteFont>("normalFont");
            backgroundTexture = _game.Content.Load<Texture2D>("scoreScreen-Bg");
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                _game.ChangeScene(new StartMenuScene(_game));
            }
        }

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

            string headingText = "High Scores";
            Vector2 headingSize = normalFont.MeasureString(headingText);
            Vector2 headingPosition = new Vector2(
                (_game.GraphicsDevice.Viewport.Width - headingSize.X) / 2,
                50
            );
            _game.SpriteBatch.DrawString(normalFont, headingText, headingPosition, Color.White);

            // Draw high scores for each difficulty level
            int yOffset = 150;
            foreach (var level in _game.HighScores.Keys)
            {
                float score = _game.HighScores[level];
                string scoreText = score == float.MaxValue
                    ? $"{level}: No score yet"
                    : $"{level}: Coins were collected in {(int)score} seconds";

                Vector2 position = new Vector2(100, yOffset);
                _game.SpriteBatch.DrawString(normalFont, scoreText, position, Color.White);

                yOffset += 50;
            }

            _game.SpriteBatch.End();
        }

        private void LoadHighScores()
        {
            if (File.Exists(ScoreFilePath))
            {
                try
                {
                    var lines = File.ReadAllLines(ScoreFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string level = parts[0].Trim();
                            if (float.TryParse(parts[1].Trim(), out float score))
                            {
                                // Only update if the level exists in the current dictionary
                                if (_game.HighScores.ContainsKey(level))
                                {
                                    _game.HighScores[level] = score;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading scores from file: " + e.Message);
                }
            }
            else
            {
                // If the file does not exist, initialize the file with the default values
                SaveHighScores();
            }
        }

        public void SaveHighScores()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var level in _game.HighScores.Keys)
                {
                    lines.Add($"{level}: {_game.HighScores[level]}");
                }
                File.WriteAllLines(ScoreFilePath, lines);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing scores to file: " + e.Message);
            }
        }
    }
}
