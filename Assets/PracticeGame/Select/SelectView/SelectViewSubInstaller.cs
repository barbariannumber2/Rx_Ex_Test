using PracticeGame;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class SelectViewSubInstaller : MonoInstaller
    {
        [SerializeField]
        private CommonButton _easyButton;

        [SerializeField]
        private CommonButton _normalButton;

        [SerializeField]
        private CommonButton _hardButton;
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


            Container.Bind<ICommonButton>()
                .WithId("EasyButton")
                .To<CommonButton>()
                .FromComponentOn(_easyButton.gameObject)
                .AsCached();

            Container.Bind<ICommonButton>()
                .WithId("NormalButton")
                .To<CommonButton>()
                .FromComponentOn(_normalButton.gameObject)
                .AsCached();

            Container.Bind<ICommonButton>()
                .WithId("HardButton")
                .To<CommonButton>()
                .FromComponentOn(_hardButton.gameObject)
                .AsCached();

            Container.Bind<CursorAutoMoveByCommand.IInjectData>()
                .FromInstance(
                new CursorAutoMoveByCommand.InjectData(new() {_easyButton, _normalButton, _hardButton}, 0)
                )
                .AsCached();

            Container.Bind<CursorAutoMoveByPointer.IInjectData>()
                .FromInstance(
                new CursorAutoMoveByPointer.InjectData(new() { _easyButton, _normalButton, _hardButton }, 0)
                )
                .AsCached();
        }
    }
}