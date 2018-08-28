using UnityEngine;
using System.Collections;
using MiniGameComm;

namespace MiniGame
{
    /// <summary>
    /// 吃到中转点触发的操作
    /// </summary>
    public class EatStarReaction : MonoBehaviour
    {
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
                MessageBus.Send(new OnAddStarMsg());
                gameObject.SetActive(false);
            }
        }

        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {
            gameObject.SetActive(true);
            return false;
        }
    }
}

