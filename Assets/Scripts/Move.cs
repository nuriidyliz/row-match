using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Move : MonoBehaviour
{
	public TextMeshPro text3;

	public float minInputDistance;
	public InputController InputController;
	BoardManager boardManager;
	
    void Start()
    {
		SetInputController();
		boardManager = BoardManager.Instance;
	}
	
    void Update()
    {

		if (InputController.hasInput && boardManager.isHeld && MinInputDistanceCheck())
		{
			TryMove();
		}
    }

	void TryMove()
	{
		if (!MinInputDistanceCheck())
			return;

		if (VerticalMove())
		{
			if (MoveUp())
			{
				//Debug.Log("move up");
				boardManager.MoveGrid(0,1);
			}
			else if (MoveDown())
			{
				//Debug.Log("move down");
				boardManager.MoveGrid(0, -1);

			}
		}
		else
		{
			if (MoveRight())
			{
				//Debug.Log("move right");
				boardManager.MoveGrid(1, 0);

			}
			else if (MoveLeft())
			{
				//Debug.Log("move left");
				boardManager.MoveGrid(-1, 0);

			}
		}
		boardManager.isHeld = false;

	}

	bool MoveUp() { return VerticalInput() > 0; }
	bool MoveDown() { return VerticalInput() < 0; }
	bool MoveRight() { return HorizontalInput() > 0; }
	bool MoveLeft() { return HorizontalInput() < 0; }

	bool VerticalMove()
	{
		return Mathf.Abs(VerticalInput()) > Mathf.Abs(HorizontalInput());
	}

	float VerticalInput()
	{
		return InputController.mousePosition.y - InputController.startPosition.y;
	}

	float HorizontalInput()
	{
		return InputController.mousePosition.x - InputController.startPosition.x;
	}

	bool MinInputDistanceCheck()
	{
		return Mathf.Abs(VerticalInput()) > minInputDistance || Mathf.Abs(HorizontalInput()) > minInputDistance;
	}

	#region Setting Up
	void SetInputController()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		InputController = GetComponent<StandaloneController>();
		InputController.enabled = true;
#elif UNITY_ANDROID && !UNITY_EDITOR
		InputController = GetComponent<MobileController>();
		InputController.enabled = true;
#endif
	}
	#endregion
}
