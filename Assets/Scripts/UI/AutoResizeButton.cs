using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResizeButton : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Percentage of the parent that the panel should occupy. 0 for current - Width")]
    [Range(0.0f, 1.0f)]
    private float width;

    [SerializeField]
    [Tooltip("Percentage of the parent that the panel should occupy. 0 for current - Height")]
    [Range(0.0f, 1.0f)]
    private float height;

    private RectTransform parentPanel;

    private RectTransform rectTransform;

    void Awake() {
        parentPanel = GetComponentsInParent<RectTransform>()[1];
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start() {
        parentPanel = GetComponentsInParent<RectTransform>()[1];
        rectTransform = GetComponent<RectTransform>();
        SetSize();
    }       
    private void SetSize() {        
        rectTransform.sizeDelta = new Vector2(parentPanel.rect.width * 0.2f, parentPanel.rect.height * 0.25f);
        int x = GetComponentInChildren<SlotManagerUI>().X;
        int y = GetComponentInChildren<SlotManagerUI>().Y;
        transform.localPosition = new Vector3((parentPanel.rect.width * 0.2f * y) - parentPanel.rect.width, -((parentPanel.rect.height * 0.25f * x) - parentPanel.rect.height/2.0f));        
    }

    private void OnEnable() {
        SetSize();
    }
}
