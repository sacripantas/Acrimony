using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
	[SerializeField] private GameObject smallMap;
	[SerializeField] private GameObject bigMap;
	private PauseMenuManager pauseMenu;
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
}
