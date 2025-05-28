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
            Container.Bind<CommonPressableObject>()
                .WithId("Start")
                .FromComponentOn(_startButton.gameObject)
                .AsTransient();
        }
    }
}