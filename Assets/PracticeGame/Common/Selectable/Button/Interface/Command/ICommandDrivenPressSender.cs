using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    public interface ICommandDrivenPressSender
    {
        public IObservable<Unit> OnPressCommand { get; }
    }
}