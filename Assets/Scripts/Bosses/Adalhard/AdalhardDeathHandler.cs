using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdalhardDeathHandler : MonoBehaviour
{
	[Header("General")]
	public GameObject Adalhard;
	public GameObject Death;
	public GameObject particles;
	public GameObject uiBar;
	public GameObject trigger;

	[Header("References")]
	private ProgressionTracker progressionTracker;
	private UIManager uIManager;
	private PlayerManager playerManager;
	private EnemyManager enemyManager;
	
	private Animator anim;	
	private bool reactivate = true;	
	public bool screenChange = true;
	public bool isDead = false;
	private float maxHP;

	public static AdalhardDeathHandler instance = null;

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
		uIManager = UIManager.instance;
		enemyManager = Adalhard.GetComponent<EnemyManager>();
		playerManager = PlayerManager.instance;
		anim = GetComponentInChildren<Animator>();
		progressionTracker = ProgressionTracker.instance;
		maxHP = enemyManager.maxHealth;

		uiBar.GetComponentInChildren<Slider>().maxValue = maxHP;
	}

	// Update is called once per frame
	void Update()
    {
		if (playerManager.isDead == true)
		{
			StartCoroutine(CancelAction());
			BossFightTrigger.instance.uniqueTilemap.gameObject.SetActive(false);
			Adalhard.SetActive(false);
			Debug.Log("Dead");
			enemyManager.health = maxHP;			
		}

		uIManager.SetBossHP(enemyManager.health);

		if(Adalhard.activeInHierarchy == true)
		{
			uiBar.SetActive(true);
		}
		else
		{
			uiBar.SetActive(false);
		}

		Vector2 lastPos = Adalhard.transform.position;
		Vector2 particlePos = lastPos - new Vector2(0, 2.2f);

		Death.transform.position = lastPos;
		particles.transform.position = particlePos;

		if(enemyManager.health >= 1)
		{
			screenChange = true;
		}
		else
		{
			screenChange = false;
		}

		if(enemyManager.health <= 0)
		{
			isDead = true;
			progressionTracker.UnlockFire();
			playerManager.currentMana = playerManager.maxMana;
		}	

		if(screenChange == false)
		{
			if (Adalhard.activeInHierarchy == false && reactivate == true)
			{
				Death.SetActive(true);
				//Instantiate(Death, lastPos, Quaternion.identity);
				StartCoroutine(DeathFX());
				trigger.SetActive(false);
			}
		}		
    }

	IEnumerator CancelAction()
	{
		AdalhardAI.instance.CancelAll();
		yield return null;
	}

	IEnumerator DeathFX()
	{
		uiBar.SetActive(false);
		reactivate = false;
		
		particles.SetActive(true);
		yield return new WaitForSeconds(3f);
		Death.SetActive(false);
		yield return new WaitForSeconds(2f);
		if (particles.GetComponent<ParticleSystem>().emission.enabled == false)
		{
			particles.SetActive(false);
		}

		progressionTracker.UnlockFire();

	}
}
