/*
* GamePlayScene.cs
* 
* Description: Manages the main gameplay logic including player, enemies, coins, collisions, sounds, and game state transitions.
*
* Revision History:
* Faruq A. Atanda: 15th November, 2024 - Implemented core game logic, including player movement, enemy behaviors, collisions, and scene transitions.
*/
using GhostHunter.SceneClasses;
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
        public GamePlayScene(Game1 game, string level) : base(game)
        {

        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();

            _game.SpriteBatch.End();
        }
    }
}