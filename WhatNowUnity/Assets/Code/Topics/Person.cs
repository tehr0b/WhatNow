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
				topicsLiked.Add(topic);
			}
		}
	}
}
