using System;
using UniRx;

namespace PracticeGame
{
    /// <summary>
    /// IPointerEnterSenderとの対称性を表現したい命名をしている
    /// ゲームパッドやキーボードの入力に対応させるなどで
    /// 「選択関数の呼び出し」が行われた場合のEnterイベントを公開する
    /// </summary>
    public interface ICommandDrivenEnterSender
    {
        public IObservable<Unit> OnEnterCommand { get; }
    }
}