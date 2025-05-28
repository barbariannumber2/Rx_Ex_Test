using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;


namespace PracticeGame
{
    public class CommonPressableObject : CommonSelectableObject, ICommonPressable
    {
        /// <summary>
        /// クリックや押したときみたいな決定系機能のオンオフ
        /// 決定出来ないけどマウスが当たってる時の見た目変化は行いたいみたいな時のために別で管理
        /// </summary>
        private readonly BoolReactiveProperty _isPressable = new(true);

        private Subject<Unit> _onPressCommand = new();

        public bool IsPressable => _isPressable.Value;

        public IObservable<bool> OnPressableChange => _isPressable.AsObservable();

        public void SetPressable(bool isPressable) { _isPressable.Value = isPressable; }

        public IObservable<PointerEventData> OnPointerClick => _eventTrigger.OnPointerClickAsObservable()
            .Where((data) => _isPressable.Value);

        public IObservable<PointerEventData> OnPointerDown => _eventTrigger.OnPointerDownAsObservable()
            .Where((data) => _isPressable.Value);

        public IObservable<PointerEventData> OnPointerUp => _eventTrigger.OnPointerUpAsObservable()
            .Where((data) => _isPressable.Value);

        public IObservable<Unit> OnPressCommand => _onPressCommand.Where((data) => _isPressable.Value);

        public override void Initialize()
        {
            base.Initialize();
#if UNITY_EDITOR
            OnPointerClick.Subscribe(_ => Debug.Log("OnClicked"));
            OnPointerDown.Subscribe(_ => Debug.Log("OnPressed"));
            OnPointerUp.Subscribe(_ => Debug.Log("OnReleased"));
#endif
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
            _onPressCommand.Dispose();
        }

        public void Press()
        {
            _onPressCommand.OnNext(Unit.Default);
        }
    }
}