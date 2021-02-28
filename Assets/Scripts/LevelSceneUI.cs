using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSceneUI : MonoBehaviour
{
	public BoardManager boardManager;
	public TextMeshPro highscore;
	public TextMeshPro score;
	public TextMeshPro moves;
	public TextMeshPro level;
	GameManager gameManager;


	void Start()
    {
		gameManager = GameManager.Instance;
		InıtValues();
		boardManager.scoreChanged += SetScore;
		boardManager.isMove += SetMoves;
		gameManager.levelLoaded += InıtValues;

    }

    void SetScore(int score)
	{
		this.score.text = score.ToString();
	}

	void SetMoves(int moves)
	{
		this.moves.text = moves.ToString();
	}

	void SetHighscore(int score)
	{
		highscore.text = score.ToString();

	}

	void InıtValues()
	{
		SetMoves(gameManager.playerInfo.LevelList[gameManager.level].moveCount);
		SetHighscore(gameManager.playerInfo.LevelList[gameManager.level].highScore);
		level.text = "Level " + gameManager.level.ToString();
	}


}
