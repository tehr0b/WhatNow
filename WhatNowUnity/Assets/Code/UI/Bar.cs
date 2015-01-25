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
	float drainRate = .2f;

	[SerializeField]
	float lerpRate = 1;

	[SerializeField]
	float target;

	[SerializeField]
	bool setTarget = false;

	float _fill = 1;
	public float fill {
		set {
			overBarRight.rectTransform.sizeDelta = new Vector2(
				value * underBar.rectTransform.sizeDelta.x/2,
				underBar.rectTransform.sizeDelta.y);
			overBarLeft.rectTransform.sizeDelta = new Vector2(
				value * underBar.rectTransform.sizeDelta.x/2,
				underBar.rectTransform.sizeDelta.y);
			_fill = value;
		}
		get {
			return _fill;
		}
	}

	void Update() {

		if (setTarget) {
			if (fill < target){
				fill = Mathf.Min(target, fill + lerpRate * Time.deltaTime);
			} else {
				fill = Mathf.Max(target, fill - lerpRate * Time.deltaTime);
			}
			if (fill == target) setTarget = false;
		} else { //If not targetted, drain
			fill = Mathf.Max (0, fill - drainRate * Time.deltaTime);
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
		target = val + fill;
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
			label.color = new Color(color.r, color.g, color.b, 1 - currTime/time);
		}
	}

}
