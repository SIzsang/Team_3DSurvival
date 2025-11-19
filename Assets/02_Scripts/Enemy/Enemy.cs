using _02_Scripts.Core.Managers;
using _02_Scripts.Quest;
using _02_Scripts.Quest.Context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public interface ICombatable
{
	void TakePhysicalDamage(int damage)
	{

	}

}
public enum AIState
{
	Idle,
	Wandering,
	Attacking,
	Death
}
public class Enemy : MonoBehaviour,ICombatable
{
	[Header("Stats")]
	
	public int health;
	
	public float walkSpeed;
	public float runSpeed;
	public ItemData dropItem;

	[Header("AI")]
	private NavMeshAgent agent;
	public float detectDistance;
	private AIState aiState;

	[Header("Wandering")]
	public float minWanderDistance;
	public float maxWanderDistance;
	public float minWanderWaitTime;
	public float maxWanderWaitTime;

	[Header("Combat")]
	public int damage;
	public float attackRate;
	private float lastAttackTime;
	public float attackDistance;
	private Coroutine myCoroutine;
	public bool isKnockback = false;

	[Header("Death")]
	bool rewarded = false;
	
	

	private float playerDistance;
	public Transform player;
	

	public float fieldOfView = 120f;

	public Animator animator;
	private SkinnedMeshRenderer[] meshRenderers; 

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		
	}

	void Start()
	{
		SetState(AIState.Wandering);
		player = GameManager.Instance.Player.transform;
	
	}

	// Update is called once per frame
	void Update()
	{
		playerDistance = Vector3.Distance(transform.position, player.transform.position);
		animator.SetBool("Moving", aiState != AIState.Idle);

		switch (aiState)
		{
			case AIState.Idle:
			case AIState.Wandering:
				PassiveUpdate();
				break;
			case AIState.Attacking:
				AttackingUpdate();
				break;
			case AIState.Death:
				
				break;
		}
	}

	public void SetState(AIState state)
	{
		aiState = state;

		switch (aiState)
		{
			case AIState.Idle:
				agent.speed = walkSpeed;
				agent.isStopped = true;
				break;
			case AIState.Wandering:
				agent.speed = walkSpeed;
				agent.isStopped = false;
				animator.speed = 2;
				break;
			case AIState.Attacking:
				agent.speed = runSpeed;
				agent.isStopped = false;
				break;
			case AIState.Death:
				agent.isStopped = true;
				
				break;
		}


		animator.speed = agent.speed / walkSpeed;
	}
	void PassiveUpdate()
	{
		if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
		{
			SetState(AIState.Idle);
			Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
		}
		if (playerDistance < detectDistance)
		{
			SetState(AIState.Attacking);
		}
	}
	void WanderToNewLocation()
	{
		if (aiState != AIState.Idle) return;
		SetState(AIState.Wandering);
		agent.SetDestination(GetWanderLocation());
	}
	Vector3 GetWanderLocation()
	{
		NavMeshHit hit;

		NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * 
			Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

		int i = 0;

		while (Vector3.Distance(transform.position, hit.position) < detectDistance)
		{
			NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * 
				Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
			i++;
			if (i == 30) break;
		}
		return hit.position;
	}

	void AttackingUpdate()
	{
		if (playerDistance < attackDistance && IsPlayerInFieldOfView())
		{
			agent.isStopped = true;
			if (Time.time - lastAttackTime > attackRate)
			{
				lastAttackTime = Time.time;
				player.GetComponent<ICombatable>().TakePhysicalDamage(damage);
				animator.speed = 1;
				animator.SetTrigger("Attack");
			}
		}
		else
		{
			if (playerDistance < detectDistance)
			{
				agent.isStopped = false;
				NavMeshPath path = new NavMeshPath();
				if (agent.CalculatePath(player.position, path))
				{
					agent.SetDestination(player.position);
				}
				else
				{
					agent.SetDestination(transform.position);
					agent.isStopped = true;
					SetState(AIState.Wandering);
				}
			}
			else
			{
				agent.SetDestination(transform.position);
				agent.isStopped = true;
				SetState(AIState.Wandering);
			}
		}
	}

	bool IsPlayerInFieldOfView()
	{
		Vector3 directionToPlayer = player.position - gameObject.transform.position;
		float angle = Vector3.Angle(transform.forward, directionToPlayer);

		return angle < fieldOfView;
	}

	public void TakePhysicalDamage(int damage)  // 몬스터가 공격을 받았을때 쓰는 함수
	{
		if(health > 0)
		{
			health -= damage;

			StartCoroutine(DamageFlash());
			SetState(AIState.Wandering);
		}
		if(health <= 0 && rewarded == false )
		{
			SetState(AIState.Death);
			animator.speed = 1;
			animator.SetTrigger("Death");
			Instantiate(dropItem.dropPrefab, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.position, Vector3.up));
			StartCoroutine(Death());
			
			rewarded = true;

		}
		
	}
	/*IEnumerator Knockback()
	{
		
		if (isKnockback == true)
		{
			Quaternion rot = Quaternion.LookRotation(player.position - gameObject.transform.position);
			Vector3 rocation = gameObject.transform.position + (rot * (Vector3.back * 0.2f));
			while (gameObject.transform.position != rocation)
			{
				gameObject.transform.position += (rot * (Vector3.back * 0.2f)) * Time.deltaTime;
				yield return null;
			}
			myCoroutine = null;
			isKnockback = false;
		}
	}*/

	IEnumerator Death()
	{
		QuestManager.Instance.CheckQuestProgress(new QuestProcessContext(QuestType.Kill));
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}

	IEnumerator DamageFlash()
	{
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].material.color = new Color (1.0f,0.6f,0.6f);
		   
		}
		yield return new WaitForSeconds(0.1f);
		for(int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].material.color = Color.white;
		}
	}
}
