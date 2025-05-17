using UnityEngine;

namespace PracticeGame
{
    //ポインターの重なり開始・終了イベント両方を持つ
    public interface IPointerHoverEventSender:IPointerEnterSender,IPointerExitSender
    {

    }
}