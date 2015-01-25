using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentDataManager : MonoBehaviour {

	public static PersistentDataManager instance;

	public Person currentDate;

	public List<Person> yourExes = new List<Person>();

	public bool lastDateSuccess;

	public int dateDifficulty;

	void Awake() {
		if (instance != null) {
			Debug.Log("Instance exists, destroying self");
			Destroy(gameObject);
			return;
		}
		
		instance = this;
		DontDestroyOnLoad (gameObject);
	}
}
