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

        //保存每一关是否可以无限次转向的Dictionary
        public static Dictionary<string, bool> mTurnInfiniteDic = new Dictionary<string, bool>();

        /// <summary>
        /// 加载每个小关卡的位置，其实就是保存位置信息到Dictionary里面
        /// TODO ： 这里为了测试先这么写，后续可以再改进数据读取方式
        /// </summary>
        public static void LoadMissionData()
        {           
            //加载关卡信息
            mMissionDataDic.Add(1, 4);
            mMissionDataDic.Add(2, 5);
            mMissionDataDic.Add(3, 7);
            //mMissionDataDic.Add(4, 5);

            //加载Player每一小关的起始位置信息
            mPlayerPositionDic.Add("1-1", new Vector3(0f, -9f, 0f));
            mPlayerPositionDic.Add("1-2", new Vector3(23.7f, -17.6f, 0));
            mPlayerPositionDic.Add("1-3", new Vector3(-6.3f, -43.7f, 0));
            mPlayerPositionDic.Add("1-4", new Vector3(27.9f, -56.1f, 0));

            mPlayerPositionDic.Add("2-1", new Vector3(-43.9f, 17.6f, 0));
            mPlayerPositionDic.Add("2-2", new Vector3(-22.7f, -2.7f, 0));
            mPlayerPositionDic.Add("2-3", new Vector3(0.9f, -17.4f, 0));
            mPlayerPositionDic.Add("2-4", new Vector3(-7.4f, -48.8f, 0));
            mPlayerPositionDic.Add("2-5", new Vector3(38.4f, -14.1f, 0));

            mPlayerPositionDic.Add("3-1", new Vector3(-16f, -41.5f, 0f));
            mPlayerPositionDic.Add("3-2", new Vector3(15.8f, -61.2f, 0f));
            mPlayerPositionDic.Add("3-3", new Vector3(52f, -23.2f, 0f));
            mPlayerPositionDic.Add("3-4", new Vector3(111.3f, -79.3f, 0f));
            mPlayerPositionDic.Add("3-5", new Vector3(69.3f, -69.25f, 0f));
            mPlayerPositionDic.Add("3-6", new Vector3(43.3f, -83.18f, 0f));
            mPlayerPositionDic.Add("3-7", new Vector3(47.0f, -109f, 0f));

            //加载Camera每一小关的位置
            mCameraPositonDic.Add("1-1", new Vector3(0f, 0f, -15));
            mCameraPositonDic.Add("1-2", new Vector3(23.7f, -8.6f, -15));
            mCameraPositonDic.Add("1-3", new Vector3(-6.3f, -34.7f, -15));
            mCameraPositonDic.Add("1-4", new Vector3(27.9f, -47.1f, -15));

            mCameraPositonDic.Add("2-1", new Vector3(-43.9f, 26.6f, -15));
            mCameraPositonDic.Add("2-2", new Vector3(-22.7f, 6.3f, -15));
            mCameraPositonDic.Add("2-3", new Vector3(0.9f, -8.4f, -15));
            mCameraPositonDic.Add("2-4", new Vector3(-7.4f, -39.8f, -15));
            mCameraPositonDic.Add("2-5", new Vector3(38.4f, -5.1f, -15));

            mCameraPositonDic.Add("3-1", new Vector3(-16f, -32.5f, -15f));
            mCameraPositonDic.Add("3-2", new Vector3(15.8f, -52.2f, -15f));
            mCameraPositonDic.Add("3-3", new Vector3(52f, -14.2f, -15f));
            mCameraPositonDic.Add("3-4", new Vector3(111.3f, -70.3f, -15f));
            mCameraPositonDic.Add("3-5", new Vector3(69.3f, -60.25f, -15f));
            mCameraPositonDic.Add("3-6", new Vector3(43.3f, -74.18f, -15f));
            mCameraPositonDic.Add("3-7", new Vector3(47.0f, -100f, -15f));

            //加载每一关是否可以无限次转向的信息
            mTurnInfiniteDic.Add("1-1", false);
            mTurnInfiniteDic.Add("1-2", false);
            mTurnInfiniteDic.Add("1-3", false);
            mTurnInfiniteDic.Add("1-4", false);

            mTurnInfiniteDic.Add("2-1", false);
            mTurnInfiniteDic.Add("2-2", false);
            mTurnInfiniteDic.Add("2-3", false);
            mTurnInfiniteDic.Add("2-4", false);
            mTurnInfiniteDic.Add("2-5", false);

            mTurnInfiniteDic.Add("3-1", false);
            mTurnInfiniteDic.Add("3-2", false);
            mTurnInfiniteDic.Add("3-3", false);
            mTurnInfiniteDic.Add("3-4", false);
            mTurnInfiniteDic.Add("3-5", false);
            mTurnInfiniteDic.Add("3-6", false);
            mTurnInfiniteDic.Add("3-7", false);

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

        //获取某个关卡能否无限转向
        public static bool GetLevelTurnInfinite(int currentLevel, int currentSubLevel)
        {
            bool canTrunInfinite = false;
            foreach (KeyValuePair<string, bool> kvp in mTurnInfiniteDic)
            {
                if (kvp.Key == currentLevel + "-" + currentSubLevel)
                {
                    canTrunInfinite = kvp.Value;
                    break;
                }
            }
            return canTrunInfinite;
        }
    }
}

