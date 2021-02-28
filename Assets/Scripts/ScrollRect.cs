using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScrollRect : MonoBehaviour
{

	public GameObject scrollRectObject;

	public List<GameObject> levelPanelList;
	public GameObject levelPanelPrefab;
	public float basePanelPosition;
	public float panelDistance;

	private float startX, startY;
	private Vector3 mousePosition;
	private bool isHeld;

	private void Start()
	{
		GameManager.Instance.gameOver += RefreshLevelsPanel;
		SetLevelsPanel(GameManager.Instance.playerInfo);
	}

	void Update()
    {
		MoveScrollRect();
    }

	void MoveScrollRect()
	{

		if (isHeld)
		{
			if (LeanTween.isTweening(scrollRectObject))
				LeanTween.cancelAll(scrollRectObject);
			GetInputPosition();
			scrollRectObject.transform.localPosition = new Vector3(scrollRectObject.transform.localPosition.x, mousePosition.y - startY, 0f);
		}
		else if(scrollRectObject.transform.localPosition.y < 0  && !LeanTween.isTweening(scrollRectObject))
		{
			LeanTween.move(scrollRectObject, new Vector3(0f, 0f, 0f), 0.5f).setEaseOutSine();

		}
		else if(scrollRectObject.transform.localPosition.y > levelPanelList.Count && !LeanTween.isTweening(scrollRectObject))
		{
			LeanTween.move(scrollRectObject, new Vector3(0f, levelPanelList.Count, 0f), 0.5f).setEaseOutSine();
		}
	}


	private void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GetInputPosition();
			startY = mousePosition.y - scrollRectObject.transform.localPosition.y;
			isHeld = true;
		}
	}

	private void OnMouseUp()
	{
		isHeld = false;
	}

	private void GetInputPosition()
	{
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
	}


	void SetLevelsPanel(PlayerInfo playerInfo)
	{
		Vector3 position = new Vector3(0f, basePanelPosition, 0f);

		foreach (LevelInfo levelInfo in playerInfo.LevelList)
		{

			GameObject newObj = Instantiate(levelPanelPrefab, position, Quaternion.identity);
			LevelPanel newLevelPanel = newObj.GetComponent<LevelPanel>();
			newLevelPanel.levelInfo = levelInfo;
			newLevelPanel.SetDisplay();

			newObj.transform.SetParent(scrollRectObject.transform);
			newObj.transform.localPosition = position;
			position.y -= panelDistance;
			levelPanelList.Add(newObj);
		}
	}

	void RefreshLevelsPanel()
	{
		levelPanelList[GameManager.Instance.level].GetComponent<LevelPanel>().SetDisplay();

		if (levelPanelList[GameManager.Instance.level].GetComponent<LevelPanel>().levelInfo.highScore != 0)
		{
			levelPanelList[GameManager.Instance.level + 1].GetComponent<LevelPanel>().SetDisplay();
		}
	}
}
