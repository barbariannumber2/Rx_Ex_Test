using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

namespace PracticeGame
{
    public abstract class SceneBase : MonoBehaviour,IScene
    {
        protected ISceneManager _sceneManager;

        public abstract SceneType GetSceneType();

        public virtual List<SceneType> UseScenes { get { return new(); } }

        public virtual async UniTask OnInitialize(ISceneData sceneData, CancellationToken token) { await UniTask.DelayFrame(1); }

        public virtual async UniTask OnStart(ISceneData sceneData, CancellationToken token) { await UniTask.DelayFrame(1); }

        public virtual async UniTask OnExit(CancellationToken token) { await UniTask.DelayFrame(1); }

        public virtual async UniTask OnFinalize(CancellationToken token) { await UniTask.DelayFrame(1); }
    }
}