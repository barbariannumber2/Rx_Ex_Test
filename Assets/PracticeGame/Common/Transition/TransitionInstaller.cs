using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "TransitionInstaller", menuName = "Installers/MyInstallers/TransitionInstaller")]
    public class TransitionInstaller : ScriptableObjectInstaller<TransitionInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ITransition>()
                .To<NormalFade>()
                .FromComponentInNewPrefabResource("Transition")
                .AsSingle();

            //多分MonoBehaviourを継承している現状の構造ならこれはNormalFade自体に[SerializeField]で設定した方が良い？
            //NormalFadeをコンポーネントにしたくない場合はInjectが活きそう
            Container.Bind<Image>()
                .WithId("NormalFade")
                .FromComponentSibling();
        }
    }
}