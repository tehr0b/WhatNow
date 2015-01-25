using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor (typeof(TopicManager))]
public class TopicManagerEditor : Editor {

	void CheckLinkDictionary(TopicManager manager){
		if (manager.topicLinks == null) {
			manager.topicLinks = new List<TopicList>();
		}

		while (manager.topicLinks.Count < (int)TopicName.MAX) {
			manager.topicLinks.Add(new TopicList());
		}

		while (manager.topicSprites.Count < (int)TopicName.MAX) {
			manager.topicSprites.Add (null);
		}
	}

	public override void OnInspectorGUI (){
		
		base.OnInspectorGUI ();

		TopicManager manager = (TopicManager)target;

		CheckLinkDictionary (manager);

		EditorGUILayout.BeginVertical ();

		EditorGUILayout.LabelField ("Links");

		TopicName topic;
		for (int i = 1; i < (int)TopicName.MAX; i++) {
			EditorGUILayout.BeginHorizontal();

			topic = (TopicName)i;

			EditorGUILayout.LabelField(topic.ToString(), GUILayout.Width(100));

			manager.topicSprites[i] = EditorGUILayout.ObjectField(
				manager.topicSprites[i], typeof(Sprite), false) as Sprite;

			while(manager.topicLinks[i].list.Count > manager.maxLinksPerTopic)
				manager.topicLinks[i].list.RemoveAt(manager.topicLinks[i].list.Count-1);
			while(manager.topicLinks[i].list.Count < manager.maxLinksPerTopic)
				manager.topicLinks[i].list.Add(TopicName.NOTHING);

			for (int j = 0; j < manager.topicLinks[i].list.Count; j++) {
				manager.topicLinks[i].list[j] = (TopicName)EditorGUILayout.EnumPopup(manager.topicLinks[i].list[j]);
			}

			EditorGUILayout.EndHorizontal();
		}


		EditorGUILayout.EndVertical ();
	}

}
