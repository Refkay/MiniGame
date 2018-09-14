using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;
using UnityEngine.SceneManagement;

/// <summary>
/// 关卡管理的Manager
/// </summary>
namespace MiniGame
{
    public class MissionManager : MonoSingleton<MissionManager>, IGameManager
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
            //TODO ：这里的数据需要从存储里面读取，因为还没做，所以先放这里
            MissionData.LoadMissionData();
            LoadMissionProgress();
            // UpdateMissionLevel(1, 1);
            mMaxLevel = MissionData.GetMaxLevel();
            mMaxSubLevel = MissionData.GetMaxSubLevel(mCurLevel);
        }

        private void LoadMissionProgress()
        {
            var dataValid =
                PlayerProgress.Instance.HasPlayed &&
                PlayerProgress.Instance.RecentMainLevel > 0 &&
                PlayerProgress.Instance.RecentSubLevel > 0;
            if (dataValid)
            {
                UpdateMissionLevel(PlayerProgress.Instance.RecentMainLevel, PlayerProgress.Instance.RecentSubLevel);
            }
            else
            {
                UpdateMissionLevel(1, 1);
            }

        }

        public void UpdateMissionLevel(int currentLevel, int currentSubLevel)
        {
            this.mCurLevel = currentLevel;
            this.mCurSubLevel = currentSubLevel;
            mMaxSubLevel = MissionData.GetMaxSubLevel(mCurLevel);
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
                //将下一大关的小关卡重置
                mMaxSubLevel = MissionData.GetMaxSubLevel(mCurLevel);     
                SceneManager.LoadSceneAsync("Level" + mCurLevel + "-" + mCurSubLevel);                                

                PlayerProgress.Instance.SubmitNewProgress(mCurLevel, mCurSubLevel);
            }
            else
            {
                Debug.Log("Last level");

                PlayerProgress.Instance.SubmitNewProgress(-1, -1);
                
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
                //不移动镜头的做法
                //SceneManager.LoadSceneAsync("Level" + mCurLevel + "-" + mCurSubLevel); 
                //移动镜头的做法              
                MessageBus.Send(new OnCameraMoveMsg(MissionData.GetCameraPosition(mCurLevel, mCurSubLevel), false));
                MessageBus.Send(new OnPlayerMoveMsg(MissionData.GetPlayerPosition(mCurLevel, mCurSubLevel), false));

                PlayerProgress.Instance.SubmitNewProgress(mCurLevel, mCurSubLevel);
            }
            else
            {
                //这里应该是跳转到某个大关卡过关完的连成星座并展示星座图的画面
                MessageBus.Send(new OnLevelCompleteMsg());               
            }
        }

        /// <summary>
        /// 获得Player初始位置
        /// </summary>
        public Vector3 GetPlayerStartPos()
        {
            return MissionData.GetPlayerPosition(mCurLevel, mCurSubLevel);
        }

        /// <summary>
        /// 获得相机初始位置
        /// </summary>
        public Vector3 GetCameraStartPos()
        {
            return MissionData.GetCameraPosition(mCurLevel, mCurSubLevel);
        }
    }
}

