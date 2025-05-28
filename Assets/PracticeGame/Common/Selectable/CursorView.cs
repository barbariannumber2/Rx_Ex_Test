using UnityEngine;
using Zenject;

namespace PracticeGame
{
    /// <summary>
    /// カーソルオンオフを使用するオブジェクトの見た目表示用
    /// </summary>
    [RequireComponent(typeof(IMultiInputSelectEventSender))]
    public class CursorView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _onCursorObject;

        [SerializeField]
        private GameObject _offCursorObject;

        private IMultiInputSelectEventSender _selectEventSender = null;

        [Inject]
        public void Construct(IMultiInputSelectEventSender selectEventSender)
        {
            _selectEventSender = selectEventSender;
        }

        private void Awake()
        {
            _selectEventSender ??= GetComponent<IMultiInputSelectEventSender>();

            _selectEventSender.OnEnterPointer.SubscribeWithAddTo((_) => ChangeCursor(true), this);
            _selectEventSender.OnExitPointer.SubscribeWithAddTo((_) => ChangeCursor(false), this);

            _selectEventSender.OnEnterCommand.SubscribeWithAddTo((_) => ChangeCursor(true), this);
            _selectEventSender.OnExitCommand.SubscribeWithAddTo((_) => ChangeCursor(false), this);

            ChangeCursor(false);
        }

        private void ChangeCursor(bool isActiveCursor) 
        {
            _onCursorObject.SetActive(isActiveCursor);
            _offCursorObject.SetActive(isActiveCursor == false);
        }

    }
}