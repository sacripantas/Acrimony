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
            GameManager.instance.SetMinimapString();
            SceneManager.LoadScene(level);            
		}
	}
}
