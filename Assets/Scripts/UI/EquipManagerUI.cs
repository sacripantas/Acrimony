using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipManagerUI : MonoBehaviour
{
    private static InventoryHandler ivnMng;
    [SerializeField]
    [Tooltip("Equipment type")]
    public EquipmentType type;

    private Button btnEquip;

    [SerializeField]
    [Tooltip("TMP for Description")]
    private TextMeshProUGUI hoverDescription;

    private static int doubleClick = 0;

    void Awake() {
        ivnMng = InventoryHandler.instance;
        Init();
    }

    public static IEnumerator WaitDoubleClick() {
        yield return new WaitForSecondsRealtime(0.2f);
        ResetClick();
    }
    public void Unequip() {        
        if (doubleClick >= 2) {
            ivnMng.Unequip(ivnMng.GetItem(this.type));
            hoverDescription.SetText("");
            ResetClick();
        }
    }

    public void Click() {
        doubleClick++;
        StartCoroutine(WaitDoubleClick());
    }

    static void ResetClick() {
        doubleClick = 0;
    }

    private void Init() {
        this.btnEquip = GetComponent<Button>();
        this.btnEquip.enabled = false;
        if (hoverDescription != null)
            this.hoverDescription.enabled = false;
        else hoverDescription = new TextMeshProUGUI();
        PositionHoverText();
    }

    //Mouse hover
    public void OnPointerEnter() {
        //hover is only available if there is an item in the slot;
        if (btnEquip.enabled) {
            hoverDescription.SetText(ivnMng.GetItem(this.type).Description);
            hoverDescription.enabled = true;
        }
    }

    //Mouse hover exit
    public void OnPointerExit() {
        hoverDescription.enabled = false;
    }

    //position the hover text according to inventory position]
    private void PositionHoverText() {
        float xPosition = 0.0f, yPosition = 0.0f;
                
        hoverDescription.transform.position = new Vector3(hoverDescription.transform.position.x + xPosition, hoverDescription.transform.position.y + yPosition, hoverDescription.transform.position.z);
    }   
}
