using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConversationManager : MonoBehaviour {

	public static ConversationManager instance;

	private TopicName currentTopic;

	private TopicList coveredTopics = new TopicList ();

	[SerializeField]
	Monster dateMonster;

	[SerializeField]
	TopicIcon[] topicOptions;

	[SerializeField]
	TopicIcon currentTopicIcon;

	[SerializeField]
	Bar bar;

	[SerializeField]
	float currentInterest = 0;

	[SerializeField]
	float maxInterest = 100;

	[SerializeField]
	float minInterest = -100;

	[SerializeField]
	float negativeInterestThreshold = -50;

	[SerializeField]
	float positiveInterestThreshold = 50;

	[SerializeField]
	float hitInterest = 10;

	[SerializeField]
	float topicChangeBonus = .1f;

	[SerializeField]
	float topicHatePenalty = -1;

	[SerializeField]
	float topicLoveBonus = 3;

	public int dateInterestsCount = 3;

	public int dateHateCount = 3;

	Person yourDate;
	List<Person> yourExes;

	public bool isConversationRunning = true;

	void Awake() {
		instance = this;
	}

	void Start() {
		StartConversation();
	}

	void OnLevelWasLoaded(int index) {
		StartConversation();
	}

	/// <summary>
	/// STUB: Sets up the converation, initializes your date, and
	/// gives you your initial topic
	/// </summary>
	void StartConversation(){
		dateMonster.GenerateNewMonster();
		dateMonster.SetPassiveState(MonsterMood.NEUTRAL);
		currentTopic = TopicManager.instance.GetStartingTopic ();
		coveredTopics.list.Add (currentTopic);

		currentTopicIcon.topic = currentTopic;

		TopicList otherTopics = getRelatedTopics (currentTopic);
		for (int i = 0; i < topicOptions.Length; i++) {
			topicOptions[i].topic = otherTopics.list[i];
		}
	}

	public TopicList getRelatedTopics (TopicName topic) {
		TopicList relatedTopics = TopicManager.instance.GetRelatedTopics (topic);
		foreach (TopicName seenTopic in coveredTopics.list) {
			relatedTopics.list.RemoveAll(x => x == seenTopic);
		}
		while (relatedTopics.list.Count < 4) {
			relatedTopics.list.Add(TopicName.NOTHING);
		}
		relatedTopics.Shuffle ();
		return relatedTopics;
	}

	/// <summary>
	/// STUB: Changes the topic to the topic of the topic icon of
	/// the specified ButtonTopicIcon
	/// </summary>
	/// <param name="buttonTopicIcon">Button topic icon.</param>
	public TopicIcon ChangeTopic(ButtonTopicIcon buttonTopicIcon){
		coveredTopics.list.Add (buttonTopicIcon.topicIcon.topic);

		TopicIcon oldCurrent = currentTopicIcon;

		TopicList newTopics = getRelatedTopics (buttonTopicIcon.topicIcon.topic);

		for (int i = 0; i < topicOptions.Length; i++) {
			if (topicOptions[i] == buttonTopicIcon.topicIcon) {
				topicOptions[i] = oldCurrent;
				oldCurrent.RunChangeToNextTopic (newTopics.list[i], buttonTopicIcon.topicIcon.transform.localPosition);
			} else {
				//TODO: Change this to its next topic
				topicOptions[i].RunChangeToNextTopic(newTopics.list[i]);
			}
		}

		buttonTopicIcon.topicIcon.RunBecomeMainTopic ();
		currentTopicIcon = buttonTopicIcon.topicIcon;
		currentTopic = currentTopicIcon.topic;

		//TODO: Make hating/loving the topic influence it
		bar.SetRelativeTarget (topicChangeBonus);

		return oldCurrent;
	}

	public void Hit() {
		ChangeInterest (bar.fill * hitInterest);
		bar.RunFlashHit();
	}

	public void Miss() {
		ChangeInterest (-hitInterest);
		bar.RunFlashMiss();
	}

	public void ChangeInterest(float interestChange){
		currentInterest += interestChange;

		if (interestChange < 0) {
			dateMonster.SetTempState(MonsterMood.NEGATIVE);
		} else if (interestChange > 0) {
			dateMonster.SetTempState(MonsterMood.HAPPY);
		}

		if (currentInterest < minInterest) {
			dateMonster.SetPassiveState(MonsterMood.OVERIT);
			isConversationRunning = false;
		} else if (currentInterest < negativeInterestThreshold) {
			dateMonster.SetPassiveState(MonsterMood.NEGATIVE);
		} else if (currentInterest > positiveInterestThreshold) {
			dateMonster.SetPassiveState(MonsterMood.HAPPY);
		} else {
			dateMonster.SetPassiveState(MonsterMood.NEUTRAL);
		}
	}
}
