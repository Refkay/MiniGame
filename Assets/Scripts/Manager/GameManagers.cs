 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;

namespace MiniGame
{
    /// <summary>
    /// 管理所有Manager的Manager
    /// </summary>
    public class GameManagers : MonoSingleton<GameManagers>
    {

        public static PlayerManager mPlayManager { get; private set; }       

        public static MissionManager mMissionManager { get; private set;}

        private List<IGameManager> mStartSequence;


        void Awake()
        {
            DontDestroyOnLoad(gameObject);   
            mPlayManager = GetComponent<PlayerManager>();         
            mMissionManager = GetComponent<MissionManager>();

            mStartSequence = new List<IGameManager>();
            mStartSequence.Add(mPlayManager);         
            mStartSequence.Add(mMissionManager);
            StartCoroutine(StartupManagers());         
        }

        private IEnumerator StartupManagers()
        {
            foreach (IGameManager manager in mStartSequence)
            {
                manager.Startup();
            }

            yield return null;
            int numManagerCount = mStartSequence.Count;
            int numReady = 0;
            
            //循环遍历直到所有的Manager都启动
            while (numReady < numManagerCount)
            {
                int lastReady = numReady;
                numReady = 0;

                foreach (IGameManager manager in mStartSequence)
                {
                    if (manager.mStatus == ManagerStatus.Started)
                    {
                        numReady++;
                    }
                }

                if (numReady > lastReady)
                {
                    //TODO: 这里可以做加载页面的进度条
                    yield return null;              
                }

                Debug.Log("All managers started up");
                yield return null;
            }
        }

        
    }
}

