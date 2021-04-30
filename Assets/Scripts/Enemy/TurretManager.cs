using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
	//General
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	public LayerMask whatIsPlayer;
	public PlayerManager playerManager;

	//Bullet
	public GameObject darkBall;
	public float fireRate;
	float nextFireTime = 0f;
	public Transform fireOrigin;
	public float force;
	public int damage = 5;
	public float duration = 2f; 

	//Targeting
	public float range;
	public Transform target;
	private bool inRange = false;
	private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		Vector2 targetPos = target.position;
		direction = targetPos - (Vector2)transform.position;

		if(direction.x > 0)
		{
			spriteRenderer.flipX = false;
		}
		else
		{
			spriteRenderer.flipX = true;
		}

		RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, range, whatIsPlayer);

		if (raycast)
		{
			if (raycast.collider.gameObject.CompareTag("Player"))
			{
				if (inRange == false)
				{
					inRange = true;
					animator.SetBool("isInAttackRange", true);
				}
			}
		}
		else
		{
			if (inRange == true)
			{
				inRange = false;
				animator.SetBool("isInAttackRange", false);
			}
		}
		if (inRange)
		{
			if(Time.time > nextFireTime)
			{
				nextFireTime = Time.time + 1 / fireRate;
				Shoot();
			}
		}
	}

	public void Shoot()
	{
		GameObject tempBall = Instantiate(darkBall, fireOrigin.position, Quaternion.identity);
		tempBall.GetComponent<Rigidbody2D>().AddForce(direction * force);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
