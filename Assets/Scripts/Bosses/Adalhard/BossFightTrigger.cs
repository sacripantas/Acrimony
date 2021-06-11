using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossFightTrigger : MonoBehaviour
{
	public GameObject boss;
	//public GameObject door;
	public Tilemap fireTilemap;
	private AdalhardDeathHandler handler;
	private PlayerManager manager;

	private void Start()
	{
		handler = AdalhardDeathHandler.instance;
		manager = PlayerManager.instance;
	}


	private void Update()
	{
		if(handler.isDead == true)
		{
			fireTilemap.gameObject.SetActive(true);
			this.gameObject.SetActive(false);
		}
		if (manager.isDead)
		{
			fireTilemap.gameObject.SetActive(false);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			boss.SetActive(true);
			fireTilemap.gameObject.SetActive(true);
			Debug.Log("Start");
			//gameObject.SetActive(false);
		}
	}
}
