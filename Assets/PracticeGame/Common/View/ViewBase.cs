using PracticeGame;
using UnityEngine;

namespace PracticeGame
{
    public class ViewBase : MonoBehaviour, IView
    {
        protected bool Initialized { get; private set; } = false;

        public void Initialize()
        {
            if(Initialized)
            {
                return;
            }
            Initialized = true;
            OnInitialize();
        }

        protected virtual void OnInitialize() { }

        private void Update()
        {
            if (Initialized == false)
            {
                return;
            }
            OnUpdate();
        }

        protected virtual void OnUpdate() { }


    }
}