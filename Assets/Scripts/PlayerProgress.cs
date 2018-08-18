// 玩家进度信息
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress {

	// 是否玩过（会影响首页是否显示“继续游戏”按钮）
	public bool hasPlayed;
	// 主关卡等级
	public int mainLevel;
	// 子关卡等级
	public int subLevel;

	private const string MAIN_LEVEL_KEY = "MainLevel";
	private const string SUB_LEVEL_KEY = "SubLevel";

	// 读取玩家信息，不会返回空
	public static PlayerProgress Load()
	{
		var progress = new PlayerProgress ();
		if (PlayerPrefs.HasKey (MAIN_LEVEL_KEY) &&
		    PlayerPrefs.HasKey (SUB_LEVEL_KEY)) {
			progress.mainLevel = PlayerPrefs.GetInt (MAIN_LEVEL_KEY);
			progress.subLevel = PlayerPrefs.GetInt (SUB_LEVEL_KEY);
			progress.hasPlayed = true;
		} else {
			progress.mainLevel = 0;
			progress.subLevel = 0;
			progress.hasPlayed = false;
		}
		return progress;
	}

	// 保存玩家信息
	public void SaveProgress()
	{
		PlayerPrefs.SetInt (MAIN_LEVEL_KEY, mainLevel);
		PlayerPrefs.SetInt (SUB_LEVEL_KEY, subLevel);
	}
}
