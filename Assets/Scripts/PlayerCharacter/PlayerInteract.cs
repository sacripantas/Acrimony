using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask; //used to avoid raycasting colliding with player
    BoxCollider2D collider;
    CharacterController controller;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
        controller = GetComponent<CharacterController>();
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //experimental only, to be changed after
        if (Input.GetButtonDown("Interact")) {
            IsInteractable();
        }
    }

    void IsInteractable() {
       
        RaycastHit2D hit;
        if (controller.hMove < 0) { //player looking left
            hit = Physics2D.Raycast(collider.bounds.center, Vector2.left, 5f, interactableMask);
            Debug.DrawRay(collider.bounds.center, Vector2.left * 5f, Color.red);
        } else { //player is looking right
            hit = Physics2D.Raycast(collider.bounds.center, Vector2.right, 5f, interactableMask);
            Debug.DrawRay(collider.bounds.center, Vector2.right * 5f, Color.red);
        }

        if (hit.collider != null) {
            try {
                Interactable obj = hit.collider.GetComponent<Interactable>();
                if (obj.CanInteract)
                    obj.OnInteract();
                else
                    Debug.Log("Interactable but cant interact right now");
            }
            catch {
                
            }
        }        
    }
}
