using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAltar : NPCBehaviour
{
	private StatusEffectManager effectManager;
	private CircleCollider2D collider;
	private ParticleSystem particles;
	private Animator anim;

	private void Start()
	{
		playerMng = PlayerManager.instance;
		effectManager = StatusEffectManager.instance;
		collider = GetComponent<CircleCollider2D>();
		particles = GetComponentInChildren<ParticleSystem>();
		anim = GetComponent<Animator>();
	}

	public override void OnInteract()
	{
		Debug.Log("Ælfnoð replenishes your power");
		playerMng.ReceiveMana(100);
		StartCoroutine(CooldownStart());
	}


	IEnumerator CooldownStart()
	{
		anim.Play("AltarOfMana");
		collider.enabled = false;
		var local = particles.emission;
		local.enabled = false;
		yield return new WaitForSeconds(5f);
		collider.enabled = true;
		local.enabled = true;
		anim.Play("New State");
	}
}
