using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : EnemyManager
{

	public int damage = 10;
	public float duration = 2f;
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
		if(collision.gameObject.tag == "Player")
		{
			DealDamage();
			Destroy(gameObject);
		}
	}

	void DealDamage()
	{
		playerManager.TakeDamage(damage);
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
}
