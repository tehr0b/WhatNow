using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utils {

	public static List<T> GetN<T>(List<T> list, int n){
		List<T> newList = new List<T> ();
		for (int i = 0; i < list.Count; i++) {
			if (Random.value < ((float)n-newList.Count)/(list.Count-i)){
				newList.Add(list[i]);
			}
		}
		return newList;
	}
}

[System.Serializable]
public class TopicList {
	public List<TopicName> list = new List<TopicName>();
}
