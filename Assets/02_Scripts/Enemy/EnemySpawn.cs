using _02_Scripts.Core.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject SpawnMonster;
	public Transform[] SpawnPoint;
	public bool isNight;

	private void Start()
	{
		GameManager.Instance.OnNightStart += SpawnMon;
	}

	private void Update()
	{
		if (Input.GetKeyDown("a"))
		{
			SpawnMon();
		}
	}


	[ContextMenu("Test")]
	public void SpawnMon()
	{
		for(int i = 0;  i < 5; i++)
		{
			Invoke("Spawn",5f);
		}
	}

	public void Spawn()
	{
		int o = Random.Range(0, SpawnPoint.Length);
		Instantiate(SpawnMonster, SpawnPoint[o]);
	}

	 
}
