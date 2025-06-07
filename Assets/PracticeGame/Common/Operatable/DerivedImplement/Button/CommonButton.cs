using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public partial class CommonButton : MonoBehaviour,ICommonButton
    {
        protected ObservableEventTrigger _eventTrigger = null;

        [Inject]
        public void Construct([Inject(Id = "Button")] ObservableEventTrigger eventTrigger)
        {
            _eventTrigger = eventTrigger;
            Debug.Log("CommonButton: Injection Complete");
        }

        public virtual void Initialize()
        {
            SelectableInitialize();
            PressableInitialize();
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


        protected virtual void OnDestroy()
        {
            SelectableOnDestroy();
            PressableOnDestroy();
            
            _eventTrigger = null;
            
        }


    }
}