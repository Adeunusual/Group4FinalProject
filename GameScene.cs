/*
* GameScene.cs
* 
* Description: The GameScene class serves as a base class for all the different "scenes" in your game
* (e.g., Start Menu, Gameplay, Game Over). Each specific scene will inherit from GameScene and override its behavior.
*
* Revision History:
* Faruq A. Atanda : 15th November, 2024
*/

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter
{
    /*
     * Base class for all game scenes.
     * Defines common methods like LoadContent, Update, and Draw.
     */
    public abstract class GameScene
    {
        protected Game1 _game; // Reference to the main game (Game1)

        // Constructor that initializes the game instance
        public GameScene(Game1 game)
        {
            _game = game; // Assigning the game instance
        }

        /*
         * Load all content needed for this scene.
         * Each derived scene will override this to load its own specific content.
         * This may include loading textures, fonts, sounds, etc.
        */
        public virtual void LoadContent()
        {
        }

        /*
         * Update the logic for this scene (e.g., user input, animations).
         * This method is called every frame to handle the game state.
         * Each derived scene will override this to add its own update logic.
         * <param name="gameTime">Provides a snapshot of timing values.</param>
        */
        public virtual void Update(GameTime gameTime)
        {
        }

        /*
         * Draw the visual elements of this scene.
         * This method is called every frame to render the scene.
         * Each derived scene will override this to add its own drawing logic.
         * <param name="gameTime">Provides a snapshot of timing values.</param>
        */
        public virtual void Draw(GameTime gameTime)
        {
        }
    }
}
