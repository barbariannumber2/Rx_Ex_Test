using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "InputActionLink", menuName = "Scriptable Objects/InputActionLink")]
    public class InputActionLinks : ScriptableObject
    {
        /// <summary>
        /// ActionKeyタイプと、それに連動させたいInputActionReferenceのセット
        /// </summary>
        [Serializable]
        public class InputActionLink
        {
            [field:SerializeField]
            public InputActionReference _actionRef { get; private set; }

            [field: SerializeField]
            public Key _actionKey { get; private set; }
        }

        [SerializeField,Tooltip("ActionKeyタイプと連動させたいInputActionReferenceのセット一覧")]
        private List<InputActionLink> _linkList;

        /// <summary>
        /// linkListのInputActionReferenceをInputActionに変換しつつ、アクションからkeyを求められる辞書を作成
        /// </summary>
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