using UnityEngine;

namespace PracticeGame
{
    public interface ICommandDrivenPressable:IPressable
    {
        public void Press();
    }
}