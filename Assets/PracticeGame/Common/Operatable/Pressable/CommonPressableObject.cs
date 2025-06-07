using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace PracticeGame
{

    [RequireComponent(typeof(ObservableEventTrigger))]
    public class CommonPressableObject : MonoBehaviour, ICommonPressable ,IInitializable
    {
        protected ObservableEventTrigger _eventTrigger = null;

        /// <summary>クリックや押したときみたいな決定系機能のオンオフ</summary>
        private readonly BoolReactiveProperty _isPressable = new(true);

        private Subject<GameObject> _onPressCommand = new();

        public bool IsPressable => _isPressable.Value;
        public IObservable<bool> OnPressableChange => _isPressable.AsObservable();

        public IObservable<PointerEventData> OnPointerClick => _eventTrigger.OnPointerClickAsObservable()
            .Where((data) => _isPressable.Value);
        public IObservable<PointerEventData> OnPointerDown => _eventTrigger.OnPointerDownAsObservable()
            .Where((data) => _isPressable.Value);
        public IObservable<PointerEventData> OnPointerUp => _eventTrigger.OnPointerUpAsObservable()
            .Where((data) => _isPressable.Value);

        public IObservable<GameObject> OnPressCommand => _onPressCommand.Where((go) => _isPressable.Value);

        [Inject]
        public void Construct([Inject(Id = "Pressable")] ObservableEventTrigger eventTrigger)
        {
            _eventTrigger = eventTrigger;
            Debug.Log("PressableObject: Injection Complete");
        }

        public void Initialize()
        {
#if UNITY_EDITOR
            OnPointerClick.Subscribe(_ => Debug.Log("OnClicked"));
            OnPointerDown.Subscribe(_ => Debug.Log("OnPressed"));
            OnPointerUp.Subscribe(_ => Debug.Log("OnReleased"));
#endif
        }

        public void SetPressable(bool isPressable)
        { 
            _isPressable.Value = isPressable;
        }

        public void Press()
        {
            _onPressCommand.OnNext(this.gameObject);
        }


        protected void OnDestroy()
        {
            _isPressable.Dispose();
            _onPressCommand.Dispose();
            _eventTrigger = null;
        }
    }
}