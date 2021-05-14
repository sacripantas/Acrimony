using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
	public GameObject boss;
	public GameObject door;
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
			door.SetActive(false);
			this.gameObject.SetActive(false);
		}
		if (manager.isDead)
		{
			door.SetActive(false);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			boss.SetActive(true);
			door.SetActive(true);
			Debug.Log("Start");
			//gameObject.SetActive(false);
		}
	}
}
