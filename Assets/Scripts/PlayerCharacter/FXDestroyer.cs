using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(DestroyUnit());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator DestroyUnit()
	{
		yield return new WaitForSeconds(0.4f);
		Destroy(gameObject);
	}
}
