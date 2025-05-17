using UnityEngine;
using Zenject;
namespace ZenjTest
{
    public class TestZenjectUser : MonoBehaviour
    {
        private BindTestMono testMonoObj = null;

        private TestPrefabCompo testPrefabComponent = null;

        [Inject]
        public void Construct(BindTestMono monoObj, TestPrefabCompo testPrefab)
        {
            Debug.Log("インジェクト処理呼び出し");
            Debug.Log($"代入処理前monoObj => {testMonoObj}:monoPrefab => {testPrefabComponent}");
            testMonoObj = monoObj;
            testPrefabComponent = testPrefab;
            Debug.Log($"代入処理後monoObj => {testMonoObj}:monoPrefab => {testPrefabComponent}");
        }
    }
}