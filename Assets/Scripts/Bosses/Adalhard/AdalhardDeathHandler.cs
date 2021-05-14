using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdalhardDeathHandler : MonoBehaviour
{
	public GameObject Adalhard;
	public GameObject Death;
	private Animator anim;
	public GameObject particles;
	public GameObject uiBar;
	private bool reactivate = true;
	private ProgressionTracker progressionTracker;
	private UIManager uIManager;
	public bool screenChange = true;
	private PlayerManager playerManager;
	private EnemyManager enemyManager;

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
		maxHP = enemyManager.health;
    }

	// Update is called once per frame
	void Update()
    {
		if (playerManager.isDead == true)
		{
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
		}	

		if(screenChange == false)
		{
			if (Adalhard.activeInHierarchy == false && reactivate == true)
			{
				Death.SetActive(true);
				//Instantiate(Death, lastPos, Quaternion.identity);
				StartCoroutine(DeathFX());
			}
		}		
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
