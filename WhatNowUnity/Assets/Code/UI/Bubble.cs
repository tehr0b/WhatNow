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

	public void PermaShow() {
		gameObject.SetActive (true);
		StartCoroutine (FadeIn ());
	}

	public void Show() {
		if (currFade != null)
			StopCoroutine (currFade);	
		
		currFade = FadeInFadeOut ();
		StartCoroutine (currFade);
	}

	public void ShowTopic(TopicName topic) {
		icon.topic = topic;

		Show();
	}

	IEnumerator FadeInFadeOut() {
		yield return StartCoroutine (FadeIn ());
		yield return new WaitForSeconds(showTime);
		yield return StartCoroutine (FadeOut ());

	}

	IEnumerator FadeIn() {
		float currTime = 0;
		while (currTime < fadeTime){
			currTime += Time.deltaTime;
			alpha = currTime/fadeTime;
			yield return null;
		}
	}

	IEnumerator FadeOut() {
		float currTime = 0;
		while (currTime < fadeTime){
			currTime += Time.deltaTime;
			alpha = 1-currTime/fadeTime;
			yield return null;
		}
		currFade = null;
	}
}
