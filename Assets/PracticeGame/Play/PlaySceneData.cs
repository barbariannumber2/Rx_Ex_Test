using UnityEngine;

namespace PracticeGame
{
    public class PlaySceneData : ISceneData
    {
        public Difficulty DifficultyLevel { get; private set; }
        
        public PlaySceneData(Difficulty difficulty)
        {
            DifficultyLevel = difficulty;
        }
    }
}