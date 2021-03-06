using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicmodManager : MonoBehaviour
{
	public float maxHealth = 3f;
	public float health;
	public int currency = 0;

	private PlayerManager playerManager;

	//used to respawn enemy in the right place
	public Vector2 originalPos;

	void Start()
	{
		originalPos = transform.localPosition; //Resets enemy position on respawn
		playerManager = PlayerManager.instance;

		health = maxHealth;
	}

	public void TakeDamage(float damage) //Kills the enemy
	{
		health -= damage;
		if (health <= 0)
		{
			playerManager.ReceiveMoney(currency);
			this.gameObject.SetActive(false);
		}
	}
}
