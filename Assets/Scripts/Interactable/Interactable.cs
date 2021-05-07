using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour
{
    protected static InventoryHandler inventory;
    protected static PlayerManager playerMng;
    private CircleCollider2D interactionTrigger;

    protected bool canInteract = false;

    public bool CanInteract { get => canInteract; }
    // Start is called before the first frame update
    void Start()
    {
        this.interactionTrigger = GetComponent<CircleCollider2D>();
        this.interactionTrigger.isTrigger = true;
    }
    void Awake() {
        inventory = InventoryHandler.instance;
        playerMng = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            canInteract = true;
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
            canInteract = false;
    }

    //TO OVERRIDE
    public virtual void OnInteract() {
        if (canInteract) {
            Debug.Log("Interacted");
        } else {
            Debug.Log("cant interact");
        }
    }
}
