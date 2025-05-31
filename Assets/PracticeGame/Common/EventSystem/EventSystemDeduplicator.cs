using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PracticeGame
{
    /// <summary>
    /// イベントシステムの重複を排除するクラス
    /// ExtenjectのProjectContextでEventSystemと一緒にシングルトン生成する想定
    /// </summary>
    [RequireComponent(typeof(EventSystem))]
    public class EventSystemDeduplicator : MonoBehaviour
    {
        /// <summary>
        /// GetComponentで取得しなくていいようにInspectorから設定する
        /// </summary>
        [SerializeField]
        private EventSystem _thisEventSystem;

        private void Awake()
        {
            //ExtenjectのAsSingleで生成した場合、DontDestroyOnLoadに属するオブジェクトの子として生成されるため
            //AwakeのタイミングでシーンがDontDestroyOnLoadになる　これによりヒエラルキー配置と区別可能
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                Destroy(gameObject);
                return;
            }

            EventSystem.current = _thisEventSystem;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Dedupe();
        }

        private void Dedupe()
        {
            EventSystem.current = _thisEventSystem;

            EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

            if (eventSystems.Length <= 1)
            {
                return;
            }

            for (int i = 0; i < eventSystems.Length; i++)
            {
                var eventSystemObject = eventSystems[i].gameObject;
                if (eventSystemObject.scene == null || eventSystemObject.scene.name == "DontDestroyOnLoad")
                {
                    continue;
                }

                if (eventSystems[i] == EventSystem.current)
                {
                    continue;
                }
#if UNITY_EDITOR
                Debug.LogWarning($"Extra EventSystem exist in scene: {eventSystemObject.scene.name}");
                Debug.Log($"Destroying extra EventSystem: {eventSystemObject.name}");
#endif
                Destroy(eventSystemObject);
                eventSystems[i] = null;

            }
        }
    }
}