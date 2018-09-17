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
	
    // 检查是否玩过
    bool CheckHasPlayed()
    {
        return MiniGame.PlayerProgress.Instance.HasPlay();
    }

    private void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SelectLevel");
    }
}
