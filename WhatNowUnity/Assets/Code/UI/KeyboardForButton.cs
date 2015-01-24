using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardForButton : MonoBehaviour {

	public KeyCode keyCode;

	[SerializeField]
	Image buttonImage;

	Button button;

	void Awake(){
		button = GetComponent<Button> ();
	}

	void Update(){
		if (Input.GetKeyDown (keyCode)) {
			button.onClick.Invoke();
		}
	}

	public void ButtonClick()
	{
		Debug.Log ("Button click went through: " + name);
	}

	IEnumerator DimButton(){
		yield return null;
	}

}
