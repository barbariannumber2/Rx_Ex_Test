using UnityEngine;

namespace PracticeGame
{
    public interface IInputInterfaceChangeNotice
    {
        public void OnChangeInputInterface(InputInterfaceType newInterfaceType);
    }
}