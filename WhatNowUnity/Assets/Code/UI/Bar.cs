using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bar : MonoBehaviour {
	
	[SerializeField]
	Image underBar;

	[SerializeField]
	Image overBarRight;

	[SerializeField]
	Image overBarLeft;

	[SerializeField]
	Text label;

	[SerializeField]
	float labelFlashTime = .5f;
	
	[SerializeField]
	Color flashHitColor = Color.green;
	
	[SerializeField]
	Color flashMissColor = Color.red;

	[SerializeField]
	AnimationCurve flashCurve;

	float drain {
		get {
			return drainFromDifficulty + drainRate + ConversationManager.instance.hitsThisTopic * drainPerHit;
		}
	}

	float drainRate;

	[SerializeField]
	float normalDrainRate = .2f;

	[SerializeField]
	float exTopicDrainRate = .3f;

	[SerializeField]
	float drainPerHit = .01f;

	[SerializeField]
	float lerpRate = 1;

	[SerializeField]
	float extraDrainPerDifficulty = .01f;

	float drainFromDifficulty {
		get {
			return extraDrainPerDifficulty * PersistentDataManager.instance.dateDifficulty;
		}
	}

	[SerializeField]
	float target;

	[SerializeField]
	bool setTarget = false;

	public bool exTopic{
		set{
			if (value) drainRate = exTopicDrainRate;
			else drainRate = normalDrainRate;
		}
	}

	float _fill = .7f;
	public float fill {
		set {
			_fill = Mathf.Min(value, 1);
			overBarRight.rectTransform.sizeDelta = new Vector2(
				_fill * underBar.rectTransform.sizeDelta.x/2,
				underBar.rectTransform.sizeDelta.y);
			overBarLeft.rectTransform.sizeDelta = new Vector2(
				_fill * underBar.rectTransform.sizeDelta.x/2,
				underBar.rectTransform.sizeDelta.y);
		}
		get {
			return _fill;
		}
	}

	void Update() {
		if (ConversationManager.instance.isConversationRunning) {
			if (setTarget) {
				if (fill < target) {
					fill = Mathf.Min (target, fill + lerpRate * Time.deltaTime);
				} else {
					fill = Mathf.Max (target, fill - lerpRate * Time.deltaTime);
				}
				if (fill == target)
					setTarget = false;
			} else { //If not targetted, drain
				fill = Mathf.Max (0, fill - drain * Time.deltaTime);
			}
		}
	}

	[ContextMenu("Reset to full")]
	public void ResetToFull(){
		SetTarget(1);
	}

	public void SetTarget(float val){
		target = val;
		setTarget = true;
	}

	public void SetRelativeTarget(float val) {
		target = Mathf.Min(val + fill, 1);
		setTarget = true;
	}

	public Coroutine RunFlashHit() {
		return RunFlashText ("Hit!", flashHitColor);
	}

	public Coroutine RunFlashMiss() {
		return RunFlashText ("Miss!", flashMissColor);
	}

	public Coroutine RunFlashText(string text, Color color) {
		return StartCoroutine (FlashText (text, color, labelFlashTime));
	}

	IEnumerator FlashText(string text, Color color, float time) {
		label.text = text;
		label.color = new Color (color.r, color.g, color.b, 1);
		float currTime = 0;
		while (currTime < time) {
			yield return null;
			currTime += Time.deltaTime;
			label.color = new Color(color.r, color.g, color.b, flashCurve.Evaluate(currTime/time));
		}
	}

}
