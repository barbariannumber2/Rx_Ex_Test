using System;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    public interface IPointerEnterSender
    {
        public IObservable<PointerEventData> OnEnterPointer { get; }
    }
}