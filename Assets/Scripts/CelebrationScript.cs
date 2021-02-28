using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CelebrationScript : MonoBehaviour
{
	public GameObject celebration;
	public TextMeshPro highscoreText;

    void Start()
    {
		highscoreText.text = "Highscore: " + GameManager.Instance.playerInfo.LevelList[GameManager.Instance.level].highScore;
		if (GameManager.Instance.isHighscore)
		{
			celebration.SetActive(true);
			GameManager.Instance.isHighscore = false;
			StartCoroutine(Wait(5));
		}
	}

	IEnumerator Wait(float second)
	{
		yield return new WaitForSeconds(second);
		celebration.SetActive(false);

	}


}
