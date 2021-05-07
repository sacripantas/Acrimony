using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : MonoBehaviour
{
	public int damage = 10;
	public float duration = 2f;
	private PlayerManager playerManager;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(SelfDestruct());
        playerManager = PlayerManager.instance;
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
			Destroy(gameObject);
		}
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
}
