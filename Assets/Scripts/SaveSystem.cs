using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
public static class SaveSystem
{
	static List<string> fileNameList;

	public static void SavePlayerInfo(PlayerInfo playerInfo)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/playerInfo";
		FileStream stream = new FileStream(path, FileMode.Create);

		formatter.Serialize(stream, playerInfo);
		stream.Close();
		Debug.Log("Saved! : " + Application.persistentDataPath + "/playerInfo");

	}

	public static PlayerInfo LoadPlayerInfo()
	{
		string path = Application.persistentDataPath + "/playerInfo";
		PlayerInfo playerInfo;

		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			playerInfo = formatter.Deserialize(stream) as PlayerInfo;
			stream.Close();

			CheckAllLevelsLoaded(playerInfo);
			Debug.Log("PlayerInfo loaded from " + Application.persistentDataPath + "/playerInfo");

		}
		else
		{
			Debug.LogWarning("'playerInfo' could not found in " + Application.persistentDataPath);
			playerInfo = new PlayerInfo();
			InitializePlayerInfo(playerInfo);
			SavePlayerInfo(playerInfo);
		}
		return playerInfo;
	}

	static void InitializePlayerInfo(PlayerInfo playerInfo)
	{
		Directory.CreateDirectory(Application.persistentDataPath + "/RMFiles");
		GetStreamingAsset("fileList");
		fileNameList = File.ReadAllLines(Application.persistentDataPath + "/fileList").ToList<string>();

		GetBuiltInLevels();
		TryGetFilesFromServer(fileNameList);
		SetPlayerInfo(playerInfo);

	}

	static void SetPlayerInfo(PlayerInfo playerInfo)
	{
		RMFileManager rmFileManager = new RMFileManager();
		RMFileData rmData;

		int startIndex = playerInfo.LevelList.Count == 0 ? 0 : 10;

		for(int i = startIndex; i < fileNameList.Count; i++)
		{
			rmData = rmFileManager.ReadRMFiles(Application.persistentDataPath + "/RMFiles/" + fileNameList[i]);
			if (rmData == null)
			{
				return;
			}
			LevelInfo levelInfo = new LevelInfo
			{
				levelNumber = rmData.levelNumber,
				gridWidth = rmData.gridWidth,
				gridHeight = rmData.gridHeight,
				moveCount = rmData.moveCount,
				ItemList = rmData.itemList,
				highScore = 0,
				locked = i < 3 ? false : true
			};

			playerInfo.LevelList.Add(levelInfo);
		}

	}

	static void GetBuiltInLevels()
	{
		for (int i = 0; i < 10; i++)
		{
			string fileName = fileNameList[i];
			GetStreamingAsset("RMFiles/" + fileName);
		}
	}

	static void GetStreamingAsset(string name)
	{
		var www = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/" + name);
		www.SendWebRequest();

		while (!www.isDone)
		{

			if (www.isNetworkError)
			{
				Debug.LogError("NetworkError! - StreamingAssets");
			}
		}

		if (www.error != null)
		{
			Debug.LogWarning("WWW data error!" + "error: " + www.error + " (" + name + ")");
			return;
		}
		File.WriteAllBytes(Application.persistentDataPath + "/" + name, www.downloadHandler.data);
	}

	static void TryGetFilesFromServer(List<string> fileNameList)
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			for (int i = 10; i < fileNameList.Count; i++)
			{
				WebClient client = new WebClient();

				Stream data = client.OpenRead("https://row-match.s3.amazonaws.com/levels/" + fileNameList[i]);
				StreamReader reader = new StreamReader(data);
				string s = reader.ReadToEnd();
				File.WriteAllText(Application.persistentDataPath + "/RMFiles/" + fileNameList[i], s);
				data.Close();
				reader.Close();

			}
		}
		
	}

	static void CheckAllLevelsLoaded(PlayerInfo playerInfo)
	{
		if (playerInfo.LevelList.Count == 10)
		{
			fileNameList = File.ReadAllLines(Application.persistentDataPath + "/fileList").ToList<string>();
			TryGetFilesFromServer(fileNameList);
			SetPlayerInfo(playerInfo);
		}
	}

	
}
