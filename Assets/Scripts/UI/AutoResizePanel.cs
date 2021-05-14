using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class AutoResizePanel : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        parentPanel = GetComponentsInParent<RectTransform>()[1];
        rectTransform = GetComponent<RectTransform>();
        SetSize();
    }
    private void SetSize() {
        rectTransform.sizeDelta = new Vector2(parentPanel.rect.width * width, parentPanel.rect.height * height); // width, height
    }
}
