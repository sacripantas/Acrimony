using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour {
    public static ShopUIManager instance = null;
    private static UIManager uiManager;
    private static GameManager manager;
    private static InventoryHandler inventory;

    
    [Header("Exposed for Debug Only- Leave Empty")]
    [Tooltip("[DEBUG] Shop Canvas")]
    [SerializeField]
    private Canvas shopCanvas;

    [SerializeField]
    [Tooltip("[DEBUG] Children Panels for shop")]
    private List<GameObject> shopPanels;

    [Tooltip("[DEBUG] Current Selected Item")]
    public Item selectedItem;

    [SerializeField]
    [Tooltip("[DEBUG] Current Items for sale")]
    private List<GameObject> saleBtns;

    [Header("Shop")]
    [SerializeField]
    [Tooltip("Prefab for shop button")]
    private GameObject btnShop;

    [SerializeField]
    [Tooltip("Attach the button for buy or sell")]
    private Button btnAction;

    [SerializeField]
    [Tooltip("TMP Title")]
    private TextMeshProUGUI title;

    private List<Item> saleItems;

    [SerializeField]
    [Tooltip("Panel for items")]
    private GameObject itemsPanel;

    [SerializeField]
    [Tooltip("Text Area for description - Buy")]
    public Text descText;

    [SerializeField]
    [Tooltip("Text Area for Stats - Buy")]
    public Text statsText;

    [SerializeField]
    [Tooltip("Update Money TMP")]
    private TextMeshProUGUI updateMoneyTMP;    

    public int shopType = 0; //0 -> nothing selected, 1 -> buy, 2 -> sell

    private bool canvasFlag = false;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        if (shopCanvas != null)
            shopCanvas.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasFlag = false;
        uiManager = UIManager.instance;
        manager = GameManager.instance;
        inventory = InventoryHandler.instance;
        this.shopCanvas = GetComponent<Canvas>();
        shopCanvas.enabled = false;
        SetChildrenPanels();
    }
    //initialize the array with all shop panels
    private void SetChildrenPanels() {
        for (int i = 0; i < transform.childCount; i++) {
            shopPanels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void OpenShop(bool flag, List<Item> sale) {
        this.shopCanvas.enabled = flag;
        uiManager.SetUIVisible(!flag);
        saleItems = sale;
        ChooseShop("PanelVendor");
        manager.Pause(flag);
        GetComponent<Animator>().SetBool("open", flag);
    }

    //close shop
    public void CloseShop(bool flag = false) {
        GetComponent<Animator>().SetBool("open", flag);
        uiManager.SetUIVisible(!flag);
        saleItems = null;
        selectedItem = null;
        manager.Pause(flag);
    }
    public void ActivateCanvas() {
        canvasFlag = !canvasFlag;
        this.shopCanvas.enabled = canvasFlag;
    }
    //Shows selected panel
    public void ChooseShop(string panelName) {        
        foreach (GameObject panel in shopPanels) {
            if (panel.name.Equals(panelName)) {
                panel.SetActive(true);
            } else
                panel.SetActive(false);
        }
    }

    public void ChooseShop(int type) {
        ClearSaleButtons();
        this.shopType = type;
        ChooseShop("PanelBuy");
        if (shopType == 1) {//buy
            CreateBuyButtons();
            title.SetText("Buy Items");
            btnAction.GetComponentInChildren<TextMeshProUGUI>().SetText("Buy");
        }
        if(shopType == 2) {//sell
            CreateSellButtons();
            title.SetText("Sell Items");
            btnAction.GetComponentInChildren<TextMeshProUGUI>().SetText("Sell");
        }
    }

    //Create list of buttons with items for sale
    public void CreateBuyButtons() {
        int pos = 0;
        GameObject btn;
        if (saleItems == null) return;
        foreach (Item i in saleItems) {
            btn = Instantiate(btnShop);
            btn.GetComponent<SaleButton>().SetItem(i, true);
            btn.transform.SetParent(itemsPanel.transform);
            btn.GetComponent<SaleButton>().SetPosition(pos);
            saleBtns.Add(btn);
            pos += 50;
        }
        RectTransform panelRT = itemsPanel.GetComponent<RectTransform>();
        if (pos >= 400)
            panelRT.offsetMin = new Vector2(panelRT.offsetMin.x, -pos - 5);
        saleBtns[0].GetComponent<SaleButton>().SetSelectedUI();
        saleBtns[0].GetComponent<SaleButton>().ItemSelected();
    }

    //Create list of buttons with items on inventory
    public void CreateSellButtons() {
        int pos = 0;
        GameObject btn;
        if (inventory.Inventory.Count == 0) return;
        foreach (Item i in inventory.Inventory) {
            btn = Instantiate(btnShop);
            btn.GetComponent<SaleButton>().SetItem(i, false);
            btn.transform.SetParent(itemsPanel.transform);
            btn.GetComponent<SaleButton>().SetPosition(pos);
            saleBtns.Add(btn);
            pos += 50;
        }
        RectTransform panelRT = itemsPanel.GetComponent<RectTransform>();
        if (pos >= 400)
            panelRT.offsetMin = new Vector2(panelRT.offsetMin.x, -pos - 5);
        saleBtns[0].GetComponent<SaleButton>().SetSelectedUI();
        saleBtns[0].GetComponent<SaleButton>().ItemSelected();
    }
    //Clears list of buttons
    public void ClearSaleButtons() {
        foreach (GameObject i in saleBtns) {
            Destroy(i);
        }
        saleBtns.Clear();
    }

    //buys selected item
    public void ActionOnSelected() {
        if (!selectedItem) return;
        if (this.shopType == 1) { //Buy selected Item
            //if its stackable dont check (return true if can add)
            if (PlayerManager.instance.CanAfford(selectedItem.BuyPrice) && InventoryHandler.instance.AddItem(Instantiate(selectedItem))) { //if money is enough and there is space
                PlayerManager.instance.SpendMoney(selectedItem.BuyPrice);
                Debug.Log("Bought: " + this.selectedItem.ToString());
            } else {
                Debug.Log("cant afford or there is no space available");
            }
        } else if(this.shopType == 2){ //Sell Selected Item
            PlayerManager.instance.SellItem(selectedItem.SellPrice);
            InventoryHandler.instance.RemoveItem(selectedItem);
            ClearSaleButtons();
            CreateSellButtons();
            Debug.Log("Sold: " + this.selectedItem.ToString());
        }
    }    
}
