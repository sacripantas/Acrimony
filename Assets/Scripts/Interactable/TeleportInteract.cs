using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInteract : NPCBehaviour
{
	[Header("Teleport")]
	public GameObject menuUI;
	public GameObject button;
	public bool isUnlocked;
	private Vector2 teleportPosition;
	public bool isActive;
	public int index;

	private PauseMenuManager pauseMenu;
	private TeleportManager teleport;
	private ProgressionTracker tracker;
	private CircleCollider2D collider;

	// Start is called before the first frame update
	void Start()
    {
		playerMng = PlayerManager.instance;
		pauseMenu = PauseMenuManager.instance;
		teleport = TeleportManager.instance;
		tracker = ProgressionTracker.instance;
		collider = GetComponent<CircleCollider2D>();

		teleportPosition = transform.position;
	}

	private new void Update()
	{
		if (isUnlocked == true)
		{
			button.SetActive(true);
		}
		else
		{
			button.SetActive(false);
		}
	}

	public override void OnInteract()
	{
		Debug.Log("Seek your next destination");
		if (isActive)
		{
			menuUI.SetActive(false);
			isActive = false;
		}
		else
		{
			menuUI.SetActive(true);
			isActive = true;
		}	
	}
}
