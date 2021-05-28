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

    public bool isOverDelete = false;
    public bool isOverEquipSlot = false;
    public bool isOverInventory = false;

    [SerializeField]
    [Tooltip("[Debbug only] Item that is being dragged")]
    private Item beingDragged;

    [SerializeField]
    [Tooltip("UI element that holds an empty image. In run time this image will be changed to any item that's being dragged. Warning, this image cannot be a target of raycas")]
    private Image dragImage;

    private Vector2 mousePosRelative;
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
        invScreen.SetActive(true);
        invScreen.SetActive(false);
        dragImage.enabled = false;
        manager.LoadInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InventoryOpen")) {
            ShowIvn();
        }
        if (dragImage.enabled) {
            /***** Adapted from https://answers.unity.com/questions/849117/46-ui-image-follow-mouse-position.html *****/
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<Canvas>().transform as RectTransform, Input.mousePosition, GetComponent<Canvas>().worldCamera, out mousePosRelative);            
            dragImage.transform.localPosition = mousePosRelative;
        }
    }

    //displays inventory screen to player
    void ShowIvn() {
        if (!manager.isPaused ^ isActive) {
            isActive = !isActive;
            manager.Pause(isActive); //pauses time
            //this.invScreen.SetActive(isActive); //activates inventory screen
            uiMng.SetUIVisible(!isActive); //deactivate player UI
            if (isActive) UpdateInventory(InventoryHandler.instance.Inventory);
            GetComponent<Animator>().SetBool("open", isActive);
        }
    }

    //called by animation
    public void ActivateScreen() {
        this.invScreen.SetActive(isActive);
    }

    //update visual on inventory
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

    //update visual on equipped items
    public void UpdateEquiped(List<Equipable> equips) {
        foreach(Equipable e in equips) {
            foreach(GameObject btn in equipBtns) {
                if (e.eType.ToString().Equals(btn.GetComponent<EquipManagerUI>().type.ToString())) {
                    btn.GetComponent<Button>().enabled = true;
                    btn.gameObject.GetComponentsInChildren<Image>()[1].sprite = e.sprite;
                    btn.gameObject.GetComponentsInChildren<Image>()[1].enabled = true;                    
                } 
            }
        }
    }

    //updates visual when item is unequipped    
    public void Unequip(Equipable e) {
        foreach (GameObject btn in equipBtns) {
            if (e.eType.ToString().Equals(btn.GetComponent<EquipManagerUI>().type.ToString())) {
                btn.GetComponent<Button>().enabled = false;
                btn.GetComponent<Button>().image.sprite = emptyIvnSprite;
                btn.gameObject.GetComponentsInChildren<Image>()[1].enabled = false;
            }
        }
    }    

    public void IsOverDelete() {
        isOverDelete = true;
    }

    public void IsOffDelete() {
        isOverDelete = false;
    }

    public void SetDragged(Item item) {
        beingDragged = item;
        if (item != null)
            dragImage.sprite = item.sprite;
    }

    public void EnableDragImage(bool flag) {
        dragImage.enabled = flag;
    }
}
