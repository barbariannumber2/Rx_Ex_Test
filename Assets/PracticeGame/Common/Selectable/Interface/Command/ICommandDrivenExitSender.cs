using System;
using UniRx;

namespace PracticeGame
{
    /// <summary>
    /// IPointerExitSenderとの対称性を表現したい命名をしている
    /// ゲームパッドやキーボードの入力に対応させるなどで
    /// 「選択関数の呼び出し」が行われた場合のExitイベントを公開する
    /// </summary>
    public interface ICommandDrivenExitSender
    {
        public IObservable<Unit> OnExitCommand { get; }
    }
}