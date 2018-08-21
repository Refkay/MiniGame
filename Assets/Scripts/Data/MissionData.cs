using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame
{
    public class MissionData
    {
        //保存关卡信息的Dictionary，主要是记录小关卡的位置信息
        public static Dictionary<int, Dictionary<int, Vector3>> mMissionDataDic = new Dictionary<int, Dictionary<int, Vector3>>();

        /// <summary>
        /// 加载每个小关卡的位置，其实就是保存位置信息到Dictionary里面
        /// TODO ： 这里为了测试先这么写，后续可以再改进数据读取方式
        /// </summary>
        public static void LoadMissionData()
        {
            Vector3 point1 = new Vector3(-0.0f, -7.7f, 0);
            Vector3 point2 = new Vector3(-0.22f, 8.3f, 0);
            Vector3 point3 = new Vector3(-9.04f, 19.64f, 0);
            Dictionary<int, Vector3> mLevelDic = new Dictionary<int, Vector3>();
            mLevelDic.Add(1, point1);
            mLevelDic.Add(2, point2);
            mLevelDic.Add(3, point3);
            mMissionDataDic.Add(1, mLevelDic);
            mMissionDataDic.Add(2, mLevelDic);
            mMissionDataDic.Add(3, mLevelDic);
        }

        /// <summary>
        /// 获取某个大关卡中某个小关卡的位置信息
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="currentSubLevel"></param>
        /// <returns></returns>
        public static Vector3 GetSubLevelPosition(int currentLevel, int currentSubLevel)
        {
            return mMissionDataDic[currentLevel][currentSubLevel];
        }
    }
}

