using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField]
    public bool isActive = false; //if this spawner is active, this is where the player will spawn
    [SerializeField]
    private Vector2 position;


    /*
     * getters and setters 
     */
    public Vector2 Position { get => position; set => position = value; }

    void Awake() {
        position = GetComponent<Transform>().localPosition;
    }
    // Start is called before the first frame update
    void Start()
    {        
        position = GetComponent<Transform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
