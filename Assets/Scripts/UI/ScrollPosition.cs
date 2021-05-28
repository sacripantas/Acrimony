using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPosition : MonoBehaviour
{
    void OnEnable() {
        GetComponent<Scrollbar>().value = 1f;
    }
}
