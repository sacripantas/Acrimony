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
    private Sprite npcSprite;

    [SerializeField]
    private string names;

    [SerializeField]
    private Animator anim;
        
    // Start is called before the first frame update
    void Start()
    {
        this.npcSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        this.anim = gameObject.GetComponent<Animator>();
        //this.image.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("GroundEnemy");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSpriteOnScreen() {
        this.image.sprite = this.npcSprite;
        this.image.preserveAspect = true;
    }
}
