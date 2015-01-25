using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConversationManager : MonoBehaviour {

	public static ConversationManager instance;

	[SerializeField]
	TopicIcon[] topicOptions;

	[SerializeField]
	TopicIcon currentTopic;

	[SerializeField]
	Bar bar;

	float currentInterest = 0;

	[SerializeField]
	float maxInterest = 500;

	[SerializeField]
	float minInterest = -100;

	[SerializeField]
	float hitInterest = 10;

	[SerializeField]
	float topicChangeBonus = .1f;

	[SerializeField]
	float topicHatePenalty = -1;

	[SerializeField]
	float topicLoveBonus = 3;

	Person yourDate;
	List<Person> yourExes;

	void Awake() {
		instance = this;
	}

	void OnLevelLoaded() {
		StartConversation();
	}

	/// <summary>
	/// STUB: Sets up the converation, initializes your date, and
	/// gives you your initial 
	/// </summary>
	void StartConversation(){

	}

	/// <summary>
	/// STUB: Changes the topic to the topic of the topic icon of
	/// the specified ButtonTopicIcon
	/// </summary>
	/// <param name="buttonTopicIcon">Button topic icon.</param>
	public TopicIcon ChangeTopic(ButtonTopicIcon buttonTopicIcon){
		TopicIcon oldCurrent = currentTopic;

		//TODO: Pick new topics based on new topic

		oldCurrent.RunChangeToNextTopic (oldCurrent.topic, buttonTopicIcon.topicIcon.transform.localPosition);
		
		buttonTopicIcon.topicIcon.RunBecomeMainTopic ();
		currentTopic = buttonTopicIcon.topicIcon;

		for (int i = 0; i < topicOptions.Length; i++) {
			if (topicOptions[i] == buttonTopicIcon.topicIcon) {
				topicOptions[i] = oldCurrent;
			} else {
				//TODO: Change this to its next topic
				topicOptions[i].RunChangeToNextTopic(topicOptions[i].topic);
			}
		}

		//TODO: Make hating/loving the topic influence it
		bar.SetRelativeTarget (topicChangeBonus);

		return oldCurrent;
	}

	public void Hit() {
		//TODO: Make bad topics give less interest?
		currentInterest += bar.fill * hitInterest;
		bar.RunFlashHit();
	}

	public void Miss() {
		currentInterest -= hitInterest;
		bar.RunFlashMiss();
	}
}
