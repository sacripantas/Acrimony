using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DestructibleTilesFire : MonoBehaviour
{
	public Tilemap tilemap;

	// Start is called before the first frame update
	void Start()
	{
		tilemap = GetComponent<Tilemap>();
		tilemap.color = new Color32(152, 32, 15, 255);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "FireBall(Clone)")
		{
			Vector3 hitPos = Vector3.zero;
			foreach (ContactPoint2D hit in collision.contacts)
			{
				hitPos.x = hit.point.x - 0.001f * hit.normal.x;
				hitPos.y = hit.point.y - 0.001f * hit.normal.y;
				tilemap.SetTile(tilemap.WorldToCell(hitPos), null);
			}
			Destroy(collision.gameObject);
		}
	}
}
