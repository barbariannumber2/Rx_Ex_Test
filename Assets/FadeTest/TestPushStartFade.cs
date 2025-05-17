using UnityEngine;

public class TestPushStartFade : MonoBehaviour
{
    [SerializeField]
    private Fade normalFade;
    [SerializeField]
    private Fade uiFade;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            normalFade.FadeIn(1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            normalFade.FadeOut(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            uiFade.FadeIn(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            uiFade.FadeOut(1);
        }
    }
}
