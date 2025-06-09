using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class CursorAutoMoveByCommand : MonoBehaviour
    {
        public interface IInjectData
        {
            public List<ICommandDrivenEnterSender> selectables { get; }
            public float moveTimeBySecond { get; }
        }

        public class InjectData : IInjectData
        {
            public List<ICommandDrivenEnterSender> selectables { get; private set; }
            public float moveTimeBySecond { get; private set; }
            public InjectData(List<ICommandDrivenEnterSender> selectables, float moveTimeBySecond)
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
        [SerializeField, Tooltip("ICommandDrivenEnterSenderを実装しているMonoBehaviourを設定してください")]
        private List<MonoBehaviour> _selectablesMono;

        [SerializeField]
        private List<ICommandDrivenEnterSender> _selectables = new();

        private IInjectData _injectData;

        private CancellationTokenSource _cancellationTokenSource = new();

        private UniTask _moveTask = default;

        

        [Inject]
        private void Construct(IInjectData injectData)
        {
            _injectData = injectData;


            Debug.Log("CursorAutoMoveByCommand: Injection Complete");
        }

        private void Start()
        {
            foreach (var selectable in _injectData.selectables)
            {
                AddSelectable(selectable);
            }

            // MonoBehaviourからICommandDrivenEnterSenderに変換
            foreach (var selectableMono in _selectablesMono)
            {
                if (selectableMono is ICommandDrivenEnterSender selectable)
                {
                    AddSelectable(selectable);
                }
                else
                {
                    Debug.LogError($"CursorAutoMoveByCommand: {selectableMono.name} does not implement ICommandDrivenEnterSender.");
                }
            }
        }

        public void AddSelectable(ICommandDrivenEnterSender selectable)
        {
            if (_selectables.Contains(selectable))
            {
                Debug.LogWarning($"CursorAutoMoveByCommand: {selectable} is already added.");
                return;
            }
            _selectables.Add(selectable);
            selectable.OnEnterCommand.SubscribeWithAddTo((data) => SubscribeMoveFunc(data), this);
        }

        public void ShowCursor()
        {
            _cursorObject.SetActive(true);
        }

        public void HideCursor()
        {
            _cursorObject.SetActive(false);
        }

        private void SubscribeMoveFunc(GameObject target)
        {
            // 既に移動中のタスクがある場合はキャンセル
            if (_moveTask.Status == UniTaskStatus.Pending)
            {
                _cancellationTokenSource.Cancel();
            }
            try
            {
                _moveTask = MoveCursor(target, _moveTimeBySecond, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("CursorAutoMoveByCommand: Move task was canceled.");
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        private async UniTask MoveCursor(GameObject target, float moveTime, CancellationToken token)
        {
            var targetPos = target.transform.position;
            var cursorStartPos = _cursorObject.transform.position;

            if (moveTime <= 0f)
            {
                _cursorObject.transform.position = targetPos;
                return;
            }


            for (float t = 0; t < moveTime; t += Time.deltaTime)
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