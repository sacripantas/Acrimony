using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateMoney : MonoBehaviour {
    public static UpdateMoney instance;
    private static PlayerManager playerManager;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI textSpent;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    [Tooltip("Color for buy")]
    private Color buyColor;

    [SerializeField]
    [Tooltip("Color for Sell")]
    private Color sellColor;

    private void Awake()//Singleton
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start() {
        playerManager = PlayerManager.instance;
        this.text = GetComponent<TextMeshProUGUI>();
        this.textSpent = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetAvailableMoney() {
        if (this.text)
            this.text.SetText("$" + playerManager.currentMoney.ToString());
    }

    private void OnEnable() {
        SetAvailableMoney();
    }

    public void Anim(string trigger) {
        TextMeshProUGUI txt = Instantiate(textSpent);
        /*
        if (ShopUIManager.instance.shopType == 1) {
            this.textSpent.SetText("- $" + ShopUIManager.instance.selectedItem.BuyPrice);
            this.textSpent.color = buyColor;
        }else if(ShopUIManager.instance.shopType == 2) {
            this.textSpent.SetText("+ $" + ShopUIManager.instance.selectedItem.SellPrice);
            this.textSpent.color = sellColor;
        }
        */
        if (ShopUIManager.instance.shopType == 1) {
            txt.SetText("- $" + ShopUIManager.instance.selectedItem.BuyPrice);
            txt.color = buyColor;
        } else if (ShopUIManager.instance.shopType == 2) {
            txt.SetText("+ $" + ShopUIManager.instance.selectedItem.SellPrice);
            txt.color = sellColor;
        }
        //anim.SetTrigger(trigger);
        txt.transform.SetParent(this.gameObject.transform);
        txt.transform.localScale = new Vector3(1f, 1f, 1f);
        txt.transform.localPosition = Vector3.zero;
        txt.transform.localPosition = new Vector3(-100f,0f,0f);
        txt.GetComponent<Animator>().SetTrigger(trigger);
    }    
}
