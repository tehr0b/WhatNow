using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor (typeof(TopicManager))]
public class TopicManagerEditor : Editor {

	void CheckLinkDictionary(TopicManager manager){
		if (manager.topicLinks == null) {
			manager.topicLinks = new Dictionary<TopicName, List<TopicName>>();
		}

		if (manager.topicLinks.Count < (int)TopicName.MAX-1) {
			for (int i = 1; i < (int)TopicName.MAX; i++) {
				if (!manager.topicLinks.ContainsKey((TopicName)i)){
					manager.topicLinks.Add((TopicName)i, new List<TopicName>());
				}
			}
		}
	}

	public override void OnInspectorGUI (){
		
		base.OnInspectorGUI ();

		TopicManager manager = (TopicManager)target;

		CheckLinkDictionary (manager);

		EditorGUILayout.BeginVertical ();

		EditorGUILayout.LabelField ("Links");

		foreach (var pair in manager.topicLinks) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(pair.Key.ToString());

			while(pair.Value.Count > manager.maxLinksPerTopic)
				pair.Value.RemoveAt(pair.Value.Count-1);
			while (pair.Value.Count < manager.maxLinksPerTopic)
				pair.Value.Add(TopicName.NOTHING);

			for (int i = 0; i < pair.Value.Count; i++) {
				pair.Value[i] = (TopicName)EditorGUILayout.EnumPopup(pair.Value[i]);
			}
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.EndVertical ();
	}

}
