using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    public interface IPointerDownSender
    {
        public IObservable<PointerEventData> OnPointerDown {  get; }
    }
}