using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	public float travelSpeed = 3f;
	public float damage = 5f;
	public int direction = 1;
	public int manaCost = 10;



	[SerializeField] private float duration = 2f;

	public Transform projectilePos;
	public float projectileRange;

	//References
	public PlayerAttack playerAttack;
	public CharacterController character;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(SelfDestruct());

		spriteRenderer = GetComponent<SpriteRenderer>();	
	}

    // Update is called once per frame
    void Update()
    {
	
		ProjectileMove();
	}

	public  void ProjectileMove()
	{
		transform.position = new Vector3(transform.position.x + (travelSpeed * direction * Time.deltaTime), transform.position.y, transform.position.z);

		if (direction == 1)
		{
			spriteRenderer.flipX = true;
			projectilePos.transform.localPosition = new Vector3(1.6f, -0.1f, 0);
		}
		else
		{
			spriteRenderer.flipX = false;
			projectilePos.transform.localPosition = new Vector3(-1.6f, -0.1f, 0);
		}

		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(projectilePos.position, projectileRange, playerAttack.whatIsEnemies);

		for (int i = 0; i < enemiesToDamage.Length; i++)
		{
			enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(damage);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(projectilePos.position, projectileRange);
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
}
