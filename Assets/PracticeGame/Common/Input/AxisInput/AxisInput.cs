using PracticeGame;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    public sealed class AxisInput : IAxisInput
    {
        /// <summary>
        /// 値
        /// </summary>
        public Vector2 Value { get; private set; }

        /// <summary>
        /// 値変更時のイベント
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnChangedValue(InputAction.CallbackContext context)
        {
            Value = context.ReadValue<Vector2>();
        }
    }
}