using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PracticeGame
{
    public interface IScene
    {
        public SceneType GetSceneType();

        public List<SceneType> UseScenes { get; }

        public UniTask OnInitialize(ISceneData sceneData, CancellationToken token);

        public UniTask OnStart(ISceneData sceneData, CancellationToken token);

        public UniTask OnExit(CancellationToken token);

        public  UniTask OnFinalize(CancellationToken token);
    }
}