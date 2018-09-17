using UnityEngine;

public class BackOnClick : MonoBehaviour {

    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
