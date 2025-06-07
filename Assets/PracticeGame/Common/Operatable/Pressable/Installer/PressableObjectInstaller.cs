using UniRx.Triggers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PressableObjectInstaller", menuName = "Installers/MyInstallers/PressableObjectInstaller")]
public class PressableObjectInstaller : ScriptableObjectInstaller<PressableObjectInstaller>
{
    public override void InstallBindings()
    {
        //WithIDのタグ付けはIInitializableをバインドする際には使えないので
        //BindInterfacesToが使えない 習熟していないのでId指定が必要ないかどうかはまだ判断できない
        // - > WhenInjectedIntoを活用したらいいかも?
        Container.Bind<ObservableEventTrigger>()
            .WithId("Pressable")
            .FromComponentSibling()
            .AsTransient();
    }
}