using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PracticeGame
{
    public class CommonButtonView : CommonSelectableObject, IPressable, IPressableChangeSender, IPressableOperator, IPointerPressEventSender
    {
        /// <summary>
        /// クリックや押したときみたいな決定系機能のオンオフ
        /// 決定出来ないけどマウスが当たってる時の見た目変化は行いたいみたいな時のために別で管理
        /// </summary>
        private readonly BoolReactiveProperty _isPressable = new(true);

        public bool IsPressable => _isPressable.Value;

        public IObservable<bool> OnPressableChange => _isPressable.AsObservable();

        public void SetPressable(bool isPressable) { _isPressable.Value = isPressable; }

        public IObservable<PointerEventData> OnClicked => _eventTrigger.OnPointerClickAsObservable()
            .Where((data) => _isPressable.Value);

        public IObservable<PointerEventData> OnPressed => _eventTrigger.OnPointerDownAsObservable()
            .Where((data) => _isPressable.Value);

        public IObservable<PointerEventData> OnReleased => _eventTrigger.OnPointerUpAsObservable()
            .Where((data) => _isPressable.Value);

        protected override void Awake()
        {
            base.Awake();
            OnClicked.Subscribe(_ => Debug.Log("OnClicked"));
            OnPressed.Subscribe(_ => Debug.Log("OnPressed"));
            OnReleased.Subscribe(_ => Debug.Log("OnReleased"));
        }

        /// <summary>
        /// 選択と決定両方の機能を一括操作する
        /// ボタンを押せなくするのではなく反応しなくする場合に使う
        /// </summary>
        /// <param name="isReaction"></param>
        public virtual void SetAllReaction(bool isReaction)
        {
            SetSelectable(isReaction);
            SetPressable(isReaction);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _isPressable.Dispose();
        }

        public void Press()
        {
            
        }
    }
}