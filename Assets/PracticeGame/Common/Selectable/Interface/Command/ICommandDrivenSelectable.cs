using UnityEngine;

namespace PracticeGame
{
    public interface ICommandDrivenSelectable : ISelectable
    {
        public void Select();

        public void Deselect();
    }
    
    public static class CommandDrivenSelectableUtility
    {
        public static void IsSelected(this ICommandDrivenSelectable selectable,bool isSelect)
        {
            if (isSelect)
            {
                selectable.Select();
            }
            else
            {
                selectable.Deselect();
            }
        }
    }
}
