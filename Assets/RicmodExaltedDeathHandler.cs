using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RicmodExaltedDeathHandler : MonoBehaviour
{
	[Header("Extinguished")]
	public GameObject Ricmod;
	public GameObject Death;
	public GameObject particles;
	public GameObject bossUI;
	public GameObject trigger;
	//private BoxCollider2D arena;

	[Header("References")]
	private ProgressionTracker progressionTracker;
	private UIManager uIManager;
	private PlayerManager playerManager;
	private RicmodManager ricmodManager;

	private Animator anim;
	private bool reactivate = true;
	public bool screenChange = true;
	public bool isDead = false;
	private float maxHP;

	public static RicmodExaltedDeathHandler instance = null;

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
		uIManager = UIManager.instance;
		ricmodManager = Ricmod.GetComponent<RicmodManager>();
		playerManager = PlayerManager.instance;
		anim = GetComponentInChildren<Animator>();
		progressionTracker = ProgressionTracker.instance;
		maxHP = ricmodManager.maxHealth;

		bossUI.GetComponentInChildren<Slider>().maxValue = maxHP;
	}

	void Update()
	{

		if (playerManager.isDead == true)
		{
			StartCoroutine(CancelAction());
			Ricmod.SetActive(false);
			ricmodManager.health = maxHP;
			BossFightTrigger.instance.uniqueTilemap.gameObject.SetActive(false);
		}

		

		if (Ricmod.activeInHierarchy == true)
		{
			bossUI.SetActive(true);
			uIManager.SetBossHP(ricmodManager.health);
			uIManager.SetBossName("RICMOD RÚN THE EXALTED");
		}
		else
		{
			bossUI.SetActive(false);
		}

		Vector2 lastPos = Ricmod.transform.position; ;

		Death.transform.position = lastPos;

		if (ricmodManager.health >= 1)
		{
			screenChange = true;
		}
		else
		{
			screenChange = false;
		}

		if (ricmodManager.health <= 0)
		{
			isDead = true;
			StartCoroutine(CancelAction());
			progressionTracker.UnlockGun();
			playerManager.currentMana = playerManager.maxMana;
			trigger.SetActive(false);
		}

		if (screenChange == false)
		{
			if (Ricmod.activeInHierarchy == false && reactivate == true)
			{
				Death.SetActive(true);
				StartCoroutine(DeathFX());
				//BossFightTrigger.instance.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	IEnumerator CancelAction()
	{
		RicmodExaltedAI.instance.CancelAll();
		yield return null;
	}

	IEnumerator DeathFX()
	{
		bossUI.SetActive(false);
		reactivate = false;

		particles.SetActive(true);
		yield return new WaitForSeconds(2f);
		Death.SetActive(false);
		yield return new WaitForSeconds(2f);
		if (particles.GetComponent<ParticleSystem>().emission.enabled == false)
		{
			particles.SetActive(false);
		}

		progressionTracker.UnlockFire();
	}
}
