using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptableUser : MonoBehaviour
{
    [SerializeField]
    private TestScriptableBase scriptable;
    // Start is called before the first frame update
    void Start()
    {
        scriptable.GetObjectID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
