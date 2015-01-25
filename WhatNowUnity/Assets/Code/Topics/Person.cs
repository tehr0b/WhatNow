using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Person {

	List<TopicName> topicsLiked = new List<TopicName>();
	List<TopicName> topicsHated = new List<TopicName>();

	public Person(){
		TopicName topic;
		while (topicsLiked.Count < ConversationManager.instance.dateInterestsCount) {
			topic = TopicManager.instance.randomTopic;
			if (!topicsLiked.Contains(topic)) {
				topicsLiked.Add(topic);
			}
		}
		
		while (topicsHated.Count < ConversationManager.instance.dateInterestsCount) {
			topic = TopicManager.instance.randomTopic;
			if (!topicsLiked.Contains(topic) && !topicsHated.Contains(topic)) {
				topicsHated.Add(topic);
			}
		}
	}

	public void TrimLikes(TopicList topicsSeen) {
		for (int i = topicsLiked.Count-1; i >= 0; i--) {
			if (!topicsSeen.list.Contains(topicsLiked[i])){
				topicsLiked.RemoveAt(i);
			}
		}
	}

	public bool Loves(TopicName topic) { return topicsLiked.Contains(topic); }
	public bool Hates(TopicName topic) { 
		return topic == TopicName.NOTHING || topicsHated.Contains(topic); 
	}
}
