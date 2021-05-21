using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAltar : NPCBehaviour
{
	private StatusEffectManager effectManager;
	private CircleCollider2D collider;
	private ParticleSystem particles;
	public int healTotal = 70;
	public int healDuration = 5;
	public float healTick = 0.2f;

	private void Start()
	{
		playerMng = PlayerManager.instance;
		effectManager = StatusEffectManager.instance;
		collider = GetComponent<CircleCollider2D>();
		particles = GetComponentInChildren<ParticleSystem>();
	}

	public override void OnInteract()
	{
		Debug.Log("Valdis smiles upon you");
		effectManager.Healed(healTotal, healDuration, healTick);
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
