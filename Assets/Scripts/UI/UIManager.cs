using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    
    private static GameManager manager;

	//HealthBar
	public Slider healthBar;
	public Gradient gradient;
	public Image healthFill;
	public Text hpValue;

	//ManaBar
	public Slider manaBar;
	public Image manaFill;

	//Currency
	public Text money;

	//Ammo
	public Text ammo;

    [SerializeField]
    [Tooltip("Attach Shop Canvas")]
    private Canvas shopCanvas;

    [SerializeField]
    [Tooltip("Attach all panels for shop")]
    private List<GameObject> shopPanels;

    [SerializeField]
    [Tooltip("Prefab for shop button")]
    private GameObject btnShop;

    private List<Item> saleItems;

    [SerializeField]
    private List<GameObject> saleBtns;

    [SerializeField]
    [Tooltip("Panel for items")]
    private GameObject itemsPanel;

    [SerializeField]
    private GameObject activePanel;

    [SerializeField]
    public Text descPanel, statsPane;

    [SerializeField]
    private float shopH, shopW;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        if(shopCanvas != null)
            shopCanvas.enabled = false;
    }

    void Start() {
        manager = GameManager.instance;
        this.shopW = shopCanvas.GetComponent<RectTransform>().rect.width - 100f;
        this.shopH = shopCanvas.GetComponent<RectTransform>().rect.height - 100f;
    }

    public void SetMaxHealth(int health)
	{
		healthBar.maxValue = health;
		healthBar.value = health;	

		healthFill.color = gradient.Evaluate(1f);//max hp color
	}

	public void SetHealth(int health)
	{
		healthBar.value = health;
		healthFill.color = gradient.Evaluate(healthBar.normalizedValue);
        if(hpValue != null)
		hpValue.text = health.ToString();

	}

	public void SetMaxMana(int mana)
	{
		manaBar.maxValue = mana;
		manaBar.value = mana;

	}

	public void SetMana(int mana)
	{
		manaBar.value = mana;
	}

    public void SetUIVisible(bool flag) {
        this.healthBar.gameObject.SetActive(flag);
        this.manaBar.gameObject.SetActive(flag);
        this.ammo.gameObject.SetActive(flag);
        this.money.gameObject.SetActive(flag);
    }

	public void SetCurrency(int currency)
	{
		money.text = "$" + currency.ToString();
	}

	public void SetAmmo(int ammunition)
	{
		ammo.text = "Ammo " + "\n"+  ammunition.ToString();
	}

    //Enables Shop Canvas
    public void OpenShop(bool flag, List<Item> sale) {
        this.shopCanvas.enabled = flag;
        this.SetUIVisible(!flag);
        saleItems = sale;
        ChooseShop("PanelVendor");
        manager.Pause(flag);
    }

    //close shop
    public void OpenShop(bool flag) {
        this.shopCanvas.enabled = flag;
        this.SetUIVisible(!flag);
        saleItems = null;
        ChooseShop("");
        manager.Pause(flag);
    }

    //Shows selected panel
    public void ChooseShop(string panelName) {
        ClearSaleButtons();
        foreach (GameObject panel in shopPanels) {
            if (panel.name.Equals(panelName)) {
                panel.SetActive(true);
                activePanel = panel;
            } else
                panel.SetActive(false);
        }
        InitPanels();
        CreateSaleButtons();
    }

    //Create list of buttons with items for sale
    public void CreateSaleButtons() {
        int pos = 0;
        GameObject btn;
        if (saleItems == null) return;
        foreach(Item i in saleItems) {
            btn = Instantiate(btnShop);
            btn.GetComponent<SaleButton>().SetItem(i);
            btn.transform.SetParent(itemsPanel.transform);
            btn.GetComponent<SaleButton>().SetPosition(pos);
            saleBtns.Add(btn);
            pos += 50;
        }
        RectTransform panelRT = itemsPanel.GetComponent<RectTransform>();
        if(pos >= 400)
            panelRT.offsetMin = new Vector2(panelRT.offsetMin.x, -pos-5);
        //EventSystem.current.SetSelectedGameObject(saleBtns[0].GetComponent<Button>().gameObject, new BaseEventData(EventSystem.current));
    }

    //Clears list of buttons
    public void ClearSaleButtons() {        
        foreach (GameObject i in saleBtns) {
            Destroy(i);
        }
        saleBtns.Clear();
    }

    private void InitPanels() {      
        /*
        GameObject rightPanel = null, leftPanel = null;

        Debug.Log(activePanel.ToString());
        
        rightPanel = activePanel.transform.Find("MainPanel").Find("RightPanel").gameObject;
        leftPanel = activePanel.transform.Find("MainPanel").Find("LeftPanel").gameObject;

        if (rightPanel == null || leftPanel == null) return;
        RectTransform rtR = rightPanel.GetComponent<RectTransform>();
        rtR.sizeDelta = new Vector2((this.shopW/3f) * 2f, 0f); // right panel takes 2/3 of the screen
        RectTransform rtL = leftPanel.GetComponent<RectTransform>();
        rtL.sizeDelta = new Vector2((this.shopW/3f), 0f); // LEft panel takes 1/3 of th screen
        */
    }
}
