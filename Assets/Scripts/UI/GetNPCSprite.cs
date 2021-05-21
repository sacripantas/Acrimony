using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNPCSprite : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Attach image from vendor screen")]
    private Image image;

    [SerializeField]
    private SpriteRenderer npcSprite;
        
    // Start is called before the first frame update
    void Start()
    {
        this.npcSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the sprite each update time that the panel is active
        this.image.sprite = this.npcSprite.sprite;
    }

    public void SetSpriteOnScreen() {
        this.image.sprite = this.npcSprite.sprite;//set first sprite when starting
        this.image.preserveAspect = true;
    }
}
