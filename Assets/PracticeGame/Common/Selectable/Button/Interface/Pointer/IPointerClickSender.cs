using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    public interface IPointerClickSender
    {
        public IObservable<PointerEventData> OnClicked {  get; }
    }
}