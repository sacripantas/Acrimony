using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Class manages equipment slot in inventory UI
 * 
 */
public class EquipManagerUI : ItemUI
{    
    [SerializeField]
    [Tooltip("Equipment type")]
    public EquipmentType type;
        
    new void Awake() {
        base.Awake();
    }
        
    public void Unequip() {        
        if (doubleClick >= 2) {
            ivnMng.Unequip(ivnMng.GetItem(this.type));
            hoverDescription.SetText("");
            ResetClick();
        }
    }    

    //Mouse hover
    public new void OnPointerEnter() {
        base.OnPointerEnter();
        //hover is only available if there is an item in the slot;
        if (btn.enabled) {
            hoverDescription.SetText(ivnMng.GetItem(this.type).Description);
        }
        ivnUIMng.isOverEquipSlot = true;
    }
     
    public new void OnPointerExit() {
        base.OnPointerExit();
        ivnUIMng.isOverEquipSlot = false;
    }

    //position the hover text according to inventory position]
    protected override void PositionHoverText() {
        float xPosition = 0.0f, yPosition = 0.0f;
                
        hoverDescription.transform.position = new Vector3(hoverDescription.transform.position.x + xPosition, hoverDescription.transform.position.y + yPosition, hoverDescription.transform.position.z);
    }

    public override void BeginDrag() {
        if (btn.enabled) {
            Debug.Log("Begin Drag:" + ivnMng.GetItem(this.type).ToString());
            ivnUIMng.SetDragged(ivnMng.GetItem(this.type));
            ivnUIMng.EnableDragImage(true);
        } else return;
    }

    public override void EndDrag() {
        if (btn.enabled) {
            if (ivnUIMng.isOverInventory) { //if its over iventory panel
                ivnMng.Unequip(ivnMng.GetItem(this.type));
            }
            ivnUIMng.EnableDragImage(false);
            ivnUIMng.SetDragged(null);
        } else return;
    }
}
