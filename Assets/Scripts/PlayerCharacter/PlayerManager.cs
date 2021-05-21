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
	private UIManager ui;
	public static PlayerManager instance = null;
    private GameManager manager;
	private CharacterController characterController;
	public bool isDead = false;
	private StatusEffectManager effectManager;

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
		ui = UIManager.instance;

		ui.SetMaxHealth(maxHealth);

		ui.SetMaxMana(maxMana);

        manager = GameManager.instance;
		characterController = CharacterController.instance;
		spriteRenderer = GetComponent<SpriteRenderer>();
		effectManager = StatusEffectManager.instance;
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
    //Set player stats;
    public void SetPlayer(int hp, int mana, int money, int ammo) {
        this.currentHealth = hp;
        this.currentMana = mana;
        this.currentMoney = money;
        this.currentAmmo = ammo;
    }
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
        if (currentMoney - money <= 0)
            currentMoney = 0;
        else
            currentMoney -= money;
        UpdateMoney.instance.SetAvailableMoney();
        UpdateMoney.instance.Anim("buy");
	}

    public void SellItem(int money) {
        currentMoney += money;
        UpdateMoney.instance.SetAvailableMoney();
        UpdateMoney.instance.Anim("sell");
    }

    public bool CanAfford(int money) {
        if (currentMoney - money < 0) return false;
        return true;
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
			isDead = true;
			
			manager.DeathHandler();
			currentHealth = maxHealth;
			currentAmmo = currentAmmo / 2;
			currentMoney = 0;
			StartCoroutine(DeathReset());
		}
	}

	IEnumerator DeathReset()
	{
		yield return new WaitForSeconds(0.01f);
		isDead = false;
	}

	public IEnumerator IFramesHandler()
	{
        StartCoroutine(characterController.Knockback());
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
	public void DamageOverTime(int damageAmount, int duration, float tickRate, StatusEffectManager.CurrentStatus status)
	{
		StartCoroutine(DamageOverTimeCo(damageAmount, duration, tickRate, status));
	}

	public void HealOverTime(int healAmount, int duration, float tickRate)
	{
		StartCoroutine(HealOverTimeCo(healAmount, duration, tickRate));
	}

	public void ClearAll()
	{
		StopAllCoroutines();		
	}

	IEnumerator DamageOverTimeCo(float damageAmount, float duration, float tickRate, StatusEffectManager.CurrentStatus status)
	{
		float amountDamaged = 0;
		float damagePerTick = damageAmount / duration;	

		switch (status)
		{
			case StatusEffectManager.CurrentStatus.Poison:
				ui.SetMaxPoison(damageAmount);
				break;
			case StatusEffectManager.CurrentStatus.Bleed:
				ui.SetMaxBleed(damageAmount);
				break;
			case StatusEffectManager.CurrentStatus.Burn:
				ui.SetMaxBurn(damageAmount);
				break;
		}

		while (amountDamaged < damageAmount)
		{
			currentHealth -= (int)damagePerTick;
			amountDamaged += damagePerTick;
			switch (status)
			{
				case StatusEffectManager.CurrentStatus.Poison:
					ui.SetPoison(damageAmount - amountDamaged);
					break;
				case StatusEffectManager.CurrentStatus.Bleed:
					ui.SetBleed(damageAmount - amountDamaged);
					break;
				case StatusEffectManager.CurrentStatus.Burn:
					ui.SetBurn(damageAmount - amountDamaged);
					break;
			}			
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
