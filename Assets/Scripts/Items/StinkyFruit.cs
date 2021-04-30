using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkyFruit : Consumable
{
    private int healthLost;    // Start is called before the first frame update
    void Start()
    {
        this.itemName = "Stinky Fruity";
        this.healthLost = 5;
    }

    // Update is called once per frame
    new void Update() {
        base.Update();
    }

    public override void OnInteract() {
        base.OnInteract();
    }

    public override void OnConsume() {
        Debug.Log(this.itemName + " was consumed");
        playerMng.TakeDamage(healthLost);
        inventory.RemoveItem(this);
    }
}
