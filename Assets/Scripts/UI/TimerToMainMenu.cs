using UnityEngine;
using System.Collections;

public class TimerToMainMenu : MonoBehaviour {

	void Start () {
        StartCoroutine(WaitAndStartMainMenu());
    }

    private IEnumerator WaitAndStartMainMenu()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
}
