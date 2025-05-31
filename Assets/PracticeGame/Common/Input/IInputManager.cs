using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    public interface IInputManager
    {
        public InputInterfaceType InputType { get; }

        public bool IsTouchEnabled { get; }

        public IObservable<InputInterfaceType> OnChangeInputInterface { get; }

        public void SetTouchEnabled(bool enabled);

        public bool GetAnyKeyDown();

        public bool GetButton(Key key);

        public bool GetButtonDown(Key key);

        public bool GetButtonUp(Key key);

        public float GetButtonHoldTime(Key key);

        public Vector2 GetAxis(Key key);
    }
}