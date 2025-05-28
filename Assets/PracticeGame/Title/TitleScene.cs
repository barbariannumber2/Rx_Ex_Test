using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class TitleScene : SceneBase
    {
        private IMultiInputPressEventSender _titleButton;

        [Inject]
        public void Construct(ISceneManager sceneManager, [Inject(Id = "Start")] IMultiInputPressEventSender titleButton)
        {
            _sceneManager = sceneManager;
            _titleButton = titleButton;
            Debug.Log("TitleScene: Injection Complete");
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Title;
        }

        public override UniTask OnInitialize(ISceneData sceneData, CancellationToken token)
        {
            UniRxUtility.SubscribeWithAddTo(_titleButton.OnPointerDown, 
                (unit) => _sceneManager.ChangeScene(SceneType.Select,null), this);
            return UniTask.CompletedTask;
        }
    }
}