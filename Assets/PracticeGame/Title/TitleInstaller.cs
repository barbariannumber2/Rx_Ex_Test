using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class TitleInstaller : MonoInstaller
    {
        [SerializeField]
        private CommonPressableObject _startButton;
        public override void InstallBindings()
        {
            Container.Bind<IMultiInputPressEventSender>()
                .WithId("Start")
                .To<CommonPressableObject>()
                .FromComponentOn(_startButton.gameObject)
                .AsTransient();
        }
    }
}