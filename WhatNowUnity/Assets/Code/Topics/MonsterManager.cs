using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour 
{
	
	public static MonsterManager instance;

	static MonsterSprites lastMonster;

	public List<MonsterSprites> monsterSprites = new List<MonsterSprites>();

	public float colorMinR = .3f;
	public float colorMaxR = .9f;
	public float colorMinG = .3f;
	public float colorMaxG = .9f;
	public float colorMinB = .3f;
	public float colorMaxB = .9f;

	public void Awake()
	{
		instance = this;
	}

	public Color GetColor() {
		return new Color (
			UnityEngine.Random.Range (colorMinR, colorMaxR),
			UnityEngine.Random.Range (colorMinG, colorMaxG),
			UnityEngine.Random.Range (colorMinB, colorMaxB));
	}

	public MonsterSprites GetMonster()
	{
		lastMonster = monsterSprites [Random.Range (0, monsterSprites.Count)];
		lastMonster.monsterColor = GetColor ();
		return lastMonster;
	}
	public MonsterSprites GetLastMonster()
	{
		if (lastMonster.boredFace != null)
			return lastMonster;
		Debug.LogError ("Was no last monster, generating a rando monster...");
		return GetMonster ();
	}

}
