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

	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
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
}
