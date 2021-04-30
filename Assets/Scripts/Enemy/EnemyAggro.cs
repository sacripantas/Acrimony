using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
	public FlyingEnemyManager enemyManager;
	private Animator animator;

	private void Start()
	{
		animator = GetComponentInParent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			enemyManager.destinationSetter.enabled = true;
			enemyManager.aiPath.canSearch = true;

			animator.SetBool("isInAttackRange", true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			enemyManager.destinationSetter.enabled = false;
			enemyManager.aiPath.canSearch = false;

			animator.SetBool("isInAttackRange", false);
		}
	}
}
