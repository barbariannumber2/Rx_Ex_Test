using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PracticeGame
{
    public class SelectScene : SceneBase
    {
        [Inject]
        public void Construct(ISceneManager sceneManager)
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