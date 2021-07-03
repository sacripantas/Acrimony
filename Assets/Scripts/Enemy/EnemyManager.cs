using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
	//General
	public float maxHealth = 3f;
	public float health;
	public int contactDamage = 25;
	public int currency = 0;

	public bool canBurn;
	public bool canBleed;
	public bool canFreeze;
	public bool canGround;

	private PlayerManager playerManager;
	private StatusEffectManager statusEffect;

	//used to respawn enemy in the right place
	public Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
		originalPos = transform.localPosition; //Resets enemy position on respawn
		playerManager = PlayerManager.instance;
		statusEffect = StatusEffectManager.instance;

		health = maxHealth;
	}

	public void TakeDamage(float damage) //Kills the enemy
	{
		health -= damage;
		if(health <= 0)
		{
			playerManager.ReceiveMoney(currency);
			this.gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			DealDamage();
		}

		if (canBurn)
		{
			statusEffect.Burning(20, 5, 1);
		}
		else if (canBleed)
		{
			statusEffect.Bleeding(50, 25, 2);
		}
		else if (canFreeze)
		{
			statusEffect.Freezing(5);
		}
		else if (canGround)
		{
			statusEffect.Grounded(5);
		}
	}

	void DealDamage()
	{
		playerManager.TakeDamage(contactDamage);
	}
}
