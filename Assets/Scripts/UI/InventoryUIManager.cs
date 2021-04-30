using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    private static UIManager uiMng;
    private static GameManager manager;
    public static InventoryUIManager instance;

    [SerializeField]
    [Tooltip("Panel for inventory")]
    private GameObject invScreen;

    [SerializeField]
    [Tooltip("Inventory buttons - The order is important!!! - Don't change in real time")]
    private List<GameObject> ivnBtns;

    [SerializeField]
    [Tooltip("Equip Buttons")]
    private List<GameObject> equipBtns;
    
    private Sprite emptyIvnSprite;
    private bool isActive;

    //Singleton Instance
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiMng = UIManager.instance;
        manager = GameManager.instance;
        emptyIvnSprite = ivnBtns[0].GetComponent<Button>().image.sprite;
        isActive = false;
        invScreen.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InventoryOpen")) {
            ShowIvn();
        }

    }

    void ShowIvn() {
        isActive = !isActive;
        manager.Pause(isActive); //pauses time
        this.invScreen.SetActive(isActive); //activates inventory screen
        uiMng.SetVisible(!isActive); //deactivate player UI
        if(isActive) UpdateInventory(InventoryHandler.instance.Inventory);
    }


    public void UpdateInventory(List<Item> items) {
        for (int i = 0; i < ivnBtns.Count; i++) {
            if(i < items.Count) { 
                ivnBtns[i].GetComponent<Button>().enabled = true;
                ivnBtns[i].gameObject.GetComponentsInChildren<Image>()[1].sprite = items[i].sprite;
                ivnBtns[i].gameObject.GetComponentsInChildren<Image>()[1].enabled = true;
                ivnBtns[i].GetComponent<SlotManagerUI>().ShowStack(true);
            } else {
                ivnBtns[i].GetComponent<Button>().enabled = false;
                ivnBtns[i].GetComponent<Button>().image.sprite = emptyIvnSprite;
                ivnBtns[i].gameObject.GetComponentsInChildren<Image>()[1].enabled = false;
                ivnBtns[i].GetComponent<SlotManagerUI>().ShowStack(false);
            }
        }        
    }

    public void UpdateEquiped(List<Equipable> equips) {
        foreach(Equipable e in equips) {
            foreach(GameObject btn in equipBtns) {
                if (e.eType.ToString().Equals(btn.GetComponent<EquipManagerUI>().type.ToString())) {
                    btn.GetComponent<Button>().enabled = true;
                    btn.gameObject.GetComponentsInChildren<Image>()[1].sprite = e.sprite;
                    btn.gameObject.GetComponentsInChildren<Image>()[1].enabled = true;                    
                } else {
                    btn.GetComponent<Button>().enabled = false;
                    btn.GetComponent<Button>().image.sprite = emptyIvnSprite;
                    btn.gameObject.GetComponentsInChildren<Image>()[1].enabled = false;                    
                }
            }
        }
    }

    public void Unequip(Equipable e) {
        foreach (GameObject btn in equipBtns) {
            if (e.eType.ToString().Equals(btn.GetComponent<EquipManagerUI>().type.ToString())) {
                btn.GetComponent<Button>().enabled = false;
                btn.GetComponent<Button>().image.sprite = emptyIvnSprite;
                btn.gameObject.GetComponentsInChildren<Image>()[1].enabled = false;
            }
        }
    }
}
