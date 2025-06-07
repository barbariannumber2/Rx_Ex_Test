using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;


namespace PracticeGame
{
    public partial class CommonButton : MonoBehaviour, ICommonButton
    {
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



        public void SetPressable(bool isPressable)
        {
            _isPressable.Value = isPressable;
        }

        public void Press()
        {
            _onPressCommand.OnNext(this.gameObject);
        }

        private void PressableInitialize()
        {
#if UNITY_EDITOR
            OnPointerClick.Subscribe(_ => Debug.Log("OnClicked"));
            OnPointerDown.Subscribe(_ => Debug.Log("OnPressed"));
            OnPointerUp.Subscribe(_ => Debug.Log("OnReleased"));
#endif
        }

        private void PressableOnDestroy()
        {
            _isPressable.Dispose();
            _onPressCommand.Dispose();
        }
    }
}