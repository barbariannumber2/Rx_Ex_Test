using UniRx;
using UnityEngine;
using System;

namespace RxTest
{
    public class RxTestPresenter : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void ConnectModelView(RxTestModel connectModel, RxTestView connectView)
        {
            _disposables.Add(connectModel);
            connectView.Button.OnClickAsObservable().Subscribe(value =>
            {
                connectModel.AddCount();
            }).AddTo(_disposables);

            connectModel.Count.Subscribe(value =>
            {
                connectView.SetValueText(value);
            }).AddTo(connectView);
        }

        //購読解除
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}