using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronArmor : Equipable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract() {
        base.OnInteract();
    }

    public override void OnEquip() {
        Debug.Log("Iron armor has been equiped");
        inventory.EquipItem(this);
    }
}
