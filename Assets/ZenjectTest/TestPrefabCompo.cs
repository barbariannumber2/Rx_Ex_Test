using UnityEngine;

namespace ZenjTest
{
    public class TestPrefabCompo : MonoBehaviour
    {
        float time = 0;
        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time > 600) 
            {
                Debug.Log(time);
                time = 0;
            }
        }
    }
}