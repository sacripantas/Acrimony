using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapManager : MonoBehaviour
{
    public static MinimapManager instance;

	[SerializeField] private GameObject smallMap;
	[SerializeField] private GameObject bigMap;
	private PauseMenuManager pauseMenu;

    [SerializeField]
    [Tooltip("List of all minimap panels")]
    private List<GameObject> rooms;

    private MiniMapStruct minimap = new MiniMapStruct();

    private List<int> teleportRooms;

    public MiniMapStruct Minimap { get => minimap; }
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
	// Start is called before the first frame update
	void Start()
    {
		pauseMenu = PauseMenuManager.instance;
        minimap.minimapString = GetMiniMapRooms();
        //minimap.teleportRooms = new int[teleportRooms.Count];
    }

    // Update is called once per frame
    void Update()
    {
		ExpandMap();   
		if(pauseMenu.isPaused == true)
		{
			smallMap.SetActive(false);
			bigMap.SetActive(false);
		}
    }

	public void ExpandMap()
	{
		if (Input.GetKey(KeyCode.Tab))
		{
			smallMap.SetActive(false);
			bigMap.SetActive(true);
		}
		else
		{
			smallMap.SetActive(true);
			bigMap.SetActive(false);
		}
	}

    public string GetMiniMapRooms() {
        string strRooms = "";
        foreach(GameObject room in rooms) {
            if (room.active) strRooms += "1";
            else strRooms += "0";
        }
        return strRooms;
    }

    public void SetMiniMapRooms(string strRooms) {
        minimap.minimapString = strRooms;
        for(int i = 0; i <strRooms.Length; i++) {
            rooms[i].SetActive(System.Convert.ToBoolean(char.GetNumericValue(strRooms[i])));
        }
    }

    public string SerializeMinimap(string[] minimaps) {
        string serializedString = "";
        foreach(string str in minimaps) {
            /*
             * Serialize a struct for teleport in multiple scenes 
             */

            serializedString += str + "#"; //delimiting each level by #
        }
        return serializedString;
    }

    public void DeserializeMinimap(string json) {
        if (json == null) return;        
        string[] minimaps = json.Split('#'); //spliting each item using the delimeter #
        for (int i = 0; i < ChoosePlayer.minimaps.Length; i++) {
            /*
             * Deserialize file in the memory for teleport in multiple scenes
             */

            ChoosePlayer.minimaps[i] = minimaps[i]; 
        }
    }
}
