using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaveHelper : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of all items prefab for reading")]
    private List<Item> allItems;

    public string SerializeInventory(List<Item> items) {
        string serializedString = "";
        foreach (Item i in items) {
            ItemStruct item = new ItemStruct();
            item.id = i.UniqueID;
            item.stack = i.Stacked;
            serializedString += JsonUtility.ToJson(item) + "#"; //delimiting each item by #
        }
        return serializedString;
    }

    public void DeserializeInventory(string json) {
        Debug.Log("Helper");
        ItemStruct item;
        Item newItem;
        string[] items = json.Split('#'); //spliting each item using the delimeter #
        for(int i = 0; i <items.Length-1; i++) {
            item = JsonUtility.FromJson<ItemStruct>(items[i]);
            newItem = Instantiate(allItems[item.id]);
            newItem.Stacked = item.stack;
            InventoryHandler.instance.AddItem(newItem);            
        }
    }
}
