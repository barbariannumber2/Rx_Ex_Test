using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class TitleScene : SceneBase
    {
        private SceneManager _sceneManager;

        private CommonButtonView _buttonView;

        [Inject]
        public void Construct(SceneManager sceneManager, [Inject(Id = "Start")] CommonButtonView buttonView)
        {
            _sceneManager = sceneManager;
            _buttonView = buttonView;
            Debug.Log("TitleScene: Injection Complete");
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Title;
        }

        public override UniTask OnInitialize(ISceneData sceneData, CancellationToken token)
        {
            UniRxUtility.SubscribeWithAddTo(_buttonView.OnPressed, 
                (unit) => _sceneManager.ChangeScene(SceneType.Select,null), this);
            return UniTask.CompletedTask;
        }
    }
}