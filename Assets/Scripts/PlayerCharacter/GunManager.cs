using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
	public GameObject bullet;
	public float force = 2f;

	private Vector2 direction;


	[SerializeField] private TargettingManager targetting;
	public ProgressionTracker tracker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		FireGun();
    }

	void FireGun()
	{
		if (tracker.unlockGun == true)
		{
			direction = targetting.targettedEnemy;

			if (Input.GetKeyDown(KeyCode.P))
			{
				GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
				bullet.GetComponent<Rigidbody2D>().AddForce(direction * force);
			}
		}
	}
}
