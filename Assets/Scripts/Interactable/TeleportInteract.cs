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
    private Teleporter teleporter;

	// Start is called before the first frame update
	void Start()
    {
		playerMng = PlayerManager.instance;
		pauseMenu = PauseMenuManager.instance;
		teleport = TeleportManager.instance;
		tracker = ProgressionTracker.instance;
		collider = GetComponent<CircleCollider2D>();

		teleportPosition = transform.position;
        teleporter = GetComponent<Teleporter>();
	}

	private new void Update()
	{
		if (isUnlocked == true)
		{
            teleporter.HasBeenSeen = true;
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
            PlayerAttack.instance.CanAttack(true);
		}
		else
		{
			menuUI.SetActive(true);
            PlayerAttack.instance.CanAttack(false);
            try {
                TeleportManager.instance.CreateRoomsButtons();
            }
            catch (System.Exception e) {
                Debug.Log(e.ToString());
            }
			isActive = true;
		}	
	}
}
