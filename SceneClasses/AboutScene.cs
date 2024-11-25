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
    internal class AboutScene
    {
        private SpriteFont font;
        private string aboutSceneText;

        public AboutScene(SpriteFont font)
        {
            this.font = font;
            aboutSceneText = "Description of the game";
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, aboutSceneText, new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }
    }
}
