using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame
{
    /// <summary>
    /// 数据管理，保存数据和读取数据等
    /// </summary>
    public class DataManager : MonoBehaviour, IGameManager
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

