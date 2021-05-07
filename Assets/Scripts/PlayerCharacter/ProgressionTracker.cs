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

    // Update is called once per frame
    void Update()
    {
		UnlockDoubleJump();
		UnlockWallJump();
		UnlockDash();
		UnlockGun();
		UnlockFire();
		UnlockIce();
		UnlockCharm();
    }

	void UnlockDoubleJump()
	{
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			unlockDoubleJump = true;
		}	
	}


	void UnlockWallJump()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			unlockWallJump = true;
		}		
	}


	void UnlockDash()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			unlockDash = true;
		}
		
	}


	void UnlockGun()
	{
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			unlockGun = true;
		}
		
	}


	void UnlockFire()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			unlockProjectileFire = true;
		}
		
	}


	void UnlockIce()
	{
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			unlockProjectileIce = true;
		}	
	}


	void UnlockCharm()
	{
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			unlockProjectileCharm = true;
		}		
	}
}
