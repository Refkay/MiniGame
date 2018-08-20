using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;

/// <summary>
/// 关卡管理的Manager
/// </summary>
namespace MiniGame
{
    public class MissionManager : MonoBehaviour, IGameManager
    {

        public ManagerStatus mStatus
        {
            get;
            private set;
        }

        //当前主关卡
        public int mCurLevel { get; private set; }
        //当前子关卡
        public int mCurSubLevel { get; private set; }
        //最高等级的主关卡
        public int mMaxLevel { get; private set; }
        //最高等级的子关卡
        public int mMaxSubLevel { get; private set; }

        public void Startup()
        {
            mStatus = ManagerStatus.Started;
        }

        private void Awake()
        {
            //TODO ：这里的数据需要从存储里面读取，因为还没做，所以先放这里
            MissionData.LoadMissionData();
            UpdateMissionLevel(1, 1);
            mMaxSubLevel = 2;
            mMaxLevel = 3;
        }


        public void UpdateMissionLevel(int currentLevel, int currentSubLevel)
        {
            this.mCurLevel = currentLevel;
            this.mCurSubLevel = currentSubLevel;
        }

        /// <summary>
        /// 进入下一个大关卡
        /// </summary>
        public void GoToNextLevel()
        {
            //首先要将小关卡重置为1
            mCurSubLevel = 1;
            if (mCurLevel < mMaxLevel)
            {
                mCurLevel++;
                string name = "Level" + mCurLevel;
                Debug.Log("Loading " + name);
                Application.LoadLevelAsync(name);        
            }
            else
            {
                Debug.Log("Last level");
                //发送消息，游戏已经到最后一关了,到这里整个游戏通关了
                MessageBus.Send(new OnGameCompleteMsg());
            }
        }

        /// <summary>
        /// 进入下一个小关卡
        /// </summary>
        public void GoToNextSubLevel()
        {
            if (mCurSubLevel < mMaxSubLevel)
            {
                mCurSubLevel++;
                Vector3 position = MissionData.GetSubLevelPosition(mCurLevel, mCurSubLevel);
                //移动到下一个小关卡，其实就是移动摄像机跟Player的位置
                MessageBus.Send(new OnCameraMoveMsg(position, false));
                MessageBus.Send(new OnPlayerPosChangeMsg(position));
            }
            else
            {
                Debug.Log("Last sublevel");
                //发送消息，游戏到当前大关卡的最后一小关，到这里说明所有小关都通关了
                MessageBus.Send(new OnLevelCompleteMsg());               
            }
        }

        /// <summary>
        /// 重制当前的大关卡
        /// </summary>
        public void RestartCurrentLevel()
        {
            string name = "Level" + mCurLevel;
            Debug.Log("Loading " + name);
            Application.LoadLevel(name);
        }

        /// <summary>
        /// 重制当前的小关卡，由于需要保存障碍物或者道具的状态，所以现在重新加载场景，然后指定到位置
        /// TODO : 后续改进加载方式
        /// </summary>
        public void RestartCurrentSubLevel()
        {           
            Vector3 position = MissionData.GetSubLevelPosition(mCurLevel, mCurSubLevel);
            MessageBus.Send(new OnCameraMoveMsg(position, false));
            MessageBus.Send(new OnPlayerPosChangeMsg(position));
        }

    }
}

