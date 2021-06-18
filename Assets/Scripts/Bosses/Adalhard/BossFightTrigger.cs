using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossFightTrigger : MonoBehaviour
{
	public GameObject boss;
	public Tilemap uniqueTilemap;

	public static BossFightTrigger instance = null;

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


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			boss.SetActive(true);
			uniqueTilemap.gameObject.SetActive(true);
			Debug.Log("Start");
		}
	}
}
