using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "InputManagerInstaller", menuName = "Installers/MyInstallers/InputManagerInstaller")]
    public class InputManagerInstaller : ScriptableObjectInstaller<InputManagerInstaller>
    {
        [SerializeField]
        private InputActionLinks _inputActionLinks = null;

        public override void InstallBindings()
        {
            Container.Bind<IInputManager>()
                .To<InputManager>()
                .FromComponentInNewPrefabResource("InputManager")
                .AsSingle()
                .NonLazy();

            Container.Bind<InputActionLinks>()
                .FromScriptableObject(_inputActionLinks)
                .AsSingle();

            Container.Bind<PlayerInput>()
                .FromComponentSibling()
                .AsTransient();

            ////EventSystemInstallerでバインドしているのでコメントアウト　解除すると競合
            //Container.Bind<EventSystem>()
            //    .FromComponentInNewPrefabResource("EventSystem")
            //    .AsSingle()
            //    .IfNotBound();
        }
    }
}