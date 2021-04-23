using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
	//HealthBar
	public Slider healthBar;
	public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(int health)
	{
		healthBar.maxValue = health;
		healthBar.value = health;

		fill.color = gradient.Evaluate(1f);//max hp color
	}

	public void SetHealth(int health)
	{
		healthBar.value = health;
		fill.color = gradient.Evaluate(healthBar.normalizedValue);
	}
}
