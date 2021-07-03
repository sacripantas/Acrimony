using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportUnlock : MonoBehaviour
{
	public TeleportInteract interact;
	private ProgressionTracker tracker;

	private void Start()
	{
		tracker = ProgressionTracker.instance;

	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			tracker.TeleportUnlock(interact.index);			
		}

		switch (interact.index)
		{
			case 0:
				if (tracker.unlockTeleport0 == true)
				{
					interact.isUnlocked = true;
				}
				break;
			case 1:
				if (tracker.unlockTeleport1)
				{
					interact.isUnlocked = true;
				}
				break;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			interact.menuUI.SetActive(false);
			interact.isActive = false;
            PlayerAttack.instance.CanAttack(true);
		}
	}
}
