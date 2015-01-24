using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopicManager : MonoBehaviour 
{
	public int maxLinksPerTopic;

	public Dictionary<TopicName, List<TopicName>> topicLinks;

}
