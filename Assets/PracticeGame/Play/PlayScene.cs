using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace PracticeGame
{
    public class PlayScene : SceneBase
    {
        public override SceneType GetSceneType()
        {
           return SceneType.Play;
        }

        public override UniTask OnInitialize(ISceneData sceneData, CancellationToken token)
        {
            PlaySceneData playSceneData = sceneData as PlaySceneData;

            Debug.Log(playSceneData.DifficultyLevel.ToString());

            return base.OnInitialize(sceneData, token);
        }
    }
}