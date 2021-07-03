using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private static GameManager manager;
	private PauseMenuManager pauseMenuManager;
	public static PlayerAttack instance = null;

	//General
	[Header("General")]
	public LayerMask whatIsEnemies;
	public CharacterController character;
	public GameObject attackArea;
	public Transform attackPos;
	public ParticleSystem swipeFX;
	public ParticleSystem lungeFX;
    private bool canAttack = true;

	//Swipe
	[Header("Swipe Attack")]
	public float damage;
	private float timeBtwAttack;
	public float startTimeBtwAttack;
	public float attackRangeX;
	public float attackRangeY;


	//Lunge
	[Header("Lunge Attack")]
	public float damageLunge;
	private float timeBtwLungeAttack;
	public float startTimeBtwLungeAttack;
	public float attackLungeRangeX;
	public float attackLungeRangeY;

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
        manager = GameManager.instance;
		pauseMenuManager = PauseMenuManager.instance;
    }
	// Update is called once per frame
	void Update()
    {        
		if(!manager.isPaused)
		{
            if (canAttack) {
                SwipeAttack();
                LungeAttack();
            }
		}		

		if (character.direction == 1)
		{
			attackPos.transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		else
		{
			attackPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

    public void CanAttack(bool flag) {
        canAttack = flag;
    }

	public void SwipeAttack()
	{
		if (timeBtwAttack <= 0)
		{
			if (Input.GetButtonDown("SwipeAttack"))
			{

				Debug.Log("Attack");

				//attackPos.transform.localScale = new Vector3(0, 5, 1);
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY), 0, whatIsEnemies);

				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
					if(enemiesToDamage[i].tag == "Enemy")
					{
						enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damage);
					}
					else if(enemiesToDamage[i].tag == "Ricmod")
					{
						enemiesToDamage[i].GetComponent<RicmodManager>().TakeDamage(damage);
					}					
				}
				swipeFX.Play();
				//attackArea.SetActive(true);		

				timeBtwAttack = startTimeBtwAttack;
			}			
		}
		else
		{
			timeBtwAttack -= Time.deltaTime;
			//attackArea.SetActive(false);
		}
	}

	public void LungeAttack()
	{
		if (timeBtwLungeAttack <= 0)
		{
			if (Input.GetButtonDown("LungeAttack"))
			{

				//attackPos.transform.localScale = new Vector3(7, 3, 1);
				Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackLungeRangeX, attackLungeRangeY), 0, whatIsEnemies);

				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
					if (enemiesToDamage[i].tag == "Enemy")
					{
						enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damage);
					}
					else if (enemiesToDamage[i].tag == "Ricmod")
					{
						enemiesToDamage[i].GetComponent<RicmodManager>().TakeDamage(damage);
					}
				}
				lungeFX.Play();
				//attackArea.SetActive(true);

				timeBtwLungeAttack = startTimeBtwLungeAttack;
			}
		}
		else
		{
			timeBtwLungeAttack -= Time.deltaTime;
			//attackArea.SetActive(false);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 0f));

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(attackPos.position, new Vector3(attackLungeRangeX, attackLungeRangeY, 0f));
	}
}
