using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTestPlayer : MonoBehaviour
{
    [SerializeField]
    InputAction action;

    [SerializeField]
    InputActionReference reference;

    [SerializeField]
    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTestInput(InputAction.CallbackContext context)
    {
        

        Debug.Log(context.phase);
        Debug.Log("ボタンが押された");
    }
}
