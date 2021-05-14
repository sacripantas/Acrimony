using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionTracker : MonoBehaviour
{
	public bool unlockDoubleJump = true;
	public bool unlockWallJump = true;
	public bool unlockDash = true;
	public bool unlockGun = true;
	public bool unlockProjectileFire = true;
	public bool unlockProjectileIce = true;
	public bool unlockProjectileCharm = true;

	public static ProgressionTracker instance = null;

	private void Awake()//Singleton
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	void UnlockDoubleJump()
	{
		unlockDoubleJump = true;
	}

	void UnlockWallJump()
	{
		unlockWallJump = true;
	}

	void UnlockDash()
	{
		unlockDash = true;
	}

	void UnlockGun()
	{
		unlockGun = true;
	}

	public void UnlockFire()
	{
		unlockProjectileFire = true;
	}

	void UnlockIce()
	{
		unlockProjectileIce = true;
	}

	void UnlockCharm()
	{
		unlockProjectileCharm = true;
	}
}
