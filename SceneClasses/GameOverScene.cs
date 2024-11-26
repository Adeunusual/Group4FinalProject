using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter.SceneClasses
{
    public class GameOverScene 
    {
        //declaration of Variables
        private bool playerWon;
        private SpriteFont normalFont;
        private int finalScore;
        private float timeUsed;
        private string difficultyLevel;

        // Constructor to initialize the Game Over scene with relevant game results
        public GameOverScene(Game1 game, bool won, int score = 0, float time = 0, string level = "Easy") 
        {
            playerWon = won;
            finalScore = score;
            timeUsed = time;
            difficultyLevel = level;
        }
    }
}
