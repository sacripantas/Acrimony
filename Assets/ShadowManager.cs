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

    // Start is called before the first frame update
    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
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
