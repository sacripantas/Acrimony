using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ***NPCBehaviour should not be instantiated***
 */ 

public class NPCBehaviour : Interactable
{
    [Tooltip("Text that will be hovering over NPC's head")]
    [SerializeField]
    protected string text;

    [Tooltip("Prefab of TextMeshPro")]
    [SerializeField]
    protected TextMeshPro TMPText;

    
    
    protected void Awake() {
        CreateTMP();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (canInteract) { //What will be done if we can interact
           this.TMPText.enabled = true;
        } else {
            this.TMPText.enabled = false;
        }
    }
    //creates a new instance of TMP
    protected void CreateTMP() {       
        TMPText.SetText(text);
    }

    public override void OnInteract() {
        base.OnInteract();
        Debug.Log("interact on behaviour");
        this.TMPText.SetText("Thanks for speaking with me");
    }

    protected void ResetText() {
        this.TMPText.SetText(text);
    }
}
