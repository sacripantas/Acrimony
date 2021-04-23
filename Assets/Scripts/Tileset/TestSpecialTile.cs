using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to be incorporate into CharacterController.cs*
 */
public class TestSpecialTile : MonoBehaviour
{
    [SerializeField] LayerMask groundMask; //used to avoid raycasting colliding with player
    CircleCollider2D feetCollider;
    CharacterController controller;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        feetCollider = GetComponentInChildren<CircleCollider2D>();
        controller = GetComponent<CharacterController>();
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("s"))
            TestTile();
    }


    void TestTile() {
        RaycastHit2D hit = Physics2D.Raycast(feetCollider.bounds.center, Vector2.down, feetCollider.bounds.extents.y + 0.1f, groundMask);
        Debug.DrawRay(feetCollider.bounds.center, Vector2.down * (feetCollider.bounds.extents.y + 0.1f), Color.green);
        if(hit.collider != null) {
            try {
                SpecialTile st = hit.collider.GetComponent<SpecialTile>();
                st.ActivateSpecial();
                controller.DropDown();
            }
            catch {
                Debug.Log("Not special" + hit.collider.name);
            }
        }
    }
}
