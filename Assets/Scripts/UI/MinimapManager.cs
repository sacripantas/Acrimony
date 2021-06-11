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
        Debug.Log(strRooms.Length);
        for(int i = 0; i <strRooms.Length; i++) {
            rooms[i].SetActive(System.Convert.ToBoolean(char.GetNumericValue(strRooms[i])));
        }
    }
}
