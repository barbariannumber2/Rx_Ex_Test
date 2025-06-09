using UnityEngine;
using Zenject;

namespace PracticeGame
{
    /// <summary>
    /// カーソルオンオフがあるオブジェクトのオンオフ切り替え用
    /// マウスポインターで反応する
    /// </summary>
    [RequireComponent(typeof(IPointerHoverEventSender))]
    public class CursorAutoSwitchByPointer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _onCursorObject;

        [SerializeField]
        private GameObject _offCursorObject;

        [SerializeField,Tooltip("ポインターが外れた時に自動でオフにするかそのままにするか")]
        private bool _isAutoOffOnExit = true;

        private IPointerHoverEventSender _selectEventSender = null;

        [Inject]
        public void Construct(IPointerHoverEventSender selectEventSender)
        {
            _selectEventSender = selectEventSender;
            Debug.Log("CursorAutoSwitchByPointer: Injection Complete");
        }

        private void Awake()
        {
            _selectEventSender ??= GetComponent<IPointerHoverEventSender>();

            _selectEventSender.OnEnterPointer.SubscribeWithAddTo((_) => ChangeCursor(true), this);
            _selectEventSender.OnExitPointer.SubscribeWithAddTo((_) => { if (_isAutoOffOnExit) ChangeCursor(false); }, this);

            ChangeCursor(false);
        }

        private void ChangeCursor(bool isActiveCursor) 
        {

            _onCursorObject.SetActive(isActiveCursor);
            _offCursorObject.SetActive(isActiveCursor == false);
        }

    }
}