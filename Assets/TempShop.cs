using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShop : MonoBehaviour
{
	[SerializeField] PlayerManager playerManager;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Debug.Log(" Y pressed");
			if(playerManager.currentMoney >= 10)
			{
				playerManager.SpendMoney(10);
				playerManager.ReceiveAmmo(5);
				Debug.Log("Thank you for your purchase!");
			}
			else
			{
				Debug.Log("You need more money!");
			}
		}
	}
}
