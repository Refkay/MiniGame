using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;

namespace MiniGame
{
    /// <summary>
    /// 管理所有石头的Manager，用于随机消失石头
    /// </summary>
    public class StoneManager : MonoBehaviour
    {
        public static StoneManager Instance;
        //石头父节点
        private GameObject mStoneContainer;

        private  List<GameObject> mStoneList = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
            MessageBus.Register<OnPlayerMoveMsg>(OnSubLevelComplete);
            MessageBus.Register<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }     

        private void OnDestroy()
        {
            MessageBus.UnRegister<OnPlayerMoveMsg>(OnSubLevelComplete);
            MessageBus.UnRegister<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }

        //关卡失败，所有的石头都显示出来
        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {
            for(int i = 0; i < mStoneList.Count; i++)
            {
                if (!mStoneList[i].activeSelf)
                {
                    mStoneList[i].SetActive(true);
                }
            }
            ClearStoneList();
            return false;
        }

        //通过小关，清空石头list，再重新进行复制
        private bool OnSubLevelComplete(OnPlayerMoveMsg msg)
        {
            ClearStoneList();
            return false;
        }   

        //获取当前小关的石头，保存到list里面
        private void SetStoneList()
        {
            //找到对应的小关的
            mStoneContainer = GetTargetGameObjectByName("StoneContainer"
                + MissionManager.Instance.mCurLevel + "-" + MissionManager.Instance.mCurSubLevel).gameObject;

            foreach (Transform child in mStoneContainer.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    mStoneList.Add(child.gameObject);
                }
            }
        }

        private GameObject GetTargetGameObjectByName(string name)
        {
            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
            {
                if (t.name == name)
                {                
                    return t.gameObject;
                }
            }
            return null;
        }

        private void ClearStoneList()
        {
            mStoneList.Clear();
        }
        
        //随机返回一个石头
        public  GameObject GetRadomStoneTrasform()
        {
            ClearStoneList();
            SetStoneList();
            int radomNum = Random.Range(0, mStoneList.Count);
            Debug.Log("石头LIst:" + mStoneList.Count + "\n随机数：" + radomNum);
            return mStoneList[radomNum];          
        }
    }
}

