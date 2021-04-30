using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public override void OnInteract() {
        base.OnInteract();
    }

    public virtual void OnConsume() {
        Debug.Log("Item has been consumed");
    }
}
