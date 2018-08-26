using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame
{
    public class MissionData
    {
        //保存关卡信息的Dictionary，主要是记录小关卡的位置信息
        public static Dictionary<int, int> mMissionDataDic = new Dictionary<int, int>();

        /// <summary>
        /// 加载每个小关卡的位置，其实就是保存位置信息到Dictionary里面
        /// TODO ： 这里为了测试先这么写，后续可以再改进数据读取方式
        /// </summary>
        public static void LoadMissionData()
        {           
            mMissionDataDic.Add(1, 3);
            mMissionDataDic.Add(2, 5);      
        }
       
        /// <summary>
        /// 取得大关卡总数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxLevel()
        {
            return mMissionDataDic.Count;
        }

        //取得小关卡总数
        public static int GetMaxSubLevel(int currentLevel)
        {
            return mMissionDataDic[currentLevel];     
        }

    }
}

