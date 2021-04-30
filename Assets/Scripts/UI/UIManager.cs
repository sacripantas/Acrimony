using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
	//HealthBar
	public Slider healthBar;
	public Gradient gradient;
	public Image healthFill;

	//ManaBar
	public Slider manaBar;
	public Image manaFill;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void SetMaxHealth(int health)
	{
		healthBar.maxValue = health;
		healthBar.value = health;

		healthFill.color = gradient.Evaluate(1f);//max hp color
	}

	public void SetHealth(int health)
	{
		healthBar.value = health;
		healthFill.color = gradient.Evaluate(healthBar.normalizedValue);
	}

	public void SetMaxMana(int mana)
	{
		manaBar.maxValue = mana;
		manaBar.value = mana;

	}

	public void SetMana(int mana)
	{
		manaBar.value = mana;
	}

    public void SetVisible(bool flag) {
        this.healthBar.gameObject.SetActive(flag);
        this.manaBar.gameObject.SetActive(flag);
    }
}
