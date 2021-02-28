using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{

	public List<LevelInfo> LevelList;

	public PlayerInfo()
	{
		LevelList = new List<LevelInfo>(10);
	}

}

[System.Serializable]
public class LevelInfo
{
	public int levelNumber;		
	public int gridWidth;
	public int gridHeight;
	public int moveCount;
	public List<string> ItemList;

	public int highScore;
	public bool locked;
}
