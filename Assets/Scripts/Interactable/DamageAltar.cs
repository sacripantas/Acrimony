using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAltar : NPCBehaviour
{
	private StatusEffectManager effectManager;
	public float strengthDuration = 60f;
	private CircleCollider2D collider;
	private ParticleSystem particles;

	private void Start()
	{
		playerMng = PlayerManager.instance;
		effectManager = StatusEffectManager.instance;
		collider = GetComponent<CircleCollider2D>();
		particles = GetComponentInChildren<ParticleSystem>();
	}

	public override void OnInteract()
	{
		Debug.Log("Vigdis gives you her strength");
		effectManager.Strengthen(strengthDuration);
		StartCoroutine(CooldownStart());
	}


	IEnumerator CooldownStart()
	{
		collider.enabled = false;
		var local = particles.emission;
		local.enabled = false;
		yield return new WaitForSeconds(60f);
		collider.enabled = true;
		local.enabled = true;
	}
}
