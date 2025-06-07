using UnityEngine;

namespace PracticeGame
{
    // コマンド駆動の選択状態変更イベントを両方持つ
    // 少々名前が長いのが気になるが、明確さを持たせたいのでこのまま
    public interface ICommandDrivenSelectStateChangedSender : ICommandDrivenEnterSender,ICommandDrivenExitSender
    {

    }
}