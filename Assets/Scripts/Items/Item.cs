using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ***Item class should not be instantiated ***
 */
public class Item : Interactable {

    [SerializeField]
    [Tooltip("Sprite for the item - mandatory")]
    public Sprite sprite;

    [SerializeField]
    [Tooltip("Item name")]
    protected string itemName;

    [SerializeField]
    [Tooltip("Item Description that will show on Mouse Hover")]
    [TextArea]
    protected string desc;

    [SerializeField]
    [Tooltip("Quantity of items that are stackable per inventory slot (0 if not stackable )")]
    protected int stacking;

    [SerializeField]
    [Tooltip("Current Amount of Stacked items")]
    private int currStacked;

    public string Description { get => desc; }
    public string ItemName { get => itemName; }
    public int CanStack { get => stacking; }
    public int Stacked { get => currStacked; set => currStacked = value; }
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
}