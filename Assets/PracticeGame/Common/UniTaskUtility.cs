using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

namespace PracticeGame
{
    public static class UniTaskUtility
    {
        public static async UniTask RunWithCancellation(Func<CancellationToken, UniTask> taskFunc, int timeoutMs = -1)
        {
            using var cts = new CancellationTokenSource();
            //CancelAfterはUnity向けに最適化されていないので後で変えた方が良い
            if (timeoutMs > 0)
            {
                cts.CancelAfter(timeoutMs); // タイムアウト設定（オプション）
            }

            try
            {
                await taskFunc(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Task was canceled.");
            }
        }


    }
}