using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	public float travelSpeed = 3f;
	public int direction = 1;

	[SerializeField] private float duration = 2f;
	public float projectileRange;
	public float damage;
	public int manaCost;
	public Transform hitBox;

	//References
	private PlayerAttack playerAttack;
	private CharacterController character;
	public ProjectileManager projectileManager;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SelfDestruct());

		playerAttack = PlayerAttack.instance;
		character = CharacterController.instance;

		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		ProjectileMove();
	}

	public void ProjectileMove()
	{
		transform.position = new Vector3(transform.position.x + (travelSpeed * direction * Time.deltaTime), transform.position.y, transform.position.z);

		if (direction == 1)
		{
			spriteRenderer.flipX = true;
			hitBox.transform.localPosition = new Vector3(1.6f, -0.1f, 0);
		}
		else
		{
			spriteRenderer.flipX = false;
			hitBox.transform.localPosition = new Vector3(-1.6f, -0.1f, 0);
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Debug.Log("Hit");
			collision.GetComponent<EnemyManager>().TakeDamage(damage);
		}
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
}
