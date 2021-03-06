using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueRicmodAI : MonoBehaviour
{
	[Header("General")]
	public Transform player;
	private bool isAttacking;
	private bool canChase;
	public float timeElapsed;
	public float cooldown;
	public int index;
	public int lastAttack;
	public Transform roomOrigin;
	public CircleCollider2D circleCollider2D;

	[Header("SpamProtection")]
	private int counter0;
	private int counter1;
	private int counter2;
	private int counter3;
	private int counter4;
	private int counter5;
	private int counter6;
	private int counter7;

	[Header("Animators")]
	public Animator columnAnim;
	public Animator waveAnim;
	public Animator crosshairAnim;
	public Animator rotateAnim;

	[Header("Projectiles")]
	public GameObject darkBall;
	public GameObject redBall;

	[Header("Particles")]
	public ParticleSystem originParticles;
	public ParticleSystem projectileParticles;
	public ParticleSystem columnParticlesA;
	public ParticleSystem columnParticlesB;
	public ParticleSystem waveParticlesA;
	public ParticleSystem waveParticlesB;
	public ParticleSystem waveParticlesC;
	public ParticleSystem projectileParticlesA;
	public ParticleSystem projectileParticlesB;
	public ParticleSystem crosshairParticlesA;
	public ParticleSystem crosshairParticlesB;
	public ParticleSystem crosshairParticlesC;
	public ParticleSystem crosshairParticlesD;
	public ParticleSystem crosshairParticlesE;
	public ParticleSystem crosshairParticlesF;
	public ParticleSystem crosshairParticlesG;
	public ParticleSystem crosshairParticlesH;
	public ParticleSystem hitParticles;
	public ParticleSystem rotateParticlesA;
	public ParticleSystem rotateParticlesB;
	public ParticleSystem rotateParticlesC;
	public ParticleSystem rotateParticlesD;

	[Header("FireOrigins")]
	public Transform fireOrigin;
	public Transform originColumnA;
	public Transform originColumnB;
	public Transform originWaveA;
	public Transform originWaveB;
	public Transform originWaveC;
	public Transform originProjectileA;
	public Transform originProjectileB;
	public Transform originCrosshair;
	public Transform originRotateA;
	public Transform originRotateB;
	public Transform originRotateC;
	public Transform originRotateD;

	[Header("Targeting")]
	public float range;
	public Transform target;
	public Transform centre;
	private Vector2 targetPos;
	private Vector2 playerDirection;
	public LayerMask whatIsPlayer;
	public float force;

	public static TrueRicmodAI instance = null;

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
		index = -1;
		cooldown = 2f;
	}

	// Update is called once per frame
	void Update()
	{
		PlayerFinder();
		AttackSelection();

		if (canChase)
		{
			CrosshairTargetting();
		}
	}

	public void PlayerFinder()
	{
		targetPos = target.position;
		playerDirection = targetPos - (Vector2)transform.position;


		RaycastHit2D raycast = Physics2D.Raycast(transform.position, playerDirection, range, whatIsPlayer);
	}

	void AttackSelection()
	{
		if (timeElapsed >= cooldown && isAttacking == false)
		{
			index = Random.Range(0, 8);
			Debug.Log(index);
			timeElapsed = 0;
			AttackHandler();
		}
		else
		{
			timeElapsed += Time.deltaTime;
		}
	}

	void AttackHandler()
	{
		switch (index)
		{
			case 0:
				counter0++;
				if (counter0 >= 3)
				{
					counter0 = 0;
					index = 1;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireProjectile());
				}
				break;
			case 1:
				counter1++;
				if (counter1 >= 3)
				{
					counter1 = 0;
					index = 2;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireColumns());
				}
				break;
			case 2:
				counter2++;
				if (counter2 >= 3)
				{
					counter2 = 0;
					index = 3;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireMultipleProjectiles());
				}
				break;
			case 3:
				counter3++;
				if (counter3 >= 3)
				{
					counter3 = 0;
					index = 4;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireWave());
				}
				break;
			case 4:
				counter4++;
				if (counter4 >= 3)
				{
					counter4 = 0;
					index = 5;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireCrosshair());
				}
				break;
			case 5:
				counter5++;
				if (counter5 >= 3)
				{
					counter5 = 0;
					index = 6;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireCircle());
				}
				break;
			case 6:
				counter6++;
				if (counter6 >= 3)
				{
					counter6 = 0;
					index = 0;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireComplexCrosshair());
				}
				break;
			case 7:
				counter7++;
				if (counter7 >= 3)
				{
					counter7 = 0;
					index = 0;
					AttackHandler();
				}
				else
				{
					StartCoroutine(FireComplexCircle());
				}
				break;
		}
	}

	public void CancelAll()
	{
		CancelInvoke();
		GameObject delete = GameObject.Find("DarkBall(Clone)");
		GameObject delete2 = GameObject.Find("RedBall(Clone)");
		Destroy(delete);
		Destroy(delete2);
		isAttacking = false;
		timeElapsed = 0;
		cooldown = 2f;
		Debug.Log("Stopped");
	}


	void ShootProjectile()
	{
		GameObject tempBall = Instantiate(darkBall, fireOrigin.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().damage = 5;
		tempBall.GetComponent<DarkBall>().duration = 0.8f;
		tempBall.GetComponent<Rigidbody2D>().AddForce(playerDirection * force);
	}

	void ShootMultipleProjectiles()
	{
		GameObject tempBall = Instantiate(darkBall, originProjectileA.position, Quaternion.identity);
		GameObject tempBall2 = Instantiate(darkBall, originProjectileB.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().damage = 5;
		tempBall.GetComponent<DarkBall>().duration = 0.8f;
		tempBall.GetComponent<Rigidbody2D>().AddForce((originProjectileA.transform.position - target.position) * -force);
		tempBall2.GetComponent<DarkBall>().damage = 5;
		tempBall2.GetComponent<DarkBall>().duration = 0.8f;
		tempBall2.GetComponent<Rigidbody2D>().AddForce((originProjectileB.transform.position - target.position) * -force);
	}

	void ShootColumns()
	{
		GameObject tempBall = Instantiate(redBall, originColumnA.position, Quaternion.identity);
		GameObject tempBall2 = Instantiate(redBall, originColumnB.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().damage = 20;
		tempBall.GetComponent<DarkBall>().duration = 3f;
		tempBall.GetComponent<Rigidbody2D>().AddForce(-transform.up * force * 10);
		tempBall2.GetComponent<DarkBall>().damage = 20;
		tempBall2.GetComponent<DarkBall>().duration = 3f;
		tempBall2.GetComponent<Rigidbody2D>().AddForce(-transform.up * force * 10);
	}

	void ShootWave()
	{
		GameObject tempBall = Instantiate(redBall, originWaveA.position, Quaternion.identity);
		GameObject tempBall2 = Instantiate(redBall, originWaveB.position, Quaternion.identity);
		GameObject tempBall3 = Instantiate(redBall, originWaveC.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().damage = 20;
		tempBall.GetComponent<DarkBall>().duration = 3f;
		tempBall.GetComponent<Rigidbody2D>().AddForce((originWaveA.transform.position - roomOrigin.transform.position) * -force);
		tempBall2.GetComponent<DarkBall>().damage = 20;
		tempBall2.GetComponent<DarkBall>().duration = 3f;
		tempBall2.GetComponent<Rigidbody2D>().AddForce((originWaveB.transform.position - roomOrigin.transform.position) * -force);
		tempBall3.GetComponent<DarkBall>().damage = 20;
		tempBall3.GetComponent<DarkBall>().duration = 3f;
		tempBall3.GetComponent<Rigidbody2D>().AddForce((originWaveC.transform.position - roomOrigin.transform.position) * -force);
	}

	void CrosshairTargetting()
	{
		originCrosshair.transform.position = Vector3.MoveTowards(originCrosshair.transform.position, centre.position, 5f);
	}

	void ShootCircle()
	{
		GameObject tempBall = Instantiate(redBall, originRotateA.position, Quaternion.identity);
		GameObject tempBall2 = Instantiate(redBall, originRotateB.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().damage = 20;
		tempBall.GetComponent<DarkBall>().duration = 2f;
		tempBall.GetComponent<Rigidbody2D>().AddForce((originRotateA.transform.position - projectileParticles.transform.position) * (-force / 2));
		tempBall2.GetComponent<DarkBall>().damage = 20;
		tempBall2.GetComponent<DarkBall>().duration = 2f;
		tempBall2.GetComponent<Rigidbody2D>().AddForce((originRotateB.transform.position - projectileParticles.transform.position) * (-force / 2));
	}

	void ShootComplexCircle()
	{
		GameObject tempBall = Instantiate(redBall, originRotateA.position, Quaternion.identity);
		GameObject tempBall2 = Instantiate(redBall, originRotateB.position, Quaternion.identity);
		GameObject tempBall3 = Instantiate(redBall, originRotateC.position, Quaternion.identity);
		GameObject tempBall4 = Instantiate(redBall, originRotateD.position, Quaternion.identity);
		tempBall.GetComponent<DarkBall>().damage = 20;
		tempBall.GetComponent<DarkBall>().duration = 1.1f;
		tempBall.GetComponent<Rigidbody2D>().AddForce((originRotateA.transform.position - projectileParticles.transform.position) * (-force / 2));
		tempBall2.GetComponent<DarkBall>().damage = 20;
		tempBall2.GetComponent<DarkBall>().duration = 1.1f;
		tempBall2.GetComponent<Rigidbody2D>().AddForce((originRotateB.transform.position - projectileParticles.transform.position) * (-force / 2));
		tempBall3.GetComponent<DarkBall>().damage = 20;
		tempBall3.GetComponent<DarkBall>().duration = 1.1f;
		tempBall3.GetComponent<Rigidbody2D>().AddForce((originRotateC.transform.position - projectileParticles.transform.position) * (-force / 2));
		tempBall4.GetComponent<DarkBall>().damage = 20;
		tempBall4.GetComponent<DarkBall>().duration = 1.1f;
		tempBall4.GetComponent<Rigidbody2D>().AddForce((originRotateD.transform.position - projectileParticles.transform.position) * (-force / 2));
	}


	IEnumerator FireProjectile()
	{
		isAttacking = true;
		cooldown = 5f;
		projectileParticles.Play();
		yield return new WaitForSeconds(2f);
		projectileParticles.Stop();
		InvokeRepeating("ShootProjectile", 0f, 0.01f);
		yield return new WaitForSeconds(3f);
		isAttacking = false;
		CancelInvoke("ShootProjectile");
	}

	IEnumerator FireColumns()
	{
		isAttacking = true;
		cooldown = 6f;
		columnParticlesA.Play();
		columnParticlesB.Play();
		yield return new WaitForSeconds(2f);
		columnAnim.Play("TrueColumns");
		columnParticlesA.Stop();
		columnParticlesB.Stop();
		InvokeRepeating("ShootColumns", 0f, 0.01f);
		yield return new WaitForSeconds(4f);
		isAttacking = false;
		CancelInvoke("ShootColumns");
	}

	IEnumerator FireWave()
	{
		isAttacking = true;
		cooldown = 5f;
		waveParticlesA.Play();
		waveParticlesB.Play();
		waveParticlesC.Play();
		originParticles.Play();
		yield return new WaitForSeconds(2f);
		waveAnim.Play("TrueWaves");
		InvokeRepeating("ShootWave", 0f, 0.01f);
		yield return new WaitForSeconds(2f);
		waveParticlesA.Stop();
		waveParticlesB.Stop();
		waveParticlesC.Stop();
		originParticles.Stop();
		isAttacking = false;
		CancelInvoke("ShootWave");
	}

	IEnumerator FireMultipleProjectiles()
	{
		isAttacking = true;
		cooldown = 5f;
		projectileParticlesA.Play();
		projectileParticlesB.Play();
		yield return new WaitForSeconds(2f);
		projectileParticlesA.Stop();
		projectileParticlesB.Stop();
		InvokeRepeating("ShootMultipleProjectiles", 0f, 0.01f);
		yield return new WaitForSeconds(3f);
		isAttacking = false;
		CancelInvoke("ShootMultipleProjectiles");
	}

	IEnumerator FireCrosshair()
	{
		isAttacking = true;
		cooldown = 4f;
		canChase = true;
		crosshairParticlesA.Play();
		yield return new WaitForSeconds(1f);
		crosshairParticlesB.Play();
		yield return new WaitForSeconds(1f);
		crosshairParticlesC.Play();
		yield return new WaitForSeconds(1f);
		crosshairParticlesD.Play();
		yield return new WaitForSeconds(1f);
		crosshairAnim.Play("RICMODCrosshair");
		crosshairParticlesA.Stop();
		crosshairParticlesB.Stop();
		crosshairParticlesC.Stop();
		crosshairParticlesD.Stop();
		yield return new WaitForSeconds(0.4f);
		circleCollider2D.enabled = true;
		hitParticles.Play();
		yield return new WaitForSeconds(0.1f);
		isAttacking = false;
		canChase = false;
		circleCollider2D.enabled = false;
	}

	IEnumerator FireComplexCrosshair()
	{
		isAttacking = true;
		cooldown = 5f;
		canChase = true;
		crosshairParticlesA.Play();
		yield return new WaitForSeconds(0.2f);
		crosshairParticlesE.Play();
		yield return new WaitForSeconds(0.2f);
		crosshairParticlesB.Play();
		yield return new WaitForSeconds(0.2f);
		crosshairParticlesH.Play();
		yield return new WaitForSeconds(0.2f);
		crosshairParticlesC.Play();
		yield return new WaitForSeconds(0.2f);
		crosshairParticlesG.Play();
		yield return new WaitForSeconds(0.2f);
		crosshairParticlesD.Play();
		yield return new WaitForSeconds(0.2f);		
		crosshairParticlesF.Play();
		yield return new WaitForSeconds(0.2f);		
		crosshairAnim.Play("TrueCrosshair");
		crosshairParticlesA.Stop();
		crosshairParticlesB.Stop();
		crosshairParticlesC.Stop();
		crosshairParticlesD.Stop();
		crosshairParticlesE.Stop();
		crosshairParticlesF.Stop();
		crosshairParticlesG.Stop();
		crosshairParticlesH.Stop();
		yield return new WaitForSeconds(0.4f);
		circleCollider2D.enabled = true;
		hitParticles.Play();
		yield return new WaitForSeconds(0.1f);
		isAttacking = false;
		canChase = false;
		circleCollider2D.enabled = false;
	}

	IEnumerator FireCircle()
	{
		isAttacking = true;
		cooldown = 5f;
		rotateParticlesA.Play();
		rotateParticlesB.Play();
		yield return new WaitForSeconds(2f);
		rotateAnim.Play("TrueCircle");
		InvokeRepeating("ShootCircle", 0f, 0.01f);
		yield return new WaitForSeconds(3f);
		rotateParticlesA.Stop();
		rotateParticlesB.Stop();
		isAttacking = false;
		CancelInvoke("ShootCircle");
	}

	IEnumerator FireComplexCircle()
	{
		isAttacking = true;
		cooldown = 6f;
		rotateParticlesA.Play();
		rotateParticlesB.Play();
		rotateParticlesC.Play();
		rotateParticlesD.Play();
		yield return new WaitForSeconds(2f);
		rotateAnim.Play("TrueCircle");
		InvokeRepeating("ShootComplexCircle", 0f, 0.01f);
		yield return new WaitForSeconds(3f);
		rotateParticlesA.Stop();
		rotateParticlesB.Stop();
		rotateParticlesC.Stop();
		rotateParticlesD.Stop();
		isAttacking = false;
		CancelInvoke("ShootComplexCircle");
	}
}
