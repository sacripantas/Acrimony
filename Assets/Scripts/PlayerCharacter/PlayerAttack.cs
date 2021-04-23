using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	//General
	public LayerMask whatIsEnemies;
	public CharacterController character;
	public GameObject attackArea;

	//Swipe
	private float timeBtwAttack;
	public float startTimeBtwAttack;

	public Transform attackPos;
	public float attackRangeX;
	public float attackRangeY;
	public float damage;

	//Lunge
	private float timeBtwLungeAttack;
	public float startTimeBtwLungeAttack;

	public float attackLungeRangeX;
	public float attackLungeRangeY;
	public float damageLunge;

	// Update is called once per frame
	void Update()
    {
		SwipeAttack();
		LungeAttack();
    }

	public void SwipeAttack()
	{
		if (timeBtwAttack <= 0)
		{
			if (Input.GetButtonDown("SwipeAttack"))
			{

				attackPos.transform.localScale = new Vector3(5, 5, 1);
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, whatIsEnemies);

				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
					enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damage);
				}

				attackArea.SetActive(true);

				if (character.direction == 1)
				{
					attackPos.transform.localPosition = new Vector3(0.5f, 0.1f, 0);
				}
				else
				{
					attackPos.transform.localPosition = new Vector3(-0.5f, 0.1f, 0);
				}

				timeBtwAttack = startTimeBtwAttack;
			}			
		}
		else
		{
			timeBtwAttack -= Time.deltaTime;
			attackArea.SetActive(false);
		}
	}

	public void LungeAttack()
	{
		if (timeBtwLungeAttack <= 0)
		{
			if (Input.GetButtonDown("LungeAttack"))
			{

				attackPos.transform.localScale = new Vector3(7, 3, 1);
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackLungeRangeX, attackLungeRangeY), 0, whatIsEnemies);

				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
					enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damageLunge);
				}

				attackArea.SetActive(true);

				if (character.direction == 1)
				{
					attackPos.transform.localPosition = new Vector3(0.7f, 0.1f, 0);

				}
				else
				{
					attackPos.transform.localPosition = new Vector3(-0.7f, 0.1f, 0);
				}

				timeBtwLungeAttack = startTimeBtwLungeAttack;
			}
		}
		else
		{
			timeBtwLungeAttack -= Time.deltaTime;
			attackArea.SetActive(false);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 0f));

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(attackPos.position, new Vector3(attackLungeRangeX, attackLungeRangeY, 0f));
	}
}
