using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class RoomManager : MonoBehaviour
{
	[Header("Camera")]
	public GameObject virtualCamera;

	[Header("Minimap Panels")]
	public GameObject lockedPanel;
	public GameObject activePanel;

	//List of enemies per room
	public GameObject[] enemies;

	//General
	private CharacterController characterController;
	private Rigidbody2D localRigid;
	private Animator localAnimator;

	private void Start()
	{
		characterController = CharacterController.instance;
		localRigid = characterController.GetComponent<Rigidbody2D>();
		localAnimator = characterController.GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("TransitionCheck") && !other.isTrigger)
		{
			lockedPanel.SetActive(false);
			activePanel.SetActive(true);
			virtualCamera.SetActive(true);
			
			foreach(GameObject enemy in enemies)
			{
				enemy.SetActive(true);
				enemy.transform.localPosition = enemy.GetComponent<EnemyManager>().originalPos;
				enemy.GetComponent<EnemyManager>().health = enemy.GetComponent<EnemyManager>().maxHealth;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("TransitionCheck") && !other.isTrigger)
		{
			Time.timeScale = 0.0001f;
			localAnimator.speed = 0f;
			
			StartCoroutine(RoomTransition());
			virtualCamera.SetActive(false);
			activePanel.SetActive(false);

			foreach (GameObject enemy in enemies)
			{
				enemy.SetActive(false);
			}			
		}
	}

	IEnumerator RoomTransition()
	{
		yield return new WaitForSecondsRealtime(0.4f);
		Time.timeScale = 1f;
		localAnimator.speed = 1f;
	}
}