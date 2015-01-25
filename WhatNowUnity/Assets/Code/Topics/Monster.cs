using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Monster : MonoBehaviour {

	public Sprite happyFace;
	public Sprite boredFace;
	public Sprite normalFace;
	public float dateSatisfaction = 1.0f;
	public Image image;

	public void Awake ()
	{
		image = GetComponent<Image>();
		image.sprite = normalFace;
	}

}
