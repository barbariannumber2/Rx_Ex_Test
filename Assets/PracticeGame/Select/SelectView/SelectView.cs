using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;


namespace PracticeGame
{
    public class SelectView : ViewBase
    {
        private IInputManager _inputManager;

        private ISceneManager _sceneManager;

        private List<ICommonButton> _selectButtons;

        private int _currentIndex = 0;

        [Inject]
        public void Construct(
            IInputManager inputManager,
            ISceneManager sceneManager,
            [Inject(Id = "EasyButton")] ICommonButton easyButton,
            [Inject(Id = "NormalButton")] ICommonButton normalButton,
            [Inject(Id = "HardButton")] ICommonButton hardButton)
        {
            _inputManager = inputManager;
            _sceneManager = sceneManager;
            _selectButtons = new() { easyButton, normalButton, hardButton };

            foreach (var button in _selectButtons)
            {
                button.SetAllReaction(false);
            }
        }

        protected override void OnInitialize()
        {
            IEnumerable<Difficulty> difficulties = System.Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
            foreach (var (button,difficulty) in _selectButtons.Zip(difficulties,(button,difficulty)=>(button,difficulty)))
            {
                button.SetAllReaction(true);
                button.OnPointerClick.SubscribeWithAddTo((data) =>
                {
                    _sceneManager.ChangeScene(SceneType.Play, new PlaySceneData(difficulty));
                }, this);
            }
            //_selectButtons.First()?.Select();
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