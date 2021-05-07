using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour {
    protected static InventoryHandler ivnMng;
    protected static InventoryUIManager ivnUIMng;

    protected Button btn;

    protected static int doubleClick = 0;

    [SerializeField]
    [Tooltip("TMP for Description")]
    protected TextMeshProUGUI hoverDescription;

    protected void Awake() {
        ivnMng = InventoryHandler.instance;
        ivnUIMng = InventoryUIManager.instance;
        Init();
    }
    // Start is called before the first frame update
    protected void Start() {

    }

    // Update is called once per frame
    protected void Update() {

    }
    protected static IEnumerator WaitDoubleClick() {
        yield return new WaitForSecondsRealtime(0.2f);
        ResetClick();
    }

    public void Click() {
        doubleClick++;
        StartCoroutine(WaitDoubleClick());
    }

    protected static void ResetClick() {
        doubleClick = 0;
    }

    public void OnPointerEnter() {
        //hover is only available if there is an item in the slot;
        if (btn.enabled) {            
            hoverDescription.enabled = true;
        }
    }

    //Mouse hover exit
    public void OnPointerExit() {
        hoverDescription.enabled = false;
    }

    protected void Init() {
        this.btn = GetComponent<Button>();
        this.btn.enabled = false;
        if (hoverDescription != null)
            this.hoverDescription.enabled = false;
        else hoverDescription = new TextMeshProUGUI();
        PositionHoverText();
    }

    public virtual void BeginDrag() {
        if (btn.enabled) {
            Debug.Log("Begin Drag:");
        } else return;
    }

    public virtual void EndDrag() {
        if (btn.enabled) {
            Debug.Log("End Drag");
        } else return;
    }

    protected virtual void PositionHoverText() {
        Debug.Log("to be override");
    }
}