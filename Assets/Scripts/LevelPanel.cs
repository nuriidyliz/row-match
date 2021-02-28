using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelPanel : MonoBehaviour
{
	public LevelInfo levelInfo;
	public ButtonScript playButton;

	private void Start()
	{
		playButton.buttonClicked += SetLevel;
	}

	public void SetDisplay()
	{
		string levelText = "Level " + levelInfo.levelNumber + " - " + levelInfo.moveCount + " Moves";
		string scoreText = "Highest Score " + levelInfo.highScore;
		transform.Find("Texts/LevelText").GetComponent<TextMeshPro>().text = levelText;
		transform.Find("Texts/ScoreText").GetComponent<TextMeshPro>().text = scoreText;
		if (levelInfo.locked)
		{
			playButton.GetComponent<BoxCollider2D>().enabled = false;
			playButton.transform.Find("Image").GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f,0.8f);
			playButton.transform.Find("Text").gameObject.SetActive(false);
			playButton.transform.Find("Lock").gameObject.SetActive(true);
		}
		else
		{
			playButton.GetComponent<BoxCollider2D>().enabled = true;
			playButton.transform.Find("Image").GetComponent<SpriteRenderer>().color = Color.white;
			playButton.transform.Find("Text").gameObject.SetActive(true);
			playButton.transform.Find("Lock").gameObject.SetActive(false);
		}
	}

	public void SetLevel()
	{
		GameManager.Instance.level = levelInfo.levelNumber - 1;
		GameManager.Instance.LoadScene("LevelScene");
	}
}
