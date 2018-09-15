using UnityEngine;
using System.Collections;

public class TimerToMainMenu : MonoBehaviour {

    public float delay = 3.0f;

	void Start () {
        StartCoroutine(WaitAndStartMainMenu());
    }

    private IEnumerator WaitAndStartMainMenu()
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
}
