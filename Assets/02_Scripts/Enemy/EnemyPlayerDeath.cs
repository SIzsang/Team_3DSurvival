using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDeath : MonoBehaviour
{
	List<Enemy> enemies = new List<Enemy>(); 
    
 
    void Start()
    {
        
    }


    void Update()
    {
        Invoke("Attack",6f);
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("닿았다!");
			Enemy i = collision.gameObject.GetComponent<Enemy>();
			enemies.Add(i);
		}

	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("닿았다!");
			Enemy i = collision.gameObject.GetComponent<Enemy>();
			enemies.Remove(i);
		}
	}

	void Attack()
	{
		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].TakePhysicalDamage(5);
		}
	
		
	}

}
