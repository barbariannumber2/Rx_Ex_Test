using UnityEngine;

namespace PracticeGame
{
    public interface ICommonPressable : ICommandDrivenPressable, IPressableChangeSender, IPressableOperator, 
        IMultiInputPressEventSender
    {

    }
}