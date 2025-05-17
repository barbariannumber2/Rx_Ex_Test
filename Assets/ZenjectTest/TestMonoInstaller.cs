using UnityEngine;
using Zenject;

//ProjectContextに使うInstaller
//DIコンテナ経由で使うものを定義する
namespace ZenjTest
{
public class TestMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
            //恐らく基本はAsSingleとAsTransientの使い分け覚えておけば大丈夫
            //★AsSingleの指定はコンテナごとに一つのインスタンスであることを保証する　(同じものはバインドできない)

            //★AsTransientはInjectごとにインスタンスを生成する

            //AsCachedはSingleに少し近く　利用可能なインスタンスがコンテナ内にあればそれをInjectするが
            //WithIdで別のidを指定すれば同じクラスでもバインド可能　idを割り振るならInject側も使うIDを指定する

            Container.Bind<TestPrefabCompo>()
                .FromComponentInNewPrefabResource("BindTestPrefab")
                .AsSingle()
                .NonLazy();

            Container.Bind<BindTestMono>()
                .To<BindTestMono>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }
}
}