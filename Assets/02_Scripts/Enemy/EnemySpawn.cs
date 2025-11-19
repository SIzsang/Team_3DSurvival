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
	public List<GameObject> addMon = new List<GameObject>();

	private void Start()
	{
		if(GameManager.Instance != null)
		{
			GameManager.Instance.OnNightStart += SpawnMon;
		}
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
		StartCoroutine(SpawnStart());
	}

	public void Spawn()
	{
		int o = Random.Range(0, SpawnPoint.Length);
		GameObject spawnMon = Instantiate(SpawnMonster, SpawnPoint[o]);
		addMon.Add(SpawnMonster);
	}

	IEnumerator SpawnStart()
	{
		for (int i = 0; i < 5; i++)
		{
			yield return new WaitForSeconds(3f);
			Spawn();
		}
	}

	public void DestroyaddMon()
	{
		for( int i = 0; i < addMon.Count; i++)
		{
			Destroy(addMon[i].gameObject);
		}
	}

}
