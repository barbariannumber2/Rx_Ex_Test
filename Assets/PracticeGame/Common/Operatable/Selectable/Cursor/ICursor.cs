using UnityEngine;

namespace PracticeGame
{
    /// <summary>
    /// カーソルのインターフェース
    /// の予定だが、Serializefieldでインターフェースは扱えないので保留
    /// MonoBehaviour継承のclassでラッパー作るのが手っ取り早いが...
    /// </summary>
    public interface ICursor
    {
        public Vector3 Position { get; set; }
        public void SetState(bool isOn);
    }
}