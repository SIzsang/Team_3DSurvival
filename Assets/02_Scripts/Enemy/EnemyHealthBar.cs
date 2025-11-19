using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
	public GameObject Health;

	public Enemy Enemy;
	float MaxHealth;
	float CurrentHealth;

	private void Start()
	{
		MaxHealth = Enemy.health;
	}
	private void Update()
	{
		UpdateHealth();
	}
	void UpdateHealth()
	{
		CurrentHealth = Enemy.health;
		float healthScale = CurrentHealth / MaxHealth;
		Health.transform.localScale = new Vector3(healthScale, Health.transform.localScale.y, Health.transform.localScale.z);
	}
}
