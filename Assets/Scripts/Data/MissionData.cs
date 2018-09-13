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
            mMissionDataDic.Add(3, 5);
            mMissionDataDic.Add(4, 5);

            //加载Player每一小关的起始位置信息
            mPlayerPositionDic.Add("1-1", new Vector3(0f, -9f, 0f));
            mPlayerPositionDic.Add("1-2", new Vector3(31.4f, -18.8f, 0));
            mPlayerPositionDic.Add("1-3", new Vector3(56.8f, -36.9f, 0));

            mPlayerPositionDic.Add("2-1", new Vector3(-43.9f, 17.6f, 0));
            mPlayerPositionDic.Add("2-2", new Vector3(-22.4f, 45.5f, 0));
            mPlayerPositionDic.Add("2-3", new Vector3(6.1f, 34f, 0));
            mPlayerPositionDic.Add("2-4", new Vector3(-22.2f, -3.13f, 0));
            mPlayerPositionDic.Add("2-5", new Vector3(18.6f, -10.3f, 0));

            mPlayerPositionDic.Add("3-1", new Vector3(-16f, -41.5f, 0f));
            mPlayerPositionDic.Add("3-2", new Vector3(13.4f, -41.5f, 0f));
            mPlayerPositionDic.Add("3-3", new Vector3(43.6f, -41.5f, 0f));
            mPlayerPositionDic.Add("3-4", new Vector3(74.1f, -41.5f, 0f));
            mPlayerPositionDic.Add("3-5", new Vector3(104.1f, -41.5f, 0f));

            mPlayerPositionDic.Add("4-1", new Vector3(-16f, -41.5f, 0f));
            mPlayerPositionDic.Add("4-2", new Vector3(13.4f, -41.5f, 0f));
            mPlayerPositionDic.Add("4-3", new Vector3(43.6f, -41.5f, 0f));
            mPlayerPositionDic.Add("4-4", new Vector3(74.1f, -41.5f, 0f));
            mPlayerPositionDic.Add("4-5", new Vector3(104.1f, -41.5f, 0f));

            //加载Camera每一小关的位置
            mCameraPositonDic.Add("1-1", new Vector3(0f, 0f, -10));
            mCameraPositonDic.Add("1-2", new Vector3(31.4f, -10.5f, -10));
            mCameraPositonDic.Add("1-3", new Vector3(56.8f, -29f, -10));

            mCameraPositonDic.Add("2-1", new Vector3(-43.9f, 26.6f, -10));
            mCameraPositonDic.Add("2-2", new Vector3(-22.4f, 54.5f, -10));
            mCameraPositonDic.Add("2-3", new Vector3(6.1f, 43f, -10));
            mCameraPositonDic.Add("2-4", new Vector3(-22.2f, 6.13f, -10));
            mCameraPositonDic.Add("2-5", new Vector3(18.6f, -1.3f, -10));

            mCameraPositonDic.Add("3-1", new Vector3(-16f, -32.5f, -10f));
            mCameraPositonDic.Add("3-2", new Vector3(13.4f, -32.5f, -10f));
            mCameraPositonDic.Add("3-3", new Vector3(43.6f, -32.5f, -10f));
            mCameraPositonDic.Add("3-4", new Vector3(74.1f, -32.5f, -10f));
            mCameraPositonDic.Add("3-5", new Vector3(104.1f, -32.5f, -10f));

            mCameraPositonDic.Add("4-1", new Vector3(-16f, -32.5f, -10f));
            mCameraPositonDic.Add("4-2", new Vector3(13.4f, -32.5f, -10f));
            mCameraPositonDic.Add("4-3", new Vector3(43.6f, -32.5f, -10f));
            mCameraPositonDic.Add("4-4", new Vector3(74.1f, -32.5f, -10f));
            mCameraPositonDic.Add("4-5", new Vector3(104.1f, -32.5f, -10f));
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
        public static Vector3 GetPlayerPosition(int currentLevel, int currentSubLevel)
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
        public static Vector3 GetCameraPosition(int currentLevel, int currentSubLevel)
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

