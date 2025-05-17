using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeTestUser : MonoBehaviour
{
    [SerializeField]
    private SerializeTestBase SerializeTestBase = new SerializeTestDerived();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
