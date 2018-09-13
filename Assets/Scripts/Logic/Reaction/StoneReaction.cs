using UnityEngine;
using System.Collections;
using MiniGameComm;

namespace MiniGame
{
    /// <summary>
    /// 石头被导弹击中的反应
    /// </summary>
    public class StoneReaction : MonoBehaviour
    {
        private void Awake()
        {
            MessageBus.Register<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }

        private void OnDestroy()
        {
            MessageBus.UnRegister<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }      

        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {
            gameObject.SetActive(true);
            return false;
        }
    }
}

