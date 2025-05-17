using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PracticeGame
{
    public interface ITransition
    {
        public UniTask ScreenOpen(float duration);

        public UniTask ScreenClose(float duration);
    }
}