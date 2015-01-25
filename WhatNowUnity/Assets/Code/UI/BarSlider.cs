using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarSlider : MonoBehaviour {

	[SerializeField]
	Image sliderImage;

	[SerializeField]
	Image barImage;

	[SerializeField]
	Bar bar;

	[SerializeField]
	Color hasColor = Color.yellow;

	[SerializeField]
	Color doesntColor = Color.yellow;

	float limit { 
		get {
			return barImage.rectTransform.rect.width/2;
		}
	}

	[SerializeField] float moveSpeed = 30;
	[SerializeField] float acceleration = .25f;
	public int direction = 1;

	bool hasHitThisPass = false;

	void Start() {
		transform.localPosition = new Vector3 (-limit, transform.localPosition.y, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
		if (Mathf.Abs (transform.localPosition.x) > limit) {
			transform.localPosition = new Vector3(
				Mathf.Clamp(transform.localPosition.x,
			            -limit,
			            limit),
				transform.localPosition.y,
				transform.localPosition.z);
			direction *= -1;

			if (!hasHitThisPass) {
				ConversationManager.instance.Miss();
			}

			hasHitThisPass = false;
			sliderImage.color = hasColor;
		}

		if (!hasHitThisPass && Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log(transform.localPosition.x + ", " + limit + ", " + bar.fill);
			if (Mathf.Abs(transform.localPosition.x) < limit * bar.fill) {
				ConversationManager.instance.Hit();
			} else {
				ConversationManager.instance.Miss();
			}
			hasHitThisPass = true;
			sliderImage.color = doesntColor;
		}

		moveSpeed += acceleration * Time.deltaTime;
	}
		
}
