using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;
using UnityEngine.UI;

namespace MiniGame
{
    public class GameSystem : MonoBehaviour
    {

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitMessage(true);
        }

        private void OnDestroy()
        {          
            InitMessage(false);
        }

        private void InitMessage(bool register)
        {
            if (register)
            {
                MessageBus.Register<OnLevelCompleteMsg>(OnLevelComplete);
                MessageBus.Register<OnSubLevelCompleteMsg>(OnSubLevelComplete);
                MessageBus.Register<OnSubLevelFailedMsg>(OnSubLevelFailed);
                MessageBus.Register<OnGameCompleteMsg>(OnGameComplete);
            }
            else
            {
                MessageBus.UnRegister<OnLevelCompleteMsg>(OnLevelComplete);
                MessageBus.UnRegister<OnSubLevelCompleteMsg>(OnSubLevelComplete);
                MessageBus.UnRegister<OnSubLevelFailedMsg>(OnSubLevelFailed);
                MessageBus.UnRegister<OnGameCompleteMsg>(OnGameComplete);
            }
        }

        /// <summary>
        /// 大关卡通关成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool OnLevelComplete(OnLevelCompleteMsg msg)
        {
            StartCoroutine(CompleteLevel());
            return false;
        }

        /// <summary>
        /// 小关卡通关成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool OnSubLevelComplete(OnSubLevelCompleteMsg msg)
        {
            StartCoroutine(CompleteSubLevel());
            return false;
        }

        /// <summary>
        /// 小关卡通关失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {
            StartCoroutine(FailSubLevel());
            return false;
        }

        private bool OnGameComplete(OnGameCompleteMsg msg)
        {
            //TODO : 整个游戏通关，这里先随便给个提示
            
            return false;
        }

        private IEnumerator CompleteLevel()
        {
            //TODO : 关卡完成，在这里可以进行相应的操作          
            yield return new WaitForSeconds(2);
            GameManagers.mMissionManager.GoToNextLevel();
        }

        private IEnumerator CompleteSubLevel()
        {
            //TODO : 小关卡完成，在这里可以进行相应的操作，现在只是移动相机
            GameManagers.mMissionManager.GoToNextSubLevel();
            yield return null;
        }

        private IEnumerator FailSubLevel()
        {
            //TODO : 小关卡失败，在这里可以进行相应的操作
            GameManagers.mMissionManager.RestartCurrentSubLevel();
            yield return null;
        }

        
    }

}
