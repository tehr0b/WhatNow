using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Person {

	List<TopicName> topicsLiked = new List<TopicName>();
	List<TopicName> topicsHated = new List<TopicName>();

	List<TopicName> knownTopicsLiked = new List<TopicName>();

	public Person(int interests, int hates){
		TopicName topic;
		while (topicsLiked.Count < interests) {
			topic = TopicManager.randomTopic;
			if (!topicsLiked.Contains(topic)) {
				topicsLiked.Add(topic);
			}
		}
		
		while (topicsHated.Count < hates) {
			topic = TopicManager.randomTopic;
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

	public bool Loves(TopicName topic) {
		if (topicsLiked.Contains(topic)) {
			if (!knownTopicsLiked.Contains(topic))
				knownTopicsLiked.Add (topic);
			return true;
		}
		return false; 
	}
	public bool Hates(TopicName topic) { 
		return topic == TopicName.NOTHING || topicsHated.Contains(topic); 
	}

	public void BecomeBadMemories() {
		topicsLiked = knownTopicsLiked;
	}
}
