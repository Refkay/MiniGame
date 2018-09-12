using UnityEngine;
using System.Collections;
using MiniGameComm;
using DG.Tweening;

namespace MiniGame
{
    /// <summary>
    /// 导弹的操作，吃到导弹随机炸掉一块石头
    /// </summary>
    public class BulletReatcion : MonoBehaviour
    {
        private Vector3 mInitialPosition;
        private bool canReact = true;
        private void Awake()
        {
            MessageBus.Register<OnSubLevelFailedMsg>(OnSubLevelFailed);
            mInitialPosition = gameObject.transform.position;
        }

        private void OnDestroy()
        {
            MessageBus.UnRegister<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && canReact)
            {             
                if (StoneManager.Instance == null)
                {
                    Debug.Log("StoneManager.Instance是空的");
                }
                else{
                    Debug.Log("StoneManager.Instance不是空的");
                }
                GameObject stone = StoneManager.Instance.GetRadomStoneTrasform();
                SetPlayerAngle(stone.transform.position);
                gameObject.transform.DOMove(stone.transform.position, 0.5f).OnComplete(() => {
                    stone.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    //这里要播放炸掉石头的特效
                });
                canReact = false;
            }
        }

        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {        
            gameObject.transform.position = mInitialPosition;
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            canReact = true;
            gameObject.SetActive(true);
            return false;
        }

        //导弹朝向某个角度
        private void SetPlayerAngle(Vector3 targetPosition)
        {
            float angle;
            Vector2 targetDir = targetPosition - gameObject.transform.position;
            angle = Vector2.Angle(targetDir, Vector3.up);
            if (targetPosition.x > gameObject.transform.position.x)
            {
                angle = -angle;
            }
            this.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}

