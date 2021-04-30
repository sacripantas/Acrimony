using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPotionGiver : NPCBehaviour
{
    [SerializeField]
    [Tooltip("HP Potion prefab")]
    private HealthPotion potion;

    private bool oneTimeInteraction = false;

    private HealthPotion potionInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void OnInteract() {
        if (oneTimeInteraction) {
            this.TMPText.SetText("Sorry I only had one Potion!!");
            return;
        }
        base.OnInteract();
        oneTimeInteraction = false;
        GivePotion();
        this.TMPText.SetText("Enjoy your Potion!!");
    }

    private void GivePotion() {
        potionInstance = Instantiate(potion);
        inventory.AddItem(potionInstance);
    }
}
