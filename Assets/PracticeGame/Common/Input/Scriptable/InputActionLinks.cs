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

        private  Dictionary<InputAction, Key> _linkDict = null;

        public IReadOnlyDictionary<InputAction, Key> Link
        {
            get
            {
                _linkDict ??= _linkList.ToDictionary(
                        link => link._actionRef.ToInputAction(),
                        link => link._actionKey
                    );

                return _linkDict;

            }
        }
    }


}