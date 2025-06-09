using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "ButtonInstaller", menuName = "Installers/MyInstallers/ButtonInstaller")]
    public class ButtonInstaller : ScriptableObjectInstaller<ButtonInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<DiContainer>()
                .WithId("Button")
                .FromInstance(Container)
                .AsCached();

            Container.Bind<ObservableEventTrigger>()
                .WithId("Button")
                .FromComponentSibling()
                .AsTransient();
        }

    }
}