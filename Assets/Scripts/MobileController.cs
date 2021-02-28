using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : InputController
{
	private Touch touch;

    void Update()
    {
        if(Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
			GetMousePosition();
		}
    }

	public override void GetMousePosition()
	{
		if (touch.phase == TouchPhase.Began)
		{
			startPosition = Input.mousePosition;
			startPosition = Camera.main.ScreenToWorldPoint(startPosition);
		}
		if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
		{
			hasInput = true;
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		}
		if (touch.phase == TouchPhase.Ended)
		{
			lastPosition = Input.mousePosition;
			lastPosition = Camera.main.ScreenToWorldPoint(lastPosition);
			hasInput = false;
		}
	}
}
