using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopicIcon : MonoBehaviour {

	const float transitionTime = .2f;
	
	[SerializeField]
	Image topicImage;

	bool _animating = false;
	public bool animating { get { return _animating; } }

	float alpha {
		set {
			topicImage.color = new Color(
				topicImage.color.r,
				topicImage.color.g,
				topicImage.color.b,
				value);
		}
	}

	[SerializeField] TopicName _topic;
	public TopicName topic {
		get { return _topic; }
		set {
			_topic = value;
			topicImage.sprite = TopicManager.instance.SpriteForTopic (value);
		}
	}

	public Coroutine RunChangeToNextTopic(TopicName nextTopic){
		return StartCoroutine (ChangeToNextTopic (nextTopic, transform.localPosition));
	}

	public Coroutine RunChangeToNextTopic(TopicName nextTopic, Vector3 nextPos) {
		return StartCoroutine (ChangeToNextTopic (nextTopic, nextPos));
	}

	IEnumerator ChangeToNextTopic(TopicName nextTopic, Vector3 nextPos) {
		_animating = true;
		float currTime = 0;
		while (currTime < transitionTime) {
			currTime += Time.deltaTime;
			alpha = 1 - currTime/transitionTime;
			yield return null;
		}
		transform.localPosition = nextPos;
		topic = nextTopic;
		currTime = 0;
		while (currTime < transitionTime) {
			currTime += Time.deltaTime;
			alpha = currTime/transitionTime;
			yield return null;
		}
		_animating = false;
	}

	public Coroutine RunBecomeMainTopic() {
		return StartCoroutine (BecomeMainTopic ());
	}

	IEnumerator BecomeMainTopic() {
		_animating = true;
		float currTime = 0;
	 	Vector3 origPos = transform.localPosition;
		while (currTime < transitionTime) {
			currTime += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(origPos, Vector3.zero, currTime/transitionTime);
			yield return null;
		}
		_animating = false;
	}

	public void Hide() {
		alpha = 0;
	}
}
