using System.Collections.Generic;
using Zenject;


namespace PracticeGame
{
    public class SelectView : ViewBase
    {
        private IInputManager _inputManager;

        private List<ICommonButton> _selectButtons;

        private int _currentIndex = 0;

        [Inject]
        public void Construct(
            IInputManager inputManager,
            [Inject(Id = "EasyButton")] ICommonButton easyButton,
            [Inject(Id = "NormalButton")] ICommonButton normalButton,
            [Inject(Id = "HardButton")] ICommonButton hardButton)
        {
            _inputManager = inputManager;
            _selectButtons = new() { easyButton, normalButton, hardButton };

            foreach (var button in _selectButtons)
            {
                button.SetAllReaction(false);
            }
        }

        protected override void OnInitialize()
        {
            foreach (var button in _selectButtons)
            {
                button.SetAllReaction(true);
            }
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
                _selectButtons[_currentIndex].Select();
            }
            else if (_inputManager.GetButtonDown(Key.CursorDown))
            {
                _currentIndex = (_currentIndex + 1) % _selectButtons.Count;
                _selectButtons[_currentIndex].Select();

            }
        }
    }
}