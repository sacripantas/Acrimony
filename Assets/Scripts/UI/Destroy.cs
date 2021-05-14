using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyAfter() {
        if(flag) //have to be called twice to be destroyed
            Destroy(this.gameObject);
        flag = !flag;
    }
}
