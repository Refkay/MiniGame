// 玩家进度信息单例
// 每次进入一关时调用一次 SubmitNewProgress(main, sub) 即可更新状态
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress {

	private PlayerProgress() {
		Load ();
	}

	public static PlayerProgress Instance { get { return Nested.instance; } }

	private class Nested
	{
		static Nested() {}

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
		if (mainLv > _highestMainLevel || 
			(mainLv == HighestSubLevel && subLv > _highestSubLevel)) {
			_highestMainLevel = mainLv;
			_highestSubLevel = subLv;
		}
		Save ();
	}

	// 重新读取玩家信息
	public void Reload()
	{
		Load ();
	}

	// 保存玩家信息
	private void Save()
	{
		PlayerPrefs.SetInt (HIGHEST_MAIN_LEVEL_KEY, _highestMainLevel);
		PlayerPrefs.SetInt (HIGHEST_SUB_LEVEL_KEY, _highestSubLevel);
		PlayerPrefs.SetInt (RECENT_MAIN_LEVEL_KEY, _recentMainLevel);
		PlayerPrefs.SetInt (RECENT_SUB_LEVEL_KEY, _recentSubLevel);
	}

	// 读取玩家信息
	private void Load()
	{
		if (DataExist()) {
			_highestMainLevel = PlayerPrefs.GetInt (HIGHEST_MAIN_LEVEL_KEY);
			_highestSubLevel = PlayerPrefs.GetInt (HIGHEST_SUB_LEVEL_KEY);
			_recentMainLevel = PlayerPrefs.GetInt (RECENT_MAIN_LEVEL_KEY);
			_recentSubLevel = PlayerPrefs.GetInt (RECENT_SUB_LEVEL_KEY);
			_hasPlayed = true;
		} else {
			_highestMainLevel = 0;
			_highestSubLevel = 0;
			_recentMainLevel = 0;
			_recentSubLevel = 0;
			_hasPlayed = false;
		}
	}

	private bool DataExist()
	{
		return 
			PlayerPrefs.HasKey (HIGHEST_MAIN_LEVEL_KEY) &&
			PlayerPrefs.HasKey (HIGHEST_SUB_LEVEL_KEY) &&
			PlayerPrefs.HasKey (RECENT_MAIN_LEVEL_KEY) &&
			PlayerPrefs.HasKey (RECENT_SUB_LEVEL_KEY);
	}

	private const string HIGHEST_MAIN_LEVEL_KEY = "HMainLevel";
	private const string HIGHEST_SUB_LEVEL_KEY = "HSubLevel";
	private const string RECENT_MAIN_LEVEL_KEY = "RMainLevel";
	private const string RECENT_SUB_LEVEL_KEY = "RSubLevel";

	private bool _hasPlayed;
	private int _highestMainLevel;
	private int _highestSubLevel;
	private int _recentMainLevel;
	private int _recentSubLevel;
}
