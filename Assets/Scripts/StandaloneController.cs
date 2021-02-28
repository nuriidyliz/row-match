using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneController : InputController
{
    void Update()
    {
		GetMousePosition();
    }

	public override void GetMousePosition()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startPosition = Input.mousePosition;
			startPosition = Camera.main.ScreenToWorldPoint(startPosition);
			hasInput = true;
		}
		if (Input.GetMouseButton(0))
		{
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		}
		if (Input.GetMouseButtonUp(0))
		{
			lastPosition = Input.mousePosition;
			lastPosition = Camera.main.ScreenToWorldPoint(lastPosition);
			hasInput = false;
		}


	}
}
