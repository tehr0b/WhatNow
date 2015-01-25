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

	float moveAmount {
		get {
			return moveSpeed * 2 * limit * direction * Time.deltaTime;
		}
	}

	float moveSpeed{
		get{
			return currSpeed + accelBonus;
		}
	}

	float currSpeed;

	[SerializeField] float normalMoveSpeed = .1f;

	[SerializeField] float exTopicMoveSpeed = .15f;

	float accelBonus = 0;

	[SerializeField] float acceleration = .01f;

	public int direction = 1;

	bool hasHitThisPass = false;

	public bool exTopic {
		set {
			if (value) currSpeed = exTopicMoveSpeed;
			else currSpeed = normalMoveSpeed;
		}
	}

	void Start() {
		transform.localPosition = new Vector3 (-limit, transform.localPosition.y, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (ConversationManager.instance.isConversationRunning) {
			transform.Translate (moveAmount, 0, 0);
			if (Mathf.Abs (transform.localPosition.x) > limit) {
				transform.localPosition = new Vector3 (
					Mathf.Clamp (transform.localPosition.x,
	    	        -limit,
	        	    limit),
				transform.localPosition.y,
				transform.localPosition.z);
				direction *= -1;

				if (!hasHitThisPass) {
					ConversationManager.instance.Miss ();
				}

				hasHitThisPass = false;
				sliderImage.color = hasColor;
			}

			if (!hasHitThisPass && Input.GetKeyDown (KeyCode.Space)) {
				///Debug.Log (transform.localPosition.x + ", " + limit + ", " + bar.fill);
				if (Mathf.Abs (transform.localPosition.x) < limit * bar.fill) {
					ConversationManager.instance.Hit ();
				} else {
					ConversationManager.instance.Miss ();
				}
				hasHitThisPass = true;
				sliderImage.color = doesntColor;
			}

			accelBonus += acceleration * Time.deltaTime;
		}
	}
		
}
