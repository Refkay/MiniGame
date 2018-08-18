// 配置信息
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Config {
	
	// 配置文件名限制为 config.json
	private static string fileName = "config.json";

	// 移动速度
	public float moveSpeed;
	// 单次拖动屏幕最大延迟时间
	public float dragTimeThreshold;
	// 单次拖动屏幕最大距离
	public float offsetThreshold;

	// 读取配置，如果配置文件不存在会返回空
	public static Config Load()
	{
		string fullPath = Path.Combine (Application.streamingAssetsPath, fileName);
		if (File.Exists (fullPath)) {
			string dataAsJson = File.ReadAllText (fullPath);
			return JsonUtility.FromJson<Config>(dataAsJson);
		} else {
			Debug.LogError ("Can't load config file from path: " + fullPath);
			return null;
		}
	}

	// 保存配置文件，当且仅当配置文件存在时可以保存成功
	public void Save()
	{
		string fullPath = Path.Combine (Application.streamingAssetsPath, fileName);
		string text = "";
		FileInfo file = new FileInfo(fullPath);
		if (file.Exists) {
			text = JsonUtility.ToJson (this);
			StreamWriter w = new StreamWriter (fullPath);
			w.Write (text);
			w.Close ();
		} else {
			Debug.LogError ("No such file: " + fullPath);
		}
	}
}
