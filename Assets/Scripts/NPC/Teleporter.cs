using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Teleporter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Room it is placed - same number as the panel number")]
    private int room;

    [SerializeField]
    [Tooltip("Debug")]
    private bool hasBeenSeen;

    private static GameManager manager;

    public bool HasBeenSeen { get => hasBeenSeen; set => hasBeenSeen = value; }
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        hasBeenSeen = false;
        manager.teleporters.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
