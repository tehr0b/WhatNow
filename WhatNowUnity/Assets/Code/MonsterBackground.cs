using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterBackground : MonoBehaviour {

	[SerializeField]
	Image image;

	[SerializeField]
	Sprite[] possibleBackgrounds;

	static int lastBackgroundIndex = -1;

	// Use this for initialization
	void Start () {
		SetBackground ();
	}

	void SetBackground () {
		int backgroundIndex;
		do {
			backgroundIndex = Random.Range(0, possibleBackgrounds.Length);
		} while (backgroundIndex == lastBackgroundIndex);

		lastBackgroundIndex = backgroundIndex;
		image.sprite = possibleBackgrounds [backgroundIndex];
		//image.SetNativeSize ();
	}
}
