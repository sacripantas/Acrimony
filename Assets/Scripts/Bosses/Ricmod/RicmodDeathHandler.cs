using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RicmodDeathHandler : MonoBehaviour
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

	public static RicmodDeathHandler instance = null;

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
		if(progressionTracker.ricmodDead == false)
		{
			if (Ricmod.activeInHierarchy == true)
			{
				bossUI.SetActive(true);
				uIManager.SetBossHP(ricmodManager.health);
				uIManager.SetBossName("RICMOD RÚN THE EXTINGUISHED");
				Debug.Log("test");
			}

			Vector2 lastPos = Ricmod.transform.position; ;

			Death.transform.position = lastPos;

			if (ricmodManager.health <= 0)
			{
				trigger.SetActive(false);
				isDead = true;
				StartCoroutine(CancelAction());
				progressionTracker.UnlockIce();
				progressionTracker.ricmodDead = true;
				playerManager.currentMana = playerManager.maxMana;
			}
		}
		else
		{
			trigger.SetActive(false);
		}
	}

	IEnumerator CancelAction()
	{
		RicmodAI.instance.CancelAll();
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
