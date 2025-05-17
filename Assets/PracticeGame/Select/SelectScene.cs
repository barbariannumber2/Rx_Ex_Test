using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PracticeGame
{
    public class SelectScene : SceneBase
    {
        private SceneManager _sceneManager;

        [Inject]
        public void Construct(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
            Debug.Log("SelectScene: Injection Complete");
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Select;
        }
    }
}