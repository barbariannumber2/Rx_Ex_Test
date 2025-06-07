using System;
using UnityEngine;

namespace PracticeGame
{
    public interface ICommandDrivenPressSender
    {
        public IObservable<GameObject> OnPressCommand { get; }
    }
}