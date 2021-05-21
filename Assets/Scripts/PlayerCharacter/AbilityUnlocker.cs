using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlocker : NPCBehaviour
{
	private ProgressionTracker tracker;
	private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
		tracker = ProgressionTracker.instance;
		playerMng = PlayerManager.instance;
    }

	public override void OnInteract()
	{
		tracker.UnlockDoubleJump();
		Debug.Log("Double jump unlocked");
		Destroy(this.gameObject);
	}
}
