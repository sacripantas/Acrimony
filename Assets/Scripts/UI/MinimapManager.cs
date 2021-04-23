using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
	[SerializeField] private GameObject smallMap;
	[SerializeField] private GameObject bigMap;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		ExpandMap();   
    }

	void ExpandMap()
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
