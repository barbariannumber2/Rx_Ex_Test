using PracticeGame;
using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public interface ICommonButton : ICommonSelectable, ICommonPressable,IInitializable
    {
        public void SetAllReaction(bool isReaction);
    }
}