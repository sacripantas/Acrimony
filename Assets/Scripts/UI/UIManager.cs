using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    
    private static GameManager manager;
    private static PlayerManager playerManager;
    private static InventoryHandler inventory;
	private StatusEffectManager effectManager;
    //Minimap
    [SerializeField]
    [Tooltip("Gameobject for minimap")]
    private GameObject minimap;

	//HealthBar
	[Header("Player Health")]
	public Slider healthBar;
	public Gradient gradient;
	public Image healthFill;
	public Text hpValue;

	//ManaBar
	[Header("Player Mana")]
	public Slider manaBar;
	public Image manaFill;

	[Header("Resources")]
	public Text money;
	public Text ammo;

	[Header("Current Status")]
	public Slider poisonBar;
	public Image posionFill;
	public Slider bleedBar;
	public Image bleedFill;
	public Slider burnBar;
	public Image burnFill;
	public Slider healBar;
	public Image healFill;

	[Header("Adalhard")]
	public Slider adalhardHpBar;
	public Image adalhardFill;
            
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        manager = GameManager.instance;
        playerManager = PlayerManager.instance;
        inventory = InventoryHandler.instance;
		effectManager = StatusEffectManager.instance;		
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
        if(hpValue != null)
		hpValue.text = health.ToString();

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

    public void SetUIVisible(bool flag) {
        this.healthBar.gameObject.SetActive(flag);
        this.manaBar.gameObject.SetActive(flag);
        this.ammo.gameObject.SetActive(flag);
        this.money.gameObject.SetActive(flag);
        this.minimap.gameObject.SetActive(flag);
    }

	public void SetCurrency(int currency)
	{
		money.text = "$" + currency.ToString();
	}

	public void SetAmmo(int ammunition)
	{
		ammo.text = "Ammo " + "\n"+  ammunition.ToString();
	}

	public void SetBossHP(float health)
	{
		adalhardHpBar.value = health;
	}

	//==================================================================================STATUS EFFECTS=======================================================

	public void SetMaxPoison(float duration)
	{
		poisonBar.maxValue = duration;
		poisonBar.value = duration;
	}

	public void SetPoison(float duration)
	{
		poisonBar.value = duration;
	}

	public void SetMaxBleed(float duration)
	{
		bleedBar.maxValue = duration;
		bleedBar.value = duration;
	}

	public void SetBleed(float duration)
	{
		bleedBar.value = duration;
	}

	public void SetMaxBurn(float duration)
	{
		burnBar.maxValue = duration;
		burnBar.value = duration;
	}

	public void SetBurn(float duration)
	{
		burnBar.value = duration;
	}	
}
