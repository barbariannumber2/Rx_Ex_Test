using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "InputActionLink", menuName = "Scriptable Objects/InputActionLink")]
    public class InputActionLinks : ScriptableObject
    {
        [System.Serializable]
        public class InputActionLink
        {
            [field:SerializeField]
            public InputActionReference _actionRef { get; private set; }

            [field: SerializeField]
            public Key _actionKey { get; private set; }
        }

        [SerializeField]
        private List<InputActionLink> _linkList;

        private Dictionary<InputAction, Key> _linkDict = null;

        public IReadOnlyDictionary<InputAction, Key> Link
        {
            get
            {
#if UNITY_EDITOR
                if (_linkList.Count!=Enum.GetValues(typeof(Key)).Length)
                {
                    Debug.LogError("InputActionLinks: LinkListの数がKeyの数と一致しません。");
                }
#endif

                _linkDict ??= _linkList.ToDictionary(
                        link => link._actionRef.ToInputAction(),
                        link => link._actionKey
                    );

                return _linkDict;

            }
        }
    }


}