using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Config {
	
	private static string fileName = "config.json";

	public float MoveSpeed;
	public float DragTimeThreshold;
	public float OffsetThreshold;

	public static Config LoadFromFile()
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

	public void SaveToFile(Config config)
	{
		string fullPath = Path.Combine (Application.streamingAssetsPath, fileName);
		string text = "";
		FileInfo file = new FileInfo(fullPath);
		if (file.Exists) {
			text = JsonUtility.ToJson (config);
			StreamWriter w = new StreamWriter (fullPath);
			w.Write (text);
			w.Close ();
		} else {
			Debug.LogError ("No such file: " + fullPath);
		}
	}
}
