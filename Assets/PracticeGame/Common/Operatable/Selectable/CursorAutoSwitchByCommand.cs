using UnityEngine;
using Zenject;

namespace PracticeGame
{
    /// <summary>
    /// カーソルオンオフがあるオブジェクトのオンオフ切り替え用
    /// 選択用関数の実行で反応する
    /// </summary>
    [RequireComponent(typeof(ICommandDrivenSelectStateChangedSender))]
    public class CursorAutoSwitchByCommand : MonoBehaviour
    {
        [SerializeField]
        private GameObject _onCursorObject;

        [SerializeField]
        private GameObject _offCursorObject;

        private ICommandDrivenSelectStateChangedSender _selectEventSender = null;

        [Inject]
        public void Construct(ICommandDrivenSelectStateChangedSender selectEventSender)
        {
            _selectEventSender = selectEventSender;
            Debug.Log("CursorAutoSwitchByCommand: Injection Complete");
        }

        private void Awake()
        {
            _selectEventSender ??= GetComponent<ICommandDrivenSelectStateChangedSender>();

            _selectEventSender.OnEnterCommand.SubscribeWithAddTo((_) => ChangeCursor(true), this);
            _selectEventSender.OnExitCommand.SubscribeWithAddTo((_) => ChangeCursor(false), this);

            ChangeCursor(false);
        }
        
        protected void ChangeCursor(bool isActiveCursor)
        {
            _onCursorObject.SetActive(isActiveCursor);
            _offCursorObject.SetActive(isActiveCursor == false);
        }
    }
}