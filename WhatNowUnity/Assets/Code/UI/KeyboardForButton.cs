using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardForButton : MonoBehaviour {

	[SerializeField]
	KeyCode keyCode;

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
		Debug.Log ("Button click went through");
	}

}
