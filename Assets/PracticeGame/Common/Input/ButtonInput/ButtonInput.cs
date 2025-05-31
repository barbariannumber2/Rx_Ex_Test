using UnityEngine;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    public sealed class ButtonInput : IButtonInput
    {
        private bool _isPrevPressed;

        private bool _isLatestPressed;

        private float _holdTime;

        public float HoldTime => _holdTime;

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            // 押下情報を保持
            _isPrevPressed = _isLatestPressed;

            // ホールド状態の場合は経過時間を更新
            if (GetKey())
            {
                _holdTime += Time.unscaledDeltaTime;
            }
            // ホールド状態でない場合は経過時間をリセット
            else if (GetKeyUp() == false)
            {
                _holdTime = 0;
            }
        }

        public bool GetKeyDown() => (_isPrevPressed == false) && _isLatestPressed;

        public bool GetKey() => _isLatestPressed;

        public bool GetKeyUp() => _isPrevPressed && (_isLatestPressed == false);

        public void OnChangedValue(InputAction.CallbackContext context)
        {
            // 入力中時
            if (context.phase == InputActionPhase.Performed)
            {
                // 既に押されている状態で、入力が呼ばれた場合
                if (_isLatestPressed)
                {
                    // リピート入力とする
                    _isPrevPressed = false;
                }

                _isLatestPressed = true;
            }
            // 入力キャンセル時
            else if (context.phase == InputActionPhase.Canceled)
            {
                _isLatestPressed = false;
            }
        }
    }
}