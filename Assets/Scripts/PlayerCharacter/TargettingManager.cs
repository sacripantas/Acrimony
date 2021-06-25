using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingManager : MonoBehaviour
{
	//Line Renderer
	private LineRenderer line;
	private Color startColor = Color.white;

	//Range
	public float range = 6f;
	//public LayerMask whatIsEnemies;

	public bool inRange = false;

	public Vector2 targettedEnemy;

	//Bullet
	public GameObject bullet;
	public Vector2 direction;
	public float force = 100f;

	private PlayerManager playerManager;
	public int localAmmo;
	private ProgressionTracker tracker;

    // Start is called before the first frame update
    void Start()
    {
		line = GetComponent<LineRenderer>();
		playerManager = PlayerManager.instance;
		tracker = ProgressionTracker.instance;
		
    }

	// Update is called once per frame
	void Update()
	{
		if(tracker.unlockGun == true)
		{
			FindClosestEnemy();
			FindBoss();
			FindWall();
			localAmmo = playerManager.currentAmmo;
		}		
	}

	void FindWall()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		GameObject closestEnemy = null;

		GameObject[] wall = GameObject.FindGameObjectsWithTag("GunWall");

		line.startWidth = 0.1f;
		line.startColor = startColor;

		if (wall.Length == 0)
		{
			inRange = false;
			line.enabled = false;
		}
		else
		{
			foreach (GameObject currentEnemy in wall)
			{
				float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToEnemy < distanceToClosestEnemy)
				{
					distanceToClosestEnemy = distanceToEnemy;
					closestEnemy = currentEnemy;
				}

				if (distanceToClosestEnemy <= range * 6.5f)
				{
					inRange = true;
					line.enabled = true;

					targettedEnemy = closestEnemy.transform.position;
				}
				else
				{
					inRange = false;
					line.enabled = false;
				}
			}

			if (inRange == true)
			{
				line.SetPosition(0, transform.position);
				line.SetPosition(1, closestEnemy.transform.position);
				line.useWorldSpace = true;
			}

			direction = targettedEnemy - (Vector2)transform.position;

			if (Input.GetKeyDown(KeyCode.P) && inRange == true && localAmmo > 0)
			{
				GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
				tempBullet.GetComponent<Rigidbody2D>().AddForce(direction * force);
				playerManager.SpendAmmo(1);
			}

			if (Input.GetKeyDown(KeyCode.P) && localAmmo == 0)
			{
				Debug.Log("No ammo");
			}

			if (Input.GetKeyDown(KeyCode.P) && inRange == false)
			{
				Debug.Log("No enemies in range");
			}

			Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
		}
	}

	void FindBoss()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		GameObject closestEnemy = null;

		GameObject[] ricmod = GameObject.FindGameObjectsWithTag("Ricmod");

		line.startWidth = 0.1f;
		line.startColor = startColor;

		if (ricmod.Length == 0)
		{
			inRange = false;
			line.enabled = false;
		}
		else
		{
			foreach (GameObject currentEnemy in ricmod)
			{
				float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToEnemy < distanceToClosestEnemy)
				{
					distanceToClosestEnemy = distanceToEnemy;
					closestEnemy = currentEnemy;
				}

				if (distanceToClosestEnemy <= range * 6.5f)
				{
					inRange = true;
					line.enabled = true;

					targettedEnemy = closestEnemy.transform.position;
				}
				else
				{
					inRange = false;
					line.enabled = false;
				}
			}

			if (inRange == true)
			{
				line.SetPosition(0, transform.position);
				line.SetPosition(1, closestEnemy.transform.position);
				line.useWorldSpace = true;
			}

			direction = targettedEnemy - (Vector2)transform.position;

			if (Input.GetKeyDown(KeyCode.P) && inRange == true && localAmmo > 0)
			{
				GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
				tempBullet.GetComponent<Rigidbody2D>().AddForce(direction * force);
				playerManager.SpendAmmo(1);
			}

			if (Input.GetKeyDown(KeyCode.P) && localAmmo == 0)
			{
				Debug.Log("No ammo");
			}

			if (Input.GetKeyDown(KeyCode.P) && inRange == false)
			{
				Debug.Log("No enemies in range");
			}

			Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
		}
	}

	void FindClosestEnemy()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		GameObject closestEnemy = null;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		line.startWidth = 0.1f;
		line.startColor = startColor;

		if(enemies.Length == 0)
		{
			inRange = false;
			line.enabled = false;
		}
		else
		{
			foreach (GameObject currentEnemy in enemies)
			{
				float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToEnemy < distanceToClosestEnemy)
				{
					distanceToClosestEnemy = distanceToEnemy;
					closestEnemy = currentEnemy;
				}

				if (distanceToClosestEnemy <= range * 6.5f)
				{
					inRange = true;
					line.enabled = true;

					targettedEnemy = closestEnemy.transform.position;
				}
				else
				{
					inRange = false;
					line.enabled = false;
				}
			}

			if (inRange == true)
			{
				line.SetPosition(0, transform.position);
				line.SetPosition(1, closestEnemy.transform.position);
				line.useWorldSpace = true;
			}

			direction = targettedEnemy - (Vector2)transform.position;

			if (Input.GetKeyDown(KeyCode.P) && inRange == true && localAmmo > 0)
			{
				GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
				tempBullet.GetComponent<Rigidbody2D>().AddForce(direction * force);
				playerManager.SpendAmmo(1);
			}

			if(Input.GetKeyDown(KeyCode.P) && localAmmo == 0)
			{
				Debug.Log("No ammo");
			}

			if (Input.GetKeyDown(KeyCode.P) && inRange == false)
			{
				Debug.Log("No enemies in range");
			}

			Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
		}		
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, range);
	}

}
