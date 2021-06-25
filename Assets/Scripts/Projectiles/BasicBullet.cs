using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
	public int damage = 10;
	public float duration = 5f;
	public EnemyManager enemyManager;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SelfDestruct());
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.GetComponent<EnemyManager>().TakeDamage(damage);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Ricmod")
		{
			collision.gameObject.GetComponent<RicmodManager>().TakeDamage(damage);
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "GunWall")
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}


	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
}
