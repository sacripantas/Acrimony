using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRefresh : MonoBehaviour
{
	private CharacterController characterController;

	private bool active;
	public float enableTimer;

	private ParticleSystem particle;

	private void Awake()
	{
		active = true;
		particle = transform.GetComponentInChildren<ParticleSystem>();
		characterController = CharacterController.instance;
	}

	private void Update()
	{
		if(active == false)
		{
			enableTimer -= Time.deltaTime;
			if (enableTimer < 0)
			{
				active = true;
				particle.Play();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(active == true && collision.gameObject.tag == "Player")
		{
			characterController.JumpRefresh();
			active = false;
			enableTimer = 2f;
			particle.Clear();
			particle.Pause();
			Debug.Log("Player collided with refresh orb");
		}
	}
}
