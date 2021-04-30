using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Class manages single slot in inventory UI
 * 
 */

public class SlotManagerUI : MonoBehaviour {
    private static InventoryHandler ivnMng;
    [SerializeField]
    [Tooltip("Position at inventory Matrix (x,y)")]
    private int x, y;

    public int itemPos; //current pos

    private Button btnItem;

    [SerializeField]
    [Tooltip("TMP for Description")]
    private TextMeshProUGUI hoverDescription;

    [SerializeField]
    [Tooltip("TMP for stack count")]
    private TextMeshProUGUI stackCount;

    private static int doubleClick = 0;

    void Awake() {
        itemPos = x * 5 + y;
        ivnMng = InventoryHandler.instance;
        Init();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public static IEnumerator WaitDoubleClick() {
        yield return new WaitForSecondsRealtime(0.2f);
        ResetClick();
    }
    public void UseItem() {
        if (doubleClick >= 2) {
            ivnMng.OnUse(this.itemPos);
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
        this.btnItem = GetComponent<Button>();
        this.btnItem.enabled = false;
        this.stackCount.enabled = false;
        if (hoverDescription != null)
            this.hoverDescription.enabled = false;
        else hoverDescription = new TextMeshProUGUI();
        PositionHoverText();
    }

    //Mouse hover
    public void OnPointerEnter() {
        //hover is only available if there is an item in the slot;
        if (btnItem.enabled) {
            hoverDescription.SetText(ivnMng.GetItem(this.itemPos).Description);
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
}
