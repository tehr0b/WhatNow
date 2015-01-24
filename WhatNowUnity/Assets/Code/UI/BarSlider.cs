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

	float limit { 
		get {
			return barImage.rectTransform.rect.width/2;
		}
	}

	public float moveSpeed;
	public int direction = 1;
	
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
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log(transform.localPosition.x + ", " + limit + ", " + bar.fill);
			if (Mathf.Abs(transform.localPosition.x) < limit * bar.fill) {
				Debug.Log("Hit!");
			} else {
				Debug.Log("Miss!");
			}
		}
	}
}
