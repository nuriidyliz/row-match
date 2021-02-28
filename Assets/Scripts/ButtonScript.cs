using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

	public TextMesh buttonText;

	[Header("Image")]
	public SpriteRenderer buttonSpriteRenderer;
	public Sprite normalImage;
	public Sprite highlightedImage;
	public Sprite pressedImage;

	public event Action buttonClicked;

	private void OnMouseDown()
	{
		buttonSpriteRenderer.sprite = pressedImage;
	}

	private void OnMouseEnter()
	{
		buttonSpriteRenderer.sprite = highlightedImage;
	}

	private void OnMouseExit()
	{
		buttonSpriteRenderer.sprite = normalImage;
	}

	public virtual void OnMouseUpAsButton()
	{
		buttonSpriteRenderer.sprite = normalImage;
		buttonClicked?.Invoke();
	}


}
