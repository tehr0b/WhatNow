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
	float drainRate;

	[SerializeField]
	float lerpRate;

	[SerializeField]
	float target;

	[SerializeField]
	bool setTarget;

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

}
