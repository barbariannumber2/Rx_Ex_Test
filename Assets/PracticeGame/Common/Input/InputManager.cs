using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UniRx;
using Zenject;

namespace PracticeGame
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        private InputActionLinks _inputActionLinks = null;

        private Dictionary<Key, IButtonInput> _buttonList = new ();

        private Dictionary<Key, IAxisInput> _axisList = new ();

        private Subject<InputInterfaceType> _onChangeInputInterface = new ();

        private EventSystem _eventSystem;

        private PlayerInput _input;

        public InputInterfaceType InputType { get; private set; }

        public bool IsTouchEnabled => _eventSystem.gameObject.activeSelf;

        public IObservable<InputInterfaceType> OnChangeInputInterface => _onChangeInputInterface;

        [Inject]
        public void Construct(EventSystem eventSystem, PlayerInput playerInput,InputActionLinks actionLinks)
        {
            _eventSystem = eventSystem;
            _input = playerInput;
            _inputActionLinks = actionLinks;
        }



        public void SetTouchEnabled(bool enabled)
        {
            _eventSystem.gameObject.SetActive(enabled);
        }

        public bool GetAnyKeyDown()
        {
            foreach (var button in _buttonList.Values)
            {
                if (button.GetKeyDown()) 
                    return true;
            }

            return Keyboard.current.anyKey.wasPressedThisFrame;
        }

        public bool GetButton(Key key)
        {
            IButtonInput buttonInput = null;
            if (_buttonList.TryGetValue(key,out buttonInput) == false)
            {
                return false;
            }
            return buttonInput.GetKey();
        }

        public bool GetButtonDown(Key key)
        {
            IButtonInput buttonInput = null;
            if (_buttonList.TryGetValue(key, out buttonInput) == false)
            {
                return false;
            }
            return buttonInput.GetKeyDown();
        }

        public bool GetButtonUp(Key key)
        {
            IButtonInput buttonInput = null;
            if (_buttonList.TryGetValue(key, out buttonInput) == false)
            {
                return false;
            }
            return buttonInput.GetKeyUp();
        }

        public float GetButtonHoldTime(Key key)
        {
            IButtonInput buttonInput = null;
            if (_buttonList.TryGetValue(key, out buttonInput) == false)
            {
                return 0;
            }
            return buttonInput.HoldTime;
        }

        public Vector2 GetAxis(Key key)
        {
            IAxisInput axisInput = null;
            if (_axisList.TryGetValue(key, out axisInput) == false)
            {
                return Vector2.zero;
            }
            return axisInput.Value;
        }




        private void Awake()
        {
            gameObject.name = "InputManager";

            foreach (var actionMap in _input.actions.actionMaps)
            {
                actionMap.Enable();
            }

            InputType = InputInterfaceType.Other;

            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (_axisList.ContainsKey(key))
                {
                    continue;
                }
                _buttonList.Add(key, new ButtonInput());
            }
        }

        private void LateUpdate()
        {
            foreach (var button in _buttonList.Values)
            {
                button.Update();
            }
        }

        private void OnEnable()
        {
            if (_input == null)
            {
                return;
            }

            _input.onActionTriggered += OnActionTriggered;
        }

        private void OnDisable()
        {
            if (_input == null)
            {
                return;
            }

            _input.onActionTriggered -= OnActionTriggered;
            _onChangeInputInterface?.Dispose();
        }


        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            CheckInputInterface(context.control.device.layout);

            InputAction action = context.action;
            Key targetKey;
            if (_inputActionLinks.Link.TryGetValue(action,out targetKey) == false)
            {
                return;
            }

            IButtonInput button = null;
            if (_buttonList.TryGetValue(targetKey,out button))
            {
                button.OnChangedValue(context);
                return;
            }

            IAxisInput axis = null;
            if (_axisList.TryGetValue(targetKey,out axis))
            {
                axis.OnChangedValue(context);
            }
        }

        private void CheckInputInterface(string deviceName)
        {
            var prev = InputType;

            switch (deviceName)
            {
                case "Mouse":
                case "Keyboard":
                    InputType = InputInterfaceType.Other;
                    break;

                default:
                    InputType = InputInterfaceType.XboxController;
                    break;
            }

            if (prev == InputType)
            {
                return;
            }
            _onChangeInputInterface?.OnNext(InputType);

        }
    }
}
