using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
	public Transform projectileSpawner;
	[SerializeField] private GameObject fireballPrefab;
	[SerializeField] private GameObject iceballPrefab;
	[SerializeField] private GameObject charmballPrefab;

	//References
	public PlayerManager playerManager;
	public CharacterController character;
	public FireBall fireBall;
	public IceBall iceBall;
	public CharmBall charmBall;

	//Cooldown
	public float fireCooldown = 1.5f;
	private float nextFireTime = 0;

	public float iceCooldown = 0.5f;
	private float nextIceTime = 0;

	public float charmCooldown = 3f;
	private float nextCharmTime = 0;

	public ProgressionTracker tracker;

	//
	enum Projectile
	{
		Fire,
		Ice,
		Charm
	}

	[SerializeField] Projectile currentProjectile;

	private void Start()
	{
		currentProjectile = Projectile.Fire;
	}

	void Update()
    {	
		ProjectileSwap();
    }	

	void ProjectileSwap()
	{
		if (currentProjectile == Projectile.Fire)
		{
			SpawnFireball();
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{				
				currentProjectile = Projectile.Ice;
				Debug.Log("Current = ice");
			}
		}
		else if (currentProjectile == Projectile.Ice)
		{
			SpawnIceball();
			if (Input.GetKeyDown(KeyCode.Keypad4) )
			{
				currentProjectile = Projectile.Charm;
				Debug.Log("Current = charm");
			}
		}
		else if (currentProjectile == Projectile.Charm)
		{
			SpawnCharmball();
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				currentProjectile = Projectile.Fire;
				Debug.Log("Current = fire");
			}
		}
	}

	void SpawnFireball()
	{
		if (Input.GetButton("Projectile"))
		{
			if (Time.time > nextFireTime)
			{
				if (playerManager.currentMana <= fireBall.manaCost - 1)
				{
					Debug.Log("Not enough mana");
				}
				else
				{
					playerManager.currentMana -= fireBall.manaCost;

					if (character.direction == 1)
					{
						fireBall.direction = 1;
					}
					else
					{
						fireBall.direction = -1;
					}
					GameObject tempFire = Instantiate(fireballPrefab);
					tempFire.transform.position = projectileSpawner.transform.position;

					nextFireTime = Time.time + fireCooldown;
				}
			}		
		}
	}

	void SpawnIceball()
	{
		if (Input.GetButton("Projectile"))
		{
			if (Time.time > nextIceTime)
			{
				if (playerManager.currentMana <= iceBall.manaCost - 1)
				{
					Debug.Log("Not enough mana");
				}
				else
				{
					playerManager.currentMana -= iceBall.manaCost;

					if (character.direction == 1)
					{
						iceBall.direction = 1;
					}
					else
					{
						iceBall.direction = -1;
					}
					GameObject tempIce = Instantiate(iceballPrefab);
					tempIce.transform.position = projectileSpawner.transform.position;

					nextIceTime = Time.time + iceCooldown;
				}
			}
		}
	}

	void SpawnCharmball()
	{
		if (Input.GetButton("Projectile"))
		{
			if (Time.time > nextCharmTime)
			{
				if (playerManager.currentMana <= charmBall.manaCost - 1)
				{
					Debug.Log("Not enough mana");
				}
				else
				{
					playerManager.currentMana -= charmBall.manaCost;

					if (character.direction == 1)
					{
						charmBall.direction = 1;
					}
					else
					{
						charmBall.direction = -1;
					}
					GameObject tempCharm = Instantiate(charmballPrefab);
					tempCharm.transform.position = projectileSpawner.transform.position;

					nextCharmTime = Time.time + charmCooldown;
				}
			}
		}
	}
}
