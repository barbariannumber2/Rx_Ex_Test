using System;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    public interface IPointerExitSender
    {
        public IObservable<PointerEventData> OnExitPointer { get; }
    }
}