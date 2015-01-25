using UnityEngine;
using System.Collections;

public class ButtonTopicIcon : MonoBehaviour {

	public TopicIcon topicIcon;

	void Click(){
		ConversationManager.instance.ChangeTopic (this);
	}
}
