using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaleButton : MonoBehaviour
{
    private static ShopUIManager shopManager;
    private Item singleItem;
    public float pos;
        
    // Start is called before the first frame update
    void Start()
    {
        shopManager = ShopUIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(Item item, bool isBuy) {
        this.singleItem = item;
        gameObject.GetComponentsInChildren<Image>()[1].sprite = singleItem.sprite;
        gameObject.GetComponentsInChildren<Text>()[1].text = singleItem.ItemName;
        if (isBuy) {
            gameObject.GetComponentsInChildren<Text>()[2].text = "$" + singleItem.BuyPrice.ToString();
            gameObject.GetComponentsInChildren<Text>()[0].text = "0";
            gameObject.GetComponentsInChildren<Text>()[0].enabled = !isBuy;
        } else {
            gameObject.GetComponentsInChildren<Text>()[2].text = "$" + singleItem.SellPrice.ToString();            
            gameObject.GetComponentsInChildren<Text>()[0].text = singleItem.Stacked.ToString();
            if (singleItem.Stacked <= 0)
                gameObject.GetComponentsInChildren<Text>()[0].enabled = isBuy;
            else
                gameObject.GetComponentsInChildren<Text>()[0].enabled = !isBuy;
        }
    }


    public void SetPosition(float pos) {
        this.pos = pos;
        RectTransform rectTransform = GetComponent<RectTransform>();
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.localPosition = Vector3.zero;

        //RectTransforming positioning
        rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y); //set left to 0
        rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y); //set right to 0;
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -pos); //set top to position;
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -(pos+50)); //set bottom to position + 50; height of 50
        //****
    }

    public void ItemSelected() {
        if (singleItem && shopManager) {
            shopManager.descText.text = singleItem.Description;
            shopManager.statsText.text = singleItem.ToString();
            shopManager.selectedItem = singleItem;
        }
    }
    private void OnEnable() {
        SetSelectedUI();
    }

    public void SetSelectedUI() {
        gameObject.GetComponent<Selectable>().Select();
    }
}
