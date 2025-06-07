using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace PracticeGame
{
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class CommonSelectableObject : MonoBehaviour,ICommonSelectable, IInitializable
    {
        protected ObservableEventTrigger _eventTrigger = null;

        /// <summary>選択動作に対しての反応オンオフ</summary>
        private readonly BoolReactiveProperty _isSelectable = new(true);

        private Subject<GameObject> _onEnterCommand = new();
        private Subject<GameObject> _onExitCommand = new();

        /// <summary>選択状態かどうか</summary>
        public bool IsSelected { get; private set; } = false;

        public bool IsSelectable => _isSelectable.Value;
        public IObservable<bool> OnSelectableChange => _isSelectable.AsObservable();


        public IObservable<PointerEventData> OnEnterPointer => _eventTrigger.OnPointerEnterAsObservable()
            .Where((data) => _isSelectable.Value);
        public IObservable<PointerEventData> OnExitPointer => _eventTrigger.OnPointerExitAsObservable()
            .Where((data) => _isSelectable.Value);

        public IObservable<GameObject> OnEnterCommand => _onEnterCommand.Where((go) => _isSelectable.Value && IsSelected == false);
        public IObservable<GameObject> OnExitCommand => _onExitCommand.Where((go) => _isSelectable.Value && IsSelected);

        [Inject]
        public void Construct([Inject(Id = "Selectable")] ObservableEventTrigger eventTrigger)
        {
            _eventTrigger = eventTrigger;
            Debug.Log("SelectableObject: Injection Complete");
        }


        public virtual void Initialize()
        {
#if UNITY_EDITOR
            OnEnterPointer.Subscribe(_ => Debug.Log("OnEnter"));
            OnExitPointer.Subscribe(_ => Debug.Log("OnExit"));
#endif
            OnEnterPointer.Subscribe(go =>
            {
                IsSelected = true;
#if UNITY_EDITOR
                Debug.Log("OnSelected");
#endif
            });

            OnExitPointer.Subscribe(go =>
            {
                IsSelected = false;
#if UNITY_EDITOR
                Debug.Log("OnDeselected");
#endif
            });

            OnEnterCommand.Subscribe(go =>
            {
                IsSelected = true;
#if UNITY_EDITOR
                Debug.Log("OnSelected");
#endif
            });

            OnExitCommand.Subscribe(go =>
            {
                IsSelected = false;
#if UNITY_EDITOR
                Debug.Log("OnDeselected");
#endif
            });
        }

        /// <summary>
        /// falseにすると選択状態の変化イベントが発火しなくなる
        /// </summary>
        /// <param name="isSelectable"></param>
        public void SetSelectable(bool isSelectable) 
        { 
            _isSelectable.Value = isSelectable;
        }

        public void Select()
        {
            _onEnterCommand.OnNext(this.gameObject);
        }

        public void Deselect()
        {
            _onExitCommand.OnNext(this.gameObject);
        }

        protected virtual void OnDestroy()
        {
            _isSelectable.Dispose();
            _onEnterCommand.Dispose();
            _onExitCommand.Dispose();
            _eventTrigger = null;
        }
    }
}