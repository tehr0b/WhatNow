using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopicManager : MonoBehaviour 
{
	public static TopicManager instance;

	public int maxLinksPerTopic;

	public Dictionary<TopicName, List<TopicName>> topicLinks = new Dictionary<TopicName, List<TopicName>>();

	public Dictionary<TopicName, Sprite> topicSprites = new Dictionary<TopicName, Sprite>();

	void Awake(){
		instance = this;
	}
	
	public Sprite SpriteForTopic(TopicName topic){
		return topicSprites [topic];
	}
}
