using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    public interface IPointerUpSender
    {
        public IObservable<PointerEventData> OnReleased {  get; }
    }
}