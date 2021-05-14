using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAltar : NPCBehaviour
{
	private StatusEffectManager effectManager;
	public int healTotal = 70;
	public int healDuration = 5;
	public float healTick = 0.2f;

	private void Start()
	{
		playerMng = PlayerManager.instance;
		effectManager = StatusEffectManager.instance;
	}

	public override void OnInteract()
	{
		Debug.Log("Valdis smiles upon you");
		effectManager.Healed(healTotal, healDuration, healTick);		
	}
}
