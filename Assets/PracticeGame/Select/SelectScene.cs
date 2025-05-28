using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PracticeGame
{
    public class SelectScene : SceneBase
    {
        private IView _selectView;

        [Inject]
        public void Construct(ISceneManager sceneManager/*,IView selectView*/)
        {
            _sceneManager = sceneManager;
            //_selectView = selectView;
            Debug.Log("SelectScene: Injection Complete");
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Select;
        }
    }
}