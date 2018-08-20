using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player吃到的道具的管理类
/// </summary>
namespace MiniGame
{
    public class InventoryManager : MonoBehaviour, IGameManager
    {

        public ManagerStatus mStatus
        {
            get;
            private set;
        }

        public void Startup()
        {
            mStatus = ManagerStatus.Started;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


