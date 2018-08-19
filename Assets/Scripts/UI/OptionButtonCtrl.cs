using UnityEngine;
using System.Collections;

public class OptionButtonCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var btn = GetComponent<UIButton>();
        btn.state = UIButton.State.Disabled;
        // 如果不取消掉碰撞器，那么鼠标滑过按钮的时候还会变成 hover 状态
        GetComponent<BoxCollider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
