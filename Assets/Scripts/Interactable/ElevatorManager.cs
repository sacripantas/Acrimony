using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
	public BoxCollider2D platform;
	public Transform goal;
	public Transform start;
	public float speed;
	public float resetTime;

	public bool isActive;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerBody")
		{
			isActive = true;
		}
	}


	private void Update()
	{
		if (isActive)
		{
			platform.transform.position = Vector2.MoveTowards(platform.transform.position, goal.position, speed * Time.deltaTime);

			if(Vector2.Distance(platform.transform.position, goal.position) < speed * Time.deltaTime)
			{
				StartCoroutine(ResetActive());
			}
		}
		else
		{
			platform.transform.position = Vector2.MoveTowards(platform.transform.position, start.position, speed * Time.deltaTime);
		}
	}

	IEnumerator ResetActive()
	{
		yield return new WaitForSeconds(resetTime);
		isActive = false;
	}

}
