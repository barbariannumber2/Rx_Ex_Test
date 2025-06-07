using PracticeGame;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class SelectViewSubInstaller : MonoInstaller
    {
        [SerializeField]
        private CommonPressableObject _easyButton;

        [SerializeField]
        private CommonPressableObject _normalButton;

        [SerializeField]
        private CommonPressableObject _hardButton;
        public override void InstallBindings()
        {
            ////　SelectInstallerの「サブコンテナ利用例1」に対応するコード
            //Container.Bind<SelectView>()
            //    .FromComponentOnRoot()
            //    .AsCached();

            ////　SelectInstallerの「サブコンテナ利用例2」に対応するコード
            Container.Bind<IView>()
                .To<SelectView>()
                .FromComponentOnRoot()
                .AsCached();


            Container.Bind<ICommonPressable>()
                .WithId("EasyButton")
                .To<CommonPressableObject>()
                .FromComponentOn(_easyButton.gameObject)
                .AsCached();

            Container.Bind<ICommonPressable>()
                .WithId("NormalButton")
                .To<CommonPressableObject>()
                .FromComponentOn(_normalButton.gameObject)
                .AsCached();

            Container.Bind<ICommonPressable>()
                .WithId("HardButton")
                .To<CommonPressableObject>()
                .FromComponentOn(_hardButton.gameObject)
                .AsCached();
        }
    }
}