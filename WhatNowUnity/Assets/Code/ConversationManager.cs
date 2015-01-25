using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConversationManager : MonoBehaviour {

	public static ConversationManager instance;
	private TopicManager topicManager;
	private TopicName currentTopic;

	private TopicList coveredTopics = new TopicList ();

	[SerializeField]
	TopicIcon[] topicOptions;

	[SerializeField]
	TopicIcon currentTopicIcon;

	float currentInterest = 0;

	[SerializeField]
	float maxInterest = 100;

	[SerializeField]
	float minInterest = -100;

	Person yourDate;
	List<Person> yourExes;

	void Awake() {
		instance = this;
	}

	void OnLevelWasLoaded() {
		topicManager = new TopicManager ();
		StartConversation();
	}

	/// <summary>
	/// STUB: Sets up the converation, initializes your date, and
	/// gives you your initial topic
	/// </summary>
	void StartConversation(){
		currentTopic = topicManager.GetStartingTopic ();
		coveredTopics.list.Add (currentTopic);
	}

	public TopicList getRelatedTopics (TopicName topic) {
		TopicList relatedTopics = topicManager.GetRelatedTopics ();
		foreach (TopicName seenTopic in coveredTopics.list) {
			relatedTopics.list.RemoveAll(seenTopic);
		}
		for (i = 4 - relatedTopics.list.Count; i > 0; i--) {
			relatedTopics.list.Add(TopicName.NOTHING);
		}
		return relatedTopics;
	}

	/// <summary>
	/// STUB: Changes the topic to the topic of the topic icon of
	/// the specified ButtonTopicIcon
	/// </summary>
	/// <param name="buttonTopicIcon">Button topic icon.</param>
	public TopicIcon ChangeTopic(ButtonTopicIcon buttonTopicIcon){
		TopicIcon oldCurrent = currentTopicIcon;

		//TODO: Pick new topics based on new topic

		oldCurrent.RunChangeToNextTopic (oldCurrent.topic, buttonTopicIcon.topicIcon.transform.localPosition);
		
		buttonTopicIcon.topicIcon.RunBecomeMainTopic ();
		currentTopicIcon = buttonTopicIcon.topicIcon;

		for (int i = 0; i < topicOptions.Length; i++) {
			if (topicOptions[i] == buttonTopicIcon.topicIcon) {
				topicOptions[i] = oldCurrent;
			} else {
				//TODO: Change this to its next topic
				topicOptions[i].RunChangeToNextTopic(topicOptions[i].topic);
			}
		}

		return oldCurrent;
	}



}
