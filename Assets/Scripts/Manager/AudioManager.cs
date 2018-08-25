using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameComm
{
    /// <summary>
    /// 音频管理类
    /// </summary>
    public class AudioManager : MonoSingleton<AudioManager>, IGameManager
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

