using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
	public float speed;
	public bool right;
	public float distanceWalked;
	public float maxDistance = 10;
	private SpriteRenderer spriteRenderer;
	private StatusEffectManager effectManager;
	public bool canFreeze;
	public bool canGround;
	public int groundDuration = 5;
	public int freezeDuration = 5;


	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
		effectManager = StatusEffectManager.instance;
	}

    // Update is called once per frame
    void Update()
    {
		if (right && Time.timeScale == 1)
		{
			transform.Translate(2 * Time.deltaTime * speed, 0, 0);
			//transform.localScale = new Vector2(1, 1);
			distanceWalked++;
			StartCoroutine(Turn());
			spriteRenderer.flipX = true;

		}
		else if(!right && Time.timeScale == 1)
		{
			transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
			//transform.localScale = new Vector2(-1, 1);
			distanceWalked++;
			StartCoroutine(Turn());
			spriteRenderer.flipX = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (canFreeze)
			{
				effectManager.Freezing(freezeDuration);
			}
			else if (canGround)
			{
				effectManager.Grounded(groundDuration);
			}
		}
	}


	IEnumerator Turn()
	{
		if(distanceWalked >= maxDistance)
		{
			right = !right;
			distanceWalked = 0;
		}
		yield return null;
	}
}
