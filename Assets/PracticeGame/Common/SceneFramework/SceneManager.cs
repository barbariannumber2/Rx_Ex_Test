using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace PracticeGame
{
    public class SceneManager : MonoBehaviour,ISceneManager
    {
        private List<IScene> _currentScenes = new();

        private ITransition _transition;

        [Inject]
        public void Construct(ITransition transition)
        {
            _transition = transition;
            Debug.Log("SceneManager: Injection Complete");
        }

        public void SceneSettingsInitialization(SceneType sceneType, ISceneData sceneData)
        {
            SceneSettingsInitializationFlow(sceneType, sceneData).Forget();
        }

        public void ChangeScene(SceneType sceneType, ISceneData sceneData)
        {
            ChangeSceneFlow(sceneType, sceneData).Forget();
        }

        private async UniTask SceneSettingsInitializationFlow(SceneType sceneType, ISceneData sceneData)
        {
            if (_currentScenes.Count != 0)
            {
                return;
            }

            var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (activeScene.name.Equals(sceneType.ToString()))
            {
                OnEntrySceneOpenPlayed(activeScene,sceneData).Forget();
            }
            else
            {
                await _transition.ScreenClose(0.2f);
                await SceneLoader.SingleLoadSceneAsync(sceneType);

                var mainSceneComponent = FindSceneComponent(sceneType);
                _currentScenes.Add(mainSceneComponent);

                await UniTask.WhenAll(LoadUseScenes(mainSceneComponent));

                await UniTask.WhenAll(SceneInitialize(sceneData));

                await _transition.ScreenOpen(1f);

                await UniTask.WhenAll(SceneStart(sceneData));
            }
        }

        private async UniTask ChangeSceneFlow(SceneType sceneType, ISceneData sceneData)
        {
            await UniTask.WhenAll(SceneExit());

            await _transition.ScreenClose(1f);

            await UniTask.WhenAll(SceneFinal());

            await SceneLoader.AddSceneAsync(sceneType);

            var nextMainScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneType.ToString());
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(nextMainScene);

            //とりあえず使ってたシーンを全削除
            //読み込み中シーンとか作るなら必要ないものだけ消すように改修が必要
            await UniTask.WhenAll(SceneLoader.UnloadScenes(_currentScenes));
            _currentScenes.Clear();

            var mainSceneComponent = FindSceneComponent(nextMainScene);
            _currentScenes.Add(mainSceneComponent);

            await UniTask.WhenAll(LoadUseScenes(mainSceneComponent));

            await UniTask.WhenAll(SceneInitialize(sceneData));

            await _transition.ScreenOpen(1f);

            await UniTask.WhenAll(SceneStart(sceneData));
        }

        /// <summary>
        /// エントリーポイントとなるシーンを開いてエディタプレイした時専用のフロー
        /// 
        /// 今回のゲームではシーン構造を簡略化していて、タイトルシーン=エントリーポイントシーンなため、
        /// 各シーンでのエディタプレイ時、自動でChangeScene関数でのタイトルへの遷移リクエストが行われ、遷移フローが実行される
        /// ChangeScene関数は本来アクティブなシーンと同じシーンへの遷移リクエストの場合何も行わない仕様
        /// だが、タイトルシーンでのエディタプレイ時に遷移フローが丸ごと行われない場合、タイトルシーンの初期化も行われなくなってしまう
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private async UniTask OnEntrySceneOpenPlayed(UnityEngine.SceneManagement.Scene scene, ISceneData sceneData)
        {
            await _transition.ScreenClose(0.2f);
            var mainSceneComponent = FindSceneComponent(scene);
            _currentScenes.Add(mainSceneComponent);
            await UniTask.WhenAll(LoadUseScenes(mainSceneComponent));
            await UniTask.WhenAll(SceneInitialize(null));
            await _transition.ScreenOpen(1f);
            await UniTask.WhenAll(SceneStart(null));
        }

        private List<UniTask> LoadUseScenes(IScene mainScene)
        {
            List<UniTask> loadTask = new();
            foreach (var useScene in mainScene.UseScenes)
            {
                loadTask.Add(SceneLoader.SingleLoadSceneAsync(useScene));
            }
            return loadTask;
        }

        private List<UniTask> SceneInitialize(ISceneData sceneData)
        {
            List<UniTask> initializeTask = new();
            foreach (IScene scene in _currentScenes)
            {
                initializeTask.Add(UniTaskUtility.RunWithCancellation
                    (async token => await scene.OnInitialize(sceneData, token)));
            }
            return initializeTask;
        }

        private List<UniTask> SceneStart(ISceneData sceneData)
        {
            List<UniTask> startTask = new();
            foreach (IScene scene in _currentScenes)
            {
                startTask.Add(UniTaskUtility.RunWithCancellation
                    (async token => await scene.OnStart(sceneData, token)));
            }
            return startTask;
        }

        private List<UniTask> SceneExit()
        {
            List<UniTask> exitTask = new();
            foreach (IScene scene in _currentScenes)
            {
                exitTask.Add(UniTaskUtility.RunWithCancellation
                    (async token => await scene.OnExit(token)));
            }
            return exitTask;
        }

        private List<UniTask> SceneFinal()
        {
            List<UniTask> finalizeTask = new();
            foreach (IScene scene in _currentScenes)
            {
                finalizeTask.Add(UniTaskUtility.RunWithCancellation
                    (async token => await scene.OnFinalize(token)));
            }
            return finalizeTask;
        }

        private IScene FindSceneComponent(SceneType sceneType)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneType.ToString());

            return FindSceneComponent(scene);
        }

        /// <summary>
        /// UnityのSceneのルートにあるオブジェクトから自作のシーンコンポーネントを探す
        /// ヒエラルキーの上から順に探すので一番上のオブジェクトにシーンコンポーネントを付けていると負荷が少ない
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private IScene FindSceneComponent(UnityEngine.SceneManagement.Scene scene)
        {
            if (scene == null)
            {
                Debug.Log("SceneManager: 引数のsceneがnull");
                return null;
            }
            if(scene.isLoaded == false)
            {
                Debug.Log("SceneManager: 引数のsceneが読み込まれていない");
                return null;
            }

            foreach (GameObject sceneObject in scene.GetRootGameObjects())
            {
                var sceneComponent = sceneObject.GetComponent<IScene>();
                if (sceneComponent == null)
                {
                    continue;
                }
                return sceneComponent;
            }

            return null;
        }
    }
}