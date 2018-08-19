using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;

namespace MiniGame
{
    /// <summary>
    /// 光球操作相关的类，用来更新Player位置等信息，触碰到障碍物的反映，以及道具的反映等
    /// </summary>
    public class PlayerAction : MonoBehaviour
    {

        private void Awake()
        {
            InitMessage(true);
        }

        private void OnDestroy()
        {
            InitMessage(false);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitMessage(bool register)
        {
            if (register)
            {
                MessageBus.Register<OnPlayerPosChangeMsg>(OnPlayerPosChange);
            }
            else
            {
                MessageBus.UnRegister<OnPlayerPosChangeMsg>(OnPlayerPosChange);
            }
        }


        private bool OnPlayerPosChange(OnPlayerPosChangeMsg msg)
        {
            gameObject.transform.position = msg.mPosition;
            GetComponent<Rigidbody2D>().Sleep();
            return false;
        }

        /// <summary>
        /// 碰撞检测
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //碰到障碍物，小关卡失败
            if (collision.gameObject.tag == "Enemy")
            {
                MessageBus.Send(new OnSubLevelFailedMsg());
            }
        }

        /// <summary>
        /// 接触到触发器
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Property")
            {
                Debug.Log("On SubLevel Complete");                               
                GetComponent<Rigidbody2D>().Sleep();
                Destroy(collision.gameObject);
                //发送消息到GameSystem，小关卡完成，通知进入下一关卡
                MessageBus.Send(new OnSubLevelCompleteMsg());
            }
        }

        /// <summary>
        /// 物体在摄像机内
        /// </summary>
        private void OnBecameVisible()
        {

        }

        /// <summary>
        /// 不在摄像机内触发改方法，Player一移动出摄像机就判定为死亡
        /// </summary>
        private void OnBecameInvisible()
        {
            MessageBus.Send(new OnSubLevelFailedMsg());
        }
    }
}


