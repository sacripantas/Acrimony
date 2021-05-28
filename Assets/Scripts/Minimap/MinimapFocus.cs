using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFocus : MonoBehaviour
{
	private Transform localTarget;
	public ActiveFocus target;
	public Vector3 localPos;

    // Start is called before the first frame update
    void Start()
    {
		//target.transform.position = container.transform.position;
		
    }

    // Update is called once per frame
    void Update()
    {
		localTarget = target.activeTarget.transform;
		localPos = localTarget.position;


		transform.position = new Vector3(localPos.x, localPos.y, -10f);
	}
}
