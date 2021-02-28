using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RM_UIManager : MonoBehaviour
{
	public ButtonScript levelsButton;
	public ButtonScript closePopupButton;
	public GameObject popUp;
	public GameObject scorePanel;
	public GameObject celebration;

	#region Singleton
	public static RM_UIManager Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
			Destroy(gameObject);
		

	}
	#endregion

	private void Start()
	{
		DontDestroyOnLoad(this);
		levelsButton.buttonClicked += OpenPopup;
		levelsButton.buttonClicked += DeactivateLevelsButton;
		closePopupButton.buttonClicked += ClosePopup;
		closePopupButton.buttonClicked += ActivateLevelsButton;
		GameManager.Instance.levelLoaded += ClosePopup;
		GameManager.Instance.gameOver += OpenPopup;

	}

	void OpenPopup()
	{
		popUp.SetActive(true);
		LeanTween.scale(popUp, new Vector3(1f, 1f, 1f), 0.2f).setEaseInCubic();

	}

	void ClosePopup()
	{
		LeanTween.scale(popUp, new Vector3(0f, 0f, 0f), 0.2f).setEaseInCubic();
		if(!LeanTween.isTweening(popUp))
			popUp.SetActive(false);
	}

	void DeactivateLevelsButton()
	{
		levelsButton.gameObject.SetActive(false);
	}

	void ActivateLevelsButton()
	{
		levelsButton.gameObject.SetActive(true);
	}

	public void SetScore(int score)
	{
		scorePanel.transform.Find("Score").GetComponent<TextMeshPro>().text = score.ToString();
	}

}
