using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject virtualCamera;

	public GameObject[] enemies;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("PlayerBody") && !other.isTrigger)
		{
			virtualCamera.SetActive(true);
			
			foreach(GameObject enemy in enemies)
			{
				enemy.SetActive(true);
			}

		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("PlayerBody") && !other.isTrigger)
		{
			virtualCamera.SetActive(false);

			foreach (GameObject enemy in enemies)
			{
				enemy.SetActive(false);
			}
		}
	}
}