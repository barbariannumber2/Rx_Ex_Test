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

        /// <summary>
        /// 選択動作に対しての反応オンオフ
        /// </summary>
        private readonly BoolReactiveProperty _isSelectable = new(true);

        private Subject<Unit> _onEnterCommand = new();

        private Subject<Unit> _onExitCommand = new();

        public bool IsSelected { get; private set; } = false;

        public bool IsSelectable => _isSelectable.Value;

        public IObservable<bool> OnSelectableChange => _isSelectable.AsObservable();

        /// <summary>
        /// falseにすると選択状態の変化イベントが発火しなくなる
        /// </summary>
        /// <param name="isSelectable"></param>
        public void SetSelectable(bool isSelectable) { _isSelectable.Value = isSelectable; }

        public IObservable<PointerEventData> OnEnterPointer => _eventTrigger.OnPointerEnterAsObservable()
            .Where((data) => _isSelectable.Value);

        public IObservable<PointerEventData> OnExitPointer => _eventTrigger.OnPointerExitAsObservable()
            .Where((data) => _isSelectable.Value);

        public IObservable<Unit> OnEnterCommand => _onEnterCommand.Where((_) => _isSelectable.Value && IsSelected == false);

        public IObservable<Unit> OnExitCommand => _onExitCommand.Where((_) => _isSelectable.Value && IsSelected);

        [Inject]
        public void Construct([Inject(Id = "Selectable")] ObservableEventTrigger eventTrigger)
        {
            _eventTrigger = eventTrigger;
        }

        protected virtual void OnDestroy()
        {
            _isSelectable.Dispose();
            _onEnterCommand.Dispose();
            _onExitCommand.Dispose();
            _eventTrigger = null;
        }

        public virtual void Initialize()
        {
#if UNITY_EDITOR
            Debug.Log("SelectableObject: Injection Complete");

            OnEnterPointer.Subscribe(_ => Debug.Log("OnEnter"));
            OnExitPointer.Subscribe(_ => Debug.Log("OnExit"));
#endif
            OnEnterPointer.Subscribe(_ =>
            {
                IsSelected = true;
#if UNITY_EDITOR
                Debug.Log("OnSelected");
#endif
            });

            OnExitPointer.Subscribe(_ =>
            {
                IsSelected = false;
#if UNITY_EDITOR
                Debug.Log("OnDeselected");
#endif
            });

            OnEnterCommand.Subscribe(_ =>
            {
                IsSelected = true;
#if UNITY_EDITOR
                Debug.Log("OnSelected");
#endif
            });

            OnExitCommand.Subscribe(_ =>
            {
                IsSelected = false;
#if UNITY_EDITOR
                Debug.Log("OnDeselected");
#endif
            });
        }

        public void Select()
        {
            _onEnterCommand.OnNext(Unit.Default);
        }

        public void Deselect()
        {
            _onExitCommand.OnNext(Unit.Default);
        }
    }
}