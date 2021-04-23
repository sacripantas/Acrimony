using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehaviour : Interactable
{
    [Tooltip("Text that will be hovering over NPC's head")]
    [SerializeField]
    private string text;

    [Tooltip("Prefab of TextMeshPro")]
    [SerializeField]
    private TextMeshPro TMPText;

    
    
    void Awake() {
        CreateTMP();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract) { //What will be done if we can interact
           this.TMPText.enabled = true;
        } else {
            this.TMPText.enabled = false;
        }
    }
    //creates a new instance of TMP
    void CreateTMP() {       
        TMPText.SetText(text);
    }

    public override void OnInteract() {
        base.OnInteract();
        this.TMPText.SetText("Thanks for speaking with me!");
    }
}
