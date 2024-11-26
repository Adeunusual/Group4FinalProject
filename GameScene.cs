using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter
{
    public abstract class GameScene
    {
        protected Game1 _game;

        public GameScene(Game1 game)
        {
            _game = game;
        }
        public virtual void LoadContent()
        {
        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(GameTime gameTime)
        {
        }
    }
}
