using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField] public Transform target;

	[SerializeField] private Vector3 offset;

	private Vector3 velocity = Vector3.zero;

	[SerializeField] [Range(0.01f , 1f)] private float smoothSpeed = 0.125f;

	private void FixedUpdate()
	{
		Vector3 desiredPos = target.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
	}
}
