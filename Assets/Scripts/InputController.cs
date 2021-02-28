using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
	
	public Vector2 mousePosition;
	public Vector2 startPosition;
	public Vector2 lastPosition;
	public bool hasInput = false;
	public abstract void GetMousePosition();
}
