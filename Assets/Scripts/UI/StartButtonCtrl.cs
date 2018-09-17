using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace MiniGame
{
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
            }
            else
            {
                btn.onClick.AddListener(LoadRecentScene);
            }
        }

        void LoadRecentScene()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadSceneAsync("Level" + MiniGame.PlayerProgress.Instance.RecentMainLevel + "-" + "1");
        }

        // 读取故事关
        void LoadStoryScene()
        {
            SceneManager.LoadSceneAsync("Level0Story");
        }

        bool CheckHasPlayed()
        {
            return MiniGame.PlayerProgress.Instance.HasPlay();
        }
    }

}
