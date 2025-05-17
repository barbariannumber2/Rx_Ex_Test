using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class TitleInstaller : MonoInstaller
    {
        [SerializeField]
        private CommonButtonView _startButton;
        public override void InstallBindings()
        {
            Container.Bind<CommonButtonView>()
                .WithId("Start")
                .FromComponentOn(_startButton.gameObject)
                .AsTransient();
        }
    }
}