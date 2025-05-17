using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PracticeGame
{
    public class NormalFade : MonoBehaviour, ITransition
    {
        
        private Image _image;

        [Inject]
        public void Construct([Inject(Id = "NormalFade")] Image image)
        {
            _image = image;
            Debug.Log("NormalFade: Injection Complete");
        }

        private void Awake()
        {
            Color color = _image.color;
            color.a = 0f;
            _image.color = color;
        }
        public async UniTask ScreenClose(float duration)
        {
            Color color = _image.color;

            for (float t = 0; t < 1f; t += Time.deltaTime / duration)
            {
                color.a = Mathf.Clamp01(t);
                _image.color = color;
                await UniTask.DelayFrame(1);
            }
        }

        public async UniTask ScreenOpen(float duration)
        {
            Color color = _image.color;
            for (float t = 1; t > 0; t -= Time.deltaTime / duration)
            {
                color.a = Mathf.Clamp01(t);
                _image.color = color;
                await UniTask.DelayFrame(1);
            }
        }
    }
}