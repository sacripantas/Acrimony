using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFocus : MonoBehaviour
{
	public GameObject activeTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
			{
				activeTarget = gameObject.transform.GetChild(i).gameObject;
			}
		}
	}
}
