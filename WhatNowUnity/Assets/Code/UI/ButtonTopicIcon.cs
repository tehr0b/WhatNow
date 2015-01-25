using UnityEngine;
using System.Collections;

public class ButtonTopicIcon : MonoBehaviour {

	public TopicIcon topicIcon;

	public void Click(){
		if (!topicIcon.animating) {
			topicIcon = ConversationManager.instance.ChangeTopic (this);
		}
	}


}
