using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
	private Vector3 mousePosition;
	private Vector3 startPos;
	private BoardManager boardManager;

	public bool isDone;
	public int xIndex, yIndex;
	public string color;
	public int point;

	private void Start()
	{
		boardManager = BoardManager.Instance;
	}

	private void OnMouseDown()
	{
		boardManager.isHeld = true;
		boardManager.selectedGrid = this.gameObject;
	}

	private void OnMouseUp()
	{
		boardManager.isHeld = false;
		boardManager.selectedGrid = null;

	}
}
