using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame
{
    public class MissionData
    {
        //保存关卡信息的Dictionary，主要是记录小关卡的位置信息,第一个int是大关卡的关卡名字，第二个int是每一个大关卡有多少的小关卡
        public static Dictionary<int, int> mMissionDataDic = new Dictionary<int, int>();

        //保存每一关Player的起始位置的Dictionary， Key是 string currentLevel-currentSubLevel （1-1,1-2等）
        public static Dictionary<string, Vector3> mPlayerPositionDic = new Dictionary<string, Vector3>();

        //保存每一关相机的起始位置的Dictionary， Key是 string currentLevel-currentSubLevel （1-1,1-2等）
        public static Dictionary<string, Vector3> mCameraPositonDic = new Dictionary<string, Vector3>();

        /// <summary>
        /// 加载每个小关卡的位置，其实就是保存位置信息到Dictionary里面
        /// TODO ： 这里为了测试先这么写，后续可以再改进数据读取方式
        /// </summary>
        public static void LoadMissionData()
        {           
            //加载关卡信息
            mMissionDataDic.Add(1, 3);
            mMissionDataDic.Add(2, 5);

            //加载Player每一小关的起始位置信息
            mPlayerPositionDic.Add("1-2", new Vector3(-20.82f, -17.59f, 0));

            //加载Camera每一小关的位置
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

        //获取某个关卡的Player的起始位置
        public static Vector3 GetNextPlayerPosition(int currentLevel, int currentSubLevel)
        {
            Vector3 position = new Vector3();
            foreach (KeyValuePair<string, Vector3> kvp in mPlayerPositionDic)
            {
                if (kvp.Key == currentLevel + "-" + currentSubLevel)
                {
                    position = kvp.Value;
                    break;
                }
            }
            return position;
        }

        //获取某个关卡的Camera的起始位置
        public static Vector3 GetNextCameraPosition(int currentLevel, int currentSubLevel)
        {
            Vector3 position = new Vector3();
            foreach (KeyValuePair<string, Vector3> kvp in mCameraPositonDic)
            {
                if (kvp.Key == currentLevel + "-" + currentSubLevel)
                {
                    position = kvp.Value;
                    break;
                }
            }
            return position;
        }

    }
}

