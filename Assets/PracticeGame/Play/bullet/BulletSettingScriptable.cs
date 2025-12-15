using UnityEngine;

namespace PracticeGame
{
    [CreateAssetMenu(fileName = "BulletSetting", menuName = "Scriptable Objects/BulletSettingScriptable")]
    public class BulletSettingScriptable : ScriptableObject, IBulletSettings
    {
        [field: SerializeField]
        public float Speed { get; set; } = 1f;

        [field: SerializeField]
        public float LifeTime { get; set; }

        [field: SerializeField]
        public int Damage { get; set; } = 1;
    }
}