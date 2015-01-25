using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopicManager : MonoBehaviour 
{
	public static TopicManager instance;

	public Sprite yourExSprite;

	public int maxLinksPerTopic;

	public List<TopicList> topicLinks = new List<TopicList>();

	//public Dictionary<TopicName, List<TopicName>> topicLinks = new Dictionary<TopicName, List<TopicName>>();

	//public Dictionary<TopicName, Sprite> topicSprites = new Dictionary<TopicName, Sprite>();

	public List<Sprite> topicSprites = new List<Sprite>();

	void Awake(){
		instance = this;
	}
	
	public Sprite SpriteForTopic(TopicName topic){
		if (topic == TopicName.NOTHING)
			return yourExSprite;
		return topicSprites [(int)topic];
	}

	public TopicList GetRelatedTopics (TopicName topic) {
		TopicList relatedTopics = topicLinks[(int)topic];
		for (int i=(relatedTopics.list.Count - 4); i > 0; i--) {
			relatedTopics.list.Add(TopicName.NOTHING);
		}
		return relatedTopics;
	}

	public TopicName GetStartingTopic() {
		return (TopicName) UnityEngine.Random.Range(1, (int)TopicName.MAX);
	}
}
