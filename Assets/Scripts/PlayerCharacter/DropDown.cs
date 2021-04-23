using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : SpecialTile
{
    [Tooltip("Amount of time the collider will be disabled")]
    [SerializeField] float delay = 0f;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public override string AmISpecial() {
        return base.AmISpecial();
    }

    public override void ActivateSpecial() {
        this.collider.enabled = false;
        Invoke("Reset", delay);
    }

    private void Reset() {
        this.collider.enabled = true;
    }
}
