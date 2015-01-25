using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour 
{
	
	public static MonsterManager instance;

	public List<MonsterSprites> monsterSprites = new List<MonsterSprites>();

	public void Awake()
	{
		instance = this;
	}
	public MonsterSprites GetMonster()
	{
		return monsterSprites[Random.Range(0,monsterSprites.Count)];
	}
}
