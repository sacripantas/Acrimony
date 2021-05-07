using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	//health
	[Header("Health")]
	public int maxHealth = 100;
	public int currentHealth;

	//I frames
	[Header("I Frames")]
	public Color flashColor;
	public Color originalColor;
	public float flashDuration;
	public int numberOfFlashes;
	public BoxCollider2D hitbox;
	private SpriteRenderer spriteRenderer;

	//Mana
	[Header("Mana")]
	public int maxMana = 100;
	public int currentMana;

	//General
	[Header("General")]
	public UIManager ui;
	public static PlayerManager instance = null;
    private GameManager manager;
	private CharacterController characterController;

	//Currency
	public int currentMoney = 0;

	//Ammo
	public int currentAmmo = 0;

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

	void Start()
	{
		currentHealth = maxHealth;
		ui.SetMaxHealth(maxHealth);

		currentMana = maxMana;
		ui.SetMaxMana(maxMana);

        manager = GameManager.instance;
		characterController = GetComponent<CharacterController>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		ui.SetHealth(currentHealth); //Updates the healthbar
		ui.SetMana(currentMana);
		ui.SetCurrency(currentMoney);
		ui.SetAmmo(currentAmmo);

		ChangeHP();
		ChangeMana();
		Death();
	}

    /***Dev only***/
	public void ChangeHP()
	{
		if (Input.GetKeyDown(KeyCode.Keypad7) && currentHealth > 0)
		{
			TakeDamage(20);
		}

		if (Input.GetKeyDown(KeyCode.Keypad8) && currentHealth < 100)
		{
			ReceiveHealth(20);
		}
	}
	/*******/

	//==========================================================================VALUE CHANGERS========================================================================
	void ChangeMana()
	{
		if(Input.GetKeyDown(KeyCode.Keypad9) && currentMana < 100)
		{
			ReceiveMana(10);
		}
	}

	void ReceiveMana(int manaHeal)
	{
		currentMana += manaHeal;
	}

	public void TakeDamage(int damage)
	{
        StartCoroutine(IFramesHandler());
		currentHealth -= damage;		
	}

	public void ReceiveHealth(int heal)
	{
		currentHealth += heal;
        if (currentHealth > 100) currentHealth = 100;
	}

	public void ReceiveMoney(int money)
	{
		currentMoney += money;
	}

	public void SpendMoney(int money)
	{
		currentMoney -= money;
	}

	public void ReceiveAmmo(int ammunition)
	{
		currentAmmo += ammunition;
	}

	public void SpendAmmo(int ammunition)
	{
		currentAmmo -= ammunition;
	}

	public void Death()
	{
		if (currentHealth <= 0) //Resets hp to 100 on death
		{
			manager.DeathHandler();
			currentHealth = maxHealth;
			currentAmmo = currentAmmo / 2;
			currentMoney = 0;

		}
	}

	public IEnumerator IFramesHandler()
	{
        StartCoroutine(characterController.Knockback(0.5f, 1, transform.position));
		int temp = 0;
		hitbox.enabled = false;
		while(temp < numberOfFlashes)
		{
			spriteRenderer.color = flashColor;
			yield return new WaitForSeconds(flashDuration);
			spriteRenderer.color = originalColor;
			yield return new WaitForSeconds(flashDuration);
			temp++;
		}
		hitbox.enabled = true;
	}

	//==========================================================================STATUS EFFECTS========================================================================
	public void DamageOverTime(int damageAmount, int duration, float tickRate)
	{
		StartCoroutine(DamageOverTimeCo(damageAmount, duration, tickRate));
	}

	public void HealOverTime(int healAmount, int duration, float tickRate)
	{
		StartCoroutine(HealOverTimeCo(healAmount, duration, tickRate));
	}

	IEnumerator DamageOverTimeCo(float damageAmount, float duration, float tickRate)
	{
		float amountDamaged = 0;
		float damagePerTick = damageAmount / duration;
		while(amountDamaged < damageAmount)
		{
			currentHealth -= (int)damagePerTick;
			amountDamaged += damagePerTick;
			yield return new WaitForSeconds(tickRate);
		}
	}

	IEnumerator HealOverTimeCo(float healAmount, float duration, float tickRate)
	{
		float amountHealed = 0;
		float healPerTick = healAmount / duration;
		while (amountHealed < healAmount)
		{
			if(currentHealth < maxHealth)
			{
				currentHealth += (int)healPerTick;
			}
			amountHealed += healPerTick;
			yield return new WaitForSeconds(tickRate);
		}			
	}
}
