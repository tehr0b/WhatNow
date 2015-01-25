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

[System.Serializable]
public struct MonsterSprites {
	
	public Sprite happyFace;
	public Sprite boredFace;
	public Sprite normalFace;

	public Color monsterColor;
}

public class Monster : MonoBehaviour {
	public MonsterSprites sprites;

	public Image image;

	[SerializeField] float tempStateTime = 3f;
	bool inTempState;

	public MonsterMood passiveState;

	IEnumerator currTempState;

	public void Awake ()
	{
		image = GetComponent<Image>();
		//GenerateNewMonster();
		image.sprite = sprites.normalFace;
	}

	public void GenerateNewMonster()
	{
		sprites = MonsterManager.instance.GetMonster();
		image.color = sprites.monsterColor;
	}

	public void RegenerateLastMonster()
	{
		sprites = MonsterManager.instance.GetLastMonster ();
		image.color = sprites.monsterColor;
	}

	public void SetTempState(MonsterMood mood) {
		if (currTempState != null)
			StopCoroutine (currTempState);
	
		currTempState = (TempState (mood));
		StartCoroutine (currTempState);
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
			image.sprite = sprites.happyFace;
			break;
		case MonsterMood.NEGATIVE:
			image.sprite = sprites.boredFace;
			break;
		case MonsterMood.NEUTRAL:
			image.sprite = sprites.normalFace;
			break;
		}
	}

}
