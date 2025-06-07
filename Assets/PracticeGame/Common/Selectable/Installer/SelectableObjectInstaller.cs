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
            //WithIDのタグ付けはIInitializableをバインドする際には使えないので
            //BindInterfacesToが使えない 習熟していないのでId指定が必要ないかどうかはまだ判断できない
            // - > WhenInjectedIntoを活用したらいいかも?
            Container.Bind<ObservableEventTrigger>()
                .WithId("Selectable")
                .FromComponentSibling()
                .AsTransient();

            Container.Bind<IMultiInputSelectEventSender>()
                .To<CommonSelectableObject>()
                .FromComponentSibling()
                .AsTransient();

            //IInitializableをバインドする責任はタイトルなどではなくこのインストーラーに持たせたいが
            //FromComponentSiblingが使えないので良い方法が思いつかない
            //Container.Bind<IInitializable>()
            //    .To<CommonSelectableObject>()
            //    .FromComponentSibling()
            //    .AsTransient();
        }
    }
}