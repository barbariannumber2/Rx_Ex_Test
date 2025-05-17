using System;
using UnityEngine;

namespace PracticeGame
{
    public interface IPressableChangeSender
    {
        public IObservable<bool> OnPressableChange { get; }
    }
}