using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum MonsterMood {
	NEUTRAL,
	HAPPY,
	NEGATIVE,
	OVERIT
}

public class Monster : MonoBehaviour {

	public Sprite happyFace;
	public Sprite boredFace;
	public Sprite normalFace;

	public float dateSatisfaction = 1.0f;
	public Image image;

	[SerializeField] float tempStateTime = 3f;
	bool inTempState;

	public MonsterMood passiveState;

	Coroutine currTempState;

	public void Awake ()
	{
		image = GetComponent<Image>();
		image.sprite = normalFace;
	}

	public void SetTempState(MonsterMood mood) {
		if (currTempState != null)
			StopCoroutine (currTempState);

		currTempState = StartCoroutine (TempState (mood));
	}

	IEnumerator TempState(MonsterMood mood) {
		SetState (mood);
		inTempState = true;
		yield return new WaitForSeconds(tempStateTime);
		SetState (passiveState);
		inTempState = false;
		currTempState = null;
	}

	public void SetPassiveState(MonsterMood mood) {
		passiveState = mood;
		if (!inTempState)
			SetState (mood);
	}

	void SetState(MonsterMood mood){
		switch (mood) {
		case MonsterMood.HAPPY:
			image.sprite = happyFace;
			break;
		case MonsterMood.NEGATIVE:
			image.sprite = boredFace;
			break;
		case MonsterMood.NEUTRAL:
			image.sprite = normalFace;
			break;
		case MonsterMood.OVERIT:
			image.sprite = null;
			break;
		}
	}

}
