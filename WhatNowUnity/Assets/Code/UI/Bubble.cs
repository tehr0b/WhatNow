using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bubble : MonoBehaviour {

	[SerializeField]
	Image[] images;

	[SerializeField]
	TopicIcon icon;

	[SerializeField]
	float showTime = 1f;

	[SerializeField]
	float fadeTime = .3f;

	IEnumerator currFade = null;

	float _alpha;
	float alpha {
		set {
			_alpha = value;
			foreach (var item in images) {
				item.color = new Color(
					item.color.r,
					item.color.g,
					item.color.b,
					value);
			}
		}
		get {
			return _alpha;
		}
	}

	public void Start(){
		alpha = 0;
	}

	public void ShowTopic(TopicName topic) {
		icon.topic = topic;

		if (currFade != null)
			StopCoroutine (currFade);	

		currFade = FadeInOutInOutInOutInOutInOut ();
		StartCoroutine (currFade);
	}

	IEnumerator FadeInOutInOutInOutInOutInOut() {
		float currTime = 0;
		while (currTime < fadeTime){
			currTime += Time.deltaTime;
			alpha = currTime/fadeTime;
			yield return null;
		}
		yield return new WaitForSeconds(showTime);
		currTime = 0;
		while (currTime < fadeTime){
			currTime += Time.deltaTime;
			alpha = 1-currTime/fadeTime;
			yield return null;
		}
		currFade = null;
	}
}
