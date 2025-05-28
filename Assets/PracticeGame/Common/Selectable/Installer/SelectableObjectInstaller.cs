using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "SelectableObjectInstaller", menuName = "Installers/MyInstallers/SelectableObjectInstaller")]
    public class SelectableObjectInstaller : ScriptableObjectInstaller<SelectableObjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ObservableEventTrigger>()
                .WithId("Selectable")
                .FromComponentSibling()
                .AsTransient();

            Container.Bind<IMultiInputSelectEventSender>()
                .To<CommonSelectableObject>()
                .FromComponentSibling()
                .AsTransient();
        }
    }
}