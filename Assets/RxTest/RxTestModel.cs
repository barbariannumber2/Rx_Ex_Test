using UnityEngine;
using UniRx;
using System;

namespace RxTest
{
    public class RxTestModel : IDisposable
    {
        public IReadOnlyReactiveProperty<int> Count => _count;

        private readonly IntReactiveProperty _count = new IntReactiveProperty(0);

        private bool _disposed = false;

        ~RxTestModel()
        {
            Debug.Log("デストラクタ");
            Dispose(false);
        }

        public void AddCount()
        {
            Debug.Log($"今から加算{_count.Value}");
            _count.Value++;//ここで即イベントが呼ばれるのでログ出力はイベント処理の後
            Debug.Log(_count.Value);
            Debug.Log($"加算終わり{_count.Value}");
        }

        public void Dispose()
        {
            Debug.Log("Dispose()");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    _count.Dispose();
                    Debug.Log("破棄");
                }
 
                _disposed = true;
            }
        }
    }
}