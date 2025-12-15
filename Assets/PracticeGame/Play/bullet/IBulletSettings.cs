using UnityEngine;

namespace PracticeGame
{
    public interface IBulletSettings
    {
        public float Speed { get; }
        public float LifeTime { get; }
        public int Damage { get; }
    }
}