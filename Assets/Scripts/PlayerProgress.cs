// 玩家进度信息单例
// 
// 调用方式：
//    var mainLv = PlayerProgress.Instance.mainLevel;
//    PlayerProgress.Instance.mainLevel = mainLv;
//    PlayerProgress.Instance.Save();
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
	public bool hasPlayed;
	// 主关卡等级
	public int mainLevel;
	// 子关卡等级
	public int subLevel;

	// 重新读取玩家信息
	public void Reload()
	{
		Load ();
	}

	// 保存玩家信息
	public void Save()
	{
		PlayerPrefs.SetInt (MAIN_LEVEL_KEY, mainLevel);
		PlayerPrefs.SetInt (SUB_LEVEL_KEY, subLevel);
	}

	// 读取玩家信息
	private void Load()
	{
		if (PlayerPrefs.HasKey (MAIN_LEVEL_KEY) &&
		    PlayerPrefs.HasKey (SUB_LEVEL_KEY)) {
			mainLevel = PlayerPrefs.GetInt (MAIN_LEVEL_KEY);
			subLevel = PlayerPrefs.GetInt (SUB_LEVEL_KEY);
			hasPlayed = true;
		} else {
			mainLevel = 0;
			subLevel = 0;
			hasPlayed = false;
		}
	}

	private const string MAIN_LEVEL_KEY = "MainLevel";
	private const string SUB_LEVEL_KEY = "SubLevel";
}
