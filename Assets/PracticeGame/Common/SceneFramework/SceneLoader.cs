using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace PracticeGame
{
    public class SceneLoader
    {
        public static async UniTask SingleLoadSceneAsync(SceneType sceneType)
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneType.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        public static async UniTask AddSceneAsync(SceneType sceneType)
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneType.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        public static async UniTask UnloadScene(SceneType sceneType)
        {
            await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneType.ToString());
        }

        public static List<UniTask> UnloadScenes(List<IScene> unloadScenes)
        {
            List<UniTask> unloadTask = new();
            foreach (var unload in unloadScenes)
            {
                unloadTask.Add(UnloadScene(unload.GetSceneType()));
            }
            return unloadTask;
        }
    }
}