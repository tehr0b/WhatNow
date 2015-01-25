using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip[] allMusics;

	static int lastMusicIndex = -1;

	void Start() {
		StartCoroutine (WaitForConversationStart ());
	}

	IEnumerator WaitForConversationStart() {
		while (!ConversationManager.instance.hasConversationStarted) {
			yield return null;
		}
		StartRandomMusic ();
	}

	void StartRandomMusic () {
		int musicIndex;
		do {
			musicIndex = Random.Range(0, allMusics.Length);
		} while (musicIndex == lastMusicIndex);

		lastMusicIndex = musicIndex;
		audioSource.clip = allMusics [musicIndex];
		audioSource.Play();
	}
}
