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
    }    

    public override void OnInteract() {
        gameObject.GetComponent<GetNPCSprite>().SetSpriteOnScreen();
        shopManager.OpenShop(true, sale);
        this.TMPText.SetText("Come back again");        
    }
}
