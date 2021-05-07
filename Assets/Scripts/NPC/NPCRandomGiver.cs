using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomGiver : NPCBehaviour
{
    [SerializeField]
    [Tooltip("List of items available")]
    private List<Item> prefabItems;

    private bool oneTimeInteraction = false;

    [SerializeField]
    [Tooltip("[Debug only] - Do not assign")]
    private Item itemInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnInteract() {
        base.OnInteract();
        if (oneTimeInteraction) {
            this.TMPText.SetText("Sorry I only had one Item to give you!!");
            return;
        }
        oneTimeInteraction = false;
        GiveItemRandom();
        TMPText.SetText("Enjoy your "+ itemInstance.ItemName +"!!");
        Invoke("ResetText", 2f);
    }

    private void GiveItemRandom() {
        int randNum = Random.Range(0, prefabItems.Count);
        itemInstance = Instantiate(prefabItems[randNum]);
        inventory.AddItem(itemInstance);
    }
}
