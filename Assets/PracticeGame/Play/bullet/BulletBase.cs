using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class BulletBase : MonoBehaviour, IBullet
    {
        public float Speed { get; protected set; }
        public int Damage { get; protected set; }

        public float LifeTime { get; protected set; }

        private IBulletSettings bulletData;

        [Inject]
        public void Construct(IBulletSettings bulletData)
        {
            this.bulletData = bulletData;
            Speed = bulletData.Speed;
            Damage = bulletData.Damage;
        }
    }
}