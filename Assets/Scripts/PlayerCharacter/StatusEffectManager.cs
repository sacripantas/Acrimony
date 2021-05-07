using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
	private PlayerManager playerManager;
	private CharacterController characterController;
	private PlayerAttack playerAttack;

	private bool isWeakened;
	private bool isStrengthened;
	private bool isPoisoned;
	private bool isBleeding;
	private bool isBurning;
	private bool isHealing;
	private bool isFreezing;
	private bool isGrounded;

	public int poisonDmg = 10;
	public int poisonDuration = 5;

	public int burnDamage = 50;
	public int burnDuration = 5;
	public float burnTick = 0.1f;

	public int bleedDamage = 40;
	public int bleedDuration = 10;
	public float bleedTick = 0.5f;

	public int healDmg = 10;
	public int healDuration = 5;

	private float weakDuration = 2f;
	private float strengthDuration = 2f;
	private float freezeDuration = 2f;
	private float groundedDuraion = 2f;

	private float startDamage;
	private float startLungeDamage;
	private float startSpeed;
	private float startJumpforce;

	public static StatusEffectManager instance = null;

	enum CurrentStatus
	{
		Weak,
		Strength,
		Poison,
		Fire,
		Bleed,
		Burn,
		Freeze,
		Grounded,
		ClearAll
	}

	[SerializeField] CurrentStatus current;

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


	// Start is called before the first frame update
	void Start()
    {
		playerManager = GetComponent<PlayerManager>();
		characterController = GetComponent<CharacterController>();
		playerAttack = GetComponent<PlayerAttack>();

		startDamage = playerAttack.damage;
		startLungeDamage = playerAttack.damageLunge;
		startSpeed = characterController.runSpeed;
		startJumpforce = characterController.jumpforce;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			Poisoned(poisonDmg, poisonDuration, 1);
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			Burning(burnDamage, burnDuration, burnTick);
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			Healed();
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			Bleeding(bleedDamage,bleedDuration,bleedTick);
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			Freezing();
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			Grounded();
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			ClearAll();
		}
	}

	public void Weaken()
	{
		if (isWeakened == false)
		{
			playerAttack.damage /= 2;
			playerAttack.damageLunge /= 2;
			isWeakened = true;

			current = CurrentStatus.Weak;
			StartCoroutine(ResetStatus(current));
		}		
	}

	public void Strengthen()
	{
		if (isStrengthened == false)
		{
			playerAttack.damage *= 2;
			playerAttack.damageLunge *= 2;
			isStrengthened = true;

			current = CurrentStatus.Strength;
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Poisoned(int totalDmg, int duration, float tickRate)
	{
		if(isPoisoned == false)
		{
			playerManager.DamageOverTime(totalDmg, duration, tickRate);
			Debug.Log("Poisoned");
			isPoisoned = true;

			current = CurrentStatus.Poison;
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Healed()
	{
		playerManager.HealOverTime(healDmg,healDuration,1f);
		Debug.Log("Healing");
	}

	public void Burning(int totalDmg, int duration, float tickRate)
	{
		if (isBurning == false)
		{
			playerManager.DamageOverTime(totalDmg, duration, tickRate);
			Debug.Log("Burning");
			isBurning = true;

			current = CurrentStatus.Burn;
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Bleeding(int totalDmg, int duration, float tickRate)
	{
		if (isBleeding == false)
		{
			playerManager.DamageOverTime(totalDmg, duration, tickRate);
			Debug.Log("Bleeding");
			isBleeding = true;

			current = CurrentStatus.Bleed;
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Freezing()
	{
		if (isFreezing == false)
		{
			characterController.runSpeed -= 1;
			Debug.Log("Freezing");
			isFreezing = true;

			current = CurrentStatus.Freeze;
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Grounded()
	{
		if (isGrounded == false)
		{
			characterController.jumpforce /= 2;
			Debug.Log("Grounded");
			isGrounded = true;

			current = CurrentStatus.Grounded;
			StartCoroutine(ResetStatus(current));
		}
	}

	public void ClearAll()
	{
		current = CurrentStatus.ClearAll;
		StartCoroutine(ResetStatus(current));
	}

	IEnumerator ResetStatus(CurrentStatus status)
	{
		switch (status)
		{
			case CurrentStatus.Weak:
				yield return new WaitForSeconds(weakDuration);
				isWeakened = false;
				playerAttack.damage = startDamage;
				playerAttack.damageLunge = startLungeDamage;

				Debug.Log("No longer weak");
				break;
			case CurrentStatus.Strength:
				yield return new WaitForSeconds(strengthDuration);
				isStrengthened = false;
				playerAttack.damage = startDamage;
				playerAttack.damageLunge = startLungeDamage;

				Debug.Log("No longer strong");
				break;
			case CurrentStatus.Poison:
				yield return new WaitForSeconds(poisonDuration);
				isPoisoned = false;

				Debug.Log("no longer Poisoned");
				break;
			case CurrentStatus.Bleed:
				yield return new WaitForSeconds(bleedDuration);
				isBleeding = false;

				Debug.Log("no longer Bleeding");
				break;
			case CurrentStatus.Burn:
				yield return new WaitForSeconds(burnDuration);
				isBurning = false;

				Debug.Log("No longer burning");
				break;
			case CurrentStatus.Freeze:
				yield return new WaitForSeconds(freezeDuration);
				isFreezing = false;
				characterController.runSpeed = startSpeed;

				Debug.Log("no longer freezing");
				break;
			case CurrentStatus.Grounded:
				yield return new WaitForSeconds(groundedDuraion);
				isGrounded = false;
				characterController.jumpforce = startJumpforce;

				Debug.Log("no longer grounded");
				break;
			case CurrentStatus.ClearAll:
				yield return new WaitForSeconds(0f);
				isWeakened = false;
				isStrengthened = false;
				isPoisoned = false;
				isBleeding = false;
				isBurning = false;
				isFreezing = false;
				isGrounded = false;
				playerAttack.damage = startDamage;
				playerAttack.damageLunge = startLungeDamage;
				characterController.runSpeed = startSpeed;
				characterController.jumpforce = startJumpforce;

				Debug.Log("You have been blessed, all status cleared");
				break;
		}
	}
}
