using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelButtonCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var btn = GetComponent<UIButton>();
        //btn.state = UIButton.State.Disabled;
        // 如果不取消掉碰撞器，那么鼠标滑过按钮的时候还会变成 hover 状态
        //GetComponent<BoxCollider>().enabled = false;
        UIEventListener.Get(gameObject).onClick = OnStartButtonClick;
    }

    void OnStartButtonClick(GameObject obj)
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
