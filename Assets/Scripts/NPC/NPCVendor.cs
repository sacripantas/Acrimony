using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVendor : NPCBehaviour
{
    private static UIManager uiManager;

    [SerializeField]
    [Tooltip("Items to be sold")]
    private List<Item> sale;
    // Start is called before the first frame update
    
    void Start()
    {
        uiManager = UIManager.instance;
    }    

    public override void OnInteract() {
        //base.OnInteract();
        Debug.Log("Interact on vendor");
        uiManager.OpenShop(true, sale);
        this.TMPText.SetText("Come back again");        
    }
}
