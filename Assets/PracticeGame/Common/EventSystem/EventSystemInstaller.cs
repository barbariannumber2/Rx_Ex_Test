using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "EventSystemInstaller", menuName = "Installers/MyInstallers/EventSystemInstaller")]
    public class EventSystemInstaller : ScriptableObjectInstaller<EventSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EventSystem>()
            .FromComponentInNewPrefabResource("EventSystem")
            .AsSingle()
            .NonLazy();
        }
    }
}