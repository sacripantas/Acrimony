using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyManager : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	//References
	public AIDestinationSetter destinationSetter;
	public AIPath aiPath;
	private StatusEffectManager effectManager;

	public bool canPoison;
	public bool canBurn;

	public int poisonDmg = 10;
	public int poisonDuration = 5;
	public float poisonTickRate = 1f;

	public int burnDmg = 10;
	public int burnDuration = 5;
	public float burnTickRate = 1f;

	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
		effectManager = StatusEffectManager.instance;
	}

    // Update is called once per frame
    void Update()
    {
		if (aiPath.desiredVelocity.x >= 0.01)
		{
			spriteRenderer.flipX = false;
		}
		else
		{
			spriteRenderer.flipX = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (canPoison)
			{
				effectManager.Burning(burnDmg, burnDuration, burnTickRate);
			}
			else if (canBurn)
			{
				effectManager.Poisoned(poisonDmg, poisonDuration, poisonTickRate);
			}
		}
	}
}
