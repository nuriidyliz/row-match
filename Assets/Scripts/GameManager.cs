using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public PlayerInfo playerInfo;
	public int level;		//starts with 0
	public int currentScore = 0;
	public int moveCount;
	public event Action levelLoaded;
	public event Action gameOver;
	public event Action celebrate;
	public bool isHighscore = false;

	#region Singleton
	public static GameManager Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this; 
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
			Destroy(gameObject);

		WhenAwake();

	}
	#endregion
	void WhenAwake()
    {
		playerInfo = SaveSystem.LoadPlayerInfo();

		DontDestroyOnLoad(Instance);
    }

    public void SetHighscore()
	{
		playerInfo.LevelList[level].highScore = currentScore;
		isHighscore = true;
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);

		if(scene.Equals("LevelScene"))
			StartLevel();
		
	}

	public void StartLevel()
	{
		moveCount = playerInfo.LevelList[level].moveCount;

		levelLoaded?.Invoke();
	}

	public void GameOver()
	{
		SaveSystem.SavePlayerInfo(playerInfo);
		currentScore = 0;
		LoadScene("MainScene");
		gameOver?.Invoke();

	}

}
