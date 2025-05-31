using UnityEngine;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    public interface IAxisInput
    {
        public Vector2 Value { get; }

        public void OnChangedValue(InputAction.CallbackContext context);
    }
}