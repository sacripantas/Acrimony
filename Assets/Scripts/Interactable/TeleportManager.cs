using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
	public GameObject[] teleportRooms;
	public static TeleportManager instance = null;
	private PlayerManager playerManager;

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

	// Start is called before the first frame update
	void Start()
    {
		teleportRooms = GameObject.FindGameObjectsWithTag("Teleport");
		playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void Teleport(int index)
	{
		Debug.Log("Teleport");
		switch (index)
		{
			case 0:
				playerManager.transform.position = teleportRooms[0].transform.position;
				break;
			case 1:
				playerManager.transform.position = teleportRooms[1].transform.position;
				break;
		}		
	}
}
