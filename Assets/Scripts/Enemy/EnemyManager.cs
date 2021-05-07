using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
	//General
	public float health = 3;
	public float speed = 3f;
	public int contactDamage = 25;
	public int currency = 0;

	private PlayerManager playerManager;

	//used to respawn enemy in the right place
	public Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
		originalPos = transform.localPosition;
		playerManager = PlayerManager.instance;
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

	IEnumerator Death()
	{
		yield return new WaitForSeconds(1.2f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			DealDamage();
		}
	}

	void DealDamage()
	{
		playerManager.TakeDamage(contactDamage);
	}
}
