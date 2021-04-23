using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public float health = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeDamage(float damage)
	{
		health -= damage;
		Debug.Log("Damage taken");
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
