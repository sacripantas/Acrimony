using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAltar : NPCBehaviour
{
	private StatusEffectManager effectManager;
	public float strengthDuration = 10f;

	private void Start()
	{
		playerMng = PlayerManager.instance;
		effectManager = StatusEffectManager.instance;
	}

	public override void OnInteract()
	{
		Debug.Log("Vigdis gives you her strength");
		effectManager.Strengthen(strengthDuration);
	}
}
