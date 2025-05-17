using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    /// <summary>
    /// カーソルオンオフを使用するオブジェクトの見た目表示用
    /// </summary>
    [RequireComponent(typeof(IPointerHoverEventSender))]
    public class CursorView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _onCursorObject;

        [SerializeField]
        private GameObject _offCursorObject;

        private IPointerHoverEventSender _selectableObject = null;

        [Inject]
        public void Construct(IPointerHoverEventSender pointerHoverEventSender)
        {
            _selectableObject = pointerHoverEventSender;
        }

        private void Awake()
        {
            _selectableObject ??= GetComponent<IPointerHoverEventSender>();

            _selectableObject.OnEnterPointer.SubscribeWithAddTo((_) => ChangeCursor(true), this);
            _selectableObject.OnExitPointer.SubscribeWithAddTo((_) => ChangeCursor(false), this);

            ChangeCursor(false);
        }

        private void ChangeCursor(bool isActiveCursor) 
        {
            _onCursorObject.SetActive(isActiveCursor);
            _offCursorObject.SetActive(isActiveCursor == false);
        }

    }
}