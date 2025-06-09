using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class TitleScene : SceneBase
    {
        private ICommonButton _titleButton;

        [Inject]
        public void Construct(ISceneManager sceneManager, [Inject(Id = "Start")] ICommonButton titleButton)
        {
            _sceneManager = sceneManager;
            _titleButton = titleButton;
            _titleButton.SetAllReaction(false);

            Debug.Log("TitleScene: Injection Complete");
        }

        public override SceneType GetSceneType()
        {
            return SceneType.Title;
        }

        public override UniTask OnInitialize(ISceneData sceneData, CancellationToken token)
        {
            Debug.Log("TitleScene: OnInitialize");

            UniRxUtility.SubscribeWithAddTo(_titleButton.OnPointerDown, 
                (unit) => _sceneManager.ChangeScene(SceneType.Select,null), this);

            _titleButton.SetAllReaction(true);

            return UniTask.CompletedTask;
        }

        
    }
}