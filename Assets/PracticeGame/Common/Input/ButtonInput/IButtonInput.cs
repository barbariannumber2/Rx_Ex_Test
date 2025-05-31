using UnityEngine;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    public interface IButtonInput
    {
        public float HoldTime { get; }

        public void Update();

        public bool GetKeyDown();

        public bool GetKey();

        public bool GetKeyUp();

        public void OnChangedValue(InputAction.CallbackContext context);
    }
}