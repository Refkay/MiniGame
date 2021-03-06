﻿// 玩家进度信息单例
// 每次进入一关时调用一次 SubmitNewProgress(main, sub) 即可更新状态
using UnityEngine;

namespace MiniGame
{

    public class PlayerProgress
    {

        private PlayerProgress()
        {
            Load();
        }

        public static PlayerProgress Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested() { }

            internal static readonly PlayerProgress instance = new PlayerProgress();
        }

        // 是否玩过（会影响首页是否显示“继续游戏”按钮）
        public bool HasPlayed { get { return _hasPlayed; } }
        // 主关卡玩到的最高等级（正在玩还没通关）
        public int HighestMainLevel { get { return _highestMainLevel; } }
        // 子关卡玩到的最高等级（正在玩还没通关）
        public int HighestSubLevel { get { return _highestSubLevel; } }
        // 最近一次游戏正在玩的主关卡
        public int RecentMainLevel { get { return _recentMainLevel; } }
        // 最近一次游戏正在玩的子关卡
        public int RecentSubLevel { get { return _recentSubLevel; } }     

        public void SubmitNewProgress(int mainLv, int subLv)
        {
            _recentMainLevel = mainLv;
            _recentSubLevel = subLv;
            if (mainLv > _highestMainLevel || (mainLv == _highestMainLevel && subLv > _highestSubLevel))
            {
                _highestMainLevel = mainLv;
                _highestSubLevel = subLv;
            }
            Save();
        }

        // 重新读取玩家信息
        public void Reload()
        {
            Load();
        }

        // 保存玩家信息
        private void Save()
        {
            PlayerPrefs.SetInt(HAS_PALY, 1);
            PlayerPrefs.SetInt(HIGHEST_MAIN_LEVEL_KEY, _highestMainLevel);
            PlayerPrefs.SetInt(HIGHEST_SUB_LEVEL_KEY, _highestSubLevel);
            PlayerPrefs.SetInt(RECENT_MAIN_LEVEL_KEY, _recentMainLevel);
            PlayerPrefs.SetInt(RECENT_SUB_LEVEL_KEY, _recentSubLevel);     
        }

        // 读取玩家信息
        private void Load()
        {          
            if (HasPlay())
            {
                _highestMainLevel = PlayerPrefs.GetInt(HIGHEST_MAIN_LEVEL_KEY);
                _highestSubLevel = PlayerPrefs.GetInt(HIGHEST_SUB_LEVEL_KEY);
                _recentMainLevel = PlayerPrefs.GetInt(RECENT_MAIN_LEVEL_KEY);
                _recentSubLevel = PlayerPrefs.GetInt(RECENT_SUB_LEVEL_KEY);
            }
            else
            {
                _highestMainLevel = 1;
                _highestSubLevel = 1;
                _recentMainLevel = 1;
                _recentSubLevel = 1;
                _hasPlayed = false;
            }
        }    

        public bool HasPlay()
        {
            int hasPlay = PlayerPrefs.GetInt(HAS_PALY, 0);
            if (hasPlay == 0)
            {
                _hasPlayed = false;
            }
            else
            {
                _hasPlayed = true;
            }
            return _hasPlayed;
        }

        private const string HAS_PALY = "HasPlay";
        private const string HIGHEST_MAIN_LEVEL_KEY = "HMainLevel";
        private const string HIGHEST_SUB_LEVEL_KEY = "HSubLevel";
        private const string RECENT_MAIN_LEVEL_KEY = "RMainLevel";
        private const string RECENT_SUB_LEVEL_KEY = "RSubLevel";

        private const string GUIDE_FIRST = "GuideFirst";
        private const string GUIDE_SECOND = "GuideSecond";
        private const string GUIDE_THIRD = "GuideThird";
        private const string GUIDE_FOURTH = "GuideTHird";

        private bool _hasPlayed;
        private int _highestMainLevel;
        private int _highestSubLevel;
        private int _recentMainLevel;
        private int _recentSubLevel;

        public bool GetGuideFirst()
        {
            if (PlayerPrefs.GetInt(GUIDE_FIRST, 0) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetGuideFirst()
        {
            PlayerPrefs.SetInt(GUIDE_FIRST, 1);
        }

        public bool GetGuideSecond()
        {
            if (PlayerPrefs.GetInt(GUIDE_SECOND, 0) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetGuideSecond()
        {
            PlayerPrefs.SetInt(GUIDE_SECOND, 1);
        }

        public bool GetGuideThird()
        {
            if (PlayerPrefs.GetInt(GUIDE_THIRD, 0) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetGuideThird()
        {
            PlayerPrefs.SetInt(GUIDE_THIRD, 1);
        }

        public bool GetGuideFourth()
        {
            if (PlayerPrefs.GetInt(GUIDE_FOURTH, 0) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetGuideFourth()
        {
            PlayerPrefs.SetInt(GUIDE_FOURTH, 1);
        }
    }
}
