using UnityEngine;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "MainCameraInstaller", menuName = "Installers/MyInstallers/MainCameraInstaller")]
    public class MainCameraInstaller : ScriptableObjectInstaller<MainCameraInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromComponentInNewPrefabResource("MainCamera")
                .AsSingle()
                .NonLazy();
        }
    }
}