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

        /// <summary>
        /// シーン初期化時の処理　フェードでの暗転中などに行う処理
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public UniTask OnInitialize(ISceneData sceneData, CancellationToken token);

        /// <summary>
        /// シーン開始時の処理　フェードが明転した後の処理
        /// UIの操作受付開始などを想定
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public UniTask OnStart(ISceneData sceneData, CancellationToken token);

        /// <summary>
        /// シーン終了時の処理　フェードでの暗転前に行う処理
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UniTask OnExit(CancellationToken token);

        /// <summary>
        /// シーン終了時の処理　フェードでの暗転後に行う処理
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UniTask OnFinalize(CancellationToken token);
    }
}