using UnityEngine;
using Zenject;
namespace PracticeGame
{

    public interface ICommonSelectable: ICommandDrivenSelectable, ISelectableChangeSender, ISelectableOperator,
        IPointerHoverEventSender, ICommandDrivenSelectStateChangedSender
    {
        
    }
}

