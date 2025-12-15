using UnityEngine;

namespace PracticeGame
{
    public interface IBullet
    {
        public float Speed { get; }
        public int Damage { get; }

        public float LifeTime { get; }
    }
}