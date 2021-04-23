using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;

	public static PlayerManager instance = null;

	public UIManager ui;

    private GameManager manager;

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

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		ui.SetMaxHealth(maxHealth);
        manager = GameManager.instance;
	}

	// Update is called once per frame
	void Update()
	{
		ui.SetHealth(currentHealth); //Updates the healthbar

		ChangeHP();
		Death();
	}

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

	void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}

	void ReceiveHealth(int heal)
	{
		currentHealth += heal;
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
