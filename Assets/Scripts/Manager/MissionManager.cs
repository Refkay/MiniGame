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

        public static MissionManager Instance;

        public ManagerStatus mStatus
        {
            get;
            private set;
        }

        //当前主关卡
        public int mCurLevel
        {
            get
            {
                return PlayerProgress.Instance.RecentMainLevel;
            }
        }
        //当前子关卡
        public int mCurSubLevel
        {
            get
            {
                return PlayerProgress.Instance.RecentSubLevel;
            }
        }
        //最高等级的主关卡
        public int mMaxLevel { get; private set; }
        //最高等级的子关卡
        public int mMaxSubLevel { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Startup()
        {
            mStatus = ManagerStatus.Started;
            //TODO ：这里的数据需要从存储里面读取，因为还没做，所以先放这里
            MissionData.LoadMissionData();
            PlayerProgress.Instance.Reload();
            LoadMissionProgress();
            mMaxLevel = MissionData.GetMaxLevel();
            mMaxSubLevel = MissionData.GetMaxSubLevel(mCurLevel);
        }

        private void LoadMissionProgress()
        {           
            if (PlayerProgress.Instance.HasPlay())
            {
                UpdateMissionLevel(PlayerProgress.Instance.RecentMainLevel, PlayerProgress.Instance.RecentSubLevel);
            }        
        }

        public void UpdateMissionLevel(int currentLevel, int currentSubLevel)
        {
            PlayerProgress.Instance.SubmitNewProgress(currentLevel, currentSubLevel);
            mMaxSubLevel = MissionData.GetMaxSubLevel(mCurLevel);
        }

        /// <summary>
        /// 进入下一个大关卡
        /// </summary>
        public void GoToNextLevel()
        {
            //首先要将小关卡重置为1
            // mCurSubLevel = 1;
            var subLv = 1;
            if (mCurLevel < mMaxLevel)
            {
                UpdateMissionLevel(mCurLevel + 1, 1);
                //将下一大关的小关卡重置
                mMaxSubLevel = MissionData.GetMaxSubLevel(mCurLevel);     
                SceneManager.LoadSceneAsync("Level" + mCurLevel + "-" + mCurSubLevel);                                
            }
            else
            {
                Debug.Log("Last level");

                PlayerProgress.Instance.SubmitNewProgress(1, 1);
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
                UpdateMissionLevel(mCurLevel, mCurSubLevel + 1);
                //不移动镜头的做法
                //SceneManager.LoadSceneAsync("Level" + mCurLevel + "-" + mCurSubLevel); 
                //移动镜头的做法              
                MessageBus.Send(new OnCameraMoveMsg(MissionData.GetCameraPosition(mCurLevel, mCurSubLevel), false));
                MessageBus.Send(new OnPlayerMoveMsg(MissionData.GetPlayerPosition(mCurLevel, mCurSubLevel), false));
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

