using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCVendor : NPCBehaviour
{
    private static ShopUIManager shopManager;

    [SerializeField]
    [Tooltip("Items to be sold")]
    private List<Item> sale;

    // Start is called before the first frame update

    void Start()
    {
        shopManager = ShopUIManager.instance;
        GetComponent<GetNPCSprite>().enabled = false;
    }    

    new void Update() {
        base.Update();
        if(!canInteract)
            GetComponent<GetNPCSprite>().enabled = false;
    }

    public override void OnInteract() {
        gameObject.GetComponent<GetNPCSprite>().SetSpriteOnScreen();
        shopManager.OpenShop(true, sale);
        GetComponent<GetNPCSprite>().enabled = true;
        this.TMPText.SetText("Come back again");        
    }
}
