using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PracticeGame
{
    public interface ISceneManager
    {
        public void SceneSettingsInitialization(SceneType sceneType, ISceneData sceneData);

        public void ChangeScene(SceneType sceneType, ISceneData sceneData);
    }
}