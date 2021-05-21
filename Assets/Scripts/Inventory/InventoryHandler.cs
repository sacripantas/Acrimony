using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ***InventoryHandler should be present on the Player*** 
 */
public class InventoryHandler : MonoBehaviour {
    public static InventoryHandler instance;
    private static InventoryUIManager ivnUIManager;

    [SerializeField]
    [Tooltip("Amount of Items the player can hold on the inventory")]
    private int itemsQty;

    [SerializeField]
    [Tooltip("(Debug only) Current Items on inventory - Do not change")]
    private List<Item> inventory = new List<Item>();

    [SerializeField]
    [Tooltip("[Debug Only] Current Equiped items")]
    private List<Equipable> equipped = new List<Equipable>();

    public List<Item> Inventory { get => inventory; }
    public List<Equipable> Equiped { get => equipped; }
    //Singleton Instance
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        if (itemsQty == 0) itemsQty = 20;
        ivnUIManager = InventoryUIManager.instance;
        Debug.Log("Handler");
    }

    // Update is called once per frame
    void Update() {
        
    }
    // checks if the inventory still has available space
    public bool HasSpace() {
        if (inventory.Count >= itemsQty) return false;
        return true;
    }

    //returns the position on inventory if stackable item is the same and IF has space to be stacked. returns -1 if not.
    private int HasStackedItem(Item item) {        
        int index = 0;
        foreach(Item i in inventory) {
            if(i.GetType().Equals(item.GetType()) && i.Stacked < i.CanStack) {
                return index;
            }
            index++;
        }
        return -1;
    }

    //add gameobject item to inventory
    public bool AddItem(Item item) {
        if (item.CanStack == 0) { //Item is not stackable,
            if (HasSpace()) { // the inventory has space so add normally
                inventory.Add(item);
            } else {
                DiscardItem(item); //item is not stackable and there is no space, so discard new item.
                return false;
            }
        } else {//item is stackable
            int position = HasStackedItem(item);
            if (position != -1) {//search on inventory if there is another item on inventory already
                inventory[position].Stacked++; // adds another
                DiscardItem(item);
            } else { //there is no other instance of the item currently in inventory OR it has surpassed the limit of stacking.
                if (HasSpace()) {
                    inventory.Add(item);
                    item.Stacked++;
                } else {
                    DiscardItem(item);
                    return false;
                }
            }
        }
        ivnUIManager.UpdateInventory(inventory); // updates inventory UI        
        return true;
    }

    //removes gameobject item from inventory but doesnt destroy instance
    public void RemoveItem(Item item) {
        foreach (Item i in inventory) {
            if (i.Equals(item)) {
                if (i.CanStack > 0 && i.Stacked > 1) { //if is stacked and is more than 1 just subtract 1
                    i.Stacked--;
                    ivnUIManager.UpdateInventory(inventory);
                    return;
                } else { //ifnot
                    inventory.Remove(i);
                    DiscardItem(i);
                    ivnUIManager.UpdateInventory(inventory);
                    return;
                }
            }
        }
        Debug.Log("It wasnt possible to remove item");
    }

    //Discards(destroys instance from obj) from inventory
    public void DiscardItem(Item item) {
        Destroy(item.gameObject);
    }

    //clicked item on position "index" => interfaces with UI
    public void OnUse(int index) {
        if (index <= inventory.Count) {
            try {
                inventory[index].GetComponent<Consumable>().OnConsume();
            }
            catch {
                Debug.Log("Not consumable: " + index);
                try {
                    inventory[index].GetComponent<Equipable>().OnEquip();
                }
                catch {
                    Debug.Log("Not equipable: " + index);
                }
            }
        } else Debug.Log("wrong index:" + index);

        ivnUIManager.UpdateInventory(inventory);
    }

    //Returns item in inventory slot 
    public Item GetItem(int pos) {
        if (pos < inventory.Count)
            return inventory[pos];
        else return null;
    }

    public Equipable GetItem(EquipmentType type) {
        foreach(Equipable e in equipped) {
            if (e.eType.ToString().Equals(type.ToString()))
                return e;
        }
        return null;
    }

    public void Unequip(Equipable equip) {
        Debug.Log("Unequipping");
        if (HasSpace()) {
            inventory.Add(equip);
            equipped.Remove(equip);
            ivnUIManager.Unequip(equip);
        } else {
            Debug.Log("Cant unequip because there is no space on inventory");
        }
        ivnUIManager.UpdateEquiped(Equiped);
        ivnUIManager.UpdateInventory(Inventory);
    }

    public void EquipItem(Equipable equip) {
        if(GetItem(equip.eType) == null) {//If there is no equiped item in the same slot
            equipped.Add(equip);
            inventory.Remove(equip);
        } else {
            Debug.Log("Swapping equipped items");
            Equipable currEquiped = GetItem(equip.eType); //find equipped item 
            inventory.Remove(equip); //remove item from inventory to create space
            Unequip(currEquiped); //unequip item and put on inventory
            equipped.Add(equip); //Equip new item
        }
        ivnUIManager.UpdateEquiped(Equiped);
        ivnUIManager.UpdateInventory(Inventory);
    }
}
