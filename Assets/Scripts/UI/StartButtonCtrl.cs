using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonCtrl : MonoBehaviour
{

    void Start()
    {
        var btn = GetComponent<Button>();
        if (!CheckHasPlayed())
        {
            var lb = transform.FindChild("StartButtonLabel").gameObject;
            lb.GetComponent<Text>().text = "Start";
            btn.onClick.AddListener(LoadStoryScene);
        } else
        {
            btn.onClick.AddListener(LoadRecentScene);
        }
    }

    // TODO: 读取最近的关卡
    void LoadRecentScene()
    {
        Debug.LogError("StartButtonCtrl::LoadRecentScene: no implementation!!!");
    }

    // 读取故事关
    void LoadStoryScene()
    {
        SceneManager.LoadScene("Level0Story");
    }

    // TODO: 检查是否玩过
    bool CheckHasPlayed()
    {
        Debug.LogError("StartButtonCtrl::CheckHasPlayed: no implementation!!!");
        return false;
    }
}
