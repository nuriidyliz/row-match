using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class RMFileManager
{

	public RMFileData ReadRMFiles(string path)
	{

		if (!File.Exists(path))
		{
			Debug.LogWarning("Missing RMFile");
			return null;
		}
		List<string> lines = File.ReadAllLines(path).ToList<string>();

		RMFileData rmData = new RMFileData
		{
			levelNumber = int.Parse(lines.Find(item => item.Contains("level_number")).Split(':')[1]),
			gridWidth = int.Parse(lines.Find(item => item.Contains("grid_width")).Split(':')[1]),
			gridHeight = int.Parse(lines.Find(item => item.Contains("grid_height")).Split(':')[1]),
			moveCount = int.Parse(lines.Find(item => item.Contains("move_count")).Split(':')[1]),
			itemList = lines.FindLast(item => item.Contains("grid")).Split(':')[1].Trim().Split(',').ToList<string>()

		};
		return rmData;
	}


}

public class RMFileData
{
	public int levelNumber;
	public int gridWidth;
	public int gridHeight;
	public int moveCount;
	public List<string> itemList;
}
