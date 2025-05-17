using System;
using UniRx;
using UnityEngine;

namespace PracticeGame
{
    public static class UniRxUtility
    {
        /// <summary>
        /// SubscribeとAddToの併用忘れケア用関数
        /// </summary>
        public static IDisposable SubscribeWithAddTo<T>(
            this IObservable<T> observable,
            Action<T> onNext,
            Component disposable)
        {
            if (disposable == null)
            {
                Debug.LogError("SubscribeWithAddTo: 引数Component が null");
                return Disposable.Empty;
            }
            return observable.Subscribe(onNext).AddTo(disposable);
        }

        /// <summary>
        /// SubscribeとAddToの併用忘れケア用関数
        /// </summary>
        public static IDisposable SubscribeWithAddTo<T>(
            this IObservable<T> observable,
            Action<T> onNext,
            CompositeDisposable disposables)
        {
            if (disposables == null)
            {
                Debug.LogError("SubscribeWithAddTo: 引数のCompositeDisposable が null");
                return Disposable.Empty;
            }
            return observable.Subscribe(onNext).AddTo(disposables);
        }
    }
}