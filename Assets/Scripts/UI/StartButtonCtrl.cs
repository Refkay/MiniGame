using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonCtrl : MonoBehaviour
{

    void Awake()
    {
        if (!PlayerProgress.Instance.HasPlayed)
        {
            var lb = transform.FindChild("Label").gameObject;
            lb.GetComponent<UILabel>().text = "NEW GAME";
        }
        UIEventListener.Get(gameObject).onClick = OnStartButtonClick;
    }

    void OnStartButtonClick(GameObject obj)
    {
        SceneManager.LoadScene("Main");
    }
}
