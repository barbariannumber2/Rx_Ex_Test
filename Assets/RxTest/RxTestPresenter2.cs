using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace RxTest
{
    public class RxTestPresenter2 : IDisposable
    {
        // 各Modelごとの購読を管理するDictionary
        private readonly Dictionary<RxTestModel, CompositeDisposable> _modelDisposables = new Dictionary<RxTestModel, CompositeDisposable>();

        public void ConnectModelView(RxTestModel connectModel, RxTestView connectView)
        {
            // 各Model用のCompositeDisposableを作成
            var modelDisposable = new CompositeDisposable();
            _modelDisposables.Add(connectModel, modelDisposable);

            // Modelの購読を追加
            modelDisposable.Add(connectModel);

            // ボタンクリックの購読
            connectView.Button.OnClickAsObservable().Subscribe(value =>
            {
                connectModel.AddCount();
            }).AddTo(modelDisposable);

            // Countの購読
            connectModel.Count.Subscribe(value =>
            {
                connectView.SetValueText(value);
            }).AddTo(modelDisposable);
        }

        // 特定のModelの購読だけを解除
        public void DisposeModel(RxTestModel model)
        {
            if (_modelDisposables.TryGetValue(model, out var disposable))
            {
                disposable.Dispose();
                _modelDisposables.Remove(model);
            }
        }

        // すべての購読を解除
        public void Dispose()
        {
            foreach (var disposable in _modelDisposables.Values)
            {
                disposable.Dispose();
            }
            _modelDisposables.Clear();
        }
    }
}