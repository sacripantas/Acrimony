using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
	public int level;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			switch (level)
			{
				case 1:
					SceneManager.LoadScene("Level1 - Chapel of The Tainted");
					break;
				case 2:
					SceneManager.LoadScene("Level2 - Iniquitous Eldenham");
					break;
			}
		}
	}
}
