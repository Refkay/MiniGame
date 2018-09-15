using UnityEngine;
using System.Collections;
using MiniGameComm;

namespace MiniGame
{
    /// <summary>
    /// 吃到中转点触发的操作
    /// </summary>
    public class TurnPointReaction : MonoBehaviour
    {
        GameObject desObj;
        private void Awake()
        {
            MessageBus.Register<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }

        private void OnDestroy()
        {
            MessageBus.UnRegister<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                //播放音乐
                AudioManager.Instance.PlayOneShotIndex(5);
                MessageBus.Send(new OnAddTurnChanceMsg());
                //展示水晶被吃掉的动画
                //desObj = GameObject.Instantiate(Resources.Load("Prefabs/CrystalDismiss")) as GameObject;
                //desObj.transform.parent = gameObject.transform;
                //desObj.transform.position = new Vector3(0, 0, 0);
                //StartCoroutine(DestoryDesObj());
                gameObject.SetActive(false);
            }
        }

        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {
            gameObject.SetActive(true);
            return false;
        }

        private IEnumerator DestoryDesObj()
        {
            yield return new WaitForSeconds(1.0f);
            if (desObj != null)
            {
                Destroy(desObj);
            }
        }
    }
}

