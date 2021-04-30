using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	//health
	public int maxHealth = 100;
	public static int currentHealth;

	//Mana
	public int maxMana = 100;
	public int currentMana;

	//General
	public static PlayerManager instance = null;
	public UIManager ui;
    private GameManager manager;

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
	}

	void Update()
	{
		ui.SetHealth(currentHealth); //Updates the healthbar
		ui.SetMana(currentMana);

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
		currentHealth -= damage;
	}

	public void ReceiveHealth(int heal)
	{
		currentHealth += heal;
        if (currentHealth > 100) currentHealth = 100;
	}

	public void Death()
	{
		if (currentHealth <= 0) //Resets hp to 100 on death
		{
            manager.DeathHandler();
			currentHealth = maxHealth;
		}
	}
}
