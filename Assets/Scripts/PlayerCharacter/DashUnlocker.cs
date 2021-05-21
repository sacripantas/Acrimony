using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUnlocker : NPCBehaviour
{
	private ProgressionTracker tracker;

	// Start is called before the first frame update
	void Start()
	{
		tracker = ProgressionTracker.instance;
		playerMng = PlayerManager.instance;
	}

	public override void OnInteract()
	{
		tracker.UnlockDash();
		Debug.Log("Dash unlocked");
		Destroy(this.gameObject);
	}
}
