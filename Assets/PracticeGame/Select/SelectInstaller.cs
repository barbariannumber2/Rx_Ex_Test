using PracticeGame;
using UnityEngine;
using Zenject;

public class SelectInstaller : MonoInstaller
{
    [SerializeField]
    private GameObjectContext _selectViewContext;

    public override void InstallBindings()
    {
        ////　サブコンテナ利用例1
        ////  これだとTo<SelectView>にしているのでサブコンテナでのBindはBind<SelectView>()で行う必要がある
        //Container.Bind<IView>()
        //    .To<SelectView>()
        //    .FromSubContainerResolve()
        //    .ByNewContextPrefab(_selectViewContext)
        //    .AsCached();

        //  サブコンテナ利用例2
        //  ここでToを書かないなら
        //  サブコンテナでのBindはBind<IView>().To<SelectView>で行える
        //  サブコンテナで解決するならToまでサブコンテナに任せるこちらの方が正しそう
        Container.Bind<IView>()
            .FromSubContainerResolve()
            .ByNewContextPrefab(_selectViewContext)
            .AsCached();
    }
}