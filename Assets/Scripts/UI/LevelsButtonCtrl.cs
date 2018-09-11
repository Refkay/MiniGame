using UnityEngine;
using UnityEngine.UI;

public class LevelsButtonCtrl : MonoBehaviour {

	void Start() {
        var btn = GetComponent<Button>();
        if (!CheckHasPlayed())
        {
            btn.interactable = false;
        } 
        btn.onClick.AddListener(OnClick);
	}
	
    // TODO: 检查是否玩过
    bool CheckHasPlayed()
    {
        Debug.LogError("LevelsButtonCtrl::CheckHasPlayed: no implementation!!!");
        return false;
    }

    private void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectLevel");
    }
}
