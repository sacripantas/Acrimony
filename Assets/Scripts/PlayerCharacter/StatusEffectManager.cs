using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectManager : MonoBehaviour
{
	private PlayerManager playerManager;
	private CharacterController characterController;
	private PlayerAttack playerAttack;
	private UIManager ui;

	[Header("UI")]
	public GameObject poisonContainer;
	public GameObject bleedContainer;
	public GameObject burnContainer;
	public GameObject freezeContainer;
	public GameObject groundedContainer;
	public GameObject weakContainer;
	public GameObject strengthContainer;
	public GameObject cleanseContainer;
	public GameObject healContainer;

	private bool isWeakened;
	private bool isStrengthened;
	private bool isPoisoned;
	private bool isBleeding;
	private bool isBurning;
	private bool isFreezing;
	private bool isGrounded;

	public int poisonDmg = 10;
	public int poisonDuration = 5;

	public int burnDamage = 50;
	public int burnDuration = 5;
	public float burnTick = 1f;

	public int bleedDamage = 40;
	public int bleedDuration = 10;
	public float bleedTick = 1f;

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

	public enum CurrentStatus
	{
		None,
		Heal,
		Weak,
		Strength,
		Poison,
		Bleed,
		Burn,
		Freeze,
		Grounded,
		ClearAll
	}

	[SerializeField] public CurrentStatus current;

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
		playerManager = PlayerManager.instance;
		characterController = CharacterController.instance;
		playerAttack = PlayerAttack.instance;
		ui = UIManager.instance;

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
			//Healed();
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
		if (Input.GetKeyDown(KeyCode.T))
		{
			//Weaken();
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			//Strengthen();
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			ClearAll();
		}
	}

	public void Healed(int healDmg, int healDuration,float healTick)
	{
		current = CurrentStatus.Heal;
		playerManager.HealOverTime(healDmg, healDuration, healTick);
		healContainer.SetActive(true);
		Debug.Log("Healing");

		StartCoroutine(ResetStatus(current));
	}

	public void Weaken(float duration)
	{
		if (isWeakened == false && isStrengthened == false)
		{
			current = CurrentStatus.Weak;
			playerAttack.damage /= 2;
			playerAttack.damageLunge /= 2;
			weakDuration = duration;
			isWeakened = true;
			weakContainer.SetActive(true);
			
			StartCoroutine(ResetStatus(current));
		}
		else if(isWeakened == false && isStrengthened == true)
		{
			strengthContainer.SetActive(false);
			playerAttack.damage = startDamage;
			playerAttack.damageLunge = startLungeDamage;
			Debug.Log("Can not be strong and weak at the same time");
		}
	}

	public void Strengthen(float duration)
	{
		if (isStrengthened == false && isWeakened == false)
		{
			current = CurrentStatus.Strength;
			playerAttack.damage *= 2;
			playerAttack.damageLunge *= 2;
			strengthDuration = duration;
			isStrengthened = true;
			strengthContainer.SetActive(true);
			
			StartCoroutine(ResetStatus(current));
		}
		else if(isStrengthened == false && isWeakened == true)
		{
			weakContainer.SetActive(false);
			playerAttack.damage = startDamage;
			playerAttack.damageLunge = startLungeDamage;
			Debug.Log("can not be weak and strong at the same time");
		}
	}

	public void Poisoned(int totalDmg, int duration, float tickRate)
	{
		if(isPoisoned == false)
		{
			current = CurrentStatus.Poison;
			playerManager.DamageOverTime(totalDmg, duration, tickRate, current);
			Debug.Log("Poisoned");
			isPoisoned = true;
			poisonContainer.SetActive(true);
			
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Burning(int totalDmg, int duration, float tickRate)
	{
		if (isBurning == false)
		{
			current = CurrentStatus.Burn;
			playerManager.DamageOverTime(totalDmg, duration, tickRate, current);
			Debug.Log("Burning");
			isBurning = true;
			burnContainer.SetActive(true);
			
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Bleeding(int totalDmg, int duration, float tickRate)
	{
		if (isBleeding == false)
		{
			current = CurrentStatus.Bleed;
			playerManager.DamageOverTime(totalDmg, duration, tickRate, current);
			Debug.Log("Bleeding");
			isBleeding = true;
			bleedContainer.SetActive(true);
			
			StartCoroutine(ResetStatus(current));
		}
	}

	public void Freezing()
	{
		if (isFreezing == false)
		{
			current = CurrentStatus.Freeze;
			characterController.runSpeed -= 1;
			Debug.Log("Freezing");
			isFreezing = true;
			freezeContainer.SetActive(true);

			StartCoroutine(ResetStatus(current));
		}
	}

	public void Grounded()
	{
		if (isGrounded == false)
		{
			current = CurrentStatus.Grounded;
			characterController.jumpforce /= 2;
			Debug.Log("Grounded");
			isGrounded = true;
			groundedContainer.SetActive(true);

			StartCoroutine(ResetStatus(current));
		}
	}

	public void ClearAll()
	{
		current = CurrentStatus.ClearAll;
		playerManager.ClearAll();
		cleanseContainer.SetActive(true);
		StartCoroutine(ResetStatus(current));
	}

	IEnumerator ResetStatus(CurrentStatus status)
	{
		switch (status)
		{
			case CurrentStatus.Heal:
				yield return new WaitForSeconds(healDuration);
				healContainer.SetActive(false);
				break;
			case CurrentStatus.Weak:
				yield return new WaitForSeconds(weakDuration);
				isWeakened = false;
				weakContainer.SetActive(false);
				playerAttack.damage = startDamage;
				playerAttack.damageLunge = startLungeDamage;

				Debug.Log("No longer weak");
				break;
			case CurrentStatus.Strength:
				yield return new WaitForSeconds(strengthDuration);
				isStrengthened = false;
				strengthContainer.SetActive(false);
				playerAttack.damage = startDamage;
				playerAttack.damageLunge = startLungeDamage;

				Debug.Log("No longer strong");
				break;
			case CurrentStatus.Poison:
				yield return new WaitForSeconds(poisonDuration);
				isPoisoned = false;
				poisonContainer.SetActive(false);

				Debug.Log("no longer Poisoned");
				break;
			case CurrentStatus.Bleed:
				yield return new WaitForSeconds(bleedDuration * bleedTick);
				isBleeding = false;
				bleedContainer.SetActive(false);

				Debug.Log("no longer Bleeding");
				break;
			case CurrentStatus.Burn:
				yield return new WaitForSeconds(burnDuration * burnTick);
				isBurning = false;
				burnContainer.SetActive(false);

				Debug.Log("No longer burning");
				break;
			case CurrentStatus.Freeze:
				yield return new WaitForSeconds(freezeDuration);
				isFreezing = false;
				freezeContainer.SetActive(false);
				characterController.runSpeed = startSpeed;

				Debug.Log("no longer freezing");
				break;
			case CurrentStatus.Grounded:
				yield return new WaitForSeconds(groundedDuraion);
				isGrounded = false;
				groundedContainer.SetActive(false);
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
				healContainer.SetActive(false);
				weakContainer.SetActive(false);
				strengthContainer.SetActive(false);
				poisonContainer.SetActive(false);
				bleedContainer.SetActive(false);
				burnContainer.SetActive(false);
				freezeContainer.SetActive(false);
				groundedContainer.SetActive(false);
				playerAttack.damage = startDamage;
				playerAttack.damageLunge = startLungeDamage;
				characterController.runSpeed = startSpeed;
				characterController.jumpforce = startJumpforce;
				
				yield return new WaitForSeconds(1f);
				cleanseContainer.SetActive(false);

				Debug.Log("You have been cleansed, all status cleared");
				break;
		}
	}
}
