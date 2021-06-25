using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailablePortals : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private float pos;
    
    private TeleportInteract portal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBtn(TeleportInteract ptr, int index) {
        this.portal = ptr;
        text.text = "Portal: " + index;
    }

    public void Teleport() {
        Debug.Log("Teleporting to: " + text.text);
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
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -(pos + 50)); //set bottom to position + 50; height of 50
        //****
    }
}
