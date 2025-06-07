using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;
using System.Reflection;

namespace PracticeGame
{
    public class SelectView : ViewBase
    {
        private IInputManager _inputManager;

        private List<ICommonPressable> _selectButtons;

        private int _currentIndex = 0;

        [Inject]
        public void Construct(
            IInputManager inputManager,
            [Inject(Id = "EasyButton")] ICommonPressable easyButton,
            [Inject(Id = "NormalButton")] ICommonPressable normalButton,
            [Inject(Id = "HardButton")] ICommonPressable hardButton)
        {
            _inputManager = inputManager;
            _selectButtons = new() { easyButton, normalButton, hardButton };
        }

        protected override void OnUpdate()
        {
            if (_inputManager.GetButtonDown(Key.Submit))
            {
                _selectButtons[_currentIndex].Press();
            }
            else if (_inputManager.GetButtonDown(Key.CursorUp))
            {
                _currentIndex = (_currentIndex - 1 + _selectButtons.Count) % _selectButtons.Count;
            }
            else if (_inputManager.GetButtonDown(Key.CursorDown))
            {
                _currentIndex = (_currentIndex + 1) % _selectButtons.Count;

            }
        }
    }
}