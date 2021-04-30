using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
	//General
	public float health = 3;
	public float speed = 3f;
	public int contactDamage = 25;

	public PlayerManager playerManager;

	//used to respawn enemy in the right place
	public Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
		originalPos = transform.localPosition;
    }

	// Update is called once per frame
	void Update()
    {
		
    }

	public void TakeDamage(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
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
