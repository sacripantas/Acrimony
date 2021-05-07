using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private static GameManager manager;

	//General
	[Header("General")]
	public LayerMask whatIsEnemies;
	public CharacterController character;
	public GameObject attackArea;
	public Transform attackPos;

	//Swipe
	[Header("Swipe Attack")]
	public float damage;
	private float timeBtwAttack;
	public float startTimeBtwAttack;
	public float attackRangeX;
	public float attackRangeY;


	//Lunge
	[Header("Lunge Attack")]
	public float damageLunge;
	private float timeBtwLungeAttack;
	public float startTimeBtwLungeAttack;
	public float attackLungeRangeX;
	public float attackLungeRangeY;

    void Start()
	{
        manager = GameManager.instance;
    }
	// Update is called once per frame
	void Update()
    {
        if (manager.isPaused) return; //if the game is paused, dont attack
		SwipeAttack();
		LungeAttack();

		if (character.direction == 1)
		{
			attackPos.transform.localPosition = new Vector3(1.5f, 0, 0);
		}
		else
		{
			attackPos.transform.localPosition = new Vector3(-1.5f, 0.1f, 0);
		}
	}

	public void SwipeAttack()
	{
		if (timeBtwAttack <= 0)
		{
			if (Input.GetButtonDown("SwipeAttack"))
			{

				Debug.Log("Attack");

				//attackPos.transform.localScale = new Vector3(0, 5, 1);
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, whatIsEnemies);

				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
					enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damage);
				}

				attackArea.SetActive(true);

				

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

				//attackPos.transform.localScale = new Vector3(7, 3, 1);
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackLungeRangeX, attackLungeRangeY), 0, whatIsEnemies);

				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
					enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damageLunge);
				}

				attackArea.SetActive(true);

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
