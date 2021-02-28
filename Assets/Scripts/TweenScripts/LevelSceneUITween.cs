using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneUITween : MonoBehaviour
{
    void Start()
    {
		transform.localScale = new Vector3(0f, 0f, 0f);
		LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.8f).setEaseOutBack();
    }

    void Update()
    {
        
    }
}
