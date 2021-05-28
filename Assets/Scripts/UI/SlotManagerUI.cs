using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Class manages single slot in inventory UI
 * 
 */

public class SlotManagerUI : ItemUI {


    [SerializeField]
    [Tooltip("Position at inventory Matrix (x,y)")]
    private int x, y;

    public int itemPos; //current pos
        
    [SerializeField]
    [Tooltip("TMP for stack count")]
    private TextMeshProUGUI stackCount;    

    public int X { get => x; }
    public int Y { get => y; }
    new void Awake() {
        base.Awake();
        itemPos = x * 5 + y;
    }
    
    public void UseItem() {
        if (doubleClick >= 2) {
            ivnMng.OnUse(this.itemPos);
            hoverDescription.SetText("");
            ResetClick();
        }
    }

    //Mouse hover
    public new void OnPointerEnter() {
        base.OnPointerEnter();
        //hover is only available if there is an item in the slot;
        if (btn.enabled) {
            hoverDescription.SetText(ivnMng.GetItem(this.itemPos).Description);
        }
        ivnUIMng.isOverInventory = true;
    }

    public new void OnPointerExit() {
        base.OnPointerExit();
        ivnUIMng.isOverInventory = false;
    }

    //position the hover text according to inventory position]
    protected override void PositionHoverText() {
        float xPosition = 0.0f, yPosition = 0.0f;

        if (x < 2) {//top rows
            yPosition = -1f;
        } else {//bottom rows
            yPosition = 1f;
        }

        if (y < 2) {
            xPosition = 1f;
        } else {
            if (y == 2) xPosition = 0.0f;
            else xPosition = -1f;
        }
        hoverDescription.transform.position = new Vector3(hoverDescription.transform.position.x + xPosition, hoverDescription.transform.position.y + yPosition, hoverDescription.transform.position.z);
    }

    //Shows amount of items current stacked
    public void ShowStack(bool flag) {
        if (flag) {
            if (ivnMng.GetItem(this.itemPos).CanStack > 0) {
                this.stackCount.SetText(ivnMng.GetItem(this.itemPos).Stacked.ToString());
                this.stackCount.enabled = true;
                return;
            }
        } 
        this.stackCount.SetText("");
        this.stackCount.enabled = false;        
    }

    public override void BeginDrag() {
        if (btn.enabled) {
            Debug.Log("Begin Drag:" + ivnMng.GetItem(this.itemPos).ToString());
            ivnUIMng.SetDragged(ivnMng.GetItem(this.itemPos));
            ivnUIMng.EnableDragImage(true);
        } else return;
    }

    public override void EndDrag() {
        if (btn.enabled) {
            if (ivnUIMng.isOverDelete) {//can delete
                Debug.Log("can discard" + ivnMng.GetItem(this.itemPos).ToString());
                ivnMng.RemoveItem(ivnMng.GetItem(this.itemPos));
            }else if (ivnUIMng.isOverEquipSlot) {
                Debug.Log("Try equip: " + ivnMng.GetItem(this.itemPos).ToString());
                try {
                    ivnMng.EquipItem((Equipable) ivnMng.GetItem(this.itemPos));
                }
                catch { //if cast fails, its not an equipment
                    Debug.Log("not equipable");
                }
            }
            ivnUIMng.EnableDragImage(false);
            ivnUIMng.SetDragged(null);
        } else return;
    }
}
