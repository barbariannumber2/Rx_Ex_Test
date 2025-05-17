using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestScriptable", menuName = "MyScriptable/Derived")]
public class TestScriptableDerived : TestScriptableBase
{
    [SerializeField]
    private TestScriptableEnum.Type type;

    public override int GetObjectID()
    {
        return (int)type;
    }
}
