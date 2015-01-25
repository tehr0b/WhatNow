using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	RectTransform mainMenu;

	[SerializeField]
	RectTransform credits;

	[SerializeField]
	RectTransform directions;

	RectTransform currScreen;

	[SerializeField]
	float targetRightX = 350;

	[SerializeField]
	float targetLeftX = 350;
	
	[SerializeField]
	float moveTime;

	[SerializeField]
	AnimationCurve moveCurve;

	void Start(){
		currScreen = mainMenu;
	}

	public void ClickStart() {
		Application.LoadLevel("TestScene");
	}

	public void ClickCredits() {
		StartCoroutine (MoveMenu (mainMenu, targetLeftX));
		StartCoroutine (MoveMenu (credits, 0));
		currScreen = credits;
	}

	public void ClickDirections() {
		StartCoroutine (MoveMenu (mainMenu, targetLeftX));
		StartCoroutine (MoveMenu (directions, 0));
		currScreen = directions;
	}

	public void ClickBack() {
		StartCoroutine (MoveMenu (currScreen, targetRightX));
		StartCoroutine (MoveMenu (mainMenu, 0));
		currScreen = mainMenu;
	}

	IEnumerator MoveMenu(RectTransform rect, float x) {
		float currTime = 0;
		Vector3 startPos = rect.transform.localPosition;
		Vector3 targetPos = new Vector3 (x, rect.transform.localPosition.y, rect.transform.localPosition.z);
		while (currTime < moveTime) {
			currTime += Time.deltaTime;
			rect.transform.localPosition = Vector3.Lerp(startPos, targetPos, currTime/moveTime);
			yield return null;
		}
	}
}
