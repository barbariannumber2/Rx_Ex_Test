using UnityEngine;
using Zenject;

namespace PracticeGame
{
    public class ButtonSpawner : MonoBehaviour
    {
        [Inject(Id = "Button")]
        private DiContainer _container;

        [SerializeField]
        private CommonPressableObject _buttonPrefab;

        public void SpawnButton()
        {
            GameObject buttonInstance = _container.InstantiatePrefab(_buttonPrefab.gameObject);

            _container.Inject(buttonInstance);
        }
    }
}