using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairHit : MonoBehaviour
{
	private CircleCollider2D circleCollider2D;
	private PlayerManager playerManager;
	public int damage;

	// Start is called before the first frame update
	void Start()
    {
		circleCollider2D = GetComponent<CircleCollider2D>();
		playerManager = PlayerManager.instance;

		circleCollider2D.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			playerManager.TakeDamage(damage);
		}
	}
}
