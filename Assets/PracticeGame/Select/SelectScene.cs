using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PracticeGame
{
    public class SelectScene : SceneBase
    {
        private IView _selectView;

        [Inject]
        public void Construct(ISceneManager sceneManager, IView selectView)
        {
            _sceneManager = sceneManager;
            _selectView = selectView;
            Debug.Log("SelectScene: Injection Complete");
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Select;
        }

        public override UniTask OnInitialize(ISceneData sceneData, CancellationToken token)
        {
            Debug.Log("SelectScene: OnInitialize");
            _selectView.Initialize();
            
            return base.OnInitialize(sceneData, token);
        }
    }
}