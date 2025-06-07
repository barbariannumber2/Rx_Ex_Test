using UnityEngine;

namespace PracticeGame
{
    /// <summary>
    /// マウス操作による選択イベントとコマンド操作による選択イベント全てをまとめたインターフェース
    /// <para></para>
    /// 各操作のExitをまとめたインターフェースなど、イベント単位のインターフェースを合体させて作ることも考えたが
    /// この先の拡張で各操作の固有イベントなどが生まれる可能性を考慮して
    /// 操作ごとインターフェースをまとめて作る方が良いと判断
    /// </summary>
    public interface IMultiInputSelectEventSender
        : ICommandDrivenSelectStateChangedSender, IPointerHoverEventSender
    {

    }
}

