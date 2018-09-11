using UnityEngine;

public class ConfirmQuitOnClick : MonoBehaviour
{

    public void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
