using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class TitleInstaller : MonoInstaller
    {
        [SerializeField]
        private CommonButton _startButton;
        public override void InstallBindings()
        {
            Container.Bind<ICommonButton>()
                .WithId("Start")
                .To<CommonButton>()
                .FromComponentOn(_startButton.gameObject)
                .AsCached();

            Container.Bind<IPointerHoverEventSender>()
                .To<CommonButton>()
                .FromComponentSibling()
                .AsTransient();

            //WithIDのタグ付けはIInitializableをバインドする際には使えないので
            //BindInterfacesToが使えない
            Container.Bind<IInitializable>()
                .To<CommonButton>()
                .FromComponentOn(_startButton.gameObject)
                .AsCached();
        }
    }
}