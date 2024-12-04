/*
* Game1.cs
* 
* Description: The main entry point for the GhostHunter game. Handles game initialization, content loading, and managing different game scenes.
* 
* Revision History:
* Faruq A. Atanda : 15th November, 2024
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GhostHunter
{
    public class Game1 : Game
    {
        //declaration of variables
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch; // Shared SpriteBatch for drawing across scenes
        private GameScene _currentScene; // Reference to the active scene

        public ScoresScene ScoresScene { get; private set; } // Added this line to hold the reference
        public Dictionary<string, float> HighScores { get; private set; } // High scores for different levels (Easy, Medium, Hard)

        // Constructor for initializing the main game components
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this); // Initialize the graphics device manager
            Content.RootDirectory = "Content"; // Set the content root directory
            IsMouseVisible = true; // Show the mouse cursor during gameplay

            // Initialize high scores dictionary with default values
            HighScores = new Dictionary<string, float>
            {
                { "Easy", float.MaxValue },   // Initial score for Easy level (no score yet)
                { "Medium", float.MaxValue }, // Initial score for Medium level (no score yet)
                { "Hard", float.MaxValue }    // Initial score for Hard level (no score yet)
            };
        }

        // Initialize the game and set the starting scene
        protected override void Initialize()
        {
            // Initialize the first scene to be the Start Menu
            _currentScene = new StartMenuScene(this);

            // Initialize ScoresScene instance
            ScoresScene = new ScoresScene(this);

            base.Initialize();
        }

        // Load the content for the active scene
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice); // Initialize the shared SpriteBatch for drawing
            _currentScene.LoadContent(); // Load content for the active scene
        }

        // Update method to manage the game logic, including changing scenes
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState(); // Get the current state of the keyboard

            // If the current scene is not the Start Menu and Escape is pressed, return to the Start Menu
            if (keyboardState.IsKeyDown(Keys.Escape) && !(_currentScene is StartMenuScene))
            {
                ChangeScene(new StartMenuScene(this));
                return; // Prevent further processing after changing the scene
            }

            // Update the active scene
            _currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        // Draw method to render the current scene
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // Clear the screen with a background color

            _currentScene.Draw(gameTime); // Draw the active scene

            base.Draw(gameTime);
        }

        /*
         * Change the active scene to a new one.
         * <param name="newScene">The new scene to switch to.</param>
        */
        public void ChangeScene(GameScene newScene)
        {
            _currentScene = newScene; // Set the new scene as the active one
            _currentScene.LoadContent(); // Load the content for the new scene
        }
    }
}
