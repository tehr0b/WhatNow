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
	BarSlider slider;

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


	float hitInterest;

	[SerializeField]
	float baseHitInterest = 10;

	[SerializeField]
	float dateLovesHitInterest = 20;

	[SerializeField]
	float dateHatesHitInterest = -5;

	float hitBonus = .05f;

	[SerializeField]
	float baseHitBonus = .05f;

	[SerializeField]
	float loveHitBonus = .1f;

	[SerializeField]
	float hateHitPenalty = -.05f;


	[SerializeField]
	float topicChangeBonus = .1f;

	[SerializeField]
	float topicHatePenalty = -1;

	[SerializeField]
	float topicLoveBonus = 3;

	[SerializeField]
	Bubble positiveBubble;

	[SerializeField]
	Bubble negativeBubble;

	[SerializeField]
	Bubble exBubble;

	public int hitsThisTopic = 0;

	int missesThisTopic = 0;

	public int dateInterestsCount = 3;

	public int dateHateCount = 3;

	Person yourDate;
	List<Person> yourExes = new List<Person>();

	public bool hasConversationStarted = false;
	public bool hasConversationEnded = false;

	public bool isConversationRunning {
		get {
			return hasConversationStarted && !hasConversationEnded;
		}
	}

	void Awake() {
		instance = this;
	}

	void Start() {
		yourExes.Add (new Person ());
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
		yourDate = new Person();
		dateMonster.GenerateNewMonster();
		dateMonster.SetPassiveState(MonsterMood.NEUTRAL);

		currentTopic = TopicName.NOTHING;
		//coveredTopics.list.Add (currentTopic);

		currentTopicIcon.Hide ();

		TopicList startingTopics = TopicManager.instance.GetStartingTopics ();
		for (int i = 0; i < startingTopics.list.Count; i++) {
			topicOptions[i].topic = startingTopics.list[i];
		}

		//currentTopicIcon.topic = currentTopic;

		//TopicList otherTopics = GetRelatedTopics (currentTopic);
		//for (int i = 0; i < topicOptions.Length; i++) {
	//		topicOptions[i].topic = otherTopics.list[i];
	//	}
	}

	public TopicList GetRelatedTopics (TopicName topic) {
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
		hasConversationStarted = true;

		hitsThisTopic = 0;

		//Keep chaining misses if the player is stuck on their ex
		//Give them the quick death
		//The easy way out
		if (buttonTopicIcon.topicIcon.topic != currentTopic)
			missesThisTopic = 0;

		coveredTopics.list.Add (buttonTopicIcon.topicIcon.topic);

		TopicIcon oldCurrent = currentTopicIcon;

		TopicList newTopics = GetRelatedTopics (buttonTopicIcon.topicIcon.topic);

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

		if (AnyExLoved (currentTopic)) {
			exBubble.ShowTopic (currentTopic);
			bar.exTopic = true;
			slider.exTopic = true;
		} else {
			bar.exTopic = false;
			slider.exTopic = false;
		}

		if (yourDate.Hates (currentTopic)) {
			hitInterest = dateHatesHitInterest;
			bar.SetRelativeTarget(topicChangeBonus * topicHatePenalty);
			ChangeInterest(hitInterest);
			negativeBubble.ShowTopic(currentTopic);
			hitBonus = hateHitPenalty;
		} else if (yourDate.Loves (currentTopic)) {
			hitInterest = dateLovesHitInterest;
			bar.SetRelativeTarget(topicChangeBonus * topicLoveBonus);
			ChangeInterest(hitInterest);
			positiveBubble.ShowTopic(currentTopic);
			hitBonus = loveHitBonus;
		} else {
			hitInterest = baseHitInterest;
			bar.SetRelativeTarget (topicChangeBonus);
			hitBonus = baseHitBonus;
		}

		return oldCurrent;
	}

	public void Hit() {
		hitsThisTopic++;
		ChangeInterest (bar.fill * hitInterest);
		bar.SetRelativeTarget(hitBonus);
		bar.RunFlashHit();
	}

	public void Miss() {
		ChangeInterest (-Mathf.Abs(hitInterest) * ++missesThisTopic);
		bar.SetRelativeTarget (hateHitPenalty);
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
			dateMonster.SetTempState(MonsterMood.OVERIT);
			dateMonster.SetPassiveState(MonsterMood.OVERIT);
			hasConversationEnded = true;
		} else if (currentInterest < negativeInterestThreshold) {
			dateMonster.SetPassiveState(MonsterMood.NEGATIVE);
		} else if (currentInterest > positiveInterestThreshold) {
			dateMonster.SetPassiveState(MonsterMood.HAPPY);
		} else {
			dateMonster.SetPassiveState(MonsterMood.NEUTRAL);
		}
	}

	bool AnyExLoved(TopicName topic) {
		foreach (var ex in yourExes) {
			if (ex.Loves(topic)) return true;
		}
		return false;
	}
}
