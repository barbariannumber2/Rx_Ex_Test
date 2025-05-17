using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace PracticeGame
{
    [RequireComponent(typeof(ObservableEventTrigger))]
    public class CommonSelectableObject : MonoBehaviour,ISelectable,ISelectableChangeSender,ISelectableOperator,IPointerHoverEventSender
    {
        protected ObservableEventTrigger _eventTrigger = null;

        /// <summary>
        /// 選択動作に対してのの反応オンオフ
        /// </summary>
        private readonly BoolReactiveProperty _isSelectable = new(true);
        public bool IsSelectable => _isSelectable.Value;

        public IObservable<bool> OnSelectableChange => _isSelectable.AsObservable();

        public void SetSelectable(bool isSelectable) { _isSelectable.Value = isSelectable; }

        public IObservable<PointerEventData> OnEnterPointer => _eventTrigger.OnPointerEnterAsObservable()
            .Where((data) => _isSelectable.Value);

        public IObservable<PointerEventData> OnExitPointer => _eventTrigger.OnPointerExitAsObservable()
            .Where((data) => _isSelectable.Value);

        [Inject]
        public void Construct([Inject(Id = "Selectable")] ObservableEventTrigger eventTrigger)
        {
            _eventTrigger = eventTrigger;
            Debug.Log("SelectableObject: Injection Complete");
        }
        protected virtual void OnDestroy()
        {
            _isSelectable.Dispose();
        }

        protected virtual void Awake()
        {
            _eventTrigger ??= GetComponent<ObservableEventTrigger>();
            OnEnterPointer.Subscribe(_ => Debug.Log("OnEnter"));
            OnExitPointer.Subscribe(_ => Debug.Log("OnExit"));
        }

    }
}