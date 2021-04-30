using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Consumable
{
    private int healthAdd;
    // Start is called before the first frame update
    void Start()
    {
        this.itemName = "Health Potion";
        healthAdd = 20;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void OnInteract() {
        base.OnInteract();
    }

    public override void OnConsume() {
        Debug.Log(this.itemName + " was consumed");
        playerMng.ReceiveHealth(healthAdd);
        inventory.RemoveItem(this);
    }

}
