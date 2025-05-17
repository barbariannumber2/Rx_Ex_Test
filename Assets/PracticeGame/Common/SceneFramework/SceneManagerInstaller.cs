using UnityEngine;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "SceneManagerInstaller", menuName = "Installers/MyInstallers/SceneManagerInstaller")]
    public class SceneManagerInstaller : ScriptableObjectInstaller<SceneManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneManager>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .OnInstantiated((context, obj) => (obj as SceneManager)?.SceneSettingsInitialization(SceneType.Title, null))
                .NonLazy();
        }
    }
}