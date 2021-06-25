using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleportManager : MonoBehaviour
{
	public GameObject[] teleportRooms;
	public static TeleportManager instance = null;
	private PlayerManager playerManager;

    [SerializeField]
    [Tooltip("Prefab for rooms list buttons")]
    private GameObject btnRoom;

    [SerializeField]
    [Tooltip("Prefab for portal list buttons")]
    private GameObject btnPortal;

    [SerializeField]
    private List<GameObject> roomsList;
    [SerializeField]
    private List<GameObject> portalsLists;

    [SerializeField]
    private GameObject roomsPanel;
    [SerializeField]
    private GameObject portalsPanel;

    [SerializeField]
    private TextMeshProUGUI title;
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

    public void SetTitle(int scene) {
        this.title.SetText(LevelNames.levelName[scene]);
    }

	public void Teleport(int index)
	{
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
    public void ClearPortalList() {
        foreach(GameObject g in portalsLists) {
            Destroy(g);
        }
        portalsLists.Clear();
    }

    //create a list of available portals
    public void SetPortals() {
        ClearPortalList();
        int pos = 0, i = 0;
        GameObject btn = null;
        foreach (GameObject portal in roomsList) {
            btn = Instantiate(btnPortal);
            btn.GetComponent<AvailablePortals>().SetBtn(portal.GetComponent<TeleportInteract>(), i);
            btn.transform.SetParent(portalsPanel.transform);
            btn.GetComponent<AvailablePortals>().SetPosition(pos);
            pos += 50;
            portalsLists.Add(btn);
            i++;
        }
    }

    //Create a list of buttons of all available rooms
    public void CreateRoomsButtons() {
        int pos = 0;
        GameObject btn = null;
        for(int i = 1; i < ChoosePlayer.minimaps.Length+1; i++) {
            btn = Instantiate(btnRoom);
            btn.GetComponent<TeleportButton>().SetBtn(i);  
            btn.transform.SetParent(roomsPanel.transform);
            btn.GetComponent<TeleportButton>().SetPosition(pos);
            pos += 50;
            roomsList.Add(btn);
        }
    }
}
