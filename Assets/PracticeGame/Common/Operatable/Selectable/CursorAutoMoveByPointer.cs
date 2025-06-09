using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.EventSystems;


namespace PracticeGame
{
    public class CursorAutoMoveByPointer : MonoBehaviour
    {
        public interface IInjectData
        {
            public List<IPointerHoverEventSender> selectables { get; }
            public float moveTimeBySecond { get; }
        }

        public class InjectData : IInjectData
        {
            public List<IPointerHoverEventSender> selectables { get; private set; }
            public float moveTimeBySecond { get; private set; }
            public InjectData(List<IPointerHoverEventSender> selectables, float moveTimeBySecond)
            {
                this.selectables = selectables;
                this.moveTimeBySecond = moveTimeBySecond;
            }
        }
        

        [SerializeField]
        private GameObject _cursorObject;

        [SerializeField]
        private float _moveTimeBySecond = 0f;

        /// <summary>
        /// TがinterfaceのListはシリアライズ出来ないのでMonoBehaviourで受け取り,変換
        /// </summary>
        [SerializeField,Tooltip("IPointerHoverEventSenderを実装しているMonoBehaviourを設定してください")]
        private List<MonoBehaviour> _selectablesMono;

        private IInjectData _injectData;

        private List<IPointerHoverEventSender> _selectables = new();

        private CancellationTokenSource _cancellationTokenSource = new();

        private UniTask _moveTask = default;

        [Inject]
        private void Construct(IInjectData injectData)
        {
            _injectData = injectData;
   
            Debug.Log("CursorAutoMoveByPointer: Injection Complete");
        }

        private void Start()
        {
            foreach (var selectable in _injectData.selectables)
            {
                AddSelectable(selectable);
            }

            // MonoBehaviourからIPointerHoverEventSenderに変換
            foreach (var selectableMono in _selectablesMono)
            {
                if (selectableMono is IPointerHoverEventSender selectable)
                {
                    AddSelectable(selectable);
                }
                else
                {
                    Debug.LogError($"CursorAutoMoveByPointer: {selectableMono.name} does not implement IPointerHoverEventSender.");
                }
            }
            if (_cursorObject == null)
            {
                Debug.LogError("CursorAutoMoveByPointer: Cursor object is not assigned.");
            }
        }

        public void AddSelectable(IPointerHoverEventSender selectable) 
        {
            if (_selectables.Contains(selectable))
            {
                Debug.LogWarning($"CursorAutoMoveByPointer: {selectable} is already added.");
                return;
            }
            _selectables.Add(selectable);
            selectable.OnEnterPointer.SubscribeWithAddTo((data) => SubscribeMoveFunc(data), this);
        }

        public void ShowCursor()
        {
            _cursorObject.SetActive(true);
        }

        public void HideCursor()
        {
            _cursorObject.SetActive(false);
        }

        private void SubscribeMoveFunc(PointerEventData data)
        {
            // 既に移動中のタスクがある場合はキャンセル
            if (_moveTask.Status == UniTaskStatus.Pending)
            {
                _cancellationTokenSource.Cancel();
            }
            try
            {
                _moveTask = MoveCursor(data.pointerEnter, _moveTimeBySecond, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("CursorAutoMoveByPointer: Move task was canceled.");
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        private async UniTask MoveCursor(GameObject target,float moveTime, CancellationToken token) 
        {
            var targetPos = target.transform.position;
            var cursorStartPos = _cursorObject.transform.position;

            if (moveTime <= 0f)
            {
                _cursorObject.transform.position = targetPos;
                return;
            }


            for(float t = 0; t < moveTime; t += Time.deltaTime)
            {
                float progress = t / moveTime;
                _cursorObject.transform.position = Vector3.Lerp(cursorStartPos, targetPos, progress);
                await UniTask.DelayFrame(1);
            }

            _cursorObject.transform.position = targetPos;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}