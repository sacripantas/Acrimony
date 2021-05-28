using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionTracker : MonoBehaviour
{
	[Header("Abilities")]
	public bool unlockDoubleJump = false;       //0
	public bool unlockWallJump = false;         //1
	public bool unlockDash = false;             //2
	public bool unlockGun = false;              //3
	public bool unlockProjectileFire = false;   //4
	public bool unlockProjectileIce = false;    //5
	public bool unlockProjectileCharm = false;  //6

	[Header("Teleporters")]
	public bool unlockTeleport0 = false; //7
	public bool unlockTeleport1 = false; //8

	public static ProgressionTracker instance = null;

	private void Awake()
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

	public void UnlockDoubleJump()
	{
		unlockDoubleJump = true;
	}

	public void UnlockWallJump()
	{
		unlockWallJump = true;
	}

	public void UnlockDash()
	{
		unlockDash = true;
	}

	public void UnlockGun()
	{
		unlockGun = true;
	}

	public void UnlockFire()
	{
		unlockProjectileFire = true;
	}

	public void UnlockIce()
	{
		unlockProjectileIce = true;
	}

	public void UnlockCharm()
	{
		unlockProjectileCharm = true;
	}

	public void TeleportUnlock(int index)
	{
		switch (index)
		{
			case 0:
				unlockTeleport0 = true;
				Debug.Log("TP 0");
				break;
			case 1:
				unlockTeleport1 = true;
				Debug.Log("TP 1");
				break;
		}	
	}

    //returns a string with progress where 0 = locked and 1 = unlocked
    public string GetProgression() {
        string progression = "";
		progression +=  Convert.ToInt32(unlockDoubleJump) + "" +
						Convert.ToInt32(unlockWallJump) + "" +
						Convert.ToInt32(unlockDash) + "" +
						Convert.ToInt32(unlockGun) + "" +
						Convert.ToInt32(unlockProjectileFire) + "" +
						Convert.ToInt32(unlockProjectileIce) + "" +
						Convert.ToInt32(unlockProjectileCharm) + "" +
						Convert.ToInt32(unlockTeleport0) + "" +
						Convert.ToInt32(unlockTeleport1);
        return progression;
    }

    //Set progression according to string of int
    public void SetProgression(string progression) {
        unlockDoubleJump = Convert.ToBoolean((int)Char.GetNumericValue(progression[0]));
        unlockWallJump = Convert.ToBoolean((int)Char.GetNumericValue(progression[1]));
        unlockDash = Convert.ToBoolean((int)Char.GetNumericValue(progression[2]));
        unlockGun = Convert.ToBoolean((int)Char.GetNumericValue(progression[3])); 
        unlockProjectileFire = Convert.ToBoolean((int)Char.GetNumericValue(progression[4]));
        unlockProjectileIce = Convert.ToBoolean((int)Char.GetNumericValue(progression[5]));
        unlockProjectileCharm = Convert.ToBoolean((int)Char.GetNumericValue(progression[6]));
		unlockTeleport0 = Convert.ToBoolean((int)Char.GetNumericValue(progression[7]));
		unlockTeleport1 = Convert.ToBoolean((int)Char.GetNumericValue(progression[8]));
	}
}
