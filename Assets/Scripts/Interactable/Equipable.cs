using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : Item
{
    [SerializeField]
    protected EquipmentType type = EquipmentType.Default;

    public EquipmentType eType { get => type; }
    // Start is called before the first frame update
    void Start()
    {
        this.stacking = 0;//all equipments are not stackable
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract() {
        base.OnInteract();
    }

    public virtual void OnEquip() {
        Debug.Log("Item has been equiped");
    }
}
