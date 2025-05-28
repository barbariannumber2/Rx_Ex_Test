using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;

namespace PracticeGame
{
    public class SelectView : ViewBase
    {
        private List<IMultiInputSelectEventSender> _selectButtons;

        [Inject]
        public void Construct(List<IMultiInputSelectEventSender> selectButtons)
        {
            _selectButtons = selectButtons;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}