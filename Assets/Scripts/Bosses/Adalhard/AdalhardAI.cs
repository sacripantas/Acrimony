using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdalhardAI : MonoBehaviour
{
	[Header("General")]
	public Transform player;
	public bool isFacingLeft;
	private Animator anim;
	public bool flip;
	private Rigidbody2D rigid;
	private int direction;
	private DashTrail trail;
	private EnemyManager enemyManager;


	//Targeting
	[Header("Targeting")]
	public GameObject darkBall;
	public float range;
	public Transform target;
	private Vector2 playerDirection;
	public LayerMask whatIsPlayer;
	public Transform fireOrigin;
	public float force;

	//Burn
	[Header("Burn")]
	public int burnDamage = 30;
	public int burnDuration = 5;
	public float burnTick = 0.2f;

	public GameObject projectileParticles;
	public GameObject fireParticles;

	private StatusEffectManager effectManager;
	private bool isEnraged;

	public static AdalhardAI instance = null;

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


	private void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		trail = GetComponentInChildren<DashTrail>();
		enemyManager = GetComponent<EnemyManager>();
		effectManager = StatusEffectManager.instance;
	}

	private void Update()
	{
		HPStage();
		PlayerFinder();
	}

	public void CancelAll()
	{
		GameObject delete = GameObject.Find("DarkBall(Clone)");
		Destroy(delete);
		StopAllCoroutines();
		projectileParticles.SetActive(false);
		Debug.Log("Stopped");
	}

	void HPStage()
	{
		if (enemyManager.health <= enemyManager.maxHealth / 2)
		{
			anim.SetBool("IsEnraged", true);
		}
	}

	public void Enrage()
	{
		StartCoroutine(EnrageDone());
		isEnraged = true;
		fireParticles.SetActive(true);
	}
	
	IEnumerator EnrageDone()
	{
		yield return new WaitForSeconds(2f);
		anim.SetBool("EnrageDone", true);
	}

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if(transform.position.x < player.position.x && flip)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			flip = false;
			isFacingLeft = false;
			direction = 1;
		}
		else if (transform.position.x > player.position.x && !flip)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			flip = true;
			isFacingLeft = true;
			direction = -1;
		}
	}

	public void PlayerFinder()
	{
		Vector2 targetPos = target.position;
		playerDirection = targetPos - (Vector2)transform.position;

		RaycastHit2D raycast = Physics2D.Raycast(transform.position, playerDirection, range, whatIsPlayer);
	}

	void ShootProjectileBarrage()
	{
		anim.SetBool("ProjectileBarrageComplete", true);
		GameObject tempBall = Instantiate(darkBall, fireOrigin.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().duration = 1f;
		tempBall.GetComponent<Rigidbody2D>().AddForce(playerDirection * force);
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && isEnraged == true)
		{
			effectManager.Burning(burnDamage, burnDuration, burnTick);
		}
	}

	public IEnumerator Barrage()
	{
		projectileParticles.SetActive(true);
		yield return new WaitForSeconds(1.5f);

		int projectilesShot = 0;
		int totalProjectiles = 5;
		while(projectilesShot < totalProjectiles)
		{
			ShootProjectileBarrage();
			projectilesShot++;
			yield return new WaitForSeconds(0.2f);
			
		}
		yield return new WaitForSeconds(2f);
		anim.SetBool("ProjectileBarrageComplete", false);
		projectileParticles.SetActive(false);
	}

	public IEnumerator EnragedBarrage()
	{
		projectileParticles.SetActive(true);
		yield return new WaitForSeconds(1.5f);

		int projectilesShot = 0;
		int totalProjectiles = 10;
		while (projectilesShot < totalProjectiles)
		{
			ShootProjectileBarrage();
			projectilesShot++;
			yield return new WaitForSeconds(0.2f);

		}
		yield return new WaitForSeconds(2f);

		anim.SetBool("ProjectileBarrageComplete", false);
		projectileParticles.SetActive(false);
	}

	public void DashAttack()
	{
		StartCoroutine(DashStart());	
	}

	public void EnragedDashAttack()
	{
		StartCoroutine(EnragedDashStart());
	}

	IEnumerator DashStart()
	{
		yield return new WaitForSeconds(1f);
		trail.SetEnabled(true);

		yield return new WaitForSeconds(2f);

		Vector2 dashTarget = new Vector2(player.position.x, rigid.position.y);
		Vector2 newPos = Vector2.MoveTowards(rigid.position, dashTarget, 400 * Time.fixedDeltaTime);
		rigid.MovePosition(newPos);

		StartCoroutine(DashReset());
	}

	IEnumerator DashReset()
	{
		anim.SetBool("DashComplete", true);
		yield return new WaitForSeconds(0.3f);
		trail.SetEnabled(false);
		yield return new WaitForSeconds(1.5f);
		anim.SetBool("DashComplete", false);

	}

	IEnumerator EnragedDashStart()
	{
		yield return new WaitForSeconds(1f);
		trail.SetEnabled(true);

		yield return new WaitForSeconds(1.5f);

		Vector2 dashTarget = new Vector2(player.position.x, rigid.position.y);
		Vector2 newPos = Vector2.MoveTowards(rigid.position, dashTarget, 500 * Time.fixedDeltaTime);
		rigid.MovePosition(newPos);

		StartCoroutine(EnragedDashReset());
	}

	IEnumerator EnragedDashReset()
	{
		anim.SetBool("DashComplete", true);
		yield return new WaitForSeconds(0.3f);
		trail.SetEnabled(false);
		yield return new WaitForSeconds(1.5f);
		anim.SetBool("DashComplete", false);
	}
}
