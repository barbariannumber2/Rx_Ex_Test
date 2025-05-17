using System;
using UnityEngine;

namespace PracticeGame
{
    public interface ISelectableChangeSender
    {
        public IObservable<bool> OnSelectableChange { get; }
    }
}