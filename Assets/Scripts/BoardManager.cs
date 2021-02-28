using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class BoardManager : MonoBehaviour
{
	[Header("Images")]
	public GameObject boardBackground;
	public GameObject gridBackground;
	public Sprite completedSprite;
	[Header("GridOffset")]
	public float xOffset; 
	public float yOffset; 
	
	public bool isHeld;
	public GameObject selectedGrid;

	public List<GameObject> gridObjects;
	public Action<int> scoreChanged;
	public Action<int> isMove;

	private LevelInfo levelInfo;
	private List<Vector3> gridPositions = new List<Vector3>();
	private GameObject[,] gridArray;
	private int columns, rows;

	#region Singleton
	public static BoardManager Instance;
	private void Awake()
	{
		Instance = this;
	}
	#endregion

	void Start()
	{
		levelInfo = GameManager.Instance.playerInfo.LevelList[GameManager.Instance.level];
		columns = levelInfo.gridWidth;
		rows = levelInfo.gridHeight;
		gridArray = new GameObject[columns, rows];
		SetupBoard();
	}


	void SetOffsets()
	{
		xOffset = (-0.5f * (columns - 1));
		yOffset = (-0.5f * (rows - 1));
	}

	void InitGridPoisitons()
	{


		for (int y = 0; y < rows; y++)
		{
			for (int x = 0; x < columns; x++)
			{
				gridPositions.Add(new Vector3(x + xOffset, y + yOffset, -1f));
			}
		}
	}

	public void MoveGrid(int x, int y)
	{
		if (selectedGrid.GetComponent<GridObject>().isDone)
			return;

		int selectedX = selectedGrid.GetComponent<GridObject>().xIndex;
		int selectedY = selectedGrid.GetComponent<GridObject>().yIndex;


		if (gridArray[selectedX + x, selectedY + y] != null)
		{
			GameObject otherObj = gridArray[selectedX + x, selectedY + y];

			if (otherObj.GetComponent<GridObject>().isDone)
				return;

			Vector3 temp = selectedGrid.transform.position;
			LeanTween.move(selectedGrid, otherObj.transform.position, 0.3f).setEase(LeanTweenType.easeOutQuad);
			
			selectedGrid.GetComponent<GridObject>().xIndex = selectedX + x;
			selectedGrid.GetComponent<GridObject>().yIndex = selectedY + y;
			LeanTween.move(otherObj, temp, 0.3f).setEase(LeanTweenType.easeOutQuad);

			otherObj.GetComponent<GridObject>().xIndex = selectedX;
			otherObj.GetComponent<GridObject>().yIndex = selectedY;

			gridArray[selectedX, selectedY] = otherObj;
			gridArray[selectedX + x, selectedY + y] = selectedGrid;

			GameManager.Instance.moveCount = GameManager.Instance.moveCount - 1;

			if(GameManager.Instance.moveCount == 0)
			{
				if (GameManager.Instance.currentScore > levelInfo.highScore)
				{
					GameManager.Instance.SetHighscore();
					GameManager.Instance.playerInfo.LevelList[levelInfo.levelNumber].locked = false;
				}
					
				GameManager.Instance.GameOver();
			}
			isMove?.Invoke(GameManager.Instance.moveCount);
			CheckCompletedRows();
		}
	}

	void CheckCompletedRows()
	{
		bool isCompleted = true;

		string rowFirstColor = "";
		for (int y = 0; y < rows; y++)
		{
			for (int x = 0; x < columns; x++)
			{
				if (gridArray[x, y].GetComponent<GridObject>().isDone)
				{
					break;
				}
				if (x == 0)
				{
					rowFirstColor = gridArray[x, y].GetComponent<GridObject>().color;
					isCompleted = true;
					continue;
				}
				else
				{

					if (gridArray[x, y].GetComponent<GridObject>().color != rowFirstColor )
					{
						isCompleted = false;
						break;
					}
				}

			}
			if (isCompleted)
				CompleteRow(y);
		}
	}

	void CompleteRow(int y)
	{
		Debug.Log(GameManager.Instance.currentScore + " " +gridArray[0, y].GetComponent<GridObject>().color + " " + gridArray[0, y].GetComponent<GridObject>().point);
		GameManager.Instance.currentScore += gridArray[0, y].GetComponent<GridObject>().point * columns;
		scoreChanged?.Invoke(GameManager.Instance.currentScore);

		for (int x = 0; x < columns; x++)
		{
			gridArray[x, y].GetComponent<SpriteRenderer>().sprite = completedSprite;
			gridArray[x, y].GetComponent<SpriteRenderer>().color = Color.white;
			LeanTween.scale(gridArray[x, y], new Vector3(1.7f,1.7f,1.7f), 1f).setEaseOutBack();
			gridArray[x, y].GetComponent<GridObject>().isDone = true;
		}
	}

	void SetupGrids()
	{
		int xIndex, yIndex;

		int i = 0;
		foreach (Vector3 position in gridPositions)
		{
			GameObject grid = new GameObject();
			grid.transform.position = position;

			GameObject gridBackgroundObj = Instantiate(gridBackground, position, Quaternion.identity);
			Vector3 gridObjPosition = new Vector3(position.x, position.y, position.z - 1);
			GameObject gridObj = CreateGridObject(levelInfo.ItemList[i], position);
			gridObj.GetComponent<GridObject>().color = levelInfo.ItemList[i];



			xIndex = i % columns;
			yIndex = (i / columns);
			gridObj.GetComponent<GridObject>().xIndex = xIndex;
			gridObj.GetComponent<GridObject>().yIndex = yIndex;
			gridArray[xIndex, yIndex] = gridObj;

			gridBackgroundObj.transform.SetParent(grid.transform);
			gridObj.transform.SetParent(grid.transform);
			grid.transform.SetParent(transform);

			gridObj.transform.localScale = new Vector3(0f, 0f, 0f);
			LeanTween.scale(gridObj, new Vector3(1f, 1f, 1f), 1f).setEaseOutBack();
			i++;
		}


	}

	GameObject CreateGridObject(string color, Vector3 pos)
	{

		GameObject newObj;
		switch (color)
		{
			case "r":
				newObj = Instantiate(gridObjects[0], pos, Quaternion.identity);
				newObj.GetComponent<GridObject>().point = 100;
				break;
			case "g":
				newObj = Instantiate(gridObjects[1], pos, Quaternion.identity);
				newObj.GetComponent<GridObject>().point = 150;
				break;
			case "y":
				newObj = Instantiate(gridObjects[2], pos, Quaternion.identity);
				newObj.GetComponent<GridObject>().point = 250;
				break;
			case "b":
				newObj = Instantiate(gridObjects[3], pos, Quaternion.identity);
				newObj.GetComponent<GridObject>().point = 200;
				break;
			default:
				Debug.LogError("Color problem");
				newObj = null;

				break;
		}

		return newObj;
	}

	void SetupBoardBackground()
	{
		float xPos = -0.5f + (columns / 2f) + xOffset;
		float yPos = -0.5f + (rows / 2f) + yOffset;
		Vector3 pos = new Vector3(xPos, yPos, 0f);

		GameObject bgObj = Instantiate(boardBackground, pos, Quaternion.identity);

		bgObj.transform.SetParent(transform);
		bgObj.transform.localScale = new Vector3(0, 0, 0);
		//LeanTween.scale(bgObj, new Vector3(columns, rows, 0), 1f).setEaseOutBack();
	}

	void SetupBoard()
	{
		SetOffsets();
		SetupBoardBackground();
		InitGridPoisitons();
		SetupGrids();
	}

}
